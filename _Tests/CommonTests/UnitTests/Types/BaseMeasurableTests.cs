using CsabaDu.FooVaria.Tests.CommonTests.Fakes.Types;
using CsabaDu.FooVaria.Tests.TestSupport.Params;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurableTests
{
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
        IBaseMeasurable actual = new BaseMeasurableChild(measureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_NullMeasureUnitArg_Enum_ThrowsArgumentNullException()
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
    public void Ctor_NotMeasureUnitEnumg_measureUnit_ThrowsInvalidEnumArgumentException()
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
        MeasureUnitTypeCode measureUnitTypeCode = Common.Statics.MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);

        // Act
        IBaseMeasurable actual = new BaseMeasurableChild(measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion
    #endregion
}