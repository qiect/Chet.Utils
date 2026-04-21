using System.Globalization;

namespace Chet.Utils.Helpers;

/// <summary>
/// 计量单位转换帮助类，提供长度、货币、质量、角度、进制、面积、体积、温度、速度、热能、功率、压强、力、数据存储、时间等多种单位转换功能。
/// </summary>
/// <remarks>
/// <para>支持的功能：</para>
/// <list type="bullet">
///   <item><description>长度单位转换：毫米、厘米、米、千米、英寸、英尺、码、英里、海里</description></item>
///   <item><description>货币单位转换：人民币、美元、欧元、英镑、日元、韩元、澳元、加元、瑞士法郎、港币</description></item>
///   <item><description>质量单位转换：毫克、克、千克、吨、盎司、磅、英石</description></item>
///   <item><description>角度单位转换：度、弧度、百分度、角分、角秒</description></item>
///   <item><description>进制转换：二进制、八进制、十进制、十六进制</description></item>
///   <item><description>面积单位转换：平方毫米、平方厘米、平方米、公顷、平方公里、平方英寸、平方英尺、平方码、英亩、平方英里</description></item>
///   <item><description>体积单位转换：立方毫米、立方厘米、立方米、升、毫升、立方英寸、立方英尺、立方码、美制加仑、英制加仑、美制夸脱、美制品脱</description></item>
///   <item><description>温度单位转换：摄氏度、华氏度、开尔文</description></item>
///   <item><description>速度单位转换：米/秒、公里/小时、英里/小时、英尺/秒、节</description></item>
///   <item><description>热能单位转换：焦耳、千焦、卡路里、千卡、瓦时、千瓦时、BTU、电子伏特</description></item>
///   <item><description>功率单位转换：瓦特、千瓦、马力、英制马力</description></item>
///   <item><description>压强单位转换：帕斯卡、千帕、巴、标准大气压、毫米汞柱、磅力/平方英寸</description></item>
///   <item><description>力单位转换：牛顿、千牛顿、达因、磅力、千克力</description></item>
///   <item><description>数据存储单位转换：字节、KB、MB、GB、TB、PB、EB</description></item>
///   <item><description>时间单位转换：毫秒、秒、分钟、小时、天、周、月、年</description></item>
/// </list>
/// </remarks>
public static partial class UnitHelper
{
    #region 长度转换

    /// <summary>
    /// 长度单位枚举。
    /// </summary>
    public enum LengthUnit
    {
        /// <summary>
        /// 毫米。
        /// </summary>
        Millimeter,

        /// <summary>
        /// 厘米。
        /// </summary>
        Centimeter,

        /// <summary>
        /// 米。
        /// </summary>
        Meter,

        /// <summary>
        /// 千米。
        /// </summary>
        Kilometer,

        /// <summary>
        /// 英寸。
        /// </summary>
        Inch,

        /// <summary>
        /// 英尺。
        /// </summary>
        Foot,

        /// <summary>
        /// 码。
        /// </summary>
        Yard,

        /// <summary>
        /// 英里。
        /// </summary>
        Mile,

        /// <summary>
        /// 海里。
        /// </summary>
        NauticalMile
    }

    /// <summary>
    /// 长度单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var meters = UnitHelper.ConvertLength(100, LengthUnit.Centimeter, LengthUnit.Meter);
    /// // meters = 1
    /// var inches = UnitHelper.ConvertLength(1, LengthUnit.Meter, LengthUnit.Inch);
    /// // inches ≈ 39.37
    /// </code>
    /// </example>
    public static double ConvertLength(double value, LengthUnit from, LengthUnit to)
    {
        if (from == to) return value;

        double meterValue = from switch
        {
            LengthUnit.Millimeter => value / 1_000,
            LengthUnit.Centimeter => value / 100,
            LengthUnit.Meter => value,
            LengthUnit.Kilometer => value * 1_000,
            LengthUnit.Inch => value * 0.0254,
            LengthUnit.Foot => value * 0.3048,
            LengthUnit.Yard => value * 0.9144,
            LengthUnit.Mile => value * 1_609.344,
            LengthUnit.NauticalMile => value * 1_852,
            _ => value
        };

        return to switch
        {
            LengthUnit.Millimeter => meterValue * 1_000,
            LengthUnit.Centimeter => meterValue * 100,
            LengthUnit.Meter => meterValue,
            LengthUnit.Kilometer => meterValue / 1_000,
            LengthUnit.Inch => meterValue / 0.0254,
            LengthUnit.Foot => meterValue / 0.3048,
            LengthUnit.Yard => meterValue / 0.9144,
            LengthUnit.Mile => meterValue / 1_609.344,
            LengthUnit.NauticalMile => meterValue / 1_852,
            _ => meterValue
        };
    }

    #endregion

    #region 货币转换

    /// <summary>
    /// 货币单位枚举。
    /// </summary>
    public enum CurrencyUnit
    {
        /// <summary>
        /// 人民币。
        /// </summary>
        CNY,

        /// <summary>
        /// 美元。
        /// </summary>
        USD,

        /// <summary>
        /// 欧元。
        /// </summary>
        EUR,

        /// <summary>
        /// 英镑。
        /// </summary>
        GBP,

        /// <summary>
        /// 日元。
        /// </summary>
        JPY,

        /// <summary>
        /// 韩元。
        /// </summary>
        KRW,

        /// <summary>
        /// 澳元。
        /// </summary>
        AUD,

        /// <summary>
        /// 加元。
        /// </summary>
        CAD,

        /// <summary>
        /// 瑞士法郎。
        /// </summary>
        CHF,

        /// <summary>
        /// 港币。
        /// </summary>
        HKD
    }

    private static readonly Dictionary<CurrencyUnit, decimal> CurrencyRates = new()
    {
        { CurrencyUnit.CNY, 7.24m },
        { CurrencyUnit.USD, 1m },
        { CurrencyUnit.EUR, 0.92m },
        { CurrencyUnit.GBP, 0.79m },
        { CurrencyUnit.JPY, 154.5m },
        { CurrencyUnit.KRW, 1365m },
        { CurrencyUnit.AUD, 1.53m },
        { CurrencyUnit.CAD, 1.37m },
        { CurrencyUnit.CHF, 0.90m },
        { CurrencyUnit.HKD, 7.82m }
    };

