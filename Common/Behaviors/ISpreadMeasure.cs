namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ISpreadMeasure : IQuantifiable<double>
{
    ISpreadMeasure GetSpreadMeasure();
    MeasureUnitTypeCode GetMeasureUnitTypeCode();

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
}
