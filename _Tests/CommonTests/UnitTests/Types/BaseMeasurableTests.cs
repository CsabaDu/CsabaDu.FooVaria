using CsabaDu.FooVaria.Tests.CommonTests.Fakes.Types;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurableTests
{
    #region Constructors
    #region BaseMeasurable(MeasureUnitTypeCode)
    [TestMethod]
    public void Ctor_InvalidArg_MeasureunitTypeCode_ThrowsInvalidEnumArgumentException()
    {
        MeasureUnitTypeCode invalidMeasureUnitTypeCode = (MeasureUnitTypeCode)Enum.GetNames(typeof(MeasureUnitTypeCode)).Length;
        void attempt() => _ = new BaseMeasurableChild(invalidMeasureUnitTypeCode);

        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        //Assert.AreEqual(CsabaDu.FooVaria.Tests.TestSupport.Statics.ParamNames.measureUnitTypeCode, ex.ParamName);
    }
    #endregion
    #endregion
}