namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.Returns;

public class BaseMeasureReturn : CommonBaseReturn
{
    public ValueType GetBaseQuantity {  get; set; }
    public IBaseMeasurement GetBaseMeasurement { get; set; }
    public LimitMode? GetLimitMode { get; set; }
}
