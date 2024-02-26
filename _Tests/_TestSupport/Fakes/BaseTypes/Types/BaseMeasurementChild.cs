namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class BaseMeasurementChild(IBaseMeasurementFactory factory) : BaseMeasurement(factory)
{
    #region TestHelpers
    internal Enum GetMeasureUnit_returns { private get; set; }
    internal string GetName_returns { private get; set; }
    #endregion

    public override Enum GetMeasureUnit()
    {
        return GetMeasureUnit_returns;
    }

    public override string GetName()
    {
        return GetName_returns;
    }
}
