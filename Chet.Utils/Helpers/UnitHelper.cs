namespace Chet.Utils.Helpers
{
    /// <summary>
    /// 计量单位转换帮助类
    /// 支持长度、货币、质量、角度、进制、面积、体积、温度、速度、热能、功率、压强、力单位转换
    /// </summary>
    public static class UnitHelper
    {
        #region 长度转换

        /// <summary>
        /// 长度单位枚举
        /// </summary>
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

        /// <summary>
        /// 长度转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertLength(double value, LengthUnit from, LengthUnit to)
        {
            // 先转换为米
            double meterValue = from switch
            {
                LengthUnit.Millimeter => value / 1_000,
                LengthUnit.Centimeter => value / 100,
                LengthUnit.Meter => value,
                LengthUnit.Kilometer => value * 1_000,
                LengthUnit.Inch => value * 0.0254,
                LengthUnit.Foot => value * 0.3048,
                LengthUnit.Yard => value * 0.9144,
                LengthUnit.Mile => value * 1_609.34,
                LengthUnit.NauticalMile => value * 1_852,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                LengthUnit.Millimeter => meterValue * 1_000,
                LengthUnit.Centimeter => meterValue * 100,
                LengthUnit.Meter => meterValue,
                LengthUnit.Kilometer => meterValue / 1_000,
                LengthUnit.Inch => meterValue / 0.0254,
                LengthUnit.Foot => meterValue / 0.3048,
                LengthUnit.Yard => meterValue / 0.9144,
                LengthUnit.Mile => meterValue / 1_609.34,
                LengthUnit.NauticalMile => meterValue / 1_852,
                _ => meterValue
            };
        }

        #endregion

        #region 货币转换

        /// <summary>
        /// 货币单位枚举
        /// </summary>
        public enum CurrencyUnit
        {
            CNY, // 人民币
            USD, // 美元
            EUR, // 欧元
            GBP, // 英镑
            JPY, // 日元
            KRW, // 韩元
            AUD, // 澳元
            CAD, // 加元
            CHF, // 瑞士法郎
            HKD  // 港币
        }

        // 汇率字典（以美元为基准，实际应用中应从API获取实时汇率）
        private static readonly Dictionary<CurrencyUnit, decimal> CurrencyRates = new Dictionary<CurrencyUnit, decimal>
        {
            { CurrencyUnit.CNY, 7.2m },
            { CurrencyUnit.USD, 1m },
            { CurrencyUnit.EUR, 0.93m },
            { CurrencyUnit.GBP, 0.79m },
            { CurrencyUnit.JPY, 153.6m },
            { CurrencyUnit.KRW, 1345.2m },
            { CurrencyUnit.AUD, 1.52m },
            { CurrencyUnit.CAD, 1.36m },
            { CurrencyUnit.CHF, 0.91m },
            { CurrencyUnit.HKD, 7.82m }
        };

        /// <summary>
        /// 货币转换
        /// </summary>
        /// <param name="amount">金额</param>
        /// <param name="from">源货币</param>
        /// <param name="to">目标货币</param>
        /// <returns>转换后的金额</returns>
        public static decimal ConvertCurrency(decimal amount, CurrencyUnit from, CurrencyUnit to)
        {
            // 先转换为美元基准
            var usdAmount = amount / CurrencyRates[from];
            // 再转换为目标货币
            return usdAmount * CurrencyRates[to];
        }

        /// <summary>
        /// 获取货币符号
        /// </summary>
        /// <param name="currency">货币单位</param>
        /// <returns>货币符号</returns>
        public static string GetCurrencySymbol(CurrencyUnit currency)
        {
            switch (currency)
            {
                case CurrencyUnit.CNY: return "¥";
                case CurrencyUnit.USD: return "$";
                case CurrencyUnit.EUR: return "€";
                case CurrencyUnit.GBP: return "£";
                case CurrencyUnit.JPY: return "¥";
                case CurrencyUnit.KRW: return "₩";
                case CurrencyUnit.AUD: return "A$";
                case CurrencyUnit.CAD: return "C$";
                case CurrencyUnit.CHF: return "CHF";
                case CurrencyUnit.HKD: return "HK$";
                default: return "";
            }
        }

        #endregion

        #region 质量转换

        /// <summary>
        /// 质量单位枚举
        /// </summary>
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

        /// <summary>
        /// 质量转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertMass(double value, MassUnit from, MassUnit to)
        {
            // 先转换为千克
            double kgValue = from switch
            {
                MassUnit.Milligram => value / 1_000_000,
                MassUnit.Gram => value / 1_000,
                MassUnit.Kilogram => value,
                MassUnit.Ton => value * 1_000,
                MassUnit.Ounce => value * 0.0283495,
                MassUnit.Pound => value * 0.453592,
                MassUnit.Stone => value * 6.35029,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                MassUnit.Milligram => kgValue * 1_000_000,
                MassUnit.Gram => kgValue * 1_000,
                MassUnit.Kilogram => kgValue,
                MassUnit.Ton => kgValue / 1_000,
                MassUnit.Ounce => kgValue / 0.0283495,
                MassUnit.Pound => kgValue / 0.453592,
                MassUnit.Stone => kgValue / 6.35029,
                _ => kgValue
            };
        }

        #endregion

        #region 角度转换

        /// <summary>
        /// 角度单位枚举
        /// </summary>
        public enum AngleUnit
        {
            Degree,      // 度
            Radian,      // 弧度
            Grad,        // 百分度
            Minute,      // 角分
            Second       // 角秒
        }

        /// <summary>
        /// 角度转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertAngle(double value, AngleUnit from, AngleUnit to)
        {
            // 先转换为度
            double degreeValue = from switch
            {
                AngleUnit.Degree => value,
                AngleUnit.Radian => value * 180 / Math.PI,
                AngleUnit.Grad => value * 0.9,
                AngleUnit.Minute => value / 60,
                AngleUnit.Second => value / 3600,
                _ => value
            };

            // 再转换为目标单位
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

        #endregion

        #region 进制转换

        /// <summary>
        /// 数字进制枚举
        /// </summary>
        public enum NumberBase
        {
            Binary = 2,      // 二进制
            Octal = 8,       // 八进制
            Decimal = 10,    // 十进制
            Hexadecimal = 16 // 十六进制
        }

        /// <summary>
        /// 进制转换
        /// </summary>
        /// <param name="value">数值字符串</param>
        /// <param name="fromBase">源进制</param>
        /// <param name="toBase">目标进制</param>
        /// <returns>转换后的数值字符串</returns>
        public static string ConvertBase(string value, NumberBase fromBase, NumberBase toBase)
        {
            // 先转换为十进制
            var decimalValue = Convert.ToInt64(value, (int)fromBase);
            // 再转换为目标进制
            return Convert.ToString(decimalValue, (int)toBase).ToUpper();
        }

        /// <summary>
        /// 十进制转换为其他进制
        /// </summary>
        /// <param name="value">十进制数值</param>
        /// <param name="toBase">目标进制</param>
        /// <returns>转换后的数值字符串</returns>
        public static string ConvertFromDecimal(long value, NumberBase toBase)
        {
            return Convert.ToString(value, (int)toBase).ToUpper();
        }

        /// <summary>
        /// 其他进制转换为十进制
        /// </summary>
        /// <param name="value">数值字符串</param>
        /// <param name="fromBase">源进制</param>
        /// <returns>十进制数值</returns>
        public static long ConvertToDecimal(string value, NumberBase fromBase)
        {
            return Convert.ToInt64(value, (int)fromBase);
        }

        #endregion

        #region 面积转换

        /// <summary>
        /// 面积单位枚举
        /// </summary>
        public enum AreaUnit
        {
            SquareMillimeter,  // 平方毫米
            SquareCentimeter,  // 平方厘米
            SquareMeter,       // 平方米
            Hectare,           // 公顷
            SquareKilometer,   // 平方公里
            SquareInch,        // 平方英寸
            SquareFoot,        // 平方英尺
            SquareYard,        // 平方码
            Acre,              // 英亩
            SquareMile         // 平方英里
        }

        /// <summary>
        /// 面积转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertArea(double value, AreaUnit from, AreaUnit to)
        {
            // 先转换为平方米
            double sqMeterValue = from switch
            {
                AreaUnit.SquareMillimeter => value / 1_000_000,
                AreaUnit.SquareCentimeter => value / 10_000,
                AreaUnit.SquareMeter => value,
                AreaUnit.Hectare => value * 10_000,
                AreaUnit.SquareKilometer => value * 1_000_000,
                AreaUnit.SquareInch => value * 0.00064516,
                AreaUnit.SquareFoot => value * 0.092903,
                AreaUnit.SquareYard => value * 0.836127,
                AreaUnit.Acre => value * 4_046.86,
                AreaUnit.SquareMile => value * 2_589_988.11,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                AreaUnit.SquareMillimeter => sqMeterValue * 1_000_000,
                AreaUnit.SquareCentimeter => sqMeterValue * 10_000,
                AreaUnit.SquareMeter => sqMeterValue,
                AreaUnit.Hectare => sqMeterValue / 10_000,
                AreaUnit.SquareKilometer => sqMeterValue / 1_000_000,
                AreaUnit.SquareInch => sqMeterValue / 0.00064516,
                AreaUnit.SquareFoot => sqMeterValue / 0.092903,
                AreaUnit.SquareYard => sqMeterValue / 0.836127,
                AreaUnit.Acre => sqMeterValue / 4_046.86,
                AreaUnit.SquareMile => sqMeterValue / 2_589_988.11,
                _ => sqMeterValue
            };
        }

        #endregion

        #region 体积转换

        /// <summary>
        /// 体积单位枚举
        /// </summary>
        public enum VolumeUnit
        {
            CubicMillimeter,  // 立方毫米
            CubicCentimeter,  // 立方厘米
            CubicMeter,       // 立方米
            Liter,            // 升
            Milliliter,       // 毫升
            CubicInch,        // 立方英寸
            CubicFoot,        // 立方英尺
            CubicYard,        // 立方码
            GallonUS,         // 美制加仑
            GallonUK,         // 英制加仑
            QuartUS,          // 美制夸脱
            PintUS            // 美制品脱
        }

        /// <summary>
        /// 体积转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertVolume(double value, VolumeUnit from, VolumeUnit to)
        {
            // 先转换为立方米
            double cbMeterValue = from switch
            {
                VolumeUnit.CubicMillimeter => value / 1_000_000_000,
                VolumeUnit.CubicCentimeter => value / 1_000_000,
                VolumeUnit.CubicMeter => value,
                VolumeUnit.Liter => value / 1_000,
                VolumeUnit.Milliliter => value / 1_000_000,
                VolumeUnit.CubicInch => value * 0.0000163871,
                VolumeUnit.CubicFoot => value * 0.0283168,
                VolumeUnit.CubicYard => value * 0.764555,
                VolumeUnit.GallonUS => value * 0.00378541,
                VolumeUnit.GallonUK => value * 0.00454609,
                VolumeUnit.QuartUS => value * 0.000946353,
                VolumeUnit.PintUS => value * 0.000473176,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                VolumeUnit.CubicMillimeter => cbMeterValue * 1_000_000_000,
                VolumeUnit.CubicCentimeter => cbMeterValue * 1_000_000,
                VolumeUnit.CubicMeter => cbMeterValue,
                VolumeUnit.Liter => cbMeterValue * 1_000,
                VolumeUnit.Milliliter => cbMeterValue * 1_000_000,
                VolumeUnit.CubicInch => cbMeterValue / 0.0000163871,
                VolumeUnit.CubicFoot => cbMeterValue / 0.0283168,
                VolumeUnit.CubicYard => cbMeterValue / 0.764555,
                VolumeUnit.GallonUS => cbMeterValue / 0.00378541,
                VolumeUnit.GallonUK => cbMeterValue / 0.00454609,
                VolumeUnit.QuartUS => cbMeterValue / 0.000946353,
                VolumeUnit.PintUS => cbMeterValue / 0.000473176,
                _ => cbMeterValue
            };
        }

        #endregion

        #region 温度转换

        /// <summary>
        /// 温度单位枚举
        /// </summary>
        public enum TemperatureUnit
        {
            Celsius,     // 摄氏度
            Fahrenheit,  // 华氏度
            Kelvin       // 开尔文
        }

        /// <summary>
        /// 温度转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertTemperature(double value, TemperatureUnit from, TemperatureUnit to)
        {
            // 先转换为摄氏度
            double celsiusValue = from switch
            {
                TemperatureUnit.Celsius => value,
                TemperatureUnit.Fahrenheit => (value - 32) * 5 / 9,
                TemperatureUnit.Kelvin => value - 273.15,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                TemperatureUnit.Celsius => celsiusValue,
                TemperatureUnit.Fahrenheit => celsiusValue * 9 / 5 + 32,
                TemperatureUnit.Kelvin => celsiusValue + 273.15,
                _ => celsiusValue
            };
        }

        #endregion

        #region 速度转换

        /// <summary>
        /// 速度单位枚举
        /// </summary>
        public enum SpeedUnit
        {
            MetersPerSecond,      // 米/秒
            KilometersPerHour,    // 公里/小时
            MilesPerHour,         // 英里/小时
            FeetPerSecond,        // 英尺/秒
            Knots                 // 节
        }

        /// <summary>
        /// 速度转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertSpeed(double value, SpeedUnit from, SpeedUnit to)
        {
            // 先转换为米/秒
            double mpsValue = from switch
            {
                SpeedUnit.MetersPerSecond => value,
                SpeedUnit.KilometersPerHour => value / 3.6,
                SpeedUnit.MilesPerHour => value * 0.44704,
                SpeedUnit.FeetPerSecond => value * 0.3048,
                SpeedUnit.Knots => value * 0.514444,
                _ => value
            };

            // 再转换为目标单位
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
        /// 热能单位枚举
        /// </summary>
        public enum EnergyUnit
        {
            Joule,           // 焦耳
            Kilojoule,       // 千焦
            Calorie,         // 卡路里
            Kilocalorie,     // 千卡
            WattHour,        // 瓦时
            KilowattHour,    // 千瓦时
            BTU,             // 英国热量单位
            ElectronVolt     // 电子伏特
        }

        /// <summary>
        /// 热能转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertEnergy(double value, EnergyUnit from, EnergyUnit to)
        {
            // 先转换为焦耳
            double jouleValue = from switch
            {
                EnergyUnit.Joule => value,
                EnergyUnit.Kilojoule => value * 1_000,
                EnergyUnit.Calorie => value * 4.184,
                EnergyUnit.Kilocalorie => value * 4_184,
                EnergyUnit.WattHour => value * 3_600,
                EnergyUnit.KilowattHour => value * 3_600_000,
                EnergyUnit.BTU => value * 1_055.06,
                EnergyUnit.ElectronVolt => value * 1.60218e-19,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                EnergyUnit.Joule => jouleValue,
                EnergyUnit.Kilojoule => jouleValue / 1_000,
                EnergyUnit.Calorie => jouleValue / 4.184,
                EnergyUnit.Kilocalorie => jouleValue / 4_184,
                EnergyUnit.WattHour => jouleValue / 3_600,
                EnergyUnit.KilowattHour => jouleValue / 3_600_000,
                EnergyUnit.BTU => jouleValue / 1_055.06,
                EnergyUnit.ElectronVolt => jouleValue / 1.60218e-19,
                _ => jouleValue
            };
        }

        #endregion

        #region 功率转换

        /// <summary>
        /// 功率单位枚举
        /// </summary>
        public enum PowerUnit
        {
            Watt,         // 瓦特
            Kilowatt,     // 千瓦
            Megawatt,     // 兆瓦
            Horsepower,   // 马力
            BTUPerHour    // 英热单位/小时
        }

        /// <summary>
        /// 功率转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertPower(double value, PowerUnit from, PowerUnit to)
        {
            // 先转换为瓦特
            double wattValue = from switch
            {
                PowerUnit.Watt => value,
                PowerUnit.Kilowatt => value * 1_000,
                PowerUnit.Megawatt => value * 1_000_000,
                PowerUnit.Horsepower => value * 745.7,
                PowerUnit.BTUPerHour => value * 0.293071,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                PowerUnit.Watt => wattValue,
                PowerUnit.Kilowatt => wattValue / 1_000,
                PowerUnit.Megawatt => wattValue / 1_000_000,
                PowerUnit.Horsepower => wattValue / 745.7,
                PowerUnit.BTUPerHour => wattValue / 0.293071,
                _ => wattValue
            };
        }

        #endregion

        #region 压强转换

        /// <summary>
        /// 压强单位枚举
        /// </summary>
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

        /// <summary>
        /// 压强转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertPressure(double value, PressureUnit from, PressureUnit to)
        {
            // 先转换为帕斯卡
            double pascalValue = from switch
            {
                PressureUnit.Pascal => value,
                PressureUnit.Kilopascal => value * 1_000,
                PressureUnit.Megapascal => value * 1_000_000,
                PressureUnit.Bar => value * 100_000,
                PressureUnit.Atmosphere => value * 101_325,
                PressureUnit.PSI => value * 6_894.76,
                PressureUnit.Torr => value * 133.322,
                PressureUnit.mmHg => value * 133.322,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                PressureUnit.Pascal => pascalValue,
                PressureUnit.Kilopascal => pascalValue / 1_000,
                PressureUnit.Megapascal => pascalValue / 1_000_000,
                PressureUnit.Bar => pascalValue / 100_000,
                PressureUnit.Atmosphere => pascalValue / 101_325,
                PressureUnit.PSI => pascalValue / 6_894.76,
                PressureUnit.Torr => pascalValue / 133.322,
                PressureUnit.mmHg => pascalValue / 133.322,
                _ => pascalValue
            };
        }

        #endregion

        #region 力转换

        /// <summary>
        /// 力单位枚举
        /// </summary>
        public enum ForceUnit
        {
            Newton,        // 牛顿
            Kilonewton,    // 千牛顿
            Dyne,          // 达因
            PoundForce,    // 磅力
            KilogramForce  // 千克力
        }

        /// <summary>
        /// 力转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="from">源单位</param>
        /// <param name="to">目标单位</param>
        /// <returns>转换后的值</returns>
        public static double ConvertForce(double value, ForceUnit from, ForceUnit to)
        {
            // 先转换为牛顿
            double newtonValue = from switch
            {
                ForceUnit.Newton => value,
                ForceUnit.Kilonewton => value * 1_000,
                ForceUnit.Dyne => value * 0.00001,
                ForceUnit.PoundForce => value * 4.44822,
                ForceUnit.KilogramForce => value * 9.80665,
                _ => value
            };

            // 再转换为目标单位
            return to switch
            {
                ForceUnit.Newton => newtonValue,
                ForceUnit.Kilonewton => newtonValue / 1_000,
                ForceUnit.Dyne => newtonValue / 0.00001,
                ForceUnit.PoundForce => newtonValue / 4.44822,
                ForceUnit.KilogramForce => newtonValue / 9.80665,
                _ => newtonValue
            };
        }

        #endregion

        #region 实用工具方法

        /// <summary>
        /// 获取单位符号
        /// </summary>
        /// <param name="unit">单位枚举</param>
        /// <returns>单位符号</returns>
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
                AreaUnit.SquareMeter => "m²",
                AreaUnit.Hectare => "ha",
                AreaUnit.SquareKilometer => "km²",
                VolumeUnit.Liter => "L",
                VolumeUnit.Milliliter => "mL",
                TemperatureUnit.Celsius => "°C",
                TemperatureUnit.Fahrenheit => "°F",
                TemperatureUnit.Kelvin => "K",
                SpeedUnit.MetersPerSecond => "m/s",
                SpeedUnit.KilometersPerHour => "km/h",
                SpeedUnit.MilesPerHour => "mph",
                EnergyUnit.Joule => "J",
                EnergyUnit.Kilocalorie => "kcal",
                EnergyUnit.KilowattHour => "kWh",
                PowerUnit.Watt => "W",
                PowerUnit.Kilowatt => "kW",
                PowerUnit.Horsepower => "hp",
                PressureUnit.Pascal => "Pa",
                PressureUnit.Bar => "bar",
                PressureUnit.Atmosphere => "atm",
                PressureUnit.PSI => "psi",
                ForceUnit.Newton => "N",
                ForceUnit.Kilonewton => "kN",
                ForceUnit.PoundForce => "lbf",
                _ => unit.ToString()
            };
        }

        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="unit">单位枚举</param>
        /// <returns>单位名称</returns>
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
                AreaUnit.SquareMeter => "平方米",
                AreaUnit.Hectare => "公顷",
                AreaUnit.SquareKilometer => "平方公里",
                VolumeUnit.Liter => "升",
                VolumeUnit.Milliliter => "毫升",
                TemperatureUnit.Celsius => "摄氏度",
                TemperatureUnit.Fahrenheit => "华氏度",
                TemperatureUnit.Kelvin => "开尔文",
                SpeedUnit.MetersPerSecond => "米/秒",
                SpeedUnit.KilometersPerHour => "公里/小时",
                SpeedUnit.MilesPerHour => "英里/小时",
                EnergyUnit.Joule => "焦耳",
                EnergyUnit.Kilocalorie => "千卡",
                EnergyUnit.KilowattHour => "千瓦时",
                PowerUnit.Watt => "瓦特",
                PowerUnit.Kilowatt => "千瓦",
                PowerUnit.Horsepower => "马力",
                PressureUnit.Pascal => "帕斯卡",
                PressureUnit.Bar => "巴",
                PressureUnit.Atmosphere => "标准大气压",
                PressureUnit.PSI => "磅力/平方英寸",
                ForceUnit.Newton => "牛顿",
                ForceUnit.Kilonewton => "千牛顿",
                ForceUnit.PoundForce => "磅力",
                _ => unit.ToString()
            };
        }

        #endregion
    }
}