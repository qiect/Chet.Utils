# UnitHelper 帮助类

## 概述

`UnitHelper` 是一个全面的单位转换工具类，提供了丰富的计量单位转换功能，支持长度、质量、面积、体积、温度、速度、能量、功率、压强、力等多种物理量的单位转换。该类使用静态方法设计，无需实例化即可直接使用，适用于各种需要单位转换的应用场景。

## 主要特性

- 支持 10 种不同物理量的单位转换
- 提供全面的单位枚举和转换方法
- 支持获取单位符号和名称
- 包含进制转换功能
- 支持货币转换（包含常用货币符号）
- 所有方法均为静态方法，使用便捷

## 类定义

```csharp
public static class UnitHelper
{
    // 转换方法和枚举定义
}
```

## 单位枚举

### 长度单位

```csharp
public enum LengthUnit
{
    Millimeter,    // 毫米
    Centimeter,    // 厘米
    Meter,         // 米
    Kilometer,     // 千米
    Inch,          // 英寸
    Foot,          // 英尺
    Yard,          // 码
    Mile,          // 英里
    NauticalMile   // 海里
}
```

### 质量单位

```csharp
public enum MassUnit
{
    Milligram,   // 毫克
    Gram,        // 克
    Kilogram,    // 千克
    Ton,         // 吨
    Ounce,       // 盎司
    Pound,       // 磅
    Stone        // 英石
}
```

### 角度单位

```csharp
public enum AngleUnit
{
    Degree,      // 度
    Radian,      // 弧度
    Gradian      // 梯度
}
```

### 进制单位

```csharp
public enum NumberBase
{
    Binary,      // 二进制
    Octal,       // 八进制
    Decimal,     // 十进制
    Hexadecimal  // 十六进制
}
```

### 面积单位

```csharp
public enum AreaUnit
{
    SquareMeter,      // 平方米
    SquareKilometer,  // 平方公里
    SquareCentimeter, // 平方厘米
    SquareMillimeter, // 平方毫米
    SquareInch,       // 平方英寸
    SquareFoot,       // 平方英尺
    SquareYard,       // 平方码
    SquareMile,       // 平方英里
    Acre,             // 英亩
    Hectare           // 公顷
}
```

### 体积单位

```csharp
public enum VolumeUnit
{
    Liter,            // 升
    Milliliter,       // 毫升
    CubicMeter,       // 立方米
    CubicCentimeter,  // 立方厘米
    CubicInch,        // 立方英寸
    CubicFoot,        // 立方英尺
    CubicYard,        // 立方码
    USGallon,         // 美制加仑
    UKGallon          // 英制加仑
}
```

### 温度单位

```csharp
public enum TemperatureUnit
{
    Celsius,    // 摄氏度
    Fahrenheit, // 华氏度
    Kelvin      // 开尔文
}
```

### 速度单位

```csharp
public enum SpeedUnit
{
    MetersPerSecond,   // 米/秒
    KilometersPerHour, // 公里/小时
    MilesPerHour       // 英里/小时
}
```

### 能量单位

```csharp
public enum EnergyUnit
{
    Joule,         // 焦耳
    Kilojoule,     // 千焦耳
    Kilocalorie,   // 千卡
    KilowattHour   // 千瓦时
}
```

### 功率单位

```csharp
public enum PowerUnit
{
    Watt,           // 瓦特
    Kilowatt,       // 千瓦
    Megawatt,       // 兆瓦
    Horsepower,     // 马力
    BTUPerHour      // 英热单位/小时
}
```

### 压强单位

```csharp
public enum PressureUnit
{
    Pascal,         // 帕斯卡
    Kilopascal,     // 千帕
    Megapascal,     // 兆帕
    Bar,            // 巴
    Atmosphere,     // 标准大气压
    PSI,            // 磅力/平方英寸
    Torr,           // 托
    mmHg            // 毫米汞柱
}
```

### 力单位