    /// <summary>
    /// 货币单位转换。
    /// </summary>
    /// <param name="amount">要转换的金额。</param>
    /// <param name="from">源货币单位。</param>
    /// <param name="to">目标货币单位。</param>
    /// <returns>转换后的金额。</returns>
    /// <remarks>
    /// 注意：汇率数据为示例数据，实际应用中应从API获取实时汇率。
    /// </remarks>
    /// <example>
    /// <code>
    /// var usd = UnitHelper.ConvertCurrency(100, CurrencyUnit.CNY, CurrencyUnit.USD);
    /// // usd ≈ 13.81
    /// </code>
    /// </example>
    public static decimal ConvertCurrency(decimal amount, CurrencyUnit from, CurrencyUnit to)
    {
        if (from == to) return amount;

        var usdAmount = amount / CurrencyRates[from];
        return usdAmount * CurrencyRates[to];
    }

    /// <summary>
    /// 更新货币汇率。
    /// </summary>
    /// <param name="currency">货币单位。</param>
    /// <param name="rate">对美元的汇率。</param>
    public static void UpdateCurrencyRate(CurrencyUnit currency, decimal rate)
    {
        CurrencyRates[currency] = rate;
    }

    /// <summary>
    /// 获取货币符号。
    /// </summary>
    /// <param name="currency">货币单位。</param>
    /// <returns>货币符号。</returns>
    /// <example>
    /// <code>
    /// var symbol = UnitHelper.GetCurrencySymbol(CurrencyUnit.CNY);
    /// // symbol = "¥"
    /// </code>
    /// </example>
    public static string GetCurrencySymbol(CurrencyUnit currency)
    {
        return currency switch
        {
            CurrencyUnit.CNY => "¥",
            CurrencyUnit.USD => "$",
            CurrencyUnit.EUR => "€",
            CurrencyUnit.GBP => "£",
            CurrencyUnit.JPY => "¥",
            CurrencyUnit.KRW => "₩",
            CurrencyUnit.AUD => "A$",
            CurrencyUnit.CAD => "C$",
            CurrencyUnit.CHF => "CHF",
            CurrencyUnit.HKD => "HK$",
            _ => currency.ToString()
        };
    }

    /// <summary>
    /// 格式化货币金额。
    /// </summary>
    /// <param name="amount">金额。</param>
    /// <param name="currency">货币单位。</param>
    /// <param name="decimalPlaces">小数位数。</param>
    /// <returns>格式化后的货币字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = UnitHelper.FormatCurrency(1234.56m, CurrencyUnit.USD);
    /// // formatted = "$1,234.56"
    /// </code>
    /// </example>
    public static string FormatCurrency(decimal amount, CurrencyUnit currency, int decimalPlaces = 2)
    {
        var symbol = GetCurrencySymbol(currency);
        return $"{symbol}{amount.ToString($"N{decimalPlaces}", CultureInfo.InvariantCulture)}";
    }

    #endregion

    #region 质量转换

    /// <summary>
    /// 质量单位枚举。
    /// </summary>
    public enum MassUnit
    {
        /// <summary>
        /// 毫克。
        /// </summary>
        Milligram,

        /// <summary>
        /// 克。
        /// </summary>
        Gram,

        /// <summary>
        /// 千克。
        /// </summary>
        Kilogram,

        /// <summary>
        /// 吨。
        /// </summary>
        Ton,

        /// <summary>
        /// 盎司。
        /// </summary>
        Ounce,

        /// <summary>
        /// 磅。
        /// </summary>
        Pound,

        /// <summary>
        /// 英石。
        /// </summary>
        Stone
    }

    /// <summary>
    /// 质量单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var kg = UnitHelper.ConvertMass(1000, MassUnit.Gram, MassUnit.Kilogram);
    /// // kg = 1
    /// </code>
    /// </example>
    public static double ConvertMass(double value, MassUnit from, MassUnit to)
    {
        if (from == to) return value;

        double kgValue = from switch
        {
            MassUnit.Milligram => value / 1_000_000,
            MassUnit.Gram => value / 1_000,
            MassUnit.Kilogram => value,
            MassUnit.Ton => value * 1_000,
            MassUnit.Ounce => value * 0.028349523125,
            MassUnit.Pound => value * 0.45359237,
            MassUnit.Stone => value * 6.35029318,
            _ => value
        };

        return to switch
        {
            MassUnit.Milligram => kgValue * 1_000_000,
            MassUnit.Gram => kgValue * 1_000,
            MassUnit.Kilogram => kgValue,
            MassUnit.Ton => kgValue / 1_000,
            MassUnit.Ounce => kgValue / 0.028349523125,
            MassUnit.Pound => kgValue / 0.45359237,
            MassUnit.Stone => kgValue / 6.35029318,
            _ => kgValue
        };
    }

    #endregion

    #region 角度转换

    /// <summary>
    /// 角度单位枚举。
    /// </summary>
    public enum AngleUnit
    {
        /// <summary>
        /// 度。
        /// </summary>
        Degree,

        /// <summary>
        /// 弧度。
        /// </summary>
        Radian,

        /// <summary>
        /// 百分度。
        /// </summary>
        Grad,

        /// <summary>
        /// 角分。
        /// </summary>
        Minute,

        /// <summary>
        /// 角秒。
        /// </summary>
        Second
    }

    /// <summary>
    /// 角度单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var radians = UnitHelper.ConvertAngle(180, AngleUnit.Degree, AngleUnit.Radian);
    /// // radians ≈ 3.14159 (π)
    /// </code>
    /// </example>
    public static double ConvertAngle(double value, AngleUnit from, AngleUnit to)
    {
        if (from == to) return value;

        double degreeValue = from switch
        {
            AngleUnit.Degree => value,
            AngleUnit.Radian => value * 180 / Math.PI,
            AngleUnit.Grad => value * 0.9,
            AngleUnit.Minute => value / 60,
            AngleUnit.Second => value / 3600,
            _ => value
        };

        return to switch
        {
            AngleUnit.Degree => degreeValue,
            AngleUnit.Radian => degreeValue * Math.PI / 180,
            AngleUnit.Grad => degreeValue / 0.9,
            AngleUnit.Minute => degreeValue * 60,
            AngleUnit.Second => degreeValue * 3600,
            _ => degreeValue
        };
    }

    /// <summary>
    /// 角度格式化为度分秒格式。
    /// </summary>
    /// <param name="degrees">角度值（度）。</param>
    /// <returns>度分秒格式字符串。</returns>
    /// <example>
    /// <code>
    /// var dms = UnitHelper.FormatAngleToDMS(45.5);
    /// // dms = "45°30'0""
    /// </code>
    /// </example>
    public static string FormatAngleToDMS(double degrees)
    {
        var d = (int)degrees;
        var minutes = (degrees - d) * 60;
        var m = (int)minutes;
        var s = (minutes - m) * 60;

        return $"{d}°{m}'{s:F0}\"";
    }

