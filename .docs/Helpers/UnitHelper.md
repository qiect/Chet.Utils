# UnitHelper 帮助类

## 概述

[UnitHelper](../../Chet.Utils/Helpers/UnitHelper.cs) 是一个静态帮助类，为计量单位转换提供了丰富的功能，包括长度、货币、质量、角度、进制、面积、体积、温度、速度、热能、功率、压强、力、数据存储、时间等多种单位转换，旨在简化计量单位转换的开发工作，提供完整的单位枚举定义和格式化输出。

## 主要特性

- 支持多种计量单位的转换
- 提供完整的单位枚举定义
- 支持货币汇率转换（可更新汇率）
- 支持温度、速度、能量等物理量转换
- 提供数据存储单位转换（B、KB、MB、GB 等）
- 支持时间单位转换
- 提供格式化输出方法

## 类定义

```csharp
public static partial class UnitHelper
```

## 长度单位转换

### LengthUnit 枚举

**值：**
- `Millimeter`: 毫米
- `Centimeter`: 厘米
- `Meter`: 米
- `Kilometer`: 千米
- `Inch`: 英寸
- `Foot`: 英尺
- `Yard`: 码
- `Mile`: 英里
- `NauticalMile`: 海里

### ConvertLength

长度单位转换。

```csharp
public static double ConvertLength(double value, LengthUnit from, LengthUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var meters = UnitHelper.ConvertLength(100, LengthUnit.Centimeter, LengthUnit.Meter);
// meters = 1

var inches = UnitHelper.ConvertLength(1, LengthUnit.Meter, LengthUnit.Inch);
// inches ≈ 39.37

var miles = UnitHelper.ConvertLength(1, LengthUnit.Kilometer, LengthUnit.Mile);
// miles ≈ 0.621
```

## 货币单位转换

### CurrencyUnit 枚举

**值：**
- `CNY`: 人民币
- `USD`: 美元
- `EUR`: 欧元
- `GBP`: 英镑
- `JPY`: 日元
- `KRW`: 韩元
- `AUD`: 澳元
- `CAD`: 加元
- `CHF`: 瑞士法郎
- `HKD`: 港币

### ConvertCurrency

货币单位转换。

```csharp
public static decimal ConvertCurrency(decimal amount, CurrencyUnit from, CurrencyUnit to)
```

**参数：**
- `amount`: 要转换的金额
- `from`: 源货币单位
- `to`: 目标货币单位

**返回值：**
- 转换后的金额

**备注：**
汇率数据为示例数据，实际应用中应从 API 获取实时汇率。

**示例：**
```csharp
var usd = UnitHelper.ConvertCurrency(100, CurrencyUnit.CNY, CurrencyUnit.USD);
// usd ≈ 13.81

var eur = UnitHelper.ConvertCurrency(100, CurrencyUnit.USD, CurrencyUnit.EUR);
// eur ≈ 92
```

### UpdateCurrencyRate

更新货币汇率。

```csharp
public static void UpdateCurrencyRate(CurrencyUnit currency, decimal rate)
```

**参数：**
- `currency`: 货币单位
- `rate`: 对美元的汇率

**示例：**
```csharp
UnitHelper.UpdateCurrencyRate(CurrencyUnit.CNY, 7.20m);
```

### GetCurrencySymbol

获取货币符号。

```csharp
public static string GetCurrencySymbol(CurrencyUnit currency)
```

**参数：**
- `currency`: 货币单位

**返回值：**
- 货币符号

**示例：**
```csharp
var symbol = UnitHelper.GetCurrencySymbol(CurrencyUnit.CNY);
// symbol = "¥"

var symbol2 = UnitHelper.GetCurrencySymbol(CurrencyUnit.USD);
// symbol2 = "$"
```

### FormatCurrency

格式化货币金额。

```csharp
public static string FormatCurrency(decimal amount, CurrencyUnit currency, int decimalPlaces = 2)
```

**参数：**
- `amount`: 金额
- `currency`: 货币单位
- `decimalPlaces`: 小数位数，默认 2

