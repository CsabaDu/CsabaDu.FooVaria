namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        _paramName = null;
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        _measureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        _measurable = new(RootObject, _paramName)
        {
            Returns = new()
            {
                GetBaseMeasureUnit = _measureUnit,
            }
        };

        _measureUnitType = _measureUnit.GetType();
    }

    [TestCleanup]
    public void CleanupMeasurableTests()
    {
        _factory = null;
        _paramName = null;
        _measureUnit = null;
        _measurable = null;
    }
    #endregion

    #region Private fields
    private MeasurableChild _measurable;
    private Enum _measureUnit;
    private MeasureUnitCode _measureUnitCode;
    private Type _measureUnitType;
    private string _paramName;
    private IFactory _factory;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region Tested in parent classes' tests
    // Measurable(IRootObject rootObject, string paramName)
    // IFactory ICommonBase.GetFactory()
    #endregion

    #region bool Equals
    #region Measurable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };

        // Act
        var actual = _measurable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Enum GetBaseMeasureUnit
    #region IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = RandomParams.GetRandomMeasureUnit();
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = expected,
        };

        // Act
        var actual = _measurable.GetBaseMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Enum GetDefaultMeasureUnit
    #region IDefaultMeasureUnit.GetDefaultMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = (Enum)Enum.ToObject(_measureUnitType, 0);

        // Act
        var actual = _measurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IEnumerable<string> GetDefaultMeasureUnitNames
    #region IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnitNames_returns_expected()
    {
        // Arrange
        string measureUnitCodeName = Enum.GetName(_measureUnitCode);
        string getDefaultMeasureUnitName(string measureUnitName) => measureUnitName + measureUnitCodeName;
        IEnumerable<string> expected = Enum.GetNames(_measureUnitType).Select(getDefaultMeasureUnitName);

        // Act
        var actual = _measurable.GetDefaultMeasureUnitNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region Measurable.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = _measureUnitCode.GetHashCode();

        // Act
        var actual = _measurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region MeasureUnitCode GetMeasureUnitCode
    #region IMeasureUnitCode.GetMeasureUnitCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCode_returns_expected()
    {
        // Arrange
        MeasureUnitCode expected = _measureUnitCode;

        // Act
        var actual = _measurable.GetMeasureUnitCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Type GetMeasureUnitType
    #region IMeasureUnit.GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returns_expected()
    {
        // Arrange
        Type expected = _measureUnit.GetType();

        // Act
        var actual = _measurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TypeCode GetQuantityTypeCode
    #region IQuantityType.GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        TypeCode expected = _measureUnitCode.GetQuantityTypeCode();

        // Act
        var actual = _measurable.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool HasMeasureUnitCode
    #region IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected)
    {
        // Arrange
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };

        // Act
        var actual = _measurable.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnit
    #region IDefaultMeasureUnit.ValidateMeasureUnit(Enum?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
        };
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(measureUnit, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
        };
        _paramName = RandomParams.GetRandomParamName();
        bool returned;

        // Act
        try
        {
            _measurable.ValidateMeasureUnit(measureUnit, _paramName);
            returned = true;
        }
        catch (InvalidEnumArgumentException)
        {
            returned = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnitCode
    #region IMeasurable.ValidateMeasureUnitCode(IMeasurable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_nullArg_IMeasurable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_invalidArg_IMeasurable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        MeasurableChild measurable = new(RootObject, _paramName);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        measurable.Returns = new()
        {
            GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode),
        };
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measurable, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_IMeasurable_arg_string_returns()
    {
        // Arrange
        MeasurableChild measurable = new(RootObject, _paramName);
        measurable.Returns = new()
        {
            GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode),
        };
        _paramName = RandomParams.GetRandomParamName();

        bool returned;

        // Act
        try
        {
            _measurable.ValidateMeasureUnitCode(measurable, _paramName);
            returned = true;
        }
        catch (InvalidEnumArgumentException)
        {
            returned = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion

    #region IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measureUnitCode, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_MeasureUnitCode_arg_string_returns()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();
        bool returned;

        // Act
        try
        {
            _measurable.ValidateMeasureUnitCode(_measureUnitCode, _paramName);
            returned = true;
        }
        catch (InvalidEnumArgumentException)
        {
            returned = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    //#region Static methods

    //#endregion
    #endregion

    #region DynamicDataSource
    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSource.GetMeasurableEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        return DynamicDataSource.GetMeasurableValidateMeasureUnitInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList()
    {
        return DynamicDataSource.GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSource.GetHasMeasureUnitCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
    {
        return DynamicDataSource.GetValidMeasureUnitArgsArrayList();
    }
    #endregion
}
