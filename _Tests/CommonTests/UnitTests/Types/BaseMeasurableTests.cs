using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;
using CsabaDu.FooVaria.Tests.TestSupport.Params;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurableTests
{
    #region ClassInitialize
    [ClassInitialize]
    public static void InitializeBaseMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }
    #endregion

    #region TestInitialize
    [TestInitialize]
    public void InitializeBaseMeasureTests()
    {
        RandomParams = new();
    }
    #endregion

    #region Private fields
    private static DynamicDataSources DynamicDataSources;
    private RandomParams RandomParams;
    #endregion

    #region Properties
    private IBaseMeasurable BaseMeasurable => new BaseMeasurableChild(RandomParams.GetRandomMeasureUnitTypeCode());
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurable(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_InvalidArg_MeasureunitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.InvalidMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_MeasureunitTypeCode_CreatesInstance()
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
    public void Ctor_NullArg_Enum_ThrowsArgumentNullException()
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
    public void Ctor_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = new BaseMeasurableChild(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_Enum_CreatesInstance()
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
    public void Ctor_NullArg_IBaseMeasurable_ThrowsArgumentNullException()
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
    public void Ctor_ValidArg_IBaseMeasurable_CreatesInstance()
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
    [DynamicData(nameof(GetBaseMeasurableEqualsObjectArgsArrayList), DynamicDataSourceType.Method)]
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
    #region GetDefaultMeasureUnit(MeasureUnitTypeCode? = null)
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_ReturnsExpected()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitTypes().First(x => x.Name == measureUnitTypeCode.GetName());
        Enum expected = (Enum)Enum.ToObject(measureUnitType, default(int));

        // Act
        var actual = baseMeasurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_NullArg_NullableMeasureUnitTpeCode_ReturnsExpected()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitTypes().First(x => x.Name == measureUnitTypeCode.GetName());
        Enum expected = (Enum)Enum.ToObject(measureUnitType, default(int));

        // Act
        var actual = baseMeasurable.GetDefaultMeasureUnit(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_InvalidArg_NullableMeasureUnitTpeCode_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.InvalidMeasureUnitTypeCode;

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

    #region GetDefaultName
    #region GetDefaultName(Enum? = null)
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultName_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        string expected = Enum.GetName(measureUnit.GetType(), measureUnit);
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnit);

        // Act
        var actual = baseMeasurable.GetDefaultName();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultName_NullArg_Enum_ReturnsExpected()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomDefaultMeasureUnit();
        string expected = Enum.GetName(measureUnit.GetType(), measureUnit);
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnit);

        // Act
        var actual = baseMeasurable.GetDefaultName(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void GetDefaultName_InvalidArg_Enum_ThrowsInvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = BaseMeasurable.GetDefaultName(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultName_ValidArg_Enum_ReturnsExpeccted()
    {
        // Arrange
        Enum measureUnit = RandomParams.GetRandomMeasureUnit();
        string expected = Enum.GetName(measureUnit.GetType(), measureUnit);

        // Act
        var actual = BaseMeasurable.GetDefaultName(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
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
    #endregion

    #region Private methods
    private static IEnumerable<object[]> GetBaseMeasurableEqualsObjectArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableEqualsObjectArgsArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList();

    }

    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion
}