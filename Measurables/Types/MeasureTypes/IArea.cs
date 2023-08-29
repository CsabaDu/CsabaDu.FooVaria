namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

internal interface IArea : IMeasure, ISpreadMeasure
{
    IArea GetArea(double quantity, AreaUnit areaUnit);
    IArea GetArea(ValueType quantity, string name);
    IArea GetArea(ValueType quantity, IMeasurement measurement);
    IArea GetArea(IBaseMeasure baseMeasure);
    IArea GetArea(IArea? other = null);
}
