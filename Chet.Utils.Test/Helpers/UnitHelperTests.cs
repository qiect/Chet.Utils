using Xunit;

namespace Chet.Utils.Helpers.Tests
{
    public class UnitHelperTests
    {
        #region 长度转换测试

        /// <summary>
        /// 测试长度转换基本功能
        /// </summary>
        [Fact]
        public void ConvertLength_BasicConversion_ReturnsCorrectValue()
        {
            // Arrange
            double value = 1000;

            // Act & Assert
            // 1000毫米 = 1米
            Assert.Equal(1.0, UnitHelper.ConvertLength(value, UnitHelper.LengthUnit.Millimeter, UnitHelper.LengthUnit.Meter), 3);
            // 100厘米 = 1米
            Assert.Equal(1.0, UnitHelper.ConvertLength(100, UnitHelper.LengthUnit.Centimeter, UnitHelper.LengthUnit.Meter), 3);
            // 1千米 = 1000米
            Assert.Equal(1000.0, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.Kilometer, UnitHelper.LengthUnit.Meter), 3);
        }

        /// <summary>
        /// 测试长度自转换
        /// </summary>
        [Fact]
        public void ConvertLength_SameUnit_ReturnsSameValue()
        {
            // Arrange
            double value = 5.5;

            // Act & Assert
            Assert.Equal(value, UnitHelper.ConvertLength(value, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Meter));
        }

        /// <summary>
        /// 测试边界值
        /// </summary>
        [Fact]
        public void ConvertLength_ZeroAndNegativeValues_ReturnsCorrectValue()
        {
            // 0值测试
            Assert.Equal(0, UnitHelper.ConvertLength(0, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Centimeter));

            // 负值测试
            Assert.Equal(-100, UnitHelper.ConvertLength(-1, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Centimeter));
        }

        #endregion

        #region 货币转换测试

        /// <summary>
        /// 测试货币转换基本功能
        /// </summary>
        [Fact]
        public void ConvertCurrency_BasicConversion_ReturnsCorrectValue()
        {
            // Arrange
            decimal amount = 7.2m; // 7.2 CNY

            // Act & Assert
            // 7.2 CNY = 1 USD (根据设定的汇率)
            Assert.Equal(1.0m, UnitHelper.ConvertCurrency(amount, UnitHelper.CurrencyUnit.CNY, UnitHelper.CurrencyUnit.USD));

            // 1 USD = 7.2 CNY
            Assert.Equal(7.2m, UnitHelper.ConvertCurrency(1, UnitHelper.CurrencyUnit.USD, UnitHelper.CurrencyUnit.CNY));
        }

        /// <summary>
        /// 测试货币符号获取
        /// </summary>
        [Fact]
        public void GetCurrencySymbol_ValidCurrency_ReturnsCorrectSymbol()
        {
            // Act & Assert
            Assert.Equal("¥", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.CNY));
            Assert.Equal("$", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.USD));
            Assert.Equal("€", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.EUR));
        }

        #endregion

        #region 质量转换测试

        /// <summary>
        /// 测试质量转换基本功能
        /// </summary>
        [Fact]
        public void ConvertMass_BasicConversion_ReturnsCorrectValue()
        {
            // 1000克 = 1千克
            Assert.Equal(1.0, UnitHelper.ConvertMass(1000, UnitHelper.MassUnit.Gram, UnitHelper.MassUnit.Kilogram), 3);

            // 1吨 = 1000千克
            Assert.Equal(1000.0, UnitHelper.ConvertMass(1, UnitHelper.MassUnit.Ton, UnitHelper.MassUnit.Kilogram), 3);
        }

        #endregion

        #region 角度转换测试

        /// <summary>
        /// 测试角度转换基本功能
        /// </summary>
        [Fact]
        public void ConvertAngle_BasicConversion_ReturnsCorrectValue()
        {
            // 180度 = π弧度
            Assert.Equal(Math.PI, UnitHelper.ConvertAngle(180, UnitHelper.AngleUnit.Degree, UnitHelper.AngleUnit.Radian), 3);

            // π弧度 = 180度
            Assert.Equal(180.0, UnitHelper.ConvertAngle(Math.PI, UnitHelper.AngleUnit.Radian, UnitHelper.AngleUnit.Degree), 3);
        }

        #endregion

        #region 进制转换测试

        /// <summary>
        /// 测试进制转换基本功能
        /// </summary>
        [Fact]
        public void ConvertBase_BasicConversion_ReturnsCorrectValue()
        {
            // 二进制1010 = 十进制10
            Assert.Equal("10", UnitHelper.ConvertBase("1010", UnitHelper.NumberBase.Binary, UnitHelper.NumberBase.Decimal));

            // 十进制10 = 十六进制A
            Assert.Equal("A", UnitHelper.ConvertBase("10", UnitHelper.NumberBase.Decimal, UnitHelper.NumberBase.Hexadecimal));
        }

        /// <summary>
        /// 测试十进制与其他进制互转
        /// </summary>
        [Fact]
        public void ConvertFromDecimalAndToDecimal_Conversion_ReturnsCorrectValue()
        {
            // 十进制10转二进制
            Assert.Equal("1010", UnitHelper.ConvertFromDecimal(10, UnitHelper.NumberBase.Binary));

            // 二进制1010转十进制
            Assert.Equal(10L, UnitHelper.ConvertToDecimal("1010", UnitHelper.NumberBase.Binary));
        }

        #endregion

        #region 面积转换测试

        /// <summary>
        /// 测试面积转换基本功能
        /// </summary>
        [Fact]
        public void ConvertArea_BasicConversion_ReturnsCorrectValue()
        {
            // 10000平方米 = 1公顷
            Assert.Equal(1.0, UnitHelper.ConvertArea(10000, UnitHelper.AreaUnit.SquareMeter, UnitHelper.AreaUnit.Hectare), 3);

            // 1平方公里 = 1000000平方米
            Assert.Equal(1000000.0, UnitHelper.ConvertArea(1, UnitHelper.AreaUnit.SquareKilometer, UnitHelper.AreaUnit.SquareMeter), 3);
        }

        #endregion

        #region 体积转换测试

        /// <summary>
        /// 测试体积转换基本功能
        /// </summary>
        [Fact]
        public void ConvertVolume_BasicConversion_ReturnsCorrectValue()
        {
            // 1000升 = 1立方米
            Assert.Equal(1.0, UnitHelper.ConvertVolume(1000, UnitHelper.VolumeUnit.Liter, UnitHelper.VolumeUnit.CubicMeter), 3);

            // 1立方英尺 ≈ 28.3168升
            double result = UnitHelper.ConvertVolume(1, UnitHelper.VolumeUnit.CubicFoot, UnitHelper.VolumeUnit.Liter);
            Assert.Equal(28.3168, result, 3);
        }

        #endregion

        #region 温度转换测试

        /// <summary>
        /// 测试温度转换基本功能
        /// </summary>
        [Fact]
        public void ConvertTemperature_BasicConversion_ReturnsCorrectValue()
        {
            // 0°C = 32°F
            Assert.Equal(32.0, UnitHelper.ConvertTemperature(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit), 2);

            // 32°F = 0°C
            Assert.Equal(0.0, UnitHelper.ConvertTemperature(32, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius), 2);

            // 0°C = 273.15K
            Assert.Equal(273.15, UnitHelper.ConvertTemperature(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Kelvin), 2);
        }

        #endregion

        #region 速度转换测试

        /// <summary>
        /// 测试速度转换基本功能
        /// </summary>
        [Fact]
        public void ConvertSpeed_BasicConversion_ReturnsCorrectValue()
        {
            // 36 km/h = 10 m/s
            Assert.Equal(10.0, UnitHelper.ConvertSpeed(36, UnitHelper.SpeedUnit.KilometersPerHour, UnitHelper.SpeedUnit.MetersPerSecond), 2);

            // 1 m/s = 3.6 km/h
            Assert.Equal(3.6, UnitHelper.ConvertSpeed(1, UnitHelper.SpeedUnit.MetersPerSecond, UnitHelper.SpeedUnit.KilometersPerHour), 2);
        }

        #endregion

        #region 热能转换测试

        /// <summary>
        /// 测试热能转换基本功能
        /// </summary>
        [Fact]
        public void ConvertEnergy_BasicConversion_ReturnsCorrectValue()
        {
            // 1千卡 = 4184焦耳
            Assert.Equal(4184.0, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.Kilocalorie, UnitHelper.EnergyUnit.Joule), 1);

            // 1千瓦时 = 3600000焦耳
            Assert.Equal(3600000.0, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.KilowattHour, UnitHelper.EnergyUnit.Joule), 1);
        }

        #endregion

        #region 功率转换测试

        /// <summary>
        /// 测试功率转换基本功能
        /// </summary>
        [Fact]
        public void ConvertPower_BasicConversion_ReturnsCorrectValue()
        {
            // 1千瓦 = 1000瓦
            Assert.Equal(1000.0, UnitHelper.ConvertPower(1, UnitHelper.PowerUnit.Kilowatt, UnitHelper.PowerUnit.Watt), 1);

            // 1马力 ≈ 745.7瓦
            Assert.Equal(745.7, UnitHelper.ConvertPower(1, UnitHelper.PowerUnit.Horsepower, UnitHelper.PowerUnit.Watt), 1);
        }

        #endregion

        #region 压强转换测试

        /// <summary>
        /// 测试压强转换基本功能
        /// </summary>
        [Fact]
        public void ConvertPressure_BasicConversion_ReturnsCorrectValue()
        {
            // 1标准大气压 = 101325帕斯卡
            Assert.Equal(101325.0, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.Atmosphere, UnitHelper.PressureUnit.Pascal), 1);

            // 1巴 = 100000帕斯卡
            Assert.Equal(100000.0, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.Bar, UnitHelper.PressureUnit.Pascal), 1);
        }

        #endregion

        #region 力转换测试

        /// <summary>
        /// 测试力转换基本功能
        /// </summary>
        [Fact]
        public void ConvertForce_BasicConversion_ReturnsCorrectValue()
        {
            // 1千克力 = 9.80665牛顿
            Assert.Equal(9.80665, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.KilogramForce, UnitHelper.ForceUnit.Newton), 5);

            // 1磅力 ≈ 4.44822牛顿
            Assert.Equal(4.44822, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.PoundForce, UnitHelper.ForceUnit.Newton), 5);
        }

        #endregion

        #region 实用工具方法测试

        /// <summary>
        /// 测试获取单位符号
        /// </summary>
        [Fact]
        public void GetUnitSymbol_ValidUnits_ReturnsCorrectSymbol()
        {
            // 长度单位
            Assert.Equal("mm", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Millimeter));
            Assert.Equal("m", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Meter));
            Assert.Equal("km", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Kilometer));

            // 质量单位
            Assert.Equal("kg", UnitHelper.GetUnitSymbol(UnitHelper.MassUnit.Kilogram));
            Assert.Equal("g", UnitHelper.GetUnitSymbol(UnitHelper.MassUnit.Gram));

            // 温度单位
            Assert.Equal("°C", UnitHelper.GetUnitSymbol(UnitHelper.TemperatureUnit.Celsius));
            Assert.Equal("K", UnitHelper.GetUnitSymbol(UnitHelper.TemperatureUnit.Kelvin));
        }

        /// <summary>
        /// 测试获取单位名称
        /// </summary>
        [Fact]
        public void GetUnitName_ValidUnits_ReturnsCorrectName()
        {
            // 长度单位
            Assert.Equal("毫米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Millimeter));
            Assert.Equal("米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Meter));
            Assert.Equal("千米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Kilometer));

            // 质量单位
            Assert.Equal("千克", UnitHelper.GetUnitName(UnitHelper.MassUnit.Kilogram));
            Assert.Equal("克", UnitHelper.GetUnitName(UnitHelper.MassUnit.Gram));

            // 温度单位
            Assert.Equal("摄氏度", UnitHelper.GetUnitName(UnitHelper.TemperatureUnit.Celsius));
            Assert.Equal("开尔文", UnitHelper.GetUnitName(UnitHelper.TemperatureUnit.Kelvin));
        }

        #endregion

        #region 参数化测试示例

        /// <summary>
        /// 使用理论数据测试长度转换
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="fromUnit">源单位</param>
        /// <param name="toUnit">目标单位</param>
        /// <param name="expected">期望值</param>
        [Theory]
        [InlineData(1000, UnitHelper.LengthUnit.Millimeter, UnitHelper.LengthUnit.Meter, 1.0)]
        [InlineData(100, UnitHelper.LengthUnit.Centimeter, UnitHelper.LengthUnit.Meter, 1.0)]
        [InlineData(1, UnitHelper.LengthUnit.Kilometer, UnitHelper.LengthUnit.Meter, 1000.0)]
        [InlineData(1, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Meter, 1.0)]
        public void ConvertLength_ParameterizedTest_ReturnsCorrectValue(
            double value,
            UnitHelper.LengthUnit fromUnit,
            UnitHelper.LengthUnit toUnit,
            double expected)
        {
            Assert.Equal(expected, UnitHelper.ConvertLength(value, fromUnit, toUnit), 3);
        }

        /// <summary>
        /// 使用理论数据测试温度转换
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="fromUnit">源单位</param>
        /// <param name="toUnit">目标单位</param>
        /// <param name="expected">期望值</param>
        [Theory]
        [InlineData(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit, 32.0)]
        [InlineData(100, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit, 212.0)]
        [InlineData(32, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius, 0.0)]
        [InlineData(212, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius, 100.0)]
        public void ConvertTemperature_ParameterizedTest_ReturnsCorrectValue(
            double value,
            UnitHelper.TemperatureUnit fromUnit,
            UnitHelper.TemperatureUnit toUnit,
            double expected)
        {
            Assert.Equal(expected, UnitHelper.ConvertTemperature(value, fromUnit, toUnit), 2);
        }

        #endregion
    }
}