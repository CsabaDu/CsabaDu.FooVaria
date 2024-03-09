namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IRootObject rootObject, string paramName) : BaseMeasurement(rootObject, paramName)
{
    #region TestHelpers
    internal Enum GetBaseMeasureUnit_returns { private get; set; }
    internal string GetName_returns { private get; set; }
    #endregion

    public override Enum GetBaseMeasureUnit() => GetBaseMeasureUnit_returns;

    public override IFactory GetFactory() => new BaseMeasurementFactoryObject();

    public override string GetName() => GetName_returns;
}
