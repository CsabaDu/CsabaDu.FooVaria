namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface ISpreadMeasure : IQuantity<double>, IMeasureUnit
{
    ISpreadMeasure GetSpreadMeasure();

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, [DisallowNull] string paramName);
}