    /// <summary>
    /// 度分秒格式转换为角度值。
    /// </summary>
    /// <param name="degrees">度。</param>
    /// <param name="minutes">分。</param>
    /// <param name="seconds">秒。</param>
    /// <returns>角度值（度）。</returns>
    /// <example>
    /// <code>
    /// var degrees = UnitHelper.ParseDMSToDegrees(45, 30, 0);
    /// // degrees = 45.5
    /// </code>
    /// </example>
    public static double ParseDMSToDegrees(int degrees, int minutes, double seconds)
    {
        return degrees + minutes / 60.0 + seconds / 3600.0;
    }

    #endregion

    #region 进制转换

    /// <summary>
    /// 数字进制枚举。
    /// </summary>
    public enum NumberBase
    {
        /// <summary>
        /// 二进制。
        /// </summary>
        Binary = 2,

        /// <summary>
        /// 八进制。
        /// </summary>
        Octal = 8,

        /// <summary>
        /// 十进制。
        /// </summary>
        Decimal = 10,

        /// <summary>
        /// 十六进制。
        /// </summary>
        Hexadecimal = 16
    }

    /// <summary>
    /// 进制转换。
    /// </summary>
    /// <param name="value">数值字符串。</param>
    /// <param name="fromBase">源进制。</param>
    /// <param name="toBase">目标进制。</param>
    /// <returns>转换后的数值字符串。</returns>
    /// <exception cref="ArgumentException">value 为空或无效时抛出。</exception>
    /// <example>
    /// <code>
    /// var hex = UnitHelper.ConvertBase("255", NumberBase.Decimal, NumberBase.Hexadecimal);
    /// // hex = "FF"
    /// var binary = UnitHelper.ConvertBase("FF", NumberBase.Hexadecimal, NumberBase.Binary);
    /// // binary = "11111111"
    /// </code>
    /// </example>
    public static string ConvertBase(string value, NumberBase fromBase, NumberBase toBase)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if (fromBase == toBase) return value.ToUpper();

