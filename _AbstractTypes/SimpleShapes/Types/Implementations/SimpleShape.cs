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
    public override sealed IShape? GetShape(params IShapeComponent[] shapeComponents)
    {
        ISimpleShapeFactory factory = (ISimpleShapeFactory)GetFactory();

        return factory.CreateShape(shapeComponents);
    }

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
        return (ISimpleShape?)ExchangeTo(extentUnit) ?? throw InvalidMeasureUnitEnumArgumentException(extentUnit, nameof(extentUnit));
    }

    public ISimpleShape GetSimpleShape(params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof(shapeExtents));

        return (ISimpleShape)GetShape(shapeExtents)!;
    }

    public IEnumerable<IExtent> GetShapeComponents(IShape shape)
    {
        if (NullChecked(shape, nameof(shape)) is not ISimpleShape simpleShape)
        {
            simpleShape = (ISimpleShape)shape.GetShape();
        }

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
        return Enum.GetValues<ShapeExtentCode>().Where(x => this[x] != null);
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

        if (shapeExtent != null || !GetShapeExtents().Contains(shapeExtent)) return false;

        shapeExtentCode = GetShapeExtentCodes().FirstOrDefault(x => this[x] == shapeExtent);

        return true;
    }

    public void ValidateShapeExtent(IExtent? shapeExtent, string paramName)
    {
        decimal quantity = NullChecked(shapeExtent, paramName).GetDecimalQuantity();

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

    #region Override methods
    #region Sealed methods
    public override sealed int CompareTo(IShape? other)
    {
        if (other == null) return 1;

        const string paramName = nameof(other);

        if (other is not ISimpleShape simpleShape)
        {
            simpleShape = (ISimpleShape)other.GetShape();
        }

        ValidateMeasureUnitCode(simpleShape.GetMeasureUnitCode(), paramName);

        return Compare(this, simpleShape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IShape? other)
    {
        return other is ISimpleShape simpleShape
            && base.Equals(simpleShape);
    }

    public override sealed ISpread? ExchangeTo(Enum? context)
    {
        if (context is not ExtentUnit extentUnit)
        {
            if (context is not MeasureUnitCode measureUnitCode
                || measureUnitCode != MeasureUnitCode.ExtentUnit)
            {
                return base.ExchangeTo(context);
            }

            extentUnit = (ExtentUnit)measureUnitCode.GetDefaultMeasureUnit()!;
        }

        IEnumerable<IExtent> exchangedShapeExtents = getExchangedShapeExtents();

        if (exchangedShapeExtents.Count() != GetShapeExtents().Count()) return null;

        return GetSimpleShape(exchangedShapeExtents.ToArray());

        #region Local methods
        IEnumerable<IExtent> getExchangedShapeExtents()
        {
            foreach (IBaseMeasure item in GetShapeExtents())
            {
                IQuantifiable? exchanged = item.ExchangeTo(extentUnit);

                if (exchanged is IExtent shapeExtent)
                {
                    yield return shapeExtent;
                }
            }
        }
        #endregion
    }

    public override sealed bool? FitsIn(IShape? other, LimitMode? limitMode)
    {
        if (other == null) return null;

        if (other is not ISimpleShape simpleShape)
        {
            simpleShape = (ISimpleShape)other.GetShape();
        }

        if (!simpleShape.IsExchangeableTo(GetMeasureUnitCode())) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (simpleShape.GetShapeComponentCount() != GetShapeComponentCount())
        {
            simpleShape = (ISimpleShape)(simpleShape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, simpleShape)?.FitsIn(limitMode);
    }

    public override ISimpleShape GetShape()
    {
        return this;
    }

    public override sealed ISpread GetSpread(ISpreadMeasure spreadMeasure)
    {
        return GetBulkSpreadFactory().CreateSpread(spreadMeasure);
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

    #region Virtual methods
    public abstract IBulkSpreadFactory GetBulkSpreadFactory();

    public abstract ITangentShapeFactory GetTangentShapeFactory();
    #endregion

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
        if (other == null) return null;

        int comparison = 0;

        foreach (ShapeExtentCode item in simpleShape.GetShapeExtentCodes())
        {
            int recentComparison = simpleShape[item]!.CompareTo(other[item]);

            if (recentComparison != 0)
            {
                if (comparison == 0)
                {
                    comparison = recentComparison;
                }

                if (comparison != recentComparison)
                {
                    return null;
                }
            }
        }

        return comparison;
    }
    #endregion
    #endregion
}
