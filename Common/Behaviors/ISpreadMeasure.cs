namespace CsabaDu.FooVaria.Common.Behaviors;

public interface ISpreadMeasure : IQuantifiable
{
    ISpreadMeasure GetSpreadMeasure();
    MeasureUnitTypeCode GetMeasureUnitTypeCode();

    void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);
}
