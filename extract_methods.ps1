$dirs = @('e:\Projects\Chet\Chet.Utils\docs\src\content\docs\extensions','e:\Projects\Chet\Chet.Utils\docs\src\content\docs\helpers')
foreach($dir in $dirs) {
  Get-ChildItem -Filter '*.mdx' -Path $dir | ForEach-Object {
    $file = $_.Name
    $content = Get-Content $_.FullName -Raw
    $methods = [regex]::Matches($content, '####\s+(\w+)') | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique
    Write-Output "$file : $($methods -join ', ')"
  }
}