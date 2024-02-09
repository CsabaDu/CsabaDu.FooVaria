namespace CsabaDu.FooVaria.SimpleShapes.Types.Implementations;

public abstract class SimpleShape : Shape, ISimpleShape
{
    #region Constructors
    protected SimpleShape(ISimpleShape other) : base(other)
    {
    }

    protected SimpleShape(ISimpleShapeFactory factory, IShape shape) : base(factory, shape)
    {
    }

    protected SimpleShape(ISimpleShapeFactory factory, MeasureUnitCode measureUnitCode, params IExtent[] shapeExtents) : base(factory, measureUnitCode, shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof(shapeExtents));
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract IExtent? this[SimpleShapeExtentCode simpleShapeExtentCode] { get; }
    #endregion
    #endregion

    #region Public methods
    public override void ValidateSimpleShapeComponent(IQuantifiable simpleShapeComponent, string paramName)
    {
        throw new NotImplementedException();
    }

    public override ISimpleShapeComponent? GetValidSimpleShapeComponent(IQuantifiable? simpleShapeComponent)
    {
        if (simpleShapeComponent == null) return null;

        throw new NotImplementedException();
    }

    public override sealed IShape? GetShape(params ISimpleShapeComponent[] shapeComponents)
    {
        return GetFactory().CreateShape(shapeComponents);
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
        ValidateShapeExtents(shapeExtents, nameof (shapeExtents));

        return (ISimpleShape)GetShape(shapeExtents)!;
    }

    public IEnumerable<IExtent>? GetShapeComponents(IShape shape)
    {
        if (shape is ISimpleShape simpleShape) return simpleShape.GetShapeExtents();

        if (shape is IComplexSimpleShape complexSimpleShape)
        {
            throw new NotImplementedException();
        }

        return null;
    }

    public IExtent GetSimpleShapeExtent(SimpleShapeExtentCode simpleShapeExtentCode)
    {
        return this[simpleShapeExtentCode] ?? throw InvalidSimpleShapeExtentCodeEnumArgumentException(simpleShapeExtentCode);
    }

    public IEnumerable<IExtent> GetShapeExtents()
    {
        foreach (SimpleShapeExtentCode item in GetSimpleShapeExtentCodes())
        {
            yield return this[item]!;
        }
    }

    public IEnumerable<SimpleShapeExtentCode> GetSimpleShapeExtentCodes()
    {
        return Enum.GetValues<SimpleShapeExtentCode>().Where(x => this[x] != null);
    }

    public IEnumerable<IExtent> GetSortedDimensions()
    {
        return GetDimensions().OrderBy(x => x);
    }

    public bool IsValidSimpleShapeExtentCode(SimpleShapeExtentCode simpleShapeExtentCode)
    {
        return GetSimpleShapeExtentCodes().Contains(simpleShapeExtentCode);
    }

    public bool TryGetSimpleShapeExtentCode(IExtent simpleShapeExtent, [NotNullWhen(true)] out SimpleShapeExtentCode? simpleShapeExtentCode)
    {
        simpleShapeExtentCode = null;

        if (simpleShapeExtent != null || !GetShapeExtents().Contains(simpleShapeExtent)) return false;

        simpleShapeExtentCode = GetSimpleShapeExtentCodes().FirstOrDefault(x => this[x] == simpleShapeExtent);

        return true;
    }

    public void ValidateSimpleShapeExtent(IExtent? simpleShapeExtent, string paramName)
    {
        decimal quantity =  NullChecked(simpleShapeExtent, paramName).GetDecimalQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    public void ValidateSimpleShapeExtentCount(int count, string paramName)
    {
        if (count == GetSimpleShapeComponentCount()) return;

        throw QuantityArgumentOutOfRangeException(paramName, count);
    }

    public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
    {
        int count = NullChecked(shapeExtents, paramName).Count();

        ValidateSimpleShapeExtentCount(count, paramName);

        foreach (IExtent item in shapeExtents)
        {
            ValidateSimpleShapeExtent(item, paramName);
        }
    }

    #region Override methods
    public override ISimpleShapeFactory GetFactory()
    {
        return (ISimpleShapeFactory)Factory;
    }

    #region Sealed methods
    public override sealed int CompareTo(IShape? other)
    {
        if (other == null) return 1;

        string paramName = nameof(other);

        if (other is not ISimpleShape simpleShape)
        {
            if (other is IComplexSimpleShape complexSimpleShape)
            {
                throw new NotImplementedException();
            }

            throw ArgumentTypeOutOfRangeException(paramName, other);
        }

        ValidateMeasureUnitCode(simpleShape.MeasureUnitCode, paramName);

        return Compare(this, simpleShape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IShape? other)
    {
        return other is ISimpleShape simpleShape
            && base.Equals(simpleShape);
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

        return GetSimpleShape(exchangedShapeExtents.ToArray());

        #region Local methods
        IEnumerable<IExtent> getExchangedShapeExtents()
        {
            foreach (IBaseMeasure item in GetShapeExtents())
            {
                IBaseMeasure? exchanged = item.ExchangeTo(extentUnit);

                if (exchanged is IExtent simpleShapeExtent)
                {
                    yield return simpleShapeExtent;
                }
            }
        }
        #endregion
    }

    public override sealed bool? FitsIn(IShape? other, LimitMode? limitMode)
    {
        if (other is not ISimpleShape simpleShape)
        {
            if (other is IComplexSimpleShape complexSimpleShape)
            {
                throw new NotImplementedException();
            }

            return null;
        }

        if (!simpleShape.IsExchangeableTo(MeasureUnitCode)) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (simpleShape.GetSimpleShapeComponentCount() != GetSimpleShapeComponentCount())
        {
            simpleShape = (ISimpleShape)(simpleShape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, simpleShape)?.FitsIn(limitMode);
    }

    public override sealed IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
    {
        return GetFactory().SpreadFactory.CreateBaseSpread(spreadMeasure);
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes();
    }

    public override sealed IEnumerable<ISimpleShapeComponent> GetShapeComponents()
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

    //public static IExtent GetDiagonal(ISimpleShape simpleShape, ExtentUnit extentUnit = default)
    //{
    //    return NullChecked(simpleShape, nameof(simpleShape)) switch
    //    {
    //        Circle circle => GetCircleDiagonal(circle, extentUnit),
    //        Cuboid cuboid => GetCuboidDiagonal(cuboid, extentUnit),
    //        Cylinder cylinder => GetCylinderDiagonal(cylinder, extentUnit),
    //        Rectangle rectangle => GetRectangleDiagonal(rectangle, extentUnit),

    //        _ => throw new InvalidOperationException(null)
    //    };
    //}
    public static IExtent GetRectangularShapeDiagonal<T>(T simpleShape, ExtentUnit extentUnit = default)
        where T : class, ISimpleShape, IRectangularShape
    {
        ValidateMeasureUnitByDefinition(extentUnit, nameof(extentUnit));

        IEnumerable<SimpleShapeExtentCode> simpleShapeExtentCodes = simpleShape.GetSimpleShapeExtentCodes();
        int i = 0;
        decimal quantitySquares = getDefaultQuantitySquare();

        for (i = 1; i < simpleShapeExtentCodes.Count(); i++)
        {
            quantitySquares += getDefaultQuantitySquare();
        }

        double quantity = decimal.ToDouble(quantitySquares);
        quantity = Math.Sqrt(quantity);
        if (!IsDefaultMeasureUnit(extentUnit))
        {
            quantity /= decimal.ToDouble(GetExchangeRate(extentUnit));
        }
        IExtent edge = getSimpleShapeExtent();

        return edge.GetMeasure(extentUnit, quantity);

        #region Local methods
        IExtent getSimpleShapeExtent()
        {
            return simpleShape.GetSimpleShapeExtent(simpleShapeExtentCodes.ElementAt(i));
        }

        decimal getDefaultQuantitySquare()
        {
            IExtent simpleShapeExtent = getSimpleShapeExtent();

            return GetDefaultQuantitySquare(simpleShapeExtent);
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
    private static int? Compare(ISimpleShape simpleShape, ISimpleShape? other)
    {
        if (other == null) return null;

        int comparison = 0;

        foreach (SimpleShapeExtentCode item in simpleShape.GetSimpleShapeExtentCodes())
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
