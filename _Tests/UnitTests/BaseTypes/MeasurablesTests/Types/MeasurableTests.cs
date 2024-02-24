namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        factoryObject = new MeasurableFactoryClass();
        measurableObject = new MeasurableChild(factoryObject);
        paramName = RandomParams.GetRandomParamName();
        measureUnitType = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(measureUnitCode));
    }
    #endregion

    #region Private fields
    private MeasureUnitCode measureUnitCode;
    private MeasurableChild measurableObject;
    private MeasurableFactoryClass factoryObject;
    private Enum measureUnit;
    private string paramName;
    Type measureUnitType;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region Measurable(IMeasurableFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        factoryObject = null;

        // Act
        void attempt() => _ = new MeasurableChild(factoryObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_MeasureunitTypeCode_creates()
    {
        // Arrange
        factoryObject = new MeasurableFactoryClass();

        // Act
        var actual = new MeasurableChild(factoryObject);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
    }
    #endregion

    #region Measurable(IMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        measurableObject = null;

        // Act
        void attempt() => _ = new MeasurableChild(measurableObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurable_creates()
    {
        // Arrange
        factoryObject = new MeasurableFactoryClass();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measurableObject = new MeasurableChild(factoryObject, measureUnitCode);

        // Act
        var actual = new MeasurableChild(measurableObject);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measurableObject, actual);
    }
    #endregion
    #endregion

    //#region Equals
    //#region Equals(object?)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    //public void Equals_arg_object_returns_expected(bool expected, object other, MeasureUnitCode measureUnitCode)
    //{
    //    // Arrange
    //    measurableObject = new MeasurableChild(factoryObject);

    //    // Act
    //    var actual = measurableObject.Equals(other);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region GetDefaultMeasureUnit
    //#region GetDefaultMeasureUnit()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetDefaultMeasureUnit_returns_expected()
    //{
    //    // Arrange
    //    Enum expected = (Enum)Enum.ToObject(measureUnitType, 0);

    //    // Act
    //    var actual = measurableObject.GetDefaultMeasureUnit();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region GetDefaultMeasureUnitNames
    //#region GetDefaultMeasureUnitNames()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetDefaultMeasureUnitNames_returns_expected()
    //{
    //    // Arrange
    //    string measureUnitCodeName = Enum.GetName(measureUnitCode);
    //    IEnumerable<string> expected = Enum.GetNames(measureUnitType).Select(x => x + measureUnitCodeName);

    //    // Act
    //    var actual = measurableObject.GetDefaultMeasureUnitNames();

    //    // Assert
    //    Assert.IsTrue(expected.SequenceEqual(actual));
    //}
    //#endregion
    //#endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returns_expected()
    {
        // Arrange
        // Act
        var actual = measurableObject.GetFactory();

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(measurableObject.GetFactory(), typeof(IMeasurableFactory));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = measureUnitCode.GetHashCode();

        // Act
        var actual = measurableObject.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitCodes
    #region GetMeasureUnitCodes()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCodes_returns_expected()
    {
        // Arrange
        IEnumerable<MeasureUnitCode> expected = Enum.GetValues<MeasureUnitCode>();

        // Act
        var actual = measurableObject.GetMeasureUnitCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetMeasureUnitType
    #region GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returns_expected()
    {
        // Arrange
        Type expected = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(measureUnitCode));

        // Act
        var actual = measurableObject.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region HasMeasureUnitCode
    #region HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(MeasureUnitCode measureUnitCode, IMeasurable measurable, bool expected)
    {
        // Arrange
        // Act
        var actual = measurable.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetQuantityTypeCode
    #region GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        TypeCode expected = measureUnitCode.GetQuantityTypeCode();

        // Act
        var actual = measurableObject.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IsValidMeasureUnitCode
    #region IsValidMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsValidMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void IsValidMeasureUnitCode_arg_MeasureUnitCode_returns_expected(bool expected, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        // Act
        var actual = measurableObject.IsValidMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnit
    #region ValidateMeasureUnit(Enum, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;

        // Act
        void attempt() => measurableObject.ValidateMeasureUnit(measureUnit, paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        measurableObject = new MeasurableChild(factoryObject, measureUnitCode);

        // Act
        void attempt() => measurableObject.ValidateMeasureUnit(measureUnit, paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns()
    {
        // Arrange
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        measurableObject = new MeasurableChild(factoryObject, measureUnitCode);
        bool returned = true;

        // Act
        try
        {
            measurableObject.ValidateMeasureUnit(measureUnit, paramName);
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnitCode
    #region ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCodem_arg_string_throws_InvalidEnumArgumentException(MeasureUnitCode measureUnitCode, IMeasurable measurable)
    {
        // Arrange
        // Act
        void attempt() => measurable.ValidateMeasureUnitCode(measureUnitCode, paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_MeasureUnitCodem_arg_string_returns()
    {
        // Arrange
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measurableObject = new MeasurableChild(factoryObject, measureUnitCode);
        bool returned = true;

        // Act
        try
        {
            measurableObject.ValidateMeasureUnitCode(measureUnitCode, paramName);
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion

    #region ValidateMeasureUnitCode(IMeasurable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_nullArg_IMeasurable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        // Act
        void attempt() => measurableObject.ValidateMeasureUnitCode(null, paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_invalidArg_IMeasurable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);

        // Act
        void attempt() => measurableObject.ValidateMeasureUnitCode(new MeasurableChild(factoryObject, measureUnitCode), paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_IMeasurable_arg_string_returns()
    {
        // Arrange
        measurableObject = new MeasurableChild(factoryObject, measureUnitCode);
        bool returned = true;

        // Act
        try
        {
            measurableObject.ValidateMeasureUnitCode(measureUnitCode, paramName);
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    #region Static methods

    #endregion
    #endregion

    #region DynamicDataSources
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
}

    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetIsValidMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSources.GetIsValidMeasureUnitCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSources.GetHasMeasureUnitCodeArgsArrayList();
    }
    #endregion
}