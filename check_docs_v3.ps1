$ErrorActionPreference = 'SilentlyContinue'

$base = "e:\Projects\Chet\Chet.Utils"

$mapping = @{
    "$base\Chet.Utils\Extensions\BoolExtensions.cs" = "$base\docs\src\content\docs\extensions\bool-extensions.mdx"
    "$base\Chet.Utils\Extensions\IntExtensions.cs" = "$base\docs\src\content\docs\extensions\int-extensions.mdx"
    "$base\Chet.Utils\Extensions\DateTimeExtensions.cs" = "$base\docs\src\content\docs\extensions\datetime-extensions.mdx"
    "$base\Chet.Utils\Extensions\DecimalExtensions.cs" = "$base\docs\src\content\docs\extensions\decimal-extensions.mdx"
    "$base\Chet.Utils\Extensions\DoubleExtensions.cs" = "$base\docs\src\content\docs\extensions\double-extensions.mdx"
    "$base\Chet.Utils\Extensions\FloatExtensions.cs" = "$base\docs\src\content\docs\extensions\float-extensions.mdx"
    "$base\Chet.Utils\Extensions\EnumExtensions.cs" = "$base\docs\src\content\docs\extensions\enum-extensions.mdx"
    "$base\Chet.Utils\Extensions\StringExtensions.cs" = "$base\docs\src\content\docs\extensions\string-extensions.mdx"
    "$base\Chet.Utils\Extensions\FileExtensions.cs" = "$base\docs\src\content\docs\extensions\file-extensions.mdx"
    "$base\Chet.Utils\Extensions\StreamExtensions.cs" = "$base\docs\src\content\docs\extensions\stream-extensions.mdx"
    "$base\Chet.Utils\Extensions\EnumerableExtensions.cs" = "$base\docs\src\content\docs\extensions\enumerable-extensions.mdx"
    "$base\Chet.Utils\Extensions\DataTableExtensions.cs" = "$base\docs\src\content\docs\extensions\datatable-extensions.mdx"
    "$base\Chet.Utils\Helpers\ApplicationHelper.cs" = "$base\docs\src\content\docs\helpers\application-helper.mdx"
    "$base\Chet.Utils\Helpers\FileHelper.cs" = "$base\docs\src\content\docs\helpers\file-helper.mdx"
    "$base\Chet.Utils\Helpers\UnitHelper.cs" = "$base\docs\src\content\docs\helpers\unit-helper.mdx"
    "$base\Chet.Utils\Helpers\TaskHelper.cs" = "$base\docs\src\content\docs\helpers\task-helper.mdx"
    "$base\Chet.Utils\Helpers\StopwatchHelper.cs" = "$base\docs\src\content\docs\helpers\stopwatch-helper.mdx"
    "$base\Chet.Utils\Helpers\ReflectHelper.cs" = "$base\docs\src\content\docs\helpers\reflect-helper.mdx"
    "$base\Chet.Utils\Helpers\RegexHelper.cs" = "$base\docs\src\content\docs\helpers\regex-helper.mdx"
    "$base\Chet.Utils\Helpers\DataTableHelper.cs" = "$base\docs\src\content\docs\helpers\datatable-helper.mdx"
}

function Get-SourceMethods {
    param([string]$FilePath, [bool]$IsInstance = $false)
    
    $content = Get-Content $FilePath -Raw
    $lines = $content -split "`n"
    $methods = @()
    
    foreach ($line in $lines) {
        if ($IsInstance) {
            if ($line -match 'public\s+(?:async\s+)?(?:override\s+)?(?:virtual\s+)?[\w<>\[\]?,''.\s]+?\s+(\w+)\s*[<(]') {
                $name = $Matches[1]
                if ($name -match '^[A-Z]' -and $name -ne 'Dispose' -and $name -ne 'Finalize') {
                    $methods += $name
                }
            }
        } else {
            if ($line -match 'public\s+static\s+') {
                if ($line -match 'public\s+static\s+(?:async\s+)?[\w<>\[\]?,''.\s]+?\s+(\w+)\s*[<(]') {
                    $name = $Matches[1]
                    $className = [System.IO.Path]::GetFileNameWithoutExtension($FilePath)
                    if ($name -ne $className -and $name -match '^[A-Z]') {
                        $methods += $name
                    }
                }
            }
        }
    }
    
    return $methods | Sort-Object -Unique
}

function Get-DocMethods {
    param([string]$FilePath)
    
    $content = Get-Content $FilePath -Raw
    
    $m1 = [regex]::Matches($content, '####\s+(\w+)') | ForEach-Object { $_.Groups[1].Value }
    $m2 = [regex]::Matches($content, 'public\s+static\s+(?:async\s+)?[\w<>\[\]?,''.\s]+?\s+(\w+)\s*[<(]') | ForEach-Object { $_.Groups[1].Value }
    $m3 = [regex]::Matches($content, '`(\w+)\s*\(') | ForEach-Object { $_.Groups[1].Value }
    $m4 = [regex]::Matches($content, '\*\*(\w+)\*\*') | ForEach-Object { $_.Groups[1].Value }
    
    return ($m1 + $m2 + $m3 + $m4) | Sort-Object -Unique
}

foreach ($entry in $mapping.GetEnumerator()) {
    $srcFile = $entry.Key
    $docFile = $entry.Value
    
    $srcName = Split-Path $srcFile -Leaf
    $docName = Split-Path $docFile -Leaf
    
    Write-Host "`n========================================" -ForegroundColor Cyan
    Write-Host "SOURCE: $srcName" -ForegroundColor Cyan
    Write-Host "DOC:    $docName" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    
    if (-not (Test-Path $srcFile) -or -not (Test-Path $docFile)) {
        Write-Host "  FILE NOT FOUND!" -ForegroundColor Red
        continue
    }
    
    $srcMethods = Get-SourceMethods -FilePath $srcFile
    $docMethods = Get-DocMethods -FilePath $docFile
    
    $missing = $srcMethods | Where-Object { $_ -notin $docMethods }
    
    Write-Host "  Source: $($srcMethods.Count) methods | Doc: $($docMethods.Count) method references" -ForegroundColor White
    
    if ($missing.Count -gt 0) {
        Write-Host "  MISSING IN DOC ($($missing.Count)):" -ForegroundColor Red
        foreach ($m in $missing) {
            Write-Host "    - $m" -ForegroundColor Red
        }
    } else {
        Write-Host "  All source methods found in doc!" -ForegroundColor Green
    }
}