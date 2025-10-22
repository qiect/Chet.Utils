# EnumExtensions 类功能文档

## 概述

[EnumExtensions](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L8-L207) 是一个静态扩展类，为枚举类型提供了丰富的扩展方法。该类包含枚举值判断、转换、描述获取、标志位操作等多种功能，旨在简化枚举类型的使用，提高代码的可读性和便利性，特别适用于需要处理枚举描述、标志位枚举等复杂场景。

## 主要功能模块

### 1. 枚举值判断方法

提供枚举值有效性检查功能。

**主要方法：**
- [IsDefined<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L13-L14) - 判断枚举值是否定义在枚举类型中

### 2. 枚举类型信息获取方法

提供枚举类型元数据获取功能。

**主要方法：**
- [GetValues<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L20-L21) - 获取枚举类型所有值列表
- [GetNames<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L27-L28) - 获取枚举类型所有名称列表
- [GetUnderlyingType<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L203-L204) - 获取枚举类型的基础类型（如 int、byte）

### 3. 枚举值转换方法

提供枚举值与各种数据类型之间的相互转换。

**主要方法：**
- [ToInt<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L34-L35) - 枚举值转为 int
- [ToLong<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L41-L42) - 枚举值转为 long
- [ToStringValue<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L48-L49) - 枚举值转为字符串
- [Parse<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L88-L96) - 字符串转为枚举值
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L104-L110) - int 转为枚举值
- [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L117-L132) - long 转为枚举值

### 4. 描述属性处理方法

提供基于 DescriptionAttribute 的枚举描述处理功能。

**主要方法：**
- [GetDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L55-L63) - 枚举值转为描述（DescriptionAttribute），无描述则返回名称
- [FromDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L70-L81) - 根据描述获取枚举值（DescriptionAttribute）
- [GetValueDescriptionDict<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L138-L146) - 获取枚举类型所有值及描述的字典
- [GetNameDescriptionDict<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L152-L160) - 获取枚举类型所有名称及描述的字典

### 5. 标志位枚举操作方法

提供针对 [Flags] 特性枚举的位操作功能。

**主要方法：**
- [HasFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L166-L167) - 判断枚举值是否包含指定标志
- [AddFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L173-L177) - 枚举值添加标志
- [RemoveFlag<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L183-L187) - 枚举值移除标志

## 使用场景

1. **UI 显示** - 将枚举值转换为用户友好的描述文本显示
2. **数据导入导出** - 处理枚举值与字符串、数字之间的相互转换
3. **配置管理** - 读取和保存枚举类型的配置项
4. **权限控制** - 使用标志位枚举进行权限位操作
5. **API 交互** - 枚举值与外部系统数据格式的转换
6. **日志记录** - 记录枚举值的描述信息而非数值
7. **数据绑定** - 为下拉列表等控件提供枚举值和描述的绑定数据
8. **业务逻辑** - 基于枚举描述进行业务规则判断

## 注意事项

1. 所有方法都是扩展方法，需要通过枚举实例或类型调用
2. [GetDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L55-L63) 方法依赖于 DescriptionAttribute 特性，未标记的枚举值返回其名称
3. [Parse<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L88-L96) 方法支持大小写不敏感的字符串解析
4. [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L104-L110) 和 [ToEnum<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L117-L132) 方法在转换失败时返回默认值，而不是抛出异常
5. 标志位操作方法适用于带有 [Flags] 特性的枚举类型
6. [FromDescription<TEnum>()](file://E:\Project\Chet\Chet.Utils\Chet.Utils\Extensions\EnumExtensions.cs#L70-L81) 方法在找不到匹配描述时返回默认枚举值
7. 字典获取方法可用于数据绑定和枚举值遍历场景
8. 基础类型获取方法有助于了解枚举的底层存储类型
9. 所有转换方法都对边界条件进行了安全处理，提高代码健壮性