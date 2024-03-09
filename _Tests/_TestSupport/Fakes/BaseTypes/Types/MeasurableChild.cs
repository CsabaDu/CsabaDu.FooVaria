namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region TestHelpers
    internal MeasurableReturns Returns { private get; set; } = new();
    #endregion

    public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Returns.GetFactory;
}
