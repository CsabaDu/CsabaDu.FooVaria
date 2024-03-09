namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region TestHelpers
    internal Enum GetBaseMeasureUnit_returns { private get; set; }
    #endregion

    public override Enum GetBaseMeasureUnit() => GetBaseMeasureUnit_returns;

    public override IFactory GetFactory() => new MeasurableFactoryObject();
}
