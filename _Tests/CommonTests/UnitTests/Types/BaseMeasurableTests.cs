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
    public static void InitializeMeasurementTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }
    #endregion

    #region Private fields
    private static DynamicDataSources DynamicDataSources;
    #endregion

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
    public void Ctor_InvalidArg_Enum_ThrowsInvalidEnumArgumentException()
    {
        // Arrange
        Enum measureUnit = SampleParams.DefaultLimitMode;

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

    #region Private methods
    private static IEnumerable<object[]> GetBaseMeasurableEqualsObjectArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableEqualsObjectArgsArrayList();
    }
    #endregion
}