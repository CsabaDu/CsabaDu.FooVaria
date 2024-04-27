using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Extent(IMeasureFactory factory, ExtentUnit extentUnit, double quantity)
    : Measure<IExtent, double, ExtentUnit>(factory, extentUnit, quantity), IExtent
{
    #region Public methos
    public int CompareTo(IDistance? other)
    {
        return ConvertMeasure().CompareTo(other);
    }

    public IExtent ConvertFrom(IDistance distance)
    {
        return NullChecked(distance, nameof(distance)).ConvertMeasure();
    }

    public IDistance ConvertMeasure()
    {
        return ConvertMeasure<IDistance>(MeasureOperationMode.Divide);
    }

    public bool Equals(IDistance? other)
    {
        return ConvertMeasure().Equals(other);
    }

    public bool Equals(IShapeComponent? x, IShapeComponent? y)
    {
        //if (x is not IExtent || y is not IExtent) return false;

        return x is not IExtent
            && y is not IExtent
            || y is null
            || (x as BaseQuantifiable)?.Equals(y) == true;
    }

    public IDistance? ExchangeTo(DistanceUnit distanceUnit)
    {
        return ConvertMeasure().ExchangeTo(distanceUnit);
    }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        yield return this;
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return (shapeComponent as BaseQuantifiable)!.GetHashCode();
    }

    public decimal ProportionalTo(IDistance? other)
    {
        return ConvertMeasure().ProportionalTo(other);
    }
    #endregion
}