**返回值：**
- 格式化后的货币字符串

**示例：**
```csharp
var formatted = UnitHelper.FormatCurrency(1234.56m, CurrencyUnit.USD);
// formatted = "$1,234.56"

var formatted2 = UnitHelper.FormatCurrency(1234.56m, CurrencyUnit.CNY);
// formatted2 = "¥1,234.56"
```

## 质量单位转换

### MassUnit 枚举

**值：**
- `Milligram`: 毫克
- `Gram`: 克
- `Kilogram`: 千克
- `Ton`: 吨
- `Ounce`: 盎司
- `Pound`: 磅
- `Stone`: 英石

### ConvertMass

质量单位转换。

```csharp
public static double ConvertMass(double value, MassUnit from, MassUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var kg = UnitHelper.ConvertMass(1000, MassUnit.Gram, MassUnit.Kilogram);
// kg = 1

var pounds = UnitHelper.ConvertMass(1, MassUnit.Kilogram, MassUnit.Pound);
// pounds ≈ 2.205
```

## 角度单位转换

### AngleUnit 枚举

**值：**
- `Degree`: 度
- `Radian`: 弧度
- `Grad`: 百分度
- `Minute`: 角分
- `Second`: 角秒

### ConvertAngle

角度单位转换。

```csharp
public static double ConvertAngle(double value, AngleUnit from, AngleUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var radians = UnitHelper.ConvertAngle(180, AngleUnit.Degree, AngleUnit.Radian);
// radians ≈ 3.14159 (π)

var degrees = UnitHelper.ConvertAngle(Math.PI, AngleUnit.Radian, AngleUnit.Degree);
// degrees ≈ 180
```

### FormatAngleToDMS

角度格式化为度分秒格式。

```csharp
public static string FormatAngleToDMS(double degrees)
```

**参数：**
- `degrees`: 角度值（度）

**返回值：**
- 度分秒格式字符串

**示例：**
```csharp
var dms = UnitHelper.FormatAngleToDMS(45.5);
// dms = "45°30'0""

var dms2 = UnitHelper.FormatAngleToDMS(90.25);
// dms2 = "90°15'0""
```

### ParseDMSToDegrees

度分秒格式转换为角度值。

```csharp
public static double ParseDMSToDegrees(int degrees, int minutes, double seconds)
```

**参数：**
- `degrees`: 度
- `minutes`: 分
- `seconds`: 秒

**返回值：**
- 角度值（度）

**示例：**
```csharp
var degrees = UnitHelper.ParseDMSToDegrees(45, 30, 0);
// degrees = 45.5
```

## 进制转换

### NumberBase 枚举

**值：**
- `Binary = 2`: 二进制
- `Octal = 8`: 八进制
- `Decimal = 10`: 十进制
- `Hexadecimal = 16`: 十六进制

### ConvertBase

进制转换。

```csharp
public static string ConvertBase(string value, NumberBase fromBase, NumberBase toBase)
```

**参数：**
- `value`: 数值字符串
- `fromBase`: 源进制
- `toBase`: 目标进制

**返回值：**
- 转换后的数值字符串

**异常：**
- `ArgumentException`: value 为空或无效时抛出

**示例：**
```csharp
var hex = UnitHelper.ConvertBase("255", NumberBase.Decimal, NumberBase.Hexadecimal);
// hex = "FF"

var binary = UnitHelper.ConvertBase("FF", NumberBase.Hexadecimal, NumberBase.Binary);
// binary = "11111111"

var octal = UnitHelper.ConvertBase("255", NumberBase.Decimal, NumberBase.Octal);
// octal = "377"
```

### ConvertFromDecimal

十进制转换为其他进制。

```csharp
public static string ConvertFromDecimal(long value, NumberBase toBase)
```

**参数：**
- `value`: 十进制数值
- `toBase`: 目标进制

**返回值：**
- 转换后的数值字符串

