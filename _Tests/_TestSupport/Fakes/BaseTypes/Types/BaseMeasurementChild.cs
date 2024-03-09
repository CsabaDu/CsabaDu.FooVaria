namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IRootObject rootObject, string paramName) : BaseMeasurement(rootObject, paramName)
{
    #region TestHelpers
    internal BaseMeasurementReturns Returns { private get; set; } = new();
    #endregion

    public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Returns.GetFactory;

    public override string GetName() => Returns.GetName;
}
