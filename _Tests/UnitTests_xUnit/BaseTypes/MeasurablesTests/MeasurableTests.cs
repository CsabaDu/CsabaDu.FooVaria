//using CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;
//using CsabaDu.FooVaria.Tests.TestHelpers.Params;
//using CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Measurables;

//namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.MeasurableTests;

//public sealed class MeasurableTests
//{
//    #region Tested in parent classes' tests

//    // Measurable(IRootObject rootObject, string paramName)
//    // IFactory ICommonBase.GetFactory()

//    #endregion

//    #region Private fields
//    private readonly DataFields _fields;
//    private readonly MeasurableChild _measurable;

//    #region Static fields
//    //private static readonly DynamicDataSource DynamicDataSource = new();
//    #endregion
//    #endregion

//    #region Constructor
//    public MeasurableTests()
//    {
//        _fields = new();
//        _measurable = new MeasurableChild(_fields.RootObject, ParamNames.rootObject);
//    }
//    #endregion

//    #region Test methods
//    #region Static methods
//    [Fact]
//    public void GetMeasureUnit_ValidCodeAndValue_ReturnsExpectedMeasureUnit()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;
//        var value = 1;

//        // Act
//        var result = Measurable.GetMeasureUnit(measureUnitCode, value);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(value, (int)(object)result);
//    }

//    [Fact]
//    public void GetAllMeasureUnits_ReturnsAllMeasureUnits()
//    {
//        // Act
//        var result = Measurable.GetAllMeasureUnits();

//        // Assert
//        Assert.NotEmpty(result);
//    }

//    [Fact]
//    public void GetDefaultMeasureUnit_ValidType_ReturnsDefaultMeasureUnit()
//    {
//        // Arrange
//        var measureUnitType = typeof(MeasureUnitCode);

//        // Act
//        var result = Measurable.GetDefaultMeasureUnit(measureUnitType);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(default(int), (int)(object)result);
//    }

//    [Fact]
//    public void GetDefaultName_ValidMeasureUnit_ReturnsDefaultName()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.GetDefaultName(measureUnit);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Contains(measureUnit.ToString(), result);
//    }

//    [Fact]
//    public void GetDefaultNames_ValidMeasureUnitCode_ReturnsDefaultNames()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.GetDefaultNames(measureUnitCode);

//        // Assert
//        Assert.NotEmpty(result);
//    }

//    [Fact]
//    public void GetDefinedMeasureUnitCode_ValidMeasureUnit_ReturnsMeasureUnitCode()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.GetDefinedMeasureUnitCode(measureUnit);

//        // Assert
//        Assert.Equal(measureUnit, result);
//    }

//    [Fact]
//    public void GetMeasureUnitCode_ValidType_ReturnsMeasureUnitCode()
//    {
//        // Arrange
//        var measureUnitType = typeof(MeasureUnitCode);

//        // Act
//        var result = Measurable.GetMeasureUnitCode(measureUnitType);

//        // Assert
//        Assert.Equal(MeasureUnitCode.AreaUnit, result);
//    }

//    [Fact]
//    public void GetMeasureUnitCode_ValidName_ReturnsMeasureUnitCode()
//    {
//        // Arrange
//        var name = "AreaUnit";

//        // Act
//        var result = Measurable.GetMeasureUnitCode(name);

//        // Assert
//        Assert.Equal(MeasureUnitCode.AreaUnit, result);
//    }

//    [Fact]
//    public void HasMeasureUnitCode_ValidMeasureUnitCodeAndMeasureUnit_ReturnsTrue()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.HasMeasureUnitCode(measureUnitCode, measureUnit);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void IsDefaultMeasureUnit_ValidMeasureUnit_ReturnsTrue()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.IsDefaultMeasureUnit(measureUnit);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void IsDefinedMeasureUnit_ValidMeasureUnit_ReturnsTrue()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = Measurable.IsDefinedMeasureUnit(measureUnit);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void TryGetMeasureUnitCode_ValidMeasureUnit_ReturnsTrueAndMeasureUnitCode()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act
//        var success = Measurable.TryGetMeasureUnitCode(measureUnit, out var measureUnitCode);

//        // Assert
//        Assert.True(success);
//        Assert.Equal(MeasureUnitCode.AreaUnit, measureUnitCode);
//    }

