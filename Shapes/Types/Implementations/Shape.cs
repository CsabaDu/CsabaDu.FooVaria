namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

public abstract class Shape : BaseShape, IShape
{
    #region Constructors
    protected Shape(IShape other) : base(other)
    {
    }

    protected Shape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }

    protected Shape(IShapeFactory factory, MeasureUnitCode measureUnitCode, params IExtent[] shapeExtents) : base(factory, measureUnitCode, shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof(shapeExtents));
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract IExtent? this[ShapeExtentCode shapeExtentCode] { get; }
    #endregion
    #endregion

    #region Public methods
    public override void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName)
    {
        throw new NotImplementedException();
    }

    public override IShapeComponent? GetValidShapeComponent(IQuantifiable? shapeComponent)
    {
        if (shapeComponent == null) return null;

        throw new NotImplementedException();
    }

    public override sealed IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents)
    {
        return GetFactory().CreateBaseShape(shapeComponents);
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

    public IShape GetShape(ExtentUnit extentUnit)
    {
        return (IShape?)ExchangeTo(extentUnit) ?? throw InvalidMeasureUnitEnumArgumentException(extentUnit, nameof(extentUnit));
    }

    public IShape GetShape(params IExtent[] shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof (shapeExtents));

        return (IShape)GetBaseShape(shapeExtents)!;
    }

    public IEnumerable<IExtent>? GetShapeComponents(IBaseShape baseShape)
    {
        if (baseShape is IShape shape) return shape.GetShapeExtents();

        if (baseShape is IComplexShape complexShape)
        {
            throw new NotImplementedException();
        }

        return null;
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

    #region Override methods
    public override IShapeFactory GetFactory()
    {
        return (IShapeFactory)Factory;
    }

    #region Sealed methods
    public override sealed int CompareTo(IBaseShape? other)
    {
        if (other == null) return 1;

        string paramName = nameof(other);

        if (other is not IShape shape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            throw ArgumentTypeOutOfRangeException(paramName, other);
        }

        ValidateMeasureUnitCode(shape.MeasureUnitCode, paramName);

        return Compare(this, shape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IBaseShape? other)
    {
        return other is IShape shape
            && base.Equals(shape);
    }

    public override sealed IBaseSpread? ExchangeTo(Enum? context)
    {
        if (context is not ExtentUnit extentUnit)
        {
            if (context is not MeasureUnitCode measureUnitCode
                || measureUnitCode != MeasureUnitCode.ExtentUnit)
            {
                return base.ExchangeTo(context);
            }

            extentUnit = (ExtentUnit)GetDefaultMeasureUnit(measureUnitCode)!;
        }

        IEnumerable<IExtent> exchangedShapeExtents = getExchangedShapeExtents();

        if (exchangedShapeExtents.Count() != GetShapeExtents().Count()) return null;

        return GetShape(exchangedShapeExtents.ToArray());

        #region Local methods
        IEnumerable<IExtent> getExchangedShapeExtents()
        {
            foreach (IBaseMeasure item in GetShapeExtents())
            {
                IBaseMeasure? exchanged = item.ExchangeTo(extentUnit);

                if (exchanged is IExtent shapeExtent)
                {
                    yield return shapeExtent;
                }
            }
        }
        #endregion
    }

    public override sealed bool? FitsIn(IBaseShape? other, LimitMode? limitMode)
    {
        if (other is not IShape shape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            return null;
        }

        if (!shape.IsExchangeableTo(MeasureUnitCode)) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (shape.GetShapeComponentCount() != GetShapeComponentCount())
        {
            shape = (IShape)(shape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, shape)?.FitsIn(limitMode);
    }

    public override sealed IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
    {
        return GetFactory().SpreadFactory.CreateBaseSpread(spreadMeasure);
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes();
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
    public virtual ISpreadFactory GetSpreadFactory()
    {
        return GetFactory().SpreadFactory;
    }

    public virtual ITangentShapeFactory GetTangentShapeFactory()
    {
        return GetFactory().TangentShapeFactory;
    }
    #endregion

    #region Static methods

    //public static IExtent GetDiagonal(IShape shape, ExtentUnit extentUnit = default)
    //{
    //    return NullChecked(shape, nameof(shape)) switch
    //    {
    //        Circle circle => GetCircleDiagonal(circle, extentUnit),
    //        Cuboid cuboid => GetCuboidDiagonal(cuboid, extentUnit),
    //        Cylinder cylinder => GetCylinderDiagonal(cylinder, extentUnit),
    //        Rectangle rectangle => GetRectangleDiagonal(rectangle, extentUnit),

    //        _ => throw new InvalidOperationException(null)
    //    };
    //}
    public static IExtent GetRectangularShapeDiagonal<T>(T shape, ExtentUnit extentUnit = default)
        where T : class, IShape, IRectangularShape
    {
        ValidateMeasureUnitByDefinition(extentUnit, nameof(extentUnit));

        IEnumerable<ShapeExtentCode> shapeExtentCodes = shape.GetShapeExtentCodes();
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
            quantity /= decimal.ToDouble(GetExchangeRate(extentUnit));
        }
        IExtent edge = getShapeExtent();

        return edge.GetMeasure(extentUnit, quantity);

        #region Local methods
        IExtent getShapeExtent()
        {
            return shape.GetShapeExtent(shapeExtentCodes.ElementAt(i));
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
        return GetSpreadFactory().Create(shapeExtents).GetSpreadMeasure();
    }
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(IShape shape, IShape? other)
    {
        if (other == null) return null;

        int comparison = 0;

        foreach (ShapeExtentCode item in shape.GetShapeExtentCodes())
        {
            int recentComparison = shape[item]!.CompareTo(other[item]);

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
