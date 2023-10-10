using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Statics;

public static class SpreadMeasures
{
    private const int CircleShapeExtentCount = 1;
    private const int RectangleShapeExtentCount = 2;
    private const int CylinderShapeExtentCount = 2;
    private const int CuboidShapeExtentCount = 3;

    public static IEnumerable<MeasureUnitTypeCode> SpreadMeasureUnitTypeCodes
    {
        get
        {
            yield return MeasureUnitTypeCode.AreaUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }
    }

    #region Public methods
    public static IArea GetArea(IMeasureFactory factory, params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(MeasureUnitTypeCode.AreaUnit, shapeExtents);

        return shapeExtents.Length switch
        {
            1 => GetCircleArea(factory, shapeExtents[0]),
            2 => GetRectangleArea(factory, shapeExtents[0], shapeExtents[1]),

            _ => throw new InvalidOperationException(null),
        };
    }

    public static IVolume GetVolume(IMeasureFactory factory, params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(MeasureUnitTypeCode.AreaUnit, shapeExtents);

        return shapeExtents.Length switch
        {
            2 => GetCylinderVolume(factory, shapeExtents[0], shapeExtents[1]),
            3 => GetCubicVolume(factory, shapeExtents[0], shapeExtents[1], shapeExtents[2]),

            _ => throw new InvalidOperationException(null),
        };
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

    public static void ValidateShapeExtents(MeasureUnitTypeCode measureUnitTypeCode, params IExtent[] shapeExtents)
    {
        string name = nameof(shapeExtents);
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
                throw new InvalidOperationException(null);
        }

        if (shapeExtents!.Any(x => x.DefaultQuantity <= 0)) throw new ArgumentOutOfRangeException(name);

        #region Local methods
        void validateShapeExtentsCount(int minValue, int maxValue)
        {
            if (count < minValue || count > maxValue)
            {
                throw new ArgumentOutOfRangeException(name, count, null);
            }
        }
        #endregion
    }

    public static IArea GetCircleArea(IMeasureFactory factory, IExtent radius)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetCircleAreaDefaultQuantity(radius);

        return GetArea(factory, quantity);
    }

    public static IVolume GetCylinderVolume(IMeasureFactory factory, IExtent radius, IExtent height)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetCircleAreaDefaultQuantity(radius);

        return GetVolume(factory, quantity, height);
    }

    public static IVolume GetCubicVolume(IMeasureFactory factory, IExtent length, IExtent width, IExtent height)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

        return GetVolume(factory, quantity, height);
    }

    public static IArea GetRectangleArea(IMeasureFactory factory, IExtent length, IExtent width)
    {
        _ = NullChecked(factory, nameof(factory));

        decimal quantity = GetRectangleAreaDefaultQuantity(length, width);

        return GetArea(factory, quantity);
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
