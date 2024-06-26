﻿
namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Types.Implementations;

public abstract class SimpleShape : Shape, ISimpleShape
{
    #region Constructors
    protected SimpleShape(ISimpleShape other) : base(other, nameof(other))
    {
    }

    protected SimpleShape(ISimpleShapeFactory factory) : base(factory, nameof(factory))
    {
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract IExtent? this[ShapeExtentCode shapeExtentCode] { get; }
    #endregion
    #endregion

    #region Public methods
    #region Static methods
    public static IExtent GetRectangularShapeDiagonal<T>(T simpleShape, ExtentUnit extentUnit = default)
        where T : class, ISimpleShape, IRectangularShape
    {
        ValidateMeasureUnitByDefinition(extentUnit, nameof(extentUnit));

        IEnumerable<ShapeExtentCode> shapeExtentCodes = simpleShape.GetShapeExtentCodes();
        int i = 0;
        decimal quantitySquares = getDefaultQuantitySquare();

        for (i = 1; i < shapeExtentCodes.Count(); i++)
        {
            quantitySquares += getDefaultQuantitySquare();
        }

        double quantity = decimal.ToDouble(quantitySquares);
        quantity = Math.Sqrt(quantity);
        if (!IsDefaultMeasureUnit(extentUnit))
        {
            quantity /= decimal.ToDouble(GetExchangeRate(extentUnit, nameof(extentUnit)));
        }
        IExtent edge = getShapeExtent();

        return edge.GetMeasure(extentUnit, quantity);

        #region Local methods
        IExtent getShapeExtent()
        {
            return simpleShape.GetShapeExtent(shapeExtentCodes.ElementAt(i));
        }

        decimal getDefaultQuantitySquare()
        {
            IExtent shapeExtent = getShapeExtent();

            return GetDefaultQuantitySquare(shapeExtent);
        }
        #endregion
    }

    #region Abstract methods
    public abstract IExtent GetDiagonal(ExtentUnit extentUnit);
    #endregion
    #endregion

    #region Override methods
    #region Sealed methods
    public override sealed int CompareTo(IShape? other)
    {
        if (other is null) return 1;

        const string paramName = nameof(other);
        ISimpleShape simpleShape = GetSimpleShape(other, paramName);

        ValidateMeasureUnitCode(simpleShape.GetMeasureUnitCode(), paramName);

        return Compare(this, simpleShape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IShape? other)
    {
        return other is ISimpleShape simpleShape
            && base.Equals(simpleShape);
    }

    public override sealed bool? FitsIn(IShape? other, LimitMode? limitMode)
    {
        if (other is null) return null;

        if (other is not ISimpleShape simpleShape)
        {
            simpleShape = (other.GetBaseShape() as ISimpleShape)!;
        }

        if (simpleShape?.IsExchangeableTo(GetMeasureUnitCode()) != true) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (simpleShape!.GetShapeComponentCount() != GetShapeComponentCount())
        {
            simpleShape = (ISimpleShape)(simpleShape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, simpleShape)?.FitsIn(limitMode);
    }

    public override ISimpleShape GetBaseShape()
    {
        return this;
    }

    public override sealed IEnumerable<IShapeComponent> GetShapeComponents()
    {
        return GetShapeExtents();
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        object converted = NullChecked(quantity, paramName)
            .ToQuantity(TypeCode.Decimal)
            ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

        if ((decimal)converted > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IBulkSpreadFactory GetBulkSpreadFactory();
    public abstract ITangentShapeFactory GetTangentShapeFactory();
    #endregion

    public IExtent GetDiagonal()
    {
        return GetDiagonal(default);
    }

    public IEnumerable<IExtent> GetDimensions()
    {
        return this is ICircularShape circularShape ?
            GetShapeComponents(circularShape.GetTangentShape(SideCode.Outer))!
            : GetShapeExtents();
    }

    public ISimpleShape GetSimpleShape(ExtentUnit extentUnit)
    {
        return ExchangeTo(extentUnit) ?? throw InvalidMeasureUnitEnumArgumentException(extentUnit, nameof(extentUnit));
    }

    public ISimpleShape GetSimpleShape(params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof (shapeExtents));

        return (ISimpleShape)GetShape(shapeExtents)!;
    }

    public IEnumerable<IExtent> GetShapeComponents(IShape shape)
    {
        ISimpleShape simpleShape = GetSimpleShape(shape, nameof(shape));

        return simpleShape.GetShapeExtents();
    }

    public IExtent GetShapeExtent(ShapeExtentCode shapeExtentCode)
    {
        return this[shapeExtentCode] ?? throw InvalidShapeExtentCodeEnumArgumentException(shapeExtentCode);
    }

    public IEnumerable<IExtent> GetShapeExtents()
    {
        foreach (ShapeExtentCode item in GetShapeExtentCodes())
        {
            yield return this[item]!;
        }
    }

    public IEnumerable<ShapeExtentCode> GetShapeExtentCodes()
    {
        return Enum.GetValues<ShapeExtentCode>().Where(x => this[x] is not null);
    }

    public IEnumerable<IExtent> GetSortedDimensions()
    {
        return GetDimensions().OrderBy(x => x);
    }

    public bool IsValidShapeExtentCode(ShapeExtentCode shapeExtentCode)
    {
        return GetShapeExtentCodes().Contains(shapeExtentCode);
    }

    public bool TryGetShapeExtentCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentCode? shapeExtentCode)
    {
        shapeExtentCode = null;

        if (shapeExtent is not null || !GetShapeExtents().Contains(shapeExtent)) return false;

        shapeExtentCode = GetShapeExtentCodes().FirstOrDefault(x => this[x] == shapeExtent);

        return true;
    }

    public void ValidateShapeExtent(IExtent? shapeExtent, string paramName)
    {
        decimal quantity =  NullChecked(shapeExtent, paramName).GetDecimalQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    public void ValidateShapeExtentCount(int count, string paramName)
    {
        if (count == GetShapeComponentCount()) return;

        throw QuantityArgumentOutOfRangeException(paramName, count);
    }

    public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
    {
        int count = NullChecked(shapeExtents, paramName).Count();

        ValidateShapeExtentCount(count, paramName);

        foreach (IExtent item in shapeExtents)
        {
            ValidateShapeExtent(item, paramName);
        }
    }
    #endregion

    #region Protected methods
    protected ISpreadMeasure GetSpreadMeasure(IExtent[] shapeExtents)
    {
        return GetBulkSpreadFactory().Create(shapeExtents).GetSpreadMeasure();
    }
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(SimpleShape simpleShape, ISimpleShape? other)
    {
        if (other is null) return null;

        int? comparison = 0;

        foreach (ShapeExtentCode item in simpleShape.GetShapeExtentCodes())
        {
            if (!comparison.HasValue) return null;
            
            int itemComparison = simpleShape[item]!.CompareTo(other[item]);
            comparison = comparison.Value.CompareToComparison(itemComparison);
        }

        return comparison;
    }

    public ISimpleShape? ExchangeTo(ExtentUnit extentUnit)
    {
        if (!IsValidMeasureUnit(extentUnit)) return null;

        IEnumerable<IExtent> shapeExtents = GetShapeExtents();
        shapeExtents = shapeExtents.Select(x => x.ExchangeTo(extentUnit)!);

        return (ISimpleShape?)GetShape(shapeExtents.ToArray());
    }

    private static ISimpleShape GetSimpleShape(IShape shape, string paramName)
    {
        if (NullChecked(shape, paramName).GetBaseShape() is ISimpleShape simpleShape) return simpleShape;

        throw ArgumentTypeOutOfRangeException(paramName, shape);
    }

    public bool Equals(IShapeComponent? x, IShapeComponent? y)
    {
        return Equals<IShapeComponent>(x, y);
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return GetHashCode<IShapeComponent>(shapeComponent);
    }
    #endregion
    #endregion
}
