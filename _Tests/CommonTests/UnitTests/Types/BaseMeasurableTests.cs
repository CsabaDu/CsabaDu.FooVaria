using CsabaDu.FooVaria.Tests.CommonTests.Fakes.Types;
using CsabaDu.FooVaria.Tests.TestSupport;
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
        static void attempt() => _ = new BaseMeasurableChild(SampleParams.InvalidMeasureUnitTypeCode);

        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Ctor_ValidArg_MeasureunitTypeCode_CreatesInstance()
    {
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable actual = new BaseMeasurableChild(measureUnitTypeCode);

        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion
    #endregion
}