```csharp
public enum ForceUnit
{
    Newton,        // 牛顿
    Kilonewton,    // 千牛顿
    Dyne,          // 达因
    PoundForce,    // 磅力
    KilogramForce  // 千克力
}
```

## 转换方法

### 长度转换

```csharp
public static double ConvertLength(double value, LengthUnit from, LengthUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（LengthUnit枚举）
- `to`: 目标单位（LengthUnit枚举）

**返回值：**
- 转换后的数值

### 货币转换

```csharp
public static double ConvertCurrency(double value, string fromCurrency, string toCurrency)
```

**参数说明：**
- `value`: 要转换的数值
- `fromCurrency`: 源货币代码（如"CNY", "USD"等）
- `toCurrency`: 目标货币代码（如"CNY", "USD"等）

**返回值：**
- 转换后的数值

### 质量转换

```csharp
public static double ConvertMass(double value, MassUnit from, MassUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（MassUnit枚举）
- `to`: 目标单位（MassUnit枚举）

**返回值：**
- 转换后的数值

### 角度转换

```csharp
public static double ConvertAngle(double value, AngleUnit from, AngleUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（AngleUnit枚举）
- `to`: 目标单位（AngleUnit枚举）

**返回值：**
- 转换后的数值

### 进制转换

```csharp
public static string ConvertBase(string value, NumberBase fromBase, NumberBase toBase)
```

**参数说明：**
- `value`: 要转换的数值字符串
- `fromBase`: 源进制（NumberBase枚举）
- `toBase`: 目标进制（NumberBase枚举）

**返回值：**
- 转换后的数值字符串

### 面积转换

```csharp
public static double ConvertArea(double value, AreaUnit from, AreaUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（AreaUnit枚举）
- `to`: 目标单位（AreaUnit枚举）

**返回值：**
- 转换后的数值

### 体积转换

```csharp
public static double ConvertVolume(double value, VolumeUnit from, VolumeUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（VolumeUnit枚举）
- `to`: 目标单位（VolumeUnit枚举）

**返回值：**
- 转换后的数值

### 温度转换

```csharp
public static double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（TemperatureUnit枚举）
- `to`: 目标单位（TemperatureUnit枚举）

**返回值：**
- 转换后的数值

### 速度转换

```csharp
public static double ConvertSpeed(double value, SpeedUnit from, SpeedUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（SpeedUnit枚举）
- `to`: 目标单位（SpeedUnit枚举）

**返回值：**
- 转换后的数值

### 能量转换

```csharp
public static double ConvertEnergy(double value, EnergyUnit from, EnergyUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（EnergyUnit枚举）
- `to`: 目标单位（EnergyUnit枚举）

**返回值：**
- 转换后的数值

### 功率转换

```csharp
public static double ConvertPower(double value, PowerUnit from, PowerUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（PowerUnit枚举）
- `to`: 目标单位（PowerUnit枚举）

**返回值：**
- 转换后的数值

### 压强转换

```csharp
public static double ConvertPressure(double value, PressureUnit from, PressureUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（PressureUnit枚举）
- `to`: 目标单位（PressureUnit枚举）

**返回值：**
- 转换后的数值

### 力转换

```csharp
public static double ConvertForce(double value, ForceUnit from, ForceUnit to)
```

**参数说明：**
- `value`: 要转换的数值
- `from`: 源单位（ForceUnit枚举）
- `to`: 目标单位（ForceUnit枚举）

**返回值：**
- 转换后的数值

## 实用工具方法

### 获取货币符号

```csharp
public static string GetCurrencySymbol(string currencyCode)
```

**参数说明：**
- `currencyCode`: 货币代码（如"CNY", "USD"等）

**返回值：**
- 对应的货币符号

### 获取单位符号

```csharp
public static string GetUnitSymbol(Enum unit)
```

**参数说明：**
- `unit`: 单位枚举值

**返回值：**
- 对应的单位符号

### 获取单位名称

```csharp
public static string GetUnitName(Enum unit)
```

**参数说明：**
- `unit`: 单位枚举值

**返回值：**
- 对应的单位中文名称

## 使用示例

### 长度转换示例

```csharp
// 将10米转换为厘米
double result = UnitHelper.ConvertLength(10, LengthUnit.Meter, LengthUnit.Centimeter);
Console.WriteLine($"10米 = {result}厘米"); // 输出: 10米 = 1000厘米

// 获取单位符号
string symbol = UnitHelper.GetUnitSymbol(LengthUnit.Centimeter);
Console.WriteLine($"厘米的符号: {symbol}"); // 输出: 厘米的符号: cm
```

### 温度转换示例

```csharp
// 将0摄氏度转换为华氏度
double fahrenheit = UnitHelper.ConvertTemperature(0, TemperatureUnit.Celsius, TemperatureUnit.Fahrenheit);
Console.WriteLine($"0摄氏度 = {fahrenheit}华氏度"); // 输出: 0摄氏度 = 32华氏度
```

### 货币转换示例

```csharp
// 将100美元转换为人民币
double rmb = UnitHelper.ConvertCurrency(100, "USD", "CNY");
Console.WriteLine($"100美元 = {rmb}人民币"); 

// 获取美元符号
string dollarSymbol = UnitHelper.GetCurrencySymbol("USD");
Console.WriteLine($"美元符号: {dollarSymbol}"); // 输出: 美元符号: $
```

### 进制转换示例

```csharp
// 将十进制数字42转换为二进制
string binaryValue = UnitHelper.ConvertBase("42", NumberBase.Decimal, NumberBase.Binary);
Console.WriteLine($"十进制42的二进制表示: {binaryValue}"); // 输出: 十进制42的二进制表示: 101010
```

### 组合使用示例

```csharp
// 计算矩形面积并进行单位转换
// 已知长10米，宽5米，计算面积并转换为公顷
double length = 10; // 米
double width = 5;   // 米

// 计算平方米面积
double areaInSquareMeters = length * width; // 50平方米

// 转换为公顷
double areaInHectares = UnitHelper.ConvertArea(areaInSquareMeters, AreaUnit.SquareMeter, AreaUnit.Hectare);

Console.WriteLine($"面积: {areaInSquareMeters} {UnitHelper.GetUnitSymbol(AreaUnit.SquareMeter)}");
Console.WriteLine($"面积: {areaInHectares} {UnitHelper.GetUnitSymbol(AreaUnit.Hectare)}");
```

## 最佳实践

1. **精度处理**：转换过程中可能产生浮点数精度问题，建议根据需求使用 `Math.Round` 进行适当的精度控制。

2. **异常处理**：对于货币转换等可能失败的操作，建议添加异常处理。

3. **单位符号显示**：在显示结果时，使用 `GetUnitSymbol` 方法获取标准单位符号，提高数据展示的专业性。

4. **货币汇率更新**：注意 `ConvertCurrency` 方法中的汇率为静态值，实际应用中应根据需要更新汇率数据。

## 注意事项

1. 所有转换方法都接受 `double` 类型的输入值，但需要注意可能存在的精度问题。

2. 货币转换方法使用的是预定义的静态汇率，可能与实时汇率有所差异。在金融应用中，应使用实时汇率数据源。

3. 对于特殊单位或自定义单位，当前版本不支持直接扩展，可以通过自定义方法实现额外的转换需求。

4. 进制转换目前仅支持整数转换，小数转换需要额外处理。

## 版本兼容性

- .NET Framework 4.0 及以上版本
- .NET Core 2.0 及以上版本
- .NET 5.0 及以上版本

## 故障排除

### 常见问题

1. **转换结果不正确**
   - 检查源单位和目标单位是否正确指定
   - 确认输入值的单位与参数中指定的单位一致

2. **货币转换失败**
   - 检查货币代码格式是否正确（如 "CNY" 而非 "￥"）
   - 确认目标货币代码是否在支持列表中

3. **进制转换抛出异常**
   - 确保输入字符串与指定的源进制匹配
   - 例如，二进制只能包含 '0' 和 '1' 字符