**示例：**
```csharp
var hex = UnitHelper.ConvertFromDecimal(255, NumberBase.Hexadecimal);
// hex = "FF"

var binary = UnitHelper.ConvertFromDecimal(255, NumberBase.Binary);
// binary = "11111111"
```

### ConvertToDecimal

其他进制转换为十进制。

```csharp
public static long ConvertToDecimal(string value, NumberBase fromBase)
```

**参数：**
- `value`: 数值字符串
- `fromBase`: 源进制

**返回值：**
- 十进制数值

**示例：**
```csharp
var decimalValue = UnitHelper.ConvertToDecimal("FF", NumberBase.Hexadecimal);
// decimalValue = 255

var decimalValue2 = UnitHelper.ConvertToDecimal("11111111", NumberBase.Binary);
// decimalValue2 = 255
```

## 面积单位转换

### AreaUnit 枚举

**值：**
- `SquareMillimeter`: 平方毫米
- `SquareCentimeter`: 平方厘米
- `SquareMeter`: 平方米
- `Hectare`: 公顷
- `SquareKilometer`: 平方公里
- `SquareInch`: 平方英寸
- `SquareFoot`: 平方英尺
- `SquareYard`: 平方码
- `Acre`: 英亩
- `SquareMile`: 平方英里
- `Mu`: 亩（中国市亩）

### ConvertArea

面积单位转换。

```csharp
public static double ConvertArea(double value, AreaUnit from, AreaUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var sqMeters = UnitHelper.ConvertArea(1, AreaUnit.Hectare, AreaUnit.SquareMeter);
// sqMeters = 10000

var acres = UnitHelper.ConvertArea(1, AreaUnit.SquareKilometer, AreaUnit.Acre);
// acres ≈ 247.105

var mu = UnitHelper.ConvertArea(1000, AreaUnit.SquareMeter, AreaUnit.Mu);
// mu = 1.5
```

## 体积单位转换

### VolumeUnit 枚举

**值：**
- `CubicMillimeter`: 立方毫米
- `CubicCentimeter`: 立方厘米
- `CubicMeter`: 立方米
- `Liter`: 升
- `Milliliter`: 毫升
- `CubicInch`: 立方英寸
- `CubicFoot`: 立方英尺
- `CubicYard`: 立方码
- `GallonUS`: 美制加仑
- `GallonUK`: 英制加仑
- `QuartUS`: 美制夸脱
- `PintUS`: 美制品脱

### ConvertVolume

体积单位转换。

```csharp
public static double ConvertVolume(double value, VolumeUnit from, VolumeUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var liters = UnitHelper.ConvertVolume(1, VolumeUnit.GallonUS, VolumeUnit.Liter);
// liters ≈ 3.785

var gallons = UnitHelper.ConvertVolume(10, VolumeUnit.Liter, VolumeUnit.GallonUS);
// gallons ≈ 2.642
```

## 温度单位转换

### TemperatureUnit 枚举

**值：**
- `Celsius`: 摄氏度
- `Fahrenheit`: 华氏度
- `Kelvin`: 开尔文

### ConvertTemperature

温度单位转换。

```csharp
public static double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var fahrenheit = UnitHelper.ConvertTemperature(100, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);
// fahrenheit = 212

var kelvin = UnitHelper.ConvertTemperature(0, TemperatureUnit.Celsius, TemperatureUnit.Kelvin);
// kelvin = 273.15

var celsius = UnitHelper.ConvertTemperature(32, TemperatureUnit.Fahrenheit, TemperatureUnit.Celsius);
// celsius = 0
```

### FormatTemperature

格式化温度显示。

```csharp
public static string FormatTemperature(double value, TemperatureUnit unit, int decimalPlaces = 1)
```

**参数：**
- `value`: 温度值
- `unit`: 温度单位
- `decimalPlaces`: 小数位数，默认 1

**返回值：**
- 格式化后的温度字符串

**示例：**
```csharp
var formatted = UnitHelper.FormatTemperature(25.5, TemperatureUnit.Celsius);
// formatted = "25.5°C"

var formatted2 = UnitHelper.FormatTemperature(98.6, TemperatureUnit.Fahrenheit);
// formatted2 = "98.6°F"
```

