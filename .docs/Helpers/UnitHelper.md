# UnitHelper 类功能文档

## 概述

[UnitHelper](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L7-L837) 是一个静态工具类，专门用于各种计量单位之间的转换。该类支持长度、货币、质量、角度、进制、面积、体积、温度、速度、热能、功率、压强、力等多种单位的转换，提供了全面的单位转换功能，旨在简化开发中常见的单位换算需求。

## 主要功能模块

### 1. 长度转换

支持国际单位制和英制长度单位之间的转换。

**枚举类型：**
- [LengthUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L17-L27) - 长度单位枚举（毫米、厘米、米、千米、英寸、英尺、码、英里、海里）

**主要方法：**
- [ConvertLength()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L36-L63) - 长度单位转换

### 2. 货币转换

支持多种国际主要货币之间的转换。

**枚举类型：**
- [CurrencyUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L74-L85) - 货币单位枚举（人民币、美元、欧元、英镑、日元、韩元、澳元、加元、瑞士法郎、港币）

**主要方法：**
- [ConvertCurrency()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L105-L111) - 货币单位转换
- [GetCurrencySymbol()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L118-L134) - 获取货币符号

### 3. 质量转换

支持国际单位制和英制质量单位之间的转换。

**枚举类型：**
- [MassUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L145-L153) - 质量单位枚举（毫克、克、千克、吨、盎司、磅、英石）

**主要方法：**
- [ConvertMass()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L162-L189) - 质量单位转换

### 4. 角度转换

支持多种角度单位之间的转换。

**枚举类型：**
- [AngleUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L200-L206) - 角度单位枚举（度、弧度、百分度、角分、角秒）

**主要方法：**
- [ConvertAngle()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L215-L242) - 角度单位转换

### 5. 进制转换

支持常见数字进制之间的转换。

**枚举类型：**
- [NumberBase](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L252-L257) - 数字进制枚举（二进制、八进制、十进制、十六进制）

**主要方法：**
- [ConvertBase()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L266-L272) - 进制转换
- [ConvertFromDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L280-L283) - 十进制转换为其他进制
- [ConvertToDecimal()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L291-L294) - 其他进制转换为十进制

### 6. 面积转换

支持国际单位制和英制面积单位之间的转换。

**枚举类型：**
- [AreaUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L305-L316) - 面积单位枚举（平方毫米、平方厘米、平方米、公顷、平方公里、平方英寸、平方英尺、平方码、英亩、平方英里）

**主要方法：**
- [ConvertArea()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L325-L352) - 面积单位转换

### 7. 体积转换

支持国际单位制和英制体积单位之间的转换。

**枚举类型：**
- [VolumeUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L363-L376) - 体积单位枚举（立方毫米、立方厘米、立方米、升、毫升、立方英寸、立方英尺、立方码、美制加仑、英制加仑、美制夸脱、美制品脱）

**主要方法：**
- [ConvertVolume()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L385-L412) - 体积单位转换

### 8. 温度转换

支持常见温度单位之间的转换。

**枚举类型：**
- [TemperatureUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L423-L427) - 温度单位枚举（摄氏度、华氏度、开尔文）

**主要方法：**
- [ConvertTemperature()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L436-L461) - 温度单位转换

### 9. 速度转换

支持常见速度单位之间的转换。

**枚举类型：**
- [SpeedUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L472-L478) - 速度单位枚举（米/秒、公里/小时、英里/小时、英尺/秒、节）

**主要方法：**
- [ConvertSpeed()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L487-L512) - 速度单位转换

### 10. 热能转换

支持常见热能单位之间的转换。

**枚举类型：**
- [EnergyUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L523-L532) - 热能单位枚举（焦耳、千焦、卡路里、千卡、瓦时、千瓦时、英国热量单位、电子伏特）

**主要方法：**
- [ConvertEnergy()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L541-L568) - 热能单位转换

### 11. 功率转换

支持常见功率单位之间的转换。

**枚举类型：**
- [PowerUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L579-L585) - 功率单位枚举（瓦特、千瓦、兆瓦、马力、英热单位/小时）

**主要方法：**
- [ConvertPower()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L594-L619) - 功率单位转换

### 12. 压强转换

支持常见压强单位之间的转换。

**枚举类型：**
- [PressureUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L630-L639) - 压强单位枚举（帕斯卡、千帕、兆帕、巴、标准大气压、磅力/平方英寸、托、毫米汞柱）

**主要方法：**
- [ConvertPressure()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L648-L673) - 压强单位转换

### 13. 力转换

支持常见力单位之间的转换。

**枚举类型：**
- [ForceUnit](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L684-L690) - 力单位枚举（牛顿、千牛顿、达因、磅力、千克力）

**主要方法：**
- [ConvertForce()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L699-L724) - 力单位转换

### 14. 实用工具方法

提供单位符号和名称获取功能。

**主要方法：**
- [GetUnitSymbol()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L734-L781) - 获取单位符号
- [GetUnitName()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Helpers\UnitHelper.cs#L790-L835) - 获取单位名称

## 使用场景

1. **科学计算** - 进行物理、化学等科学计算时的单位转换
2. **工程应用** - 工程设计和计算中的单位换算
3. **国际贸易** - 跨国贸易中的货币和度量衡转换
4. **教育工具** - 教学和学习中的单位换算演示
5. **数据处理** - 处理来自不同国家或系统的数据时的单位标准化
6. **移动应用** - 为用户提供便捷的单位转换功能
7. **桌面软件** - 集成单位转换功能的计算器或工具软件
8. **Web服务** - 提供单位转换API服务

## 注意事项

1. 货币汇率为静态值，实际应用中应从API获取实时汇率
2. 所有转换方法使用double类型进行计算，可能存在精度问题
3. 温度转换采用标准物理公式
4. 角度转换支持弧度和度之间的相互转换
5. 进制转换仅支持整数类型的转换
6. 压强转换中Torr和mmHg视为等效单位
7. 部分单位转换系数为近似值，高精度应用需使用更精确的系数
8. 方法参数需要指定正确的源单位和目标单位枚举值