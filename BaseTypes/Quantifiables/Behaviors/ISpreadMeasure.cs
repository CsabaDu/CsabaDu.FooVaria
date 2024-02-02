namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface ISpreadMeasure : IQuantity<double>
{
    ISpreadMeasure GetSpreadMeasure();
    MeasureUnitCode GetMeasureUnitCode();

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
}
