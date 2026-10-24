using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#if NETSTANDARD2_1
using System.Net.Http;
using System.Security.Cryptography;
#endif

namespace System.Runtime.CompilerServices
{
#if NETSTANDARD2_1
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class CallerArgumentExpressionAttribute : Attribute
    {
        public CallerArgumentExpressionAttribute(string parameterName) => ParameterName = parameterName;
        public string ParameterName { get; }
    }

    internal sealed class IsExternalInit { }
#endif
}

namespace Chet.Utils
{
    internal static class ThrowHelper
    {
        internal static void ThrowIfNull(object argument, [CallerArgumentExpression(nameof(argument))] string paramName = null)
        {
#if NETSTANDARD2_1
            if (argument is null)
                throw new ArgumentNullException(paramName);
#else
            ArgumentNullException.ThrowIfNull(argument, paramName);
#endif
        }

        internal static void ThrowIfNullOrEmpty(string argument, [CallerArgumentExpression(nameof(argument))] string paramName = null)
        {
#if NETSTANDARD2_1
            if (argument is null || argument.Length == 0)
                throw new ArgumentException("Value cannot be null or an empty string.", paramName);
#else
            ArgumentException.ThrowIfNullOrEmpty(argument, paramName);
#endif
        }

        internal static void ThrowIfNegative(int value, [CallerArgumentExpression(nameof(value))] string paramName = null)
        {
#if NETSTANDARD2_1
            if (value < 0)
                throw new ArgumentOutOfRangeException(paramName, value, "value must be non-negative.");
#else
            ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
#endif
        }

        internal static void ThrowIfLessThanOrEqual(int value, int other, [CallerArgumentExpression(nameof(value))] string paramName = null)
        {
#if NETSTANDARD2_1
            if (value <= other)
                throw new ArgumentOutOfRangeException(paramName, value, $"value must be greater than {other}.");
#else
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, other, paramName);
#endif
        }

        internal static void ThrowIfGreaterThan(int value, int other, [CallerArgumentExpression(nameof(value))] string paramName = null)
        {
#if NETSTANDARD2_1
            if (value > other)
                throw new ArgumentOutOfRangeException(paramName, value, $"value must be less than or equal to {other}.");
#else
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other, paramName);
#endif
        }
    }

    /// <summary>
    /// Parallel.*Async polyfill helper. On net8.0 delegates to BCL; on netstandard2.1
    /// provides an equivalent implementation based on SemaphoreSlim + Task.WhenAll.
    /// </summary>
    internal static class ParallelHelper
    {
        public static Task ForEachAsync<T>(
            IEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
#if NET8_0
            return Parallel.ForEachAsync(source, parallelOptions, body);
#else
            return PolyfillForEachAsync(source, parallelOptions, body);
#endif
        }

        public static Task ForAsync(
            int fromInclusive,
            int toExclusive,
            ParallelOptions parallelOptions,
            Func<int, CancellationToken, ValueTask> body)
        {
#if NET8_0
            return Parallel.ForAsync(fromInclusive, toExclusive, parallelOptions, body);
#else
            if (toExclusive < fromInclusive)
                throw new ArgumentOutOfRangeException(nameof(toExclusive), "toExclusive must be greater than or equal to fromInclusive.");
            var range = Enumerable.Range(fromInclusive, toExclusive - fromInclusive);
            return PolyfillForEachAsync(range, parallelOptions, body);
#endif
        }

#if NETSTANDARD2_1
        private static async Task PolyfillForEachAsync<T>(
            IEnumerable<T> source,
            ParallelOptions parallelOptions,
            Func<T, CancellationToken, ValueTask> body)
        {
            var cancellationToken = parallelOptions.CancellationToken;
            var maxDegreeOfParallelism = parallelOptions.MaxDegreeOfParallelism;
            if (maxDegreeOfParallelism <= 0)
                maxDegreeOfParallelism = Environment.ProcessorCount;

            using var semaphore = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
            var tasks = new List<Task>();
            foreach (var item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await body(item, cancellationToken).ConfigureAwait(false);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }, cancellationToken));
            }
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
#endif
    }
}

#if NETSTANDARD2_1
namespace System.Net.Http
{
    /// <summary>
    /// Polyfill for HttpContent CT-aware overloads added in .NET 5.
    /// On netstandard2.1 the underlying CT-less API is wrapped (CT checked before call).
    /// </summary>
    internal static class HttpContentPolyfillExtensions
    {
        public static async Task<Stream> ReadAsStreamAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public static async Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public static async Task<byte[]> ReadAsByteArrayAsync(this HttpContent content, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }
    }
}

namespace System.Security.Cryptography
{
    /// <summary>
    /// Polyfill for HashAlgorithm.ComputeHashAsync(Stream, CancellationToken) added in .NET 5.
    /// Uses the incremental TransformBlock / TransformFinalBlock API so cancellation
    /// is honored between buffer reads.
    /// </summary>
    internal static class HashAlgorithmPolyfillExtensions
    {
        public static async Task<byte[]> ComputeHashAsync(this HashAlgorithm hashAlgorithm, Stream stream, CancellationToken cancellationToken = default)
        {
            var buffer = new byte[81920];
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
            {
                hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
            }
            hashAlgorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            return hashAlgorithm.Hash;
        }
    }
}
#endif
