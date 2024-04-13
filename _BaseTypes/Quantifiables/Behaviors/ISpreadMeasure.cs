namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface ISpreadMeasure : IQuantity<double>, IMeasureUnit
{
    ISpreadMeasure GetSpreadMeasure();
    //MeasureUnitCode GetSpreadMeasureUnitCode();

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, [DisallowNull] string paramName);
}
