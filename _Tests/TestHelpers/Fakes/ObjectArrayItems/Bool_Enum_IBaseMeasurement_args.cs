namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.ObjectArrayItems;

public record Bool_Enum_IBaseMeasurement_args(bool IsTrue, Enum MeasureUnit, IBaseMeasurement BaseMeasurement) : Bool_Enum_args(IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit, BaseMeasurement];
}
