using CsabaDu.FooVaria.Common.Behaviors;
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
        Factory = new FactoryImplementation();
        BaseMeasurable = new BaseMeasurableChild(Factory, RandomParams.GetRandomMeasureUnitTypeCode());
        BaseMeasurableVariant = new(RandomParams);
    }
    #endregion

    #region Private fields
    private static DynamicDataSources DynamicDataSources;
    private RandomParams RandomParams;
    private IFactory Factory;
    private IBaseMeasurable BaseMeasurable;
    private BaseMeasurableVariant BaseMeasurableVariant;
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurable(IFactory, MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_IFactory_Arg_MeasureunitTypeCode_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = null;
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_IFactory_InvalidArg_MeasureunitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArgs_IFactory_MeasureunitTypeCode_CreatesInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new BaseMeasurableChild(factory, expected_MeasureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_IFactory_Arg_Enum_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = null;
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_IFactory_NullArg_Enum_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void BaseMeasurable_ValidArg_IFactory_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        IFactory factory = new FactoryImplementation();

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArgs_IFactory_Enum_CreatesInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(expected_MeasureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_IFactory_Arg_IBaseMeasurable_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = null;
        IBaseMeasurable baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArg_IFactory_NullArg_IBaseMeasurable_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        IBaseMeasurable baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.baseMeasurable, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_ValidArgs_IFactory_IBaseMeasurable_CreatesInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable expected = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, expected);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region BaseMeasurable(IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_NullArg_BaseMeasurable_ThrowsArgumentNullException()
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
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable expected = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(expected);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    [TestMethod, TestCategory("UnitTest")]

    #endregion


    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion
}

//    #region Equals
//    #region Equals(object?)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(BaseMeasurableEqualsObjectArgsArrayList), DynamicDataSourceType.Method)]
//    public void Equals_Arg_object_ReturnsExpected(bool expected, object obj, MeasureUnitTypeCode measureUnit)
//    {
//        // Arrange
//        IBaseMeasurable other = new BaseMeasurableChild(measureUnit);

//        // Act
//        var actual = other.Equals(obj);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetDefaultMeasureUnit
//    #region GetDefaultMeasureUnit()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetDefaultMeasureUnit_ReturnsExpected()
//    {
//        // Arrange
//        Enum expected = BaseMeasurableVariant.GetDefaultMeasureUnit(out MeasureUnitTypeCode measureUnit);
//        IBaseMeasurable other = new BaseMeasurableChild(measureUnit);

//        // Act
//        var actual = other.GetDefaultMeasureUnit();

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion

//    #region GetDefaultMeasureUnit(MeasureUnitTypeCode?)
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetDefaultMeasureUnit_InvalidArg_MeasureUnitTpeCode_ThrowsInvalidEnumArgumentException()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//        void attempt() => _ = BaseMeasurable.GetDefaultMeasureUnit(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    //[DynamicData(nameof(GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
//    public void GetDefaultMeasureUnit_VaLidArg_MeasureUnitTpeCode_ReturnsExpected()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = RandomParams.GetRandomMeasureUnitTypeCode();
//        Enum expected = BaseMeasurableVariant.GetDefaultMeasureUnit(measureUnit);

//        // Act
//        var actual = BaseMeasurable.GetDefaultMeasureUnit(measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetDefaultNames
//    #region GetDefaultNames()
//    //[TestMethod, TestCategory("UnitTest")]
//    //public void GetDefaultNames_ReturnsExpected()
//    //{
//    //    // Arrange
//    //    IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(null);

//    //    // Act
//    //    var actual = BaseMeasurable.GetDefaultNames(null);

//    //    // Assert
//    //    Assert.IsTrue(expected.SequenceEqual(actual));
//    //}
//    #endregion

//    #region GetDefaultNames(MeasureUnitTypeCode?)
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetDefaultNames_InvalidArg_NullableMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//       void attempt() => _ = BaseMeasurable.GetDefaultNames(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    //[DynamicData(nameof(GetNullableMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
//    public void GetDefaultNames_ValidArg_NullableMeasureUnitTypeCode_ReturnsExpected()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = RandomParams.GetRandomMeasureUnitTypeCode();
//        IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(measureUnit);

//        // Act
//        var actual = BaseMeasurable.GetDefaultNames(measureUnit);

//        // Assert
//        Assert.IsTrue(expected.SequenceEqual(actual));
//    }
//    #endregion
//    #endregion

//    #region GetHashCode
//    #region GetHashCode()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetHashCode_ReturnsExpected()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = RandomParams.GetRandomMeasureUnitTypeCode();
//        IBaseMeasurable other = new BaseMeasurableChild(measureUnit);
//        int expected = measureUnit.GetHashCode();

//        // Act
//        var actual = other.GetHashCode();

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetMeasureUnitType
//    #region GetMeasureUnitType()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetMeasureUnitType_ReturnsExpected()
//    {
//        // Arrange
//        Type expected = RandomParams.GetRandomMeasureUnitType(out MeasureUnitTypeCode measureUnit);
//        IBaseMeasurable other = new BaseMeasurableChild(measureUnit);

//        // Act
//        var actual = other.GetMeasureUnitType();

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion

//    #region GetMeasureUnitType(MeasureUnitTypeCode?)
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetMeasureUnitType_InvalidArg_NullableMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
//    {
//        // Arrange
//        MeasureUnitTypeCode measureUnit = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//        void attempt() => _ = BaseMeasurable.GetMeasureUnitType(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetNullableMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
//    public void GetMeasureUnitType_ValidArg_NullableMeasureUnitTypeCode_ReturnsExpected(MeasureUnitTypeCode? measureUnit)
//    {
//        // Arrange
//        Type expected = BaseMeasurableVariant.GetMeasureUnitType(measureUnit ?? BaseMeasurable.MeasureUnitTypeCode);

//        // Act
//        var actual = BaseMeasurable.GetMeasureUnitType(measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetMeasureUnitTypeCode
//    #region GetMeasureUnitTypeCode()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetMeasureUnitTypeCode_ReturnsExpected()
//    {
//        // Arrange
//        IBaseMeasurable other = new BaseMeasurableChild(RandomParams.GetRandomMeasureUnit());
//        MeasureUnitTypeCode expected = other.MeasureUnitTypeCode;

//        // Act
//        var actual = other.GetMeasureUnitTypeCode();

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion

//    #region GetMeasureUnitTypeCode(Enum?)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//    public void GetMeasureUnitTypeCode_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        void attempt() => _ = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetNullableEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//    public void GGetMeasureUnitTypeCode_ValidArg_Enum_ReturnsExpected(Enum measureUnit)
//    {
//        // Arrange
//        MeasureUnitTypeCode expected = BaseMeasurableVariant.GetMeasureUnitTypeCode(measureUnit ?? BaseMeasurable.GetDefaultMeasureUnit());

//        // Act
//        var actual = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetMeasureUnitTypeCodes
//    #region GetMeasureUnitTypeCodes()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetMeasureUnitTypeCodes_ReturnsExpected()
//    {
//        // Arrange
//        IEnumerable<MeasureUnitTypeCode> expected = BaseMeasurableVariant.GetMeasureUnitTypeCodes();

//        // Act
//        var actual = BaseMeasurable.GetMeasureUnitTypeCodes();

//        // Assert
//        Assert.IsTrue(expected.SequenceEqual(actual));
//    }
//    #endregion
//    #endregion

//    #region HasMeasureUnitTypeCode
//    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(HasMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
//    public void HasMeasureUnitTypeCode_Arg_MeasureUnitTypeCode_ReturnsExpected(bool expected, MeasureUnitTypeCode measureUnit, IBaseMeasurable other)
//    {
//        // Arrange
//        // Actt
//        var actual = other.HasMeasureUnitTypeCode(measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion

//    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode, Enum?)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(HasMeasureUnitTypeCodeMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
//    public void HasMeasureUnitTypeCode_Args_MeasureUnitTypeCode_Enum_ReturnsExpected(bool expected, MeasureUnitTypeCode measureUnit, IBaseMeasurable other, Enum measureUnit)
//    {
//        // Arrange
//        // Actt
//        var actual = other.HasMeasureUnitTypeCode(measureUnit, measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region IsDefinedMeasureUnit
//    #region IsDefinedMeasureUnit(Enum)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(IsDefinedMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
//    public void IsDefinedMeasureUnit_Arg_Enum_ReturnsExpected(bool expected, Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        var actual = BaseMeasurable.IsDefinedMeasureUnit(measureUnit);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region ValidateMeasureUnit
//    #region ValidateMeasureUnit(Enum)
//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnit_NullArg_Enum_ThrowsArgumentNullException()
//    {
//        // Arrange
//        Enum measureUnit = null;

//        // Act
//        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, null);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnit_InvalidArg_Enum_ThrowsArgumentNullException(Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, null);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnit_ValidArg_Enum_Returns()
//    {
//        // Arrange
//        Enum measureUnit = RandomParams.GetRandomMeasureUnit();

//        // Act
//        bool returns()
//        {
//            try
//            {
//                BaseMeasurable.ValidateMeasureUnit(measureUnit, null);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        // Assert
//        Assert.IsTrue(returns());
//    }
//    #endregion

//    #region ValidateMeasureUnit(Enum, MeasureUnitTypeCode?)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(ValidateMeasureUnitNullArgsArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnit_NullArg_Enum_Arg_NblMeasureUnitTypeCode_ThrowsArgumentNullException(MeasureUnitTypeCode? measureUnit, Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(ValidateMeasureUnitInvalidEnumArgArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnit_InvalidArg_Enum_Arg_NblMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException(MeasureUnitTypeCode? measureUnit, Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnit_Arg_Enum_InvalidArg_NblMeasureUnitTypeCode_ThrowsInvalidEnumArgumentException()
//    {
//        // Arrange
//        Enum measureUnit = RandomParams.GetRandomMeasureUnit();
//        MeasureUnitTypeCode? measureUnit = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnit_ValidArgs_Enum_NblMeasureUnitTypeCode_Returns(MeasureUnitTypeCode? measureUnit, Enum measureUnit)
//    {
//        // Arrange
//        // Act
//        bool returns()
//        {
//            try
//            {
//                BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnit);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        // Assert
//        Assert.IsTrue(returns());
//    }
//    #endregion
//    #endregion
//    #endregion

//    #region Private methods
//    #region ObjectArrayLists
//    private static IEnumerable<object[]> BaseMeasurableEqualsObjectArgsArrayList()
//    {
//        return DynamicDataSources.BaseMeasurableEqualsObjectArgsArrayList();
//    }

//    //private static IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList()
//    //{
//    //    return DynamicDataSources.GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList();
//    //}


//    private static IEnumerable<object[]> HasMeasureUnitTypeCodeArgsArrayList()
//    {
//        return DynamicDataSources.HasMeasureUnitTypeCodeArgsArrayList();
//    }

//    private static IEnumerable<object[]> GetNullableEnumMeasureUnitArgArrayList()
//    {
//        return DynamicDataSources.GetNullableEnumMeasureUnitArgArrayList();
//    }

//    private static IEnumerable<object[]> GetNullableMeasureUnitTypeCodeArgArrayList()
//    {
//        return DynamicDataSources.GetNullableMeasureUnitTypeCodeArgArrayList();
//    }

//    private static IEnumerable<object[]> HasMeasureUnitTypeCodeMeasureUnitArgsArrayList()
//    {
//        return DynamicDataSources.HasMeasureUnitTypeCodeMeasureUnitArgsArrayList();
//    }

//    private static IEnumerable<object[]> IsDefinedMeasureUnitArgsArrayList()
//    {
//        return DynamicDataSources.IsDefinedMeasureUnitArgsArrayList();
//    }

//    private static IEnumerable<object[]> ValidateMeasureUnitInvalidEnumArgArrayList()
//    {
//        return DynamicDataSources.ValidateMeasureUnitInvalidEnumArgArrayList();
//    }

//    private static IEnumerable<object[]> ValidateMeasureUnitNullArgsArrayList()
//    {
//        return DynamicDataSources.ValidateMeasureUnitNullArgsArrayList();
//    }

//    private static IEnumerable<object[]> ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList()
//    {
//        return DynamicDataSources.ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList();
//    }

//    #endregion
//    #endregion
//}