## 速度单位转换

### SpeedUnit 枚举

**值：**
- `MetersPerSecond`: 米/秒
- `KilometersPerHour`: 公里/小时
- `MilesPerHour`: 英里/小时
- `FeetPerSecond`: 英尺/秒
- `Knots`: 节（海里/小时）

### ConvertSpeed

速度单位转换。

```csharp
public static double ConvertSpeed(double value, SpeedUnit from, SpeedUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var kmh = UnitHelper.ConvertSpeed(10, SpeedUnit.MetersPerSecond, SpeedUnit.KilometersPerHour);
// kmh = 36

var mph = UnitHelper.ConvertSpeed(100, SpeedUnit.KilometersPerHour, SpeedUnit.MilesPerHour);
// mph ≈ 62.137

var knots = UnitHelper.ConvertSpeed(50, SpeedUnit.KilometersPerHour, SpeedUnit.Knots);
// knots ≈ 26.999
```

## 热能单位转换

### EnergyUnit 枚举

**值：**
- `Joule`: 焦耳
- `Kilojoule`: 千焦
- `Calorie`: 卡路里
- `Kilocalorie`: 千卡
- `WattHour`: 瓦时
- `KilowattHour`: 千瓦时
- `BTU`: 英热单位
- `ElectronVolt`: 电子伏特

### ConvertEnergy

热能单位转换。

```csharp
public static double ConvertEnergy(double value, EnergyUnit from, EnergyUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var kcal = UnitHelper.ConvertEnergy(1000, EnergyUnit.Calorie, EnergyUnit.Kilocalorie);
// kcal = 1

var kwh = UnitHelper.ConvertEnergy(3600000, EnergyUnit.Joule, EnergyUnit.KilowattHour);
// kwh = 1
```

## 功率单位转换

### PowerUnit 枚举

**值：**
- `Watt`: 瓦特
- `Kilowatt`: 千瓦
- `Horsepower`: 公制马力
- `HorsepowerUK`: 英制马力

### ConvertPower

功率单位转换。

```csharp
public static double ConvertPower(double value, PowerUnit from, PowerUnit to)
```

**示例：**
```csharp
var kw = UnitHelper.ConvertPower(1000, PowerUnit.Watt, PowerUnit.Kilowatt);
// kw = 1

var hp = UnitHelper.ConvertPower(100, PowerUnit.Kilowatt, PowerUnit.Horsepower);
// hp ≈ 135.962
```

## 压强单位转换

### PressureUnit 枚举

**值：**
- `Pascal`: 帕斯卡
- `Kilopascal`: 千帕
- `Bar`: 巴
- `Atmosphere`: 标准大气压
- `MmHg`: 毫米汞柱
- `Psi`: 磅力/平方英寸

### ConvertPressure

压强单位转换。

```csharp
public static double ConvertPressure(double value, PressureUnit from, PressureUnit to)
```

**示例：**
```csharp
var atm = UnitHelper.ConvertPressure(101325, PressureUnit.Pascal, PressureUnit.Atmosphere);
// atm = 1

var psi = UnitHelper.ConvertPressure(1, PressureUnit.Atmosphere, PressureUnit.Psi);
// psi ≈ 14.696
```

## 力单位转换

### ForceUnit 枚举

**值：**
- `Newton`: 牛顿
- `Kilonewton`: 千牛顿
- `Dyne`: 达因
- `PoundForce`: 磅力
- `KilogramForce`: 千克力

### ConvertForce

力单位转换。

```csharp
public static double ConvertForce(double value, ForceUnit from, ForceUnit to)
```

**示例：**
```csharp
var kgf = UnitHelper.ConvertForce(9.80665, ForceUnit.Newton, ForceUnit.KilogramForce);
// kgf = 1

var lbf = UnitHelper.ConvertForce(1, ForceUnit.KilogramForce, ForceUnit.PoundForce);
// lbf ≈ 2.205
```

## 数据存储单位转换

### DataStorageUnit 枚举

**值：**
- `Byte`: 字节
- `Kilobyte`: KB
- `Megabyte`: MB
- `Gigabyte`: GB
- `Terabyte`: TB
- `Petabyte`: PB
- `Exabyte`: EB

### ConvertDataStorage

数据存储单位转换。

```csharp
public static double ConvertDataStorage(double value, DataStorageUnit from, DataStorageUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var mb = UnitHelper.ConvertDataStorage(1024, DataStorageUnit.Kilobyte, DataStorageUnit.Megabyte);
// mb = 1

var gb = UnitHelper.ConvertDataStorage(1024, DataStorageUnit.Megabyte, DataStorageUnit.Gigabyte);
// gb = 1

var tb = UnitHelper.ConvertDataStorage(1024, DataStorageUnit.Gigabyte, DataStorageUnit.Terabyte);
// tb = 1
```

### FormatDataSize

格式化数据大小（自动选择合适的单位）。

```csharp
public static string FormatDataSize(double bytes, int decimalPlaces = 2)
```

**参数：**
- `bytes`: 字节数
- `decimalPlaces`: 小数位数，默认 2

**返回值：**
- 格式化后的数据大小字符串

**示例：**
```csharp
var size1 = UnitHelper.FormatDataSize(1024);
// size1 = "1.00 KB"

var size2 = UnitHelper.FormatDataSize(1536);
// size2 = "1.50 KB"

var size3 = UnitHelper.FormatDataSize(1048576);
// size3 = "1.00 MB"
```

## 时间单位转换

### TimeUnit 枚举

**值：**
- `Millisecond`: 毫秒
- `Second`: 秒
- `Minute`: 分钟
- `Hour`: 小时
- `Day`: 天
- `Week`: 周
- `Month`: 月（按 30 天计算）
- `Year`: 年（按 365 天计算）

### ConvertTime

时间单位转换。

```csharp
public static double ConvertTime(double value, TimeUnit from, TimeUnit to)
```

**参数：**
- `value`: 要转换的数值
- `from`: 源单位
- `to`: 目标单位

**返回值：**
- 转换后的数值

**示例：**
```csharp
var minutes = UnitHelper.ConvertTime(1, TimeUnit.Hour, TimeUnit.Minute);
// minutes = 60

var hours = UnitHelper.ConvertTime(3600, TimeUnit.Second, TimeUnit.Hour);
// hours = 1

var days = UnitHelper.ConvertTime(1, TimeUnit.Week, TimeUnit.Day);
// days = 7
```

### FormatDuration

格式化时长（自动选择合适的单位）。

```csharp
public static string FormatDuration(double seconds, int decimalPlaces = 2)
```

**参数：**
- `seconds`: 秒数
- `decimalPlaces`: 小数位数，默认 2

**返回值：**
- 格式化后的时长字符串

**示例：**
```csharp
var duration1 = UnitHelper.FormatDuration(90);
// duration1 = "1.50 分钟"

var duration2 = UnitHelper.FormatDuration(3661);
// duration2 = "1.02 小时"

var duration3 = UnitHelper.FormatDuration(0.5);
// duration3 = "500.00 毫秒"
```

## 使用建议

1. **单位转换**：使用对应的 `Convert*` 方法进行单位转换
2. **格式化输出**：使用 `Format*` 方法获得友好的输出格式
3. **货币转换**：注意汇率数据需要定期更新
4. **精度问题**：浮点数计算可能存在精度误差，建议使用 `decimal` 类型进行货币计算
5. **温度转换**：温度转换有特殊的计算公式，不能简单地使用比例转换

## 注意事项

1. 货币汇率为示例数据，实际应用中应从 API 获取实时汇率
2. 时间单位转换中，月和年为近似值（30 天/月，365 天/年）
3. 数据存储单位使用二进制前缀（1024 进位）
4. 角度转换中，弧度与度的转换使用 π 值
