namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes
{
    public interface IArea : IMeasure, IMeasure<IArea, double, AreaUnit>, ISpreadMeasure
    {
    }
}

        //IArea GetArea(IBaseMeasure baseMeasure);
        //IArea GetArea(IArea? other = null);
        //IArea GetArea(double quantity, AreaUnit areaUnit);
        //IArea GetArea(ValueType quantity, string name);
        //IArea GetArea(ValueType quantity, IMeasurement? measurement = null);
