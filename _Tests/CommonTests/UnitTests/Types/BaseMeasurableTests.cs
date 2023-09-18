using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;
using CsabaDu.FooVaria.Tests.TestSupport.Params;
using CsabaDu.FooVaria.Tests.TestSupport.Variants;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeBaseMeasureTests()
    {
        RandomParams = new();
        BaseMeasurable = new BaseMeasurableChild(RandomParams.GetRandomMeasureUnitTypeCode());
        BaseMeasurableVariant = new(RandomParams);
    }
    #endregion

    #region Private fields
    private static DynamicDataSources DynamicDataSources;
    private RandomParams RandomParams;
    private IBaseMeasurable BaseMeasurable;
    private BaseMeasurableVariant BaseMeasurableVariant;
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurable(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_InvalidArg_MeasureunitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_MeasureunitTypeCode_CreatesInstance()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new BaseMeasurableChild(measureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_Enum_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void BaseMeasurable_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = new BaseMeasurableChild(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_Enum_CreatesInstance()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomMeasureUnit();
        MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);

        // Act
        var actual = new BaseMeasurableChild(measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_IBaseMeasurable_ThrowsArgumentNullException()
    {
        // Arrange
        IBaseMeasurable other = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_IBaseMeasurable_CreatesInstance()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        var expected = new BaseMeasurableChild(measureUnitTypeCode);

        // Act
        IBaseMeasurable actual = new BaseMeasurableChild(expected);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(BaseMeasurableEqualsObjectArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_Arg_object_ReturnsExpected(bool expected, object obj, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetDefaultMeasureUnit
    #region GetDefaultMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_ReturnsExpected()
    {
        // Arrange
        Enum expected = BaseMeasurableVariant.GetDefaultMeasureUnit(out MeasureUnitTypeCode measureUnitTypeCode);
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetDefaultMeasureUnit(MeasureUnitTypeCode?)
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_InvalidArg_NullableMeasureUnitTpeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = BaseMeasurable.GetDefaultMeasureUnit(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void GetDefaultMeasureUnit_VaLidArg_NullableMeasureUnitTpeCode_ReturnsExpected(MeasureUnitTypeCode? measureUnitTypeCode, Enum expected, IBaseMeasurable baseMeasurable)
    {
        // Arrange
        // Act
        var actual = baseMeasurable.GetDefaultMeasureUnit(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetDefaultNames
    #region GetDefaultNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultNames_ReturnsExpected()
    {
        // Arrange
        IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(null);

        // Act
        var actual = BaseMeasurable.GetDefaultNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion

    #region GetDefaultNames(MeasureUnitTypeCode?)
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultNames_InvalidArg_NullableMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
       void attempt() => _ = BaseMeasurable.GetDefaultNames(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetNullableMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    public void GetDefaultNames_ValidArg_NullableMeasureUnitTypeCode_ReturnsExpected(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        // Arrange
        IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(measureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetDefaultNames(measureUnitTypeCode);

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_ReturnsExpected()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
        int expected = measureUnitTypeCode.GetHashCode();

        // Act
        var actual = baseMeasurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitType
    #region GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_ReturnsExpected()
    {
        // Arrange
        Type expected = RandomParams.GetRandomMeasureUnitType(out MeasureUnitTypeCode measureUnitTypeCode);
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasureUnitType(MeasureUnitTypeCode?)
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_InvalidArg_NullableMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = BaseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetNullableMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    public void GetMeasureUnitType_ValidArg_NullableMeasureUnitTypeCode_ReturnsExpected(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        // Arrange
        Type expected = BaseMeasurableVariant.GetMeasureUnitType(measureUnitTypeCode ?? BaseMeasurable.MeasureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitTypeCode
    #region GetMeasureUnitTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCode_ReturnsExpected()
    {
        // Arrange
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(RandomParams.GetRandomMeasureUnit());
        MeasureUnitTypeCode expected = baseMeasurable.MeasureUnitTypeCode;

        // Act
        var actual = baseMeasurable.GetMeasureUnitTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasureUnitTypeCode(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void GetMeasureUnitTypeCode_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetNullableEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void GGetMeasureUnitTypeCode_ValidArg_Enum_ReturnsExpected(Enum measureUnit)
    {
        // Arrange
        MeasureUnitTypeCode expected = BaseMeasurableVariant.GetMeasureUnitTypeCode(measureUnit ?? BaseMeasurable.GetDefaultMeasureUnit());

        // Act
        var actual = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitTypeCodes
    #region GetMeasureUnitTypeCodes()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCodes_ReturnsExpected()
    {
        // Arrange
        IEnumerable<MeasureUnitTypeCode> expected = BaseMeasurableVariant.GetMeasureUnitTypeCodes();

        // Act
        var actual = BaseMeasurable.GetMeasureUnitTypeCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region HasMeasureUnitTypeCode
    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(HasMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitTypeCode_Arg_MeasureUnitTypeCode_ReturnsExpected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, IBaseMeasurable baseMeasurable)
    {
        // Arrange
        // Actt
        var actual = baseMeasurable.HasMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode, Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(HasMeasureUnitTypeCodeMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitTypeCode_Args_MeasureUnitTypeCode_Enum_ReturnsExpected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, IBaseMeasurable baseMeasurable, Enum measureUnit)
    {
        // Arrange
        // Actt
        var actual = baseMeasurable.HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IsDefinedMeasureUnit
    #region IsDefinedMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(IsDefinedMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
    public void IsDefinedMeasureUnit_Arg_Enum_ReturnsExpected(bool expected, Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = BaseMeasurable.IsDefinedMeasureUnit(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnit
    #region ValidateMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_NullArg_Enum_ThrowsArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_InvalidArg_Enum_ThrowsArgumentNullException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_ValidArg_Enum_Returns()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomMeasureUnit();

        // Act
        bool returns()
        {
            try
            {
                BaseMeasurable.ValidateMeasureUnit(measureUnit);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Assert
        Assert.IsTrue(returns());
    }
    #endregion

    #region ValidateMeasureUnit(Enum, MeasureUnitTypeCode?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(ValidateMeasureUnitNullArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_NullArg_Enum_Arg_NblMeasureUnitTypeCode_ThrowsArgumentNullException(MeasureUnitTypeCode? measureUnitTypeCode, Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(ValidateMeasureUnitInvalidEnumArgArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_InvalidArg_Enum_Arg_NblMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException(MeasureUnitTypeCode? measureUnitTypeCode, Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_Arg_Enum_InvalidArg_NblMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomMeasureUnit();
        MeasureUnitTypeCode? measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_ValidArgs_Enum_NblMeasureUnitTypeCode_Returns(MeasureUnitTypeCode? measureUnitTypeCode, Enum measureUnit)
    {
        // Arrange
        // Act
        bool returns()
        {
            try
            {
                BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Assert
        Assert.IsTrue(returns());
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    #region ObjectArrayLists
    private static IEnumerable<object[]> BaseMeasurableEqualsObjectArgsArrayList()
    {
        return DynamicDataSources.BaseMeasurableEqualsObjectArgsArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList();

    }

    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> HasMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.HasMeasureUnitTypeCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetNullableEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetNullableEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetNullableMeasureUnitTypeCodeArgArrayList()
    {
        return DynamicDataSources.GetNullableMeasureUnitTypeCodeArgArrayList();
    }

    private static IEnumerable<object[]> HasMeasureUnitTypeCodeMeasureUnitArgsArrayList()
    {
        return DynamicDataSources.HasMeasureUnitTypeCodeMeasureUnitArgsArrayList();
    }

    private static IEnumerable<object[]> IsDefinedMeasureUnitArgsArrayList()
    {
        return DynamicDataSources.IsDefinedMeasureUnitArgsArrayList();
    }

    private static IEnumerable<object[]> ValidateMeasureUnitInvalidEnumArgArrayList()
    {
        return DynamicDataSources.ValidateMeasureUnitInvalidEnumArgArrayList();
    }

    private static IEnumerable<object[]> ValidateMeasureUnitNullArgsArrayList()
    {
        return DynamicDataSources.ValidateMeasureUnitNullArgsArrayList();
    }

    private static IEnumerable<object[]> ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList()
    {
        return DynamicDataSources.ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList();
    }

    #endregion
    #endregion
}