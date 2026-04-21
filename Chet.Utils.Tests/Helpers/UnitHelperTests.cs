using Xunit;

namespace Chet.Utils.Helpers.Tests
{
    public class UnitHelperTests
    {
        #region 长度转换测试

        [Fact]
        public void ConvertLength_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1.0, UnitHelper.ConvertLength(1000, UnitHelper.LengthUnit.Millimeter, UnitHelper.LengthUnit.Meter), 3);
            Assert.Equal(1.0, UnitHelper.ConvertLength(100, UnitHelper.LengthUnit.Centimeter, UnitHelper.LengthUnit.Meter), 3);
            Assert.Equal(1000.0, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.Kilometer, UnitHelper.LengthUnit.Meter), 3);
        }

        [Fact]
        public void ConvertLength_SameUnit_ReturnsSameValue()
        {
            Assert.Equal(5.5, UnitHelper.ConvertLength(5.5, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Meter));
        }

        [Fact]
        public void ConvertLength_ZeroAndNegativeValues_ReturnsCorrectValue()
        {
            Assert.Equal(0, UnitHelper.ConvertLength(0, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Centimeter));
            Assert.Equal(-100, UnitHelper.ConvertLength(-1, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Centimeter));
        }

        [Fact]
        public void ConvertLength_InchToCentimeter_ReturnsCorrectValue()
        {
            Assert.Equal(2.54, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.Inch, UnitHelper.LengthUnit.Centimeter), 2);
        }

        [Fact]
        public void ConvertLength_FootToMeter_ReturnsCorrectValue()
        {
            Assert.Equal(0.3048, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.Foot, UnitHelper.LengthUnit.Meter), 4);
        }

        [Fact]
        public void ConvertLength_MileToKilometer_ReturnsCorrectValue()
        {
            Assert.Equal(1.60934, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.Mile, UnitHelper.LengthUnit.Kilometer), 4);
        }

        [Fact]
        public void ConvertLength_NauticalMileToKilometer_ReturnsCorrectValue()
        {
            Assert.Equal(1.852, UnitHelper.ConvertLength(1, UnitHelper.LengthUnit.NauticalMile, UnitHelper.LengthUnit.Kilometer), 3);
        }

        [Theory]
        [InlineData(1000, UnitHelper.LengthUnit.Millimeter, UnitHelper.LengthUnit.Meter, 1.0)]
        [InlineData(100, UnitHelper.LengthUnit.Centimeter, UnitHelper.LengthUnit.Meter, 1.0)]
        [InlineData(1, UnitHelper.LengthUnit.Kilometer, UnitHelper.LengthUnit.Meter, 1000.0)]
        [InlineData(1, UnitHelper.LengthUnit.Meter, UnitHelper.LengthUnit.Meter, 1.0)]
        public void ConvertLength_ParameterizedTest_ReturnsCorrectValue(
            double value, UnitHelper.LengthUnit fromUnit, UnitHelper.LengthUnit toUnit, double expected)
        {
            Assert.Equal(expected, UnitHelper.ConvertLength(value, fromUnit, toUnit), 3);
        }

        #endregion

        #region 货币转换测试

        [Fact]
        public void ConvertCurrency_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1.0m, UnitHelper.ConvertCurrency(7.2m, UnitHelper.CurrencyUnit.CNY, UnitHelper.CurrencyUnit.USD), 1);
            Assert.Equal(7.2m, UnitHelper.ConvertCurrency(1, UnitHelper.CurrencyUnit.USD, UnitHelper.CurrencyUnit.CNY), 1);
        }

        [Fact]
        public void GetCurrencySymbol_ValidCurrency_ReturnsCorrectSymbol()
        {
            Assert.Equal("¥", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.CNY));
            Assert.Equal("$", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.USD));
            Assert.Equal("€", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.EUR));
            Assert.Equal("£", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.GBP));
            Assert.Equal("¥", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.JPY));
            Assert.Equal("₩", UnitHelper.GetCurrencySymbol(UnitHelper.CurrencyUnit.KRW));
        }

        [Fact]
        public void ConvertCurrency_SameCurrency_ReturnsSameValue()
        {
            Assert.Equal(100m, UnitHelper.ConvertCurrency(100m, UnitHelper.CurrencyUnit.USD, UnitHelper.CurrencyUnit.USD));
        }

        [Fact]
        public void ConvertCurrency_ZeroAmount_ReturnsZero()
        {
            Assert.Equal(0m, UnitHelper.ConvertCurrency(0m, UnitHelper.CurrencyUnit.USD, UnitHelper.CurrencyUnit.CNY));
        }

        #endregion

        #region 质量转换测试

        [Fact]
        public void ConvertMass_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1.0, UnitHelper.ConvertMass(1000, UnitHelper.MassUnit.Gram, UnitHelper.MassUnit.Kilogram), 3);
            Assert.Equal(1000.0, UnitHelper.ConvertMass(1, UnitHelper.MassUnit.Ton, UnitHelper.MassUnit.Kilogram), 3);
        }

        [Fact]
        public void ConvertMass_PoundToKilogram_ReturnsCorrectValue()
        {
            Assert.Equal(0.453592, UnitHelper.ConvertMass(1, UnitHelper.MassUnit.Pound, UnitHelper.MassUnit.Kilogram), 5);
        }

        [Fact]
        public void ConvertMass_OunceToGram_ReturnsCorrectValue()
        {
            Assert.Equal(28.3495, UnitHelper.ConvertMass(1, UnitHelper.MassUnit.Ounce, UnitHelper.MassUnit.Gram), 3);
        }

        [Fact]
        public void ConvertMass_SameUnit_ReturnsSameValue()
        {
            Assert.Equal(5.5, UnitHelper.ConvertMass(5.5, UnitHelper.MassUnit.Kilogram, UnitHelper.MassUnit.Kilogram));
        }

        #endregion

        #region 角度转换测试

        [Fact]
        public void ConvertAngle_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(Math.PI, UnitHelper.ConvertAngle(180, UnitHelper.AngleUnit.Degree, UnitHelper.AngleUnit.Radian), 3);
            Assert.Equal(180.0, UnitHelper.ConvertAngle(Math.PI, UnitHelper.AngleUnit.Radian, UnitHelper.AngleUnit.Degree), 3);
        }

        [Fact]
        public void ConvertAngle_GradToDegree_ReturnsCorrectValue()
        {
            Assert.Equal(90.0, UnitHelper.ConvertAngle(100, UnitHelper.AngleUnit.Grad, UnitHelper.AngleUnit.Degree), 1);
        }

        [Fact]
        public void ConvertAngle_DegreeToMinute_ReturnsCorrectValue()
        {
            Assert.Equal(60.0, UnitHelper.ConvertAngle(1, UnitHelper.AngleUnit.Degree, UnitHelper.AngleUnit.Minute));
        }

        [Fact]
        public void ConvertAngle_DegreeToSecond_ReturnsCorrectValue()
        {
            Assert.Equal(3600.0, UnitHelper.ConvertAngle(1, UnitHelper.AngleUnit.Degree, UnitHelper.AngleUnit.Second));
        }

        #endregion

        #region 进制转换测试

        [Fact]
        public void ConvertBase_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal("10", UnitHelper.ConvertBase("1010", UnitHelper.NumberBase.Binary, UnitHelper.NumberBase.Decimal));
            Assert.Equal("A", UnitHelper.ConvertBase("10", UnitHelper.NumberBase.Decimal, UnitHelper.NumberBase.Hexadecimal));
        }

        [Fact]
        public void ConvertFromDecimalAndToDecimal_Conversion_ReturnsCorrectValue()
        {
            Assert.Equal("1010", UnitHelper.ConvertFromDecimal(10, UnitHelper.NumberBase.Binary));
            Assert.Equal(10L, UnitHelper.ConvertToDecimal("1010", UnitHelper.NumberBase.Binary));
        }

        [Fact]
        public void ConvertBase_BinaryToHexadecimal_ReturnsCorrectValue()
        {
            Assert.Equal("A", UnitHelper.ConvertBase("1010", UnitHelper.NumberBase.Binary, UnitHelper.NumberBase.Hexadecimal));
        }

        [Fact]
        public void ConvertBase_OctalToDecimal_ReturnsCorrectValue()
        {
            Assert.Equal("8", UnitHelper.ConvertBase("10", UnitHelper.NumberBase.Octal, UnitHelper.NumberBase.Decimal));
        }

        [Fact]
        public void ConvertBase_HexadecimalToDecimal_ReturnsCorrectValue()
        {
            Assert.Equal("255", UnitHelper.ConvertBase("FF", UnitHelper.NumberBase.Hexadecimal, UnitHelper.NumberBase.Decimal));
        }

        [Fact]
        public void ConvertFromDecimal_LargeNumber_ReturnsCorrectValue()
        {
            Assert.Equal("FF", UnitHelper.ConvertFromDecimal(255, UnitHelper.NumberBase.Hexadecimal));
            Assert.Equal("377", UnitHelper.ConvertFromDecimal(255, UnitHelper.NumberBase.Octal));
        }

        #endregion

        #region 面积转换测试

        [Fact]
        public void ConvertArea_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1.0, UnitHelper.ConvertArea(10000, UnitHelper.AreaUnit.SquareMeter, UnitHelper.AreaUnit.Hectare), 3);
            Assert.Equal(1000000.0, UnitHelper.ConvertArea(1, UnitHelper.AreaUnit.SquareKilometer, UnitHelper.AreaUnit.SquareMeter), 3);
        }

        [Fact]
        public void ConvertArea_AcreToHectare_ReturnsCorrectValue()
        {
            Assert.Equal(0.404686, UnitHelper.ConvertArea(1, UnitHelper.AreaUnit.Acre, UnitHelper.AreaUnit.Hectare), 4);
        }

        [Fact]
        public void ConvertArea_SquareFootToSquareMeter_ReturnsCorrectValue()
        {
            Assert.Equal(0.092903, UnitHelper.ConvertArea(1, UnitHelper.AreaUnit.SquareFoot, UnitHelper.AreaUnit.SquareMeter), 5);
        }

        #endregion

        #region 体积转换测试

        [Fact]
        public void ConvertVolume_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1.0, UnitHelper.ConvertVolume(1000, UnitHelper.VolumeUnit.Liter, UnitHelper.VolumeUnit.CubicMeter), 3);
            double result = UnitHelper.ConvertVolume(1, UnitHelper.VolumeUnit.CubicFoot, UnitHelper.VolumeUnit.Liter);
            Assert.Equal(28.3168, result, 3);
        }

        [Fact]
        public void ConvertVolume_GallonUSToLiter_ReturnsCorrectValue()
        {
            Assert.Equal(3.78541, UnitHelper.ConvertVolume(1, UnitHelper.VolumeUnit.GallonUS, UnitHelper.VolumeUnit.Liter), 4);
        }

        [Fact]
        public void ConvertVolume_MilliliterToLiter_ReturnsCorrectValue()
        {
            Assert.Equal(0.001, UnitHelper.ConvertVolume(1, UnitHelper.VolumeUnit.Milliliter, UnitHelper.VolumeUnit.Liter), 4);
        }

        #endregion

        #region 温度转换测试

        [Fact]
        public void ConvertTemperature_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(32.0, UnitHelper.ConvertTemperature(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit), 2);
            Assert.Equal(0.0, UnitHelper.ConvertTemperature(32, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius), 2);
            Assert.Equal(273.15, UnitHelper.ConvertTemperature(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Kelvin), 2);
        }

        [Fact]
        public void ConvertTemperature_BoilingPoint_ReturnsCorrectValue()
        {
            Assert.Equal(212.0, UnitHelper.ConvertTemperature(100, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit), 2);
            Assert.Equal(373.15, UnitHelper.ConvertTemperature(100, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Kelvin), 2);
        }

        [Theory]
        [InlineData(0, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit, 32.0)]
        [InlineData(100, UnitHelper.TemperatureUnit.Celsius, UnitHelper.TemperatureUnit.Fahrenheit, 212.0)]
        [InlineData(32, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius, 0.0)]
        [InlineData(212, UnitHelper.TemperatureUnit.Fahrenheit, UnitHelper.TemperatureUnit.Celsius, 100.0)]
        public void ConvertTemperature_ParameterizedTest_ReturnsCorrectValue(
            double value, UnitHelper.TemperatureUnit fromUnit, UnitHelper.TemperatureUnit toUnit, double expected)
        {
            Assert.Equal(expected, UnitHelper.ConvertTemperature(value, fromUnit, toUnit), 2);
        }

        #endregion

        #region 速度转换测试

        [Fact]
        public void ConvertSpeed_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(10.0, UnitHelper.ConvertSpeed(36, UnitHelper.SpeedUnit.KilometersPerHour, UnitHelper.SpeedUnit.MetersPerSecond), 2);
            Assert.Equal(3.6, UnitHelper.ConvertSpeed(1, UnitHelper.SpeedUnit.MetersPerSecond, UnitHelper.SpeedUnit.KilometersPerHour), 2);
        }

        [Fact]
        public void ConvertSpeed_MilesPerHourToKilometersPerHour_ReturnsCorrectValue()
        {
            Assert.Equal(1.60934, UnitHelper.ConvertSpeed(1, UnitHelper.SpeedUnit.MilesPerHour, UnitHelper.SpeedUnit.KilometersPerHour), 4);
        }

        [Fact]
        public void ConvertSpeed_KnotsToKilometersPerHour_ReturnsCorrectValue()
        {
            Assert.Equal(1.852, UnitHelper.ConvertSpeed(1, UnitHelper.SpeedUnit.Knots, UnitHelper.SpeedUnit.KilometersPerHour), 3);
        }

        #endregion

        #region 热能转换测试

        [Fact]
        public void ConvertEnergy_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(4184.0, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.Kilocalorie, UnitHelper.EnergyUnit.Joule), 1);
            Assert.Equal(3600000.0, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.KilowattHour, UnitHelper.EnergyUnit.Joule), 1);
        }

        [Fact]
        public void ConvertEnergy_CalorieToJoule_ReturnsCorrectValue()
        {
            Assert.Equal(4.184, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.Calorie, UnitHelper.EnergyUnit.Joule), 3);
        }

        [Fact]
        public void ConvertEnergy_BTUToJoule_ReturnsCorrectValue()
        {
            Assert.Equal(1055.06, UnitHelper.ConvertEnergy(1, UnitHelper.EnergyUnit.BTU, UnitHelper.EnergyUnit.Joule), 1);
        }

        #endregion

        #region 功率转换测试

        [Fact]
        public void ConvertPower_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(1000.0, UnitHelper.ConvertPower(1, UnitHelper.PowerUnit.Kilowatt, UnitHelper.PowerUnit.Watt), 1);
            Assert.Equal(735.5, UnitHelper.ConvertPower(1, UnitHelper.PowerUnit.Horsepower, UnitHelper.PowerUnit.Watt), 1);
        }

        [Fact]
        public void ConvertPower_MegawattToKilowatt_ReturnsCorrectValue()
        {
            Assert.Equal(1000.0, UnitHelper.ConvertPower(1, UnitHelper.PowerUnit.Megawatt, UnitHelper.PowerUnit.Kilowatt), 1);
        }

        #endregion

        #region 压强转换测试

        [Fact]
        public void ConvertPressure_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(101325.0, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.Atmosphere, UnitHelper.PressureUnit.Pascal), 1);
            Assert.Equal(100000.0, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.Bar, UnitHelper.PressureUnit.Pascal), 1);
        }

        [Fact]
        public void ConvertPressure_PSIToPascal_ReturnsCorrectValue()
        {
            Assert.Equal(6894.76, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.PSI, UnitHelper.PressureUnit.Pascal), 1);
        }

        [Fact]
        public void ConvertPressure_TorrToPascal_ReturnsCorrectValue()
        {
            Assert.Equal(133.322, UnitHelper.ConvertPressure(1, UnitHelper.PressureUnit.Torr, UnitHelper.PressureUnit.Pascal), 2);
        }

        #endregion

        #region 力转换测试

        [Fact]
        public void ConvertForce_BasicConversion_ReturnsCorrectValue()
        {
            Assert.Equal(9.80665, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.KilogramForce, UnitHelper.ForceUnit.Newton), 5);
            Assert.Equal(4.44822, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.PoundForce, UnitHelper.ForceUnit.Newton), 5);
        }

        [Fact]
        public void ConvertForce_KilonewtonToNewton_ReturnsCorrectValue()
        {
            Assert.Equal(1000.0, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.Kilonewton, UnitHelper.ForceUnit.Newton), 1);
        }

        [Fact]
        public void ConvertForce_DyneToNewton_ReturnsCorrectValue()
        {
            Assert.Equal(0.00001, UnitHelper.ConvertForce(1, UnitHelper.ForceUnit.Dyne, UnitHelper.ForceUnit.Newton), 6);
        }

        #endregion

        #region 实用工具方法测试

        [Fact]
        public void GetUnitSymbol_ValidUnits_ReturnsCorrectSymbol()
        {
            Assert.Equal("mm", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Millimeter));
            Assert.Equal("m", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Meter));
            Assert.Equal("km", UnitHelper.GetUnitSymbol(UnitHelper.LengthUnit.Kilometer));
            Assert.Equal("kg", UnitHelper.GetUnitSymbol(UnitHelper.MassUnit.Kilogram));
            Assert.Equal("g", UnitHelper.GetUnitSymbol(UnitHelper.MassUnit.Gram));
            Assert.Equal("°C", UnitHelper.GetUnitSymbol(UnitHelper.TemperatureUnit.Celsius));
            Assert.Equal("K", UnitHelper.GetUnitSymbol(UnitHelper.TemperatureUnit.Kelvin));
        }

        [Fact]
        public void GetUnitName_ValidUnits_ReturnsCorrectName()
        {
            Assert.Equal("毫米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Millimeter));
            Assert.Equal("米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Meter));
            Assert.Equal("千米", UnitHelper.GetUnitName(UnitHelper.LengthUnit.Kilometer));
            Assert.Equal("千克", UnitHelper.GetUnitName(UnitHelper.MassUnit.Kilogram));
            Assert.Equal("克", UnitHelper.GetUnitName(UnitHelper.MassUnit.Gram));
            Assert.Equal("摄氏度", UnitHelper.GetUnitName(UnitHelper.TemperatureUnit.Celsius));
            Assert.Equal("开尔文", UnitHelper.GetUnitName(UnitHelper.TemperatureUnit.Kelvin));
        }

        [Fact]
        public void GetUnitSymbol_EnergyUnits_ReturnsCorrectSymbol()
        {
            Assert.Equal("J", UnitHelper.GetUnitSymbol(UnitHelper.EnergyUnit.Joule));
            Assert.Equal("kcal", UnitHelper.GetUnitSymbol(UnitHelper.EnergyUnit.Kilocalorie));
            Assert.Equal("kWh", UnitHelper.GetUnitSymbol(UnitHelper.EnergyUnit.KilowattHour));
        }

        [Fact]
        public void GetUnitSymbol_PowerUnits_ReturnsCorrectSymbol()
        {
            Assert.Equal("W", UnitHelper.GetUnitSymbol(UnitHelper.PowerUnit.Watt));
            Assert.Equal("kW", UnitHelper.GetUnitSymbol(UnitHelper.PowerUnit.Kilowatt));
            Assert.Equal("hp", UnitHelper.GetUnitSymbol(UnitHelper.PowerUnit.Horsepower));
        }

        [Fact]
        public void GetUnitSymbol_PressureUnits_ReturnsCorrectSymbol()
        {
            Assert.Equal("Pa", UnitHelper.GetUnitSymbol(UnitHelper.PressureUnit.Pascal));
            Assert.Equal("bar", UnitHelper.GetUnitSymbol(UnitHelper.PressureUnit.Bar));
            Assert.Equal("atm", UnitHelper.GetUnitSymbol(UnitHelper.PressureUnit.Atmosphere));
            Assert.Equal("psi", UnitHelper.GetUnitSymbol(UnitHelper.PressureUnit.PSI));
        }

        [Fact]
        public void GetUnitName_EnergyUnits_ReturnsCorrectName()
        {
            Assert.Equal("焦耳", UnitHelper.GetUnitName(UnitHelper.EnergyUnit.Joule));
            Assert.Equal("千卡", UnitHelper.GetUnitName(UnitHelper.EnergyUnit.Kilocalorie));
            Assert.Equal("千瓦时", UnitHelper.GetUnitName(UnitHelper.EnergyUnit.KilowattHour));
        }

        [Fact]
        public void GetUnitName_PowerUnits_ReturnsCorrectName()
        {
            Assert.Equal("瓦特", UnitHelper.GetUnitName(UnitHelper.PowerUnit.Watt));
            Assert.Equal("千瓦", UnitHelper.GetUnitName(UnitHelper.PowerUnit.Kilowatt));
            Assert.Equal("马力", UnitHelper.GetUnitName(UnitHelper.PowerUnit.Horsepower));
        }

        [Fact]
        public void GetUnitName_PressureUnits_ReturnsCorrectName()
        {
            Assert.Equal("帕斯卡", UnitHelper.GetUnitName(UnitHelper.PressureUnit.Pascal));
            Assert.Equal("巴", UnitHelper.GetUnitName(UnitHelper.PressureUnit.Bar));
            Assert.Equal("标准大气压", UnitHelper.GetUnitName(UnitHelper.PressureUnit.Atmosphere));
        }

        #endregion

        #region 边界值测试

        [Fact]
        public void ConvertLength_VeryLargeValue_ReturnsCorrectValue()
        {
            var result = UnitHelper.ConvertLength(1000000, UnitHelper.LengthUnit.Millimeter, UnitHelper.LengthUnit.Kilometer);
            Assert.Equal(1.0, result, 3);
        }

        [Fact]
        public void ConvertTemperature_AbsoluteZero_ReturnsCorrectValue()
        {
            var result = UnitHelper.ConvertTemperature(0, UnitHelper.TemperatureUnit.Kelvin, UnitHelper.TemperatureUnit.Celsius);
            Assert.Equal(-273.15, result, 2);
        }

        [Fact]
        public void ConvertMass_VerySmallValue_ReturnsCorrectValue()
        {
            var result = UnitHelper.ConvertMass(1, UnitHelper.MassUnit.Milligram, UnitHelper.MassUnit.Kilogram);
            Assert.Equal(0.000001, result, 7);
        }

        #endregion
    }
}