//    [Fact]
//    public void ValidateMeasureUnitByDefinition_ValidMeasureUnit_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnitByDefinition(measureUnit, nameof(measureUnit)));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnit_ValidMeasureUnit_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;
//        var measureUnitName = "AreaUnit";
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnit(measureUnit, measureUnitName, measureUnitCode));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnitType_ValidMeasureUnitType_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnitType = typeof(MeasureUnitCode);

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnitType(measureUnitType));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnitType_ValidMeasureUnitTypeAndCode_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnitType = typeof(MeasureUnitCode);
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnitType(measureUnitType, measureUnitCode));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnit_ValidMeasureUnitAndType_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;
//        var measureUnitType = typeof(MeasureUnitCode);

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnit(measureUnit, measureUnitType));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnitCodeByDefinition_ValidMeasureUnitCode_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => Measurable.ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode)));
//        Assert.Null(exception);
//    }
//    #endregion

//    #region Instance methods
//    [Fact]
//    public void GetDefaultMeasureUnit_ReturnsDefaultMeasureUnit()
//    {
//        // Act
//        var result = _measurable.GetDefaultMeasureUnit();

//        // Assert
//        Assert.NotNull(result);
//    }

//    [Fact]
//    public void GetDefaultMeasureUnitNames_ReturnsDefaultMeasureUnitNames()
//    {
//        // Act
//        var result = _measurable.GetDefaultMeasureUnitNames();

//        // Assert
//        Assert.NotEmpty(result);
//    }

//    [Fact]
//    public void GetMeasureUnitType_ReturnsMeasureUnitType()
//    {
//        // Act
//        var result = _measurable.GetMeasureUnitType();

//        // Assert
//        Assert.NotNull(result);
//    }

//    [Fact]
//    public void ValidateMeasureUnitCode_ValidMeasurable_DoesNotThrow()
//    {
//        // Arrange
//        var measurable = new Mock<IMeasurable>().Object;

//        // Act & Assert
//        var exception = Record.Exception(() => _measurable.ValidateMeasureUnitCode(measurable, _paramName));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void Equals_ValidObject_ReturnsTrue()
//    {
//        // Arrange
//        var other = new Mock<IMeasurable>().Object;

//        // Act
//        var result = _measurable.Equals(other);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void GetHashCode_ReturnsExpectedHashCode()
//    {
//        // Act
//        var result = _measurable.GetHashCode();

//        // Assert
//        Assert.Equal(_measurable.GetMeasureUnitCode().GetHashCode(), result);
//    }

//    [Fact]
//    public void GetMeasureUnitCode_ReturnsMeasureUnitCode()
//    {
//        // Act
//        var result = _measurable.GetMeasureUnitCode();

//        // Assert
//        Assert.NotNull(result);
//    }

//    [Fact]
//    public void GetQuantityTypeCode_ReturnsQuantityTypeCode()
//    {
//        // Act
//        var result = _measurable.GetQuantityTypeCode();

//        // Assert
//        Assert.NotNull(result);
//    }

//    [Fact]
//    public void HasMeasureUnitCode_ValidMeasureUnitCode_ReturnsTrue()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act
//        var result = _measurable.HasMeasureUnitCode(measureUnitCode);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public void ValidateMeasureUnit_ValidMeasureUnit_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnit = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => _measurable.ValidateMeasureUnit(measureUnit, _paramName));
//        Assert.Null(exception);
//    }

//    [Fact]
//    public void ValidateMeasureUnitCode_ValidMeasureUnitCode_DoesNotThrow()
//    {
//        // Arrange
//        var measureUnitCode = MeasureUnitCode.AreaUnit;

//        // Act & Assert
//        var exception = Record.Exception(() => _measurable.ValidateMeasureUnitCode(measureUnitCode, _paramName));
//        Assert.Null(exception);
//    }
//    #endregion
//    #endregion

//    #region Private classes
//    //private class MeasurableChild : Measurable
//    //{
//    //    public MeasurableChild(IRootObject rootObject, string paramName) : base(rootObject, paramName) { }

//    //    public override Enum GetBaseMeasureUnit()
//    //    {
//    //        return MeasureUnitCode.AreaUnit;
//    //    }
//    //}
//    #endregion
//}