        var decimalValue = Convert.ToInt64(value, (int)fromBase);
        return Convert.ToString(decimalValue, (int)toBase).ToUpper();
    }

    /// <summary>
    /// 十进制转换为其他进制。
    /// </summary>
    /// <param name="value">十进制数值。</param>
    /// <param name="toBase">目标进制。</param>
    /// <returns>转换后的数值字符串。</returns>
    /// <example>
    /// <code>
    /// var hex = UnitHelper.ConvertFromDecimal(255, NumberBase.Hexadecimal);
    /// // hex = "FF"
    /// </code>
    /// </example>
    public static string ConvertFromDecimal(long value, NumberBase toBase)
    {
        return Convert.ToString(value, (int)toBase).ToUpper();
    }

    /// <summary>
    /// 其他进制转换为十进制。
    /// </summary>
    /// <param name="value">数值字符串。</param>
    /// <param name="fromBase">源进制。</param>
    /// <returns>十进制数值。</returns>
    /// <exception cref="ArgumentException">value 为空或无效时抛出。</exception>
    /// <example>
    /// <code>
    /// var decimalValue = UnitHelper.ConvertToDecimal("FF", NumberBase.Hexadecimal);
    /// // decimalValue = 255
    /// </code>
    /// </example>
    public static long ConvertToDecimal(string value, NumberBase fromBase)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        return Convert.ToInt64(value, (int)fromBase);
    }

    #endregion

    #region 面积转换

    /// <summary>
    /// 面积单位枚举。
    /// </summary>
    public enum AreaUnit
    {
        /// <summary>
        /// 平方毫米。
        /// </summary>
        SquareMillimeter,

        /// <summary>
        /// 平方厘米。
        /// </summary>
        SquareCentimeter,

        /// <summary>
        /// 平方米。
        /// </summary>
        SquareMeter,

        /// <summary>
        /// 公顷。
        /// </summary>
        Hectare,

        /// <summary>
        /// 平方公里。
        /// </summary>
        SquareKilometer,

        /// <summary>
        /// 平方英寸。
        /// </summary>
        SquareInch,

        /// <summary>
        /// 平方英尺。
        /// </summary>
        SquareFoot,

        /// <summary>
        /// 平方码。
        /// </summary>
        SquareYard,

        /// <summary>
        /// 英亩。
        /// </summary>
        Acre,

        /// <summary>
        /// 平方英里。
        /// </summary>
        SquareMile,

        /// <summary>
        /// 亩（中国市亩）。
        /// </summary>
        Mu
    }

    /// <summary>
    /// 面积单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var sqMeters = UnitHelper.ConvertArea(1, AreaUnit.Hectare, AreaUnit.SquareMeter);
    /// // sqMeters = 10000
    /// </code>
    /// </example>
    public static double ConvertArea(double value, AreaUnit from, AreaUnit to)
    {
        if (from == to) return value;

        double sqMeterValue = from switch
        {
            AreaUnit.SquareMillimeter => value / 1_000_000,
            AreaUnit.SquareCentimeter => value / 10_000,
            AreaUnit.SquareMeter => value,
            AreaUnit.Hectare => value * 10_000,
            AreaUnit.SquareKilometer => value * 1_000_000,
            AreaUnit.SquareInch => value * 0.00064516,
            AreaUnit.SquareFoot => value * 0.09290304,
            AreaUnit.SquareYard => value * 0.83612736,
            AreaUnit.Acre => value * 4046.8564224,
            AreaUnit.SquareMile => value * 2_589_988.110336,
            AreaUnit.Mu => value * 666.666666667,
            _ => value
        };

        return to switch
        {
            AreaUnit.SquareMillimeter => sqMeterValue * 1_000_000,
            AreaUnit.SquareCentimeter => sqMeterValue * 10_000,
            AreaUnit.SquareMeter => sqMeterValue,
            AreaUnit.Hectare => sqMeterValue / 10_000,
            AreaUnit.SquareKilometer => sqMeterValue / 1_000_000,
            AreaUnit.SquareInch => sqMeterValue / 0.00064516,
            AreaUnit.SquareFoot => sqMeterValue / 0.09290304,
            AreaUnit.SquareYard => sqMeterValue / 0.83612736,
            AreaUnit.Acre => sqMeterValue / 4046.8564224,
            AreaUnit.SquareMile => sqMeterValue / 2_589_988.110336,
            AreaUnit.Mu => sqMeterValue / 666.666666667,
            _ => sqMeterValue
        };
    }

    #endregion

    #region 体积转换

    /// <summary>
    /// 体积单位枚举。
    /// </summary>
    public enum VolumeUnit
    {
        /// <summary>
        /// 立方毫米。
        /// </summary>
        CubicMillimeter,

        /// <summary>
        /// 立方厘米。
        /// </summary>
        CubicCentimeter,

        /// <summary>
        /// 立方米。
        /// </summary>
        CubicMeter,

        /// <summary>
        /// 升。
        /// </summary>
        Liter,

        /// <summary>
        /// 毫升。
        /// </summary>
        Milliliter,

        /// <summary>
        /// 立方英寸。
        /// </summary>
        CubicInch,

        /// <summary>
        /// 立方英尺。
        /// </summary>
        CubicFoot,

        /// <summary>
        /// 立方码。
        /// </summary>
        CubicYard,

        /// <summary>
        /// 美制加仑。
        /// </summary>
        GallonUS,

        /// <summary>
        /// 英制加仑。
        /// </summary>
        GallonUK,

        /// <summary>
        /// 美制夸脱。
        /// </summary>
        QuartUS,

        /// <summary>
        /// 美制品脱。
        /// </summary>
        PintUS
    }

    /// <summary>
    /// 体积单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var liters = UnitHelper.ConvertVolume(1, VolumeUnit.GallonUS, VolumeUnit.Liter);
    /// // liters ≈ 3.785
    /// </code>
    /// </example>
    public static double ConvertVolume(double value, VolumeUnit from, VolumeUnit to)
    {
        if (from == to) return value;

        double cbMeterValue = from switch
        {
            VolumeUnit.CubicMillimeter => value / 1_000_000_000,
            VolumeUnit.CubicCentimeter => value / 1_000_000,
            VolumeUnit.CubicMeter => value,
            VolumeUnit.Liter => value / 1_000,
            VolumeUnit.Milliliter => value / 1_000_000,
            VolumeUnit.CubicInch => value * 0.000016387064,
            VolumeUnit.CubicFoot => value * 0.028316846592,
            VolumeUnit.CubicYard => value * 0.764554857984,
            VolumeUnit.GallonUS => value * 0.003785411784,
            VolumeUnit.GallonUK => value * 0.00454609,
            VolumeUnit.QuartUS => value * 0.000946352946,
            VolumeUnit.PintUS => value * 0.000473176473,
            _ => value
        };

        return to switch
        {
            VolumeUnit.CubicMillimeter => cbMeterValue * 1_000_000_000,
            VolumeUnit.CubicCentimeter => cbMeterValue * 1_000_000,
            VolumeUnit.CubicMeter => cbMeterValue,
            VolumeUnit.Liter => cbMeterValue * 1_000,
            VolumeUnit.Milliliter => cbMeterValue * 1_000_000,
            VolumeUnit.CubicInch => cbMeterValue / 0.000016387064,
            VolumeUnit.CubicFoot => cbMeterValue / 0.028316846592,
            VolumeUnit.CubicYard => cbMeterValue / 0.764554857984,
            VolumeUnit.GallonUS => cbMeterValue / 0.003785411784,
            VolumeUnit.GallonUK => cbMeterValue / 0.00454609,
            VolumeUnit.QuartUS => cbMeterValue / 0.000946352946,
            VolumeUnit.PintUS => cbMeterValue / 0.000473176473,
            _ => cbMeterValue
        };
    }

    #endregion

    #region 温度转换

    /// <summary>
    /// 温度单位枚举。
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>
        /// 摄氏度。
        /// </summary>
        Celsius,

        /// <summary>
        /// 华氏度。
        /// </summary>
        Fahrenheit,

        /// <summary>
        /// 开尔文。
        /// </summary>
        Kelvin
    }

    /// <summary>
    /// 温度单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var fahrenheit = UnitHelper.ConvertTemperature(100, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);
    /// // fahrenheit = 212
    /// var kelvin = UnitHelper.ConvertTemperature(0, TemperatureUnit.Celsius, TemperatureUnit.Kelvin);
    /// // kelvin = 273.15
    /// </code>
    /// </example>
    public static double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
    {
        if (from == to) return value;

        double celsiusValue = from switch
        {
            TemperatureUnit.Celsius => value,
            TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
            TemperatureUnit.Kelvin => value - 273.15,
            _ => value
        };

        return to switch
        {
            TemperatureUnit.Celsius => celsiusValue,
            TemperatureUnit.Fahrenheit => celsiusValue * 9 / 5 + 32,
            TemperatureUnit.Kelvin => celsiusValue + 273.15,
            _ => celsiusValue
        };
    }

    /// <summary>
    /// 格式化温度显示。
    /// </summary>
    /// <param name="value">温度值。</param>
    /// <param name="unit">温度单位。</param>
    /// <param name="decimalPlaces">小数位数。</param>
    /// <returns>格式化后的温度字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = UnitHelper.FormatTemperature(25.5, TemperatureUnit.Celsius);
    /// // formatted = "25.5°C"
    /// </code>
    /// </example>
    public static string FormatTemperature(double value, TemperatureUnit unit, int decimalPlaces = 1)
    {
        var symbol = unit switch
        {
            TemperatureUnit.Celsius => "°C",
            TemperatureUnit.Fahrenheit => "°F",
            TemperatureUnit.Kelvin => "K",
            _ => unit.ToString()
        };

        return $"{Math.Round(value, decimalPlaces)}{symbol}";
    }

    #endregion

    #region 速度转换

    /// <summary>
    /// 速度单位枚举。
    /// </summary>
    public enum SpeedUnit
    {
        /// <summary>
        /// 米/秒。
        /// </summary>
        MetersPerSecond,

        /// <summary>
        /// 公里/小时。
        /// </summary>
        KilometersPerHour,

        /// <summary>
        /// 英里/小时。
        /// </summary>
        MilesPerHour,

        /// <summary>
        /// 英尺/秒。
        /// </summary>
        FeetPerSecond,

        /// <summary>
        /// 节（海里/小时）。
        /// </summary>
        Knots
    }

    /// <summary>
    /// 速度单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var kmh = UnitHelper.ConvertSpeed(10, SpeedUnit.MetersPerSecond, SpeedUnit.KilometersPerHour);
    /// // kmh = 36
    /// </code>
    /// </example>
    public static double ConvertSpeed(double value, SpeedUnit from, SpeedUnit to)
    {
        if (from == to) return value;

        double mpsValue = from switch
        {
            SpeedUnit.MetersPerSecond => value,
            SpeedUnit.KilometersPerHour => value / 3.6,
            SpeedUnit.MilesPerHour => value * 0.44704,
            SpeedUnit.FeetPerSecond => value * 0.3048,
            SpeedUnit.Knots => value * 0.514444,
            _ => value
        };

        return to switch
        {
            SpeedUnit.MetersPerSecond => mpsValue,
            SpeedUnit.KilometersPerHour => mpsValue * 3.6,
            SpeedUnit.MilesPerHour => mpsValue / 0.44704,
            SpeedUnit.FeetPerSecond => mpsValue / 0.3048,
            SpeedUnit.Knots => mpsValue / 0.514444,
            _ => mpsValue
        };
    }

    #endregion

    #region 热能转换

    /// <summary>
    /// 热能单位枚举。
    /// </summary>
    public enum EnergyUnit
    {
        /// <summary>
        /// 焦耳。
        /// </summary>
        Joule,

        /// <summary>
        /// 千焦。
        /// </summary>
        Kilojoule,

        /// <summary>
        /// 卡路里。
        /// </summary>
        Calorie,

        /// <summary>
        /// 千卡（大卡）。
        /// </summary>
        Kilocalorie,

        /// <summary>
        /// 瓦时。
        /// </summary>
        WattHour,

        /// <summary>
        /// 千瓦时（度）。
        /// </summary>
        KilowattHour,

        /// <summary>
        /// 英国热量单位。
        /// </summary>
        BTU,

        /// <summary>
        /// 电子伏特。
        /// </summary>
        ElectronVolt
    }

    /// <summary>
    /// 热能单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var kcal = UnitHelper.ConvertEnergy(1000, EnergyUnit.Calorie, EnergyUnit.Kilocalorie);
    /// // kcal = 1
    /// </code>
    /// </example>
    public static double ConvertEnergy(double value, EnergyUnit from, EnergyUnit to)
    {
        if (from == to) return value;

        double jouleValue = from switch
        {
            EnergyUnit.Joule => value,
            EnergyUnit.Kilojoule => value * 1_000,
            EnergyUnit.Calorie => value * 4.184,
            EnergyUnit.Kilocalorie => value * 4_184,
            EnergyUnit.WattHour => value * 3_600,
            EnergyUnit.KilowattHour => value * 3_600_000,
            EnergyUnit.BTU => value * 1_055.06,
            EnergyUnit.ElectronVolt => value * 1.602176634e-19,
            _ => value
        };

        return to switch
        {
            EnergyUnit.Joule => jouleValue,
            EnergyUnit.Kilojoule => jouleValue / 1_000,
            EnergyUnit.Calorie => jouleValue / 4.184,
            EnergyUnit.Kilocalorie => jouleValue / 4_184,
            EnergyUnit.WattHour => jouleValue / 3_600,
            EnergyUnit.KilowattHour => jouleValue / 3_600_000,
            EnergyUnit.BTU => jouleValue / 1_055.06,
            EnergyUnit.ElectronVolt => jouleValue / 1.602176634e-19,
            _ => jouleValue
        };
    }

    #endregion

    #region 功率转换

    /// <summary>
    /// 功率单位枚举。
    /// </summary>
    public enum PowerUnit
    {
        /// <summary>
        /// 瓦特。
        /// </summary>
        Watt,

        /// <summary>
        /// 千瓦。
        /// </summary>
        Kilowatt,

        /// <summary>
        /// 马力（公制）。
        /// </summary>
        Horsepower,

        /// <summary>
        /// 英制马力。
        /// </summary>
        HorsepowerUK,

        /// <summary>
        /// 兆瓦。
        /// </summary>
        Megawatt
    }

    /// <summary>
    /// 功率单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var kw = UnitHelper.ConvertPower(100, PowerUnit.Horsepower, PowerUnit.Kilowatt);
    /// // kw ≈ 73.5
    /// </code>
    /// </example>
    public static double ConvertPower(double value, PowerUnit from, PowerUnit to)
    {
        if (from == to) return value;

        double wattValue = from switch
        {
            PowerUnit.Watt => value,
            PowerUnit.Kilowatt => value * 1_000,
            PowerUnit.Horsepower => value * 735.49875,
            PowerUnit.HorsepowerUK => value * 745.699872,
            PowerUnit.Megawatt => value * 1_000_000,
            _ => value
        };

        return to switch
        {
            PowerUnit.Watt => wattValue,
            PowerUnit.Kilowatt => wattValue / 1_000,
            PowerUnit.Horsepower => wattValue / 735.49875,
            PowerUnit.HorsepowerUK => wattValue / 745.699872,
            PowerUnit.Megawatt => wattValue / 1_000_000,
            _ => wattValue
        };
    }

    #endregion

    #region 压强转换

    /// <summary>
    /// 压强单位枚举。
    /// </summary>
    public enum PressureUnit
    {
        /// <summary>
        /// 帕斯卡。
        /// </summary>
        Pascal,

        /// <summary>
        /// 千帕。
        /// </summary>
        Kilopascal,

        /// <summary>
        /// 巴。
        /// </summary>
        Bar,

        /// <summary>
        /// 标准大气压。
        /// </summary>
        Atmosphere,

        /// <summary>
        /// 毫米汞柱。
        /// </summary>
        MillimeterOfMercury,

        /// <summary>
        /// 托（Torr）。
        /// </summary>
        Torr,

        /// <summary>
        /// 磅力/平方英寸。
        /// </summary>
        PSI
    }

    /// <summary>
    /// 压强单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var atm = UnitHelper.ConvertPressure(101325, PressureUnit.Pascal, PressureUnit.Atmosphere);
    /// // atm = 1
    /// </code>
    /// </example>
    public static double ConvertPressure(double value, PressureUnit from, PressureUnit to)
    {
        if (from == to) return value;

        double pascalValue = from switch
        {
            PressureUnit.Pascal => value,
            PressureUnit.Kilopascal => value * 1_000,
            PressureUnit.Bar => value * 100_000,
            PressureUnit.Atmosphere => value * 101_325,
            PressureUnit.MillimeterOfMercury => value * 133.322,
            PressureUnit.Torr => value * 133.322,
            PressureUnit.PSI => value * 6_894.76,
            _ => value
        };

        return to switch
        {
            PressureUnit.Pascal => pascalValue,
            PressureUnit.Kilopascal => pascalValue / 1_000,
            PressureUnit.Bar => pascalValue / 100_000,
            PressureUnit.Atmosphere => pascalValue / 101_325,
            PressureUnit.MillimeterOfMercury => pascalValue / 133.322,
            PressureUnit.Torr => pascalValue / 133.322,
            PressureUnit.PSI => pascalValue / 6_894.76,
            _ => pascalValue
        };
    }

    #endregion

    #region 力转换

    /// <summary>
    /// 力单位枚举。
    /// </summary>
    public enum ForceUnit
    {
        /// <summary>
        /// 牛顿。
        /// </summary>
        Newton,

        /// <summary>
        /// 千牛顿。
        /// </summary>
        Kilonewton,

        /// <summary>
        /// 达因。
        /// </summary>
        Dyne,

        /// <summary>
        /// 磅力。
        /// </summary>
        PoundForce,

        /// <summary>
        /// 千克力。
        /// </summary>
        KilogramForce
    }

    /// <summary>
    /// 力单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var lbf = UnitHelper.ConvertForce(1, ForceUnit.KilogramForce, ForceUnit.PoundForce);
    /// // lbf ≈ 2.205
    /// </code>
    /// </example>
    public static double ConvertForce(double value, ForceUnit from, ForceUnit to)
    {
        if (from == to) return value;

        double newtonValue = from switch
        {
            ForceUnit.Newton => value,
            ForceUnit.Kilonewton => value * 1_000,
            ForceUnit.Dyne => value * 0.00001,
            ForceUnit.PoundForce => value * 4.4482216153,
            ForceUnit.KilogramForce => value * 9.80665,
            _ => value
        };

        return to switch
        {
            ForceUnit.Newton => newtonValue,
            ForceUnit.Kilonewton => newtonValue / 1_000,
            ForceUnit.Dyne => newtonValue / 0.00001,
            ForceUnit.PoundForce => newtonValue / 4.4482216153,
            ForceUnit.KilogramForce => newtonValue / 9.80665,
            _ => newtonValue
        };
    }

    #endregion

    #region 数据存储转换

    /// <summary>
    /// 数据存储单位枚举。
    /// </summary>
    public enum DataStorageUnit
    {
        /// <summary>
        /// 字节。
        /// </summary>
        Byte,

        /// <summary>
        /// 千字节（KB，二进制）。
        /// </summary>
        Kilobyte,

        /// <summary>
        /// 兆字节（MB，二进制）。
        /// </summary>
        Megabyte,

        /// <summary>
        /// 吉字节（GB，二进制）。
        /// </summary>
        Gigabyte,

        /// <summary>
        /// 太字节（TB，二进制）。
        /// </summary>
        Terabyte,

        /// <summary>
        /// 拍字节（PB，二进制）。
        /// </summary>
        Petabyte,

        /// <summary>
        /// 艾字节（EB，二进制）。
        /// </summary>
        Exabyte,

        /// <summary>
        /// 千字节（KB，十进制）。
        /// </summary>
        KilobyteDecimal,

        /// <summary>
        /// 兆字节（MB，十进制）。
        /// </summary>
        MegabyteDecimal,

        /// <summary>
        /// 吉字节（GB，十进制）。
        /// </summary>
        GigabyteDecimal,

        /// <summary>
        /// 太字节（TB，十进制）。
        /// </summary>
        TerabyteDecimal
    }

    /// <summary>
    /// 数据存储单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <remarks>
    /// 二进制单位使用1024作为进率（1KB = 1024B），十进制单位使用1000作为进率（1KB = 1000B）。
    /// </remarks>
    /// <example>
    /// <code>
    /// var gb = UnitHelper.ConvertDataStorage(1024, DataStorageUnit.Megabyte, DataStorageUnit.Gigabyte);
    /// // gb = 1
    /// </code>
    /// </example>
    public static double ConvertDataStorage(double value, DataStorageUnit from, DataStorageUnit to)
    {
        if (from == to) return value;

        double byteValue = from switch
        {
            DataStorageUnit.Byte => value,
            DataStorageUnit.Kilobyte => value * 1024,
            DataStorageUnit.Megabyte => value * 1024 * 1024,
            DataStorageUnit.Gigabyte => value * 1024 * 1024 * 1024,
            DataStorageUnit.Terabyte => value * 1024L * 1024 * 1024 * 1024,
            DataStorageUnit.Petabyte => value * 1024L * 1024 * 1024 * 1024 * 1024,
            DataStorageUnit.Exabyte => value * 1024L * 1024 * 1024 * 1024 * 1024 * 1024,
            DataStorageUnit.KilobyteDecimal => value * 1000,
            DataStorageUnit.MegabyteDecimal => value * 1000 * 1000,
            DataStorageUnit.GigabyteDecimal => value * 1000 * 1000 * 1000,
            DataStorageUnit.TerabyteDecimal => value * 1000L * 1000 * 1000 * 1000,
            _ => value
        };

        return to switch
        {
            DataStorageUnit.Byte => byteValue,
            DataStorageUnit.Kilobyte => byteValue / 1024,
            DataStorageUnit.Megabyte => byteValue / (1024 * 1024),
            DataStorageUnit.Gigabyte => byteValue / (1024 * 1024 * 1024),
            DataStorageUnit.Terabyte => byteValue / (1024L * 1024 * 1024 * 1024),
            DataStorageUnit.Petabyte => byteValue / (1024L * 1024 * 1024 * 1024 * 1024),
            DataStorageUnit.Exabyte => byteValue / (1024L * 1024 * 1024 * 1024 * 1024 * 1024),
            DataStorageUnit.KilobyteDecimal => byteValue / 1000,
            DataStorageUnit.MegabyteDecimal => byteValue / (1000 * 1000),
            DataStorageUnit.GigabyteDecimal => byteValue / (1000 * 1000 * 1000),
            DataStorageUnit.TerabyteDecimal => byteValue / (1000L * 1000 * 1000 * 1000),
            _ => byteValue
        };
    }

    /// <summary>
    /// 格式化数据存储大小为友好字符串。
    /// </summary>
    /// <param name="bytes">字节数。</param>
    /// <param name="decimalPlaces">小数位数。</param>
    /// <returns>格式化后的字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = UnitHelper.FormatDataStorage(1536);
    /// // formatted = "1.50 KB"
    /// var formatted2 = UnitHelper.FormatDataStorage(1536000);
    /// // formatted2 = "1.46 MB"
    /// </code>
    /// </example>
    public static string FormatDataStorage(double bytes, int decimalPlaces = 2)
    {
        string[] suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        var counter = 0;
        var number = bytes;

        while (Math.Round(number, decimalPlaces) >= 1024 && counter < suffixes.Length - 1)
        {
            number /= 1024;
            counter++;
        }

        return $"{Math.Round(number, decimalPlaces)} {suffixes[counter]}";
    }

    #endregion

    #region 时间转换

    /// <summary>
    /// 时间单位枚举。
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// 毫秒。
        /// </summary>
        Millisecond,

        /// <summary>
        /// 秒。
        /// </summary>
        Second,

        /// <summary>
        /// 分钟。
        /// </summary>
        Minute,

        /// <summary>
        /// 小时。
        /// </summary>
        Hour,

        /// <summary>
        /// 天。
        /// </summary>
        Day,

        /// <summary>
        /// 周。
        /// </summary>
        Week,

        /// <summary>
        /// 月（按30天计算）。
        /// </summary>
        Month,

        /// <summary>
        /// 年（按365天计算）。
        /// </summary>
        Year
    }

    /// <summary>
    /// 时间单位转换。
    /// </summary>
    /// <param name="value">要转换的数值。</param>
    /// <param name="from">源单位。</param>
    /// <param name="to">目标单位。</param>
    /// <returns>转换后的数值。</returns>
    /// <example>
    /// <code>
    /// var minutes = UnitHelper.ConvertTime(1, TimeUnit.Hour, TimeUnit.Minute);
    /// // minutes = 60
    /// </code>
    /// </example>
    public static double ConvertTime(double value, TimeUnit from, TimeUnit to)
    {
        if (from == to) return value;

        double secondValue = from switch
        {
            TimeUnit.Millisecond => value / 1_000,
            TimeUnit.Second => value,
            TimeUnit.Minute => value * 60,
            TimeUnit.Hour => value * 3600,
            TimeUnit.Day => value * 86400,
            TimeUnit.Week => value * 604800,
            TimeUnit.Month => value * 2592000,
            TimeUnit.Year => value * 31536000,
            _ => value
        };

        return to switch
        {
            TimeUnit.Millisecond => secondValue * 1_000,
            TimeUnit.Second => secondValue,
            TimeUnit.Minute => secondValue / 60,
            TimeUnit.Hour => secondValue / 3600,
            TimeUnit.Day => secondValue / 86400,
            TimeUnit.Week => secondValue / 604800,
            TimeUnit.Month => secondValue / 2592000,
            TimeUnit.Year => secondValue / 31536000,
            _ => secondValue
        };
    }

    /// <summary>
    /// 格式化时间跨度为友好字符串。
    /// </summary>
    /// <param name="seconds">秒数。</param>
    /// <param name="decimalPlaces">小数位数。</param>
    /// <returns>格式化后的字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = UnitHelper.FormatTime(3665);
    /// // formatted = "1小时1分钟5秒"
    /// </code>
    /// </example>
    public static string FormatTime(double seconds, int decimalPlaces = 0)
    {
        if (seconds < 0) return "0秒";

        var timeSpan = TimeSpan.FromSeconds(seconds);

        var parts = new List<string>();

        if (timeSpan.Days > 0)
            parts.Add($"{timeSpan.Days}天");

        if (timeSpan.Hours > 0)
            parts.Add($"{timeSpan.Hours}小时");

        if (timeSpan.Minutes > 0)
            parts.Add($"{timeSpan.Minutes}分钟");

        if (timeSpan.Seconds > 0 || parts.Count == 0)
            parts.Add($"{timeSpan.Seconds}秒");

        return string.Join("", parts);
    }

    #endregion

    #region 实用工具方法

    /// <summary>
    /// 获取单位符号。
    /// </summary>
    /// <param name="unit">单位枚举值。</param>
    /// <returns>单位符号。</returns>
    /// <example>
    /// <code>
    /// var symbol = UnitHelper.GetUnitSymbol(LengthUnit.Meter);
    /// // symbol = "m"
    /// </code>
    /// </example>
    public static string GetUnitSymbol(Enum unit)
    {
        return unit switch
        {
            LengthUnit.Millimeter => "mm",
            LengthUnit.Centimeter => "cm",
            LengthUnit.Meter => "m",
            LengthUnit.Kilometer => "km",
            LengthUnit.Inch => "in",
            LengthUnit.Foot => "ft",
            LengthUnit.Yard => "yd",
            LengthUnit.Mile => "mi",
            LengthUnit.NauticalMile => "nmi",
            MassUnit.Milligram => "mg",
            MassUnit.Gram => "g",
            MassUnit.Kilogram => "kg",
            MassUnit.Ton => "t",
            MassUnit.Ounce => "oz",
            MassUnit.Pound => "lb",
            MassUnit.Stone => "st",
            AreaUnit.SquareMillimeter => "mm²",
            AreaUnit.SquareCentimeter => "cm²",
            AreaUnit.SquareMeter => "m²",
            AreaUnit.Hectare => "ha",
            AreaUnit.SquareKilometer => "km²",
            AreaUnit.SquareInch => "in²",
            AreaUnit.SquareFoot => "ft²",
            AreaUnit.SquareYard => "yd²",
            AreaUnit.Acre => "ac",
            AreaUnit.SquareMile => "mi²",
            AreaUnit.Mu => "亩",
            VolumeUnit.CubicMillimeter => "mm³",
            VolumeUnit.CubicCentimeter => "cm³",
            VolumeUnit.CubicMeter => "m³",
            VolumeUnit.Liter => "L",
            VolumeUnit.Milliliter => "mL",
            VolumeUnit.CubicInch => "in³",
            VolumeUnit.CubicFoot => "ft³",
            VolumeUnit.CubicYard => "yd³",
            VolumeUnit.GallonUS => "gal(US)",
            VolumeUnit.GallonUK => "gal(UK)",
            TemperatureUnit.Celsius => "°C",
            TemperatureUnit.Fahrenheit => "°F",
            TemperatureUnit.Kelvin => "K",
            SpeedUnit.MetersPerSecond => "m/s",
            SpeedUnit.KilometersPerHour => "km/h",
            SpeedUnit.MilesPerHour => "mph",
            SpeedUnit.FeetPerSecond => "ft/s",
            SpeedUnit.Knots => "kn",
            EnergyUnit.Joule => "J",
            EnergyUnit.Kilojoule => "kJ",
            EnergyUnit.Calorie => "cal",
            EnergyUnit.Kilocalorie => "kcal",
            EnergyUnit.WattHour => "Wh",
            EnergyUnit.KilowattHour => "kWh",
            EnergyUnit.BTU => "BTU",
            PowerUnit.Watt => "W",
            PowerUnit.Kilowatt => "kW",
            PowerUnit.Horsepower => "hp",
            PowerUnit.Megawatt => "MW",
            PressureUnit.Pascal => "Pa",
            PressureUnit.Kilopascal => "kPa",
            PressureUnit.Bar => "bar",
            PressureUnit.Atmosphere => "atm",
            PressureUnit.MillimeterOfMercury => "mmHg",
            PressureUnit.Torr => "Torr",
            PressureUnit.PSI => "psi",
            ForceUnit.Newton => "N",
            ForceUnit.Kilonewton => "kN",
            ForceUnit.Dyne => "dyn",
            ForceUnit.PoundForce => "lbf",
            ForceUnit.KilogramForce => "kgf",
            DataStorageUnit.Byte => "B",
            DataStorageUnit.Kilobyte => "KB",
            DataStorageUnit.Megabyte => "MB",
            DataStorageUnit.Gigabyte => "GB",
            DataStorageUnit.Terabyte => "TB",
            DataStorageUnit.Petabyte => "PB",
            DataStorageUnit.Exabyte => "EB",
            TimeUnit.Millisecond => "ms",
            TimeUnit.Second => "s",
            TimeUnit.Minute => "min",
            TimeUnit.Hour => "h",
            TimeUnit.Day => "d",
            TimeUnit.Week => "wk",
            TimeUnit.Month => "mo",
            TimeUnit.Year => "yr",
            _ => unit.ToString()
        };
    }

    /// <summary>
    /// 获取单位名称（中文）。
    /// </summary>
    /// <param name="unit">单位枚举值。</param>
    /// <returns>单位名称。</returns>
    /// <example>
    /// <code>
    /// var name = UnitHelper.GetUnitName(LengthUnit.Meter);
    /// // name = "米"
    /// </code>
    /// </example>
    public static string GetUnitName(Enum unit)
    {
        return unit switch
        {
            LengthUnit.Millimeter => "毫米",
            LengthUnit.Centimeter => "厘米",
            LengthUnit.Meter => "米",
            LengthUnit.Kilometer => "千米",
            LengthUnit.Inch => "英寸",
            LengthUnit.Foot => "英尺",
            LengthUnit.Yard => "码",
            LengthUnit.Mile => "英里",
            LengthUnit.NauticalMile => "海里",
            MassUnit.Milligram => "毫克",
            MassUnit.Gram => "克",
            MassUnit.Kilogram => "千克",
            MassUnit.Ton => "吨",
            MassUnit.Ounce => "盎司",
            MassUnit.Pound => "磅",
            MassUnit.Stone => "英石",
            AreaUnit.SquareMillimeter => "平方毫米",
            AreaUnit.SquareCentimeter => "平方厘米",
            AreaUnit.SquareMeter => "平方米",
            AreaUnit.Hectare => "公顷",
            AreaUnit.SquareKilometer => "平方公里",
            AreaUnit.SquareInch => "平方英寸",
            AreaUnit.SquareFoot => "平方英尺",
            AreaUnit.SquareYard => "平方码",
            AreaUnit.Acre => "英亩",
            AreaUnit.SquareMile => "平方英里",
            AreaUnit.Mu => "亩",
            VolumeUnit.CubicMillimeter => "立方毫米",
            VolumeUnit.CubicCentimeter => "立方厘米",
            VolumeUnit.CubicMeter => "立方米",
            VolumeUnit.Liter => "升",
            VolumeUnit.Milliliter => "毫升",
            VolumeUnit.CubicInch => "立方英寸",
            VolumeUnit.CubicFoot => "立方英尺",
            VolumeUnit.CubicYard => "立方码",
            VolumeUnit.GallonUS => "美制加仑",
            VolumeUnit.GallonUK => "英制加仑",
            TemperatureUnit.Celsius => "摄氏度",
            TemperatureUnit.Fahrenheit => "华氏度",
            TemperatureUnit.Kelvin => "开尔文",
            SpeedUnit.MetersPerSecond => "米/秒",
            SpeedUnit.KilometersPerHour => "公里/小时",
            SpeedUnit.MilesPerHour => "英里/小时",
            SpeedUnit.FeetPerSecond => "英尺/秒",
            SpeedUnit.Knots => "节",
            EnergyUnit.Joule => "焦耳",
            EnergyUnit.Kilojoule => "千焦",
            EnergyUnit.Calorie => "卡路里",
            EnergyUnit.Kilocalorie => "千卡",
            EnergyUnit.WattHour => "瓦时",
            EnergyUnit.KilowattHour => "千瓦时",
            EnergyUnit.BTU => "英热单位",
            PowerUnit.Watt => "瓦特",
            PowerUnit.Kilowatt => "千瓦",
            PowerUnit.Horsepower => "马力",
            PowerUnit.Megawatt => "兆瓦",
            PressureUnit.Pascal => "帕斯卡",
            PressureUnit.Kilopascal => "千帕",
            PressureUnit.Bar => "巴",
            PressureUnit.Atmosphere => "标准大气压",
            PressureUnit.MillimeterOfMercury => "毫米汞柱",
            PressureUnit.Torr => "托",
            PressureUnit.PSI => "磅力/平方英寸",
            ForceUnit.Newton => "牛顿",
            ForceUnit.Kilonewton => "千牛顿",
            ForceUnit.Dyne => "达因",
            ForceUnit.PoundForce => "磅力",
            ForceUnit.KilogramForce => "千克力",
            DataStorageUnit.Byte => "字节",
            DataStorageUnit.Kilobyte => "千字节",
            DataStorageUnit.Megabyte => "兆字节",
            DataStorageUnit.Gigabyte => "吉字节",
            DataStorageUnit.Terabyte => "太字节",
            DataStorageUnit.Petabyte => "拍字节",
            DataStorageUnit.Exabyte => "艾字节",
            TimeUnit.Millisecond => "毫秒",
            TimeUnit.Second => "秒",
            TimeUnit.Minute => "分钟",
            TimeUnit.Hour => "小时",
            TimeUnit.Day => "天",
            TimeUnit.Week => "周",
            TimeUnit.Month => "月",
            TimeUnit.Year => "年",
            _ => unit.ToString()
        };
    }

    /// <summary>
    /// 格式化带单位的数值。
    /// </summary>
    /// <param name="value">数值。</param>
    /// <param name="unit">单位。</param>
    /// <param name="decimalPlaces">小数位数。</param>
    /// <returns>格式化后的字符串。</returns>
    /// <example>
    /// <code>
    /// var formatted = UnitHelper.FormatWithUnit(25.5, LengthUnit.Meter);
    /// // formatted = "25.5 m"
    /// </code>
    /// </example>
    public static string FormatWithUnit(double value, Enum unit, int decimalPlaces = 2)
    {
        var symbol = GetUnitSymbol(unit);
        return $"{Math.Round(value, decimalPlaces)} {symbol}";
    }

    #endregion
}
