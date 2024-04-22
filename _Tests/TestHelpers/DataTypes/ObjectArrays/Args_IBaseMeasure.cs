namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record IBaseMeasure_args(string Case, IBaseMeasure BaseMeasure) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [Case, BaseMeasure];
    }
}
