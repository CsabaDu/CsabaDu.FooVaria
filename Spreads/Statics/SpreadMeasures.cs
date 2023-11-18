using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Spreads.Statics;

public static class SpreadMeasures
{
    #region Public constants
    public const int CircleShapeExtentCount = 1;
    public const int RectangleShapeExtentCount = 2;
    public const int CylinderShapeExtentCount = 2;
    public const int CuboidShapeExtentCount = 3;
    #endregion

    #region Public methods
    public static IArea GetArea(IMeasureFactory factory, params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(MeasureUnitTypeCode.AreaUnit, nameof(shapeExtents), shapeExtents);

        return shapeExtents.Length switch
        {
            1 => GetCircleArea(factory, shapeExtents[0]),
            2 => GetRectangleArea(factory, shapeExtents[0], shapeExtents[1]),

            _ => throw new InvalidOperationException(null),
        };
    }

    public static IArea GetCircleArea(IMeasureFactory factory, IExtent radius)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetCircleAreaDefaultQuantity(radius);

        return GetArea(factory, quantity);
    }

    public static IVolume GetCuboidVolume(IMeasureFactory factory, IExtent length, IExtent width, IExtent height)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

        return GetVolume(factory, quantity, height);
    }

    public static IVolume GetCylinderVolume(IMeasureFactory factory, IExtent radius, IExtent height)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetCircleAreaDefaultQuantity(radius);

        return GetVolume(factory, quantity, height);
    }

    public static IArea GetRectangleArea(IMeasureFactory factory, IExtent length, IExtent width)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

        return GetArea(factory, quantity);
    }

    public static IMeasure GetSpreadMeasure(IMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IExtent[] shapeExtents)
    {
        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.AreaUnit => GetArea(factory, shapeExtents),
            MeasureUnitTypeCode.VolumeUnit => GetVolume(factory, shapeExtents),

            _ => throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode),
        };
    }

    public static IMeasure GetValidSpreadMeasure(ISpreadMeasure? spreadMeasure)
    {
        string paramName = nameof(spreadMeasure);
        spreadMeasure = NullChecked(spreadMeasure, nameof(spreadMeasure)).GetSpreadMeasure();

        if (spreadMeasure is not IMeasure measure) throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);

        double quantity = (double)measure.Quantity;
            
        if (quantity > 0) return measure;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    public static IMeasure GetValidSpreadMeasure(MeasureUnitTypeCode measureUnitTypeCode, ISpreadMeasure? spreadMeasure)
    {
        string paramName = nameof(spreadMeasure);

        MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

        IMeasure measure = GetValidSpreadMeasure(spreadMeasure);

        if (measure.IsExchangeableTo(measureUnitTypeCode)) return measure;
        
        throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);
    }

    public static IVolume GetVolume(IMeasureFactory factory, params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(MeasureUnitTypeCode.VolumeUnit, nameof(shapeExtents), shapeExtents);

        return shapeExtents.Length switch
        {
            2 => GetCylinderVolume(factory, shapeExtents[0], shapeExtents[1]),
            3 => GetCuboidVolume(factory, shapeExtents[0], shapeExtents[1], shapeExtents[2]),

            _ => throw new InvalidOperationException(null),
        };
    }

    public static void ValidateShapeExtents(MeasureUnitTypeCode measureUnitTypeCode, string paramName, params IExtent[] shapeExtents)
    {
        int count = shapeExtents?.Length ?? 0;

        switch (measureUnitTypeCode)
        {
            case MeasureUnitTypeCode.AreaUnit:
                validateShapeExtentsCount(CircleShapeExtentCount, RectangleShapeExtentCount);
                break;
            case MeasureUnitTypeCode.VolumeUnit:
                validateShapeExtentsCount(CylinderShapeExtentCount, CuboidShapeExtentCount);
                break;

            default:
                throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
        }

        foreach (IExtent item in shapeExtents!)
        {
            decimal quantity = item.GetDecimalQuantity();

            if (item.DefaultQuantity <= 0)
            {
                throw QuantityArgumentOutOfRangeException(paramName, quantity);
            }
        }

        #region Local methods
        void validateShapeExtentsCount(int minValue, int maxValue)
        {
            if (count < minValue || count > maxValue)
            {
                throw new ArgumentOutOfRangeException(paramName, count, null);
            }

        }
        #endregion
    }
    #endregion

    #region Private methods
    private static IArea GetArea(IMeasureFactory factory, decimal quantity)
    {
        return (IArea)factory.Create(quantity, default(AreaUnit));
    }

    private static decimal GetCircleAreaDefaultQuantity(IExtent radius)
    {
        decimal quantity = GetValidShapeExtentDefaultQuantity(radius, nameof(radius));
        quantity *= quantity;

        return quantity * Convert.ToDecimal(Math.PI);
    }

    private static decimal GetRectangleAreaDefaultQuantity(IExtent length, IExtent width)
    {
        decimal quantity = GetValidShapeExtentDefaultQuantity(length, nameof(length));

        return quantity * GetValidShapeExtentDefaultQuantity(width, nameof(width));
    }

    private static decimal GetValidShapeExtentDefaultQuantity(IExtent shapeExtent, string name)
    {
        decimal quantity = NullChecked(shapeExtent, name).DefaultQuantity;

        return quantity > 0 ?
            quantity
            : throw new ArgumentOutOfRangeException(name, quantity, null);
    }

    private static IVolume GetVolume(IMeasureFactory factory, decimal quantity, IExtent height)
    {
        quantity *= GetValidShapeExtentDefaultQuantity(height, nameof(height));

        return (IVolume)factory.Create(quantity, default(VolumeUnit));
    }
    #endregion
}