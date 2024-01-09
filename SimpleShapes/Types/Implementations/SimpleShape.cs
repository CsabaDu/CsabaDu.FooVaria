namespace CsabaDu.FooVaria.SimpleShapes.Types.Implementations;

internal abstract class SimpleShape : BaseShape, ISimpleShape
{
    #region Constructors
    private protected SimpleShape(ISimpleShape other) : base(other)
    {
    }

    private protected SimpleShape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }

    private protected SimpleShape(IShapeFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IExtent[] shapeExtents) : base(factory, measureUnitTypeCode, shapeExtents)
    {
        ValidateShapeExtents(shapeExtents, nameof(shapeExtents));
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }
    #endregion
    #endregion

    #region Public methods
    public override sealed IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents)
    {
        return GetFactory().CreateBaseShape(shapeComponents);
    }

    public IExtent GetDiagonal(ExtentUnit extentUnit)
    {
        return ShapeExtents.GetDiagonal(this, extentUnit);
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

        return (ISimpleShape)GetBaseShape(shapeExtents)!;
    }

    public IEnumerable<IExtent>? GetShapeComponents(IBaseShape baseShape)
    {
        if (baseShape is ISimpleShape simpleShape) return simpleShape.GetShapeExtents();

        if (baseShape is IComplexShape complexShape)
        {
            throw new NotImplementedException();
        }

        return null;
    }

    public IExtent GetShapeExtent(ShapeExtentTypeCode shapeExtentTypeCode)
    {
        return this[shapeExtentTypeCode] ?? throw InvalidShapeExtentTypeCodeEnumArgumentException(shapeExtentTypeCode);
    }

    public IEnumerable<IExtent> GetShapeExtents()
    {
        foreach (ShapeExtentTypeCode item in GetShapeExtentTypeCodes())
        {
            yield return this[item]!;
        }
    }

    public IEnumerable<ShapeExtentTypeCode> GetShapeExtentTypeCodes()
    {
        return Enum.GetValues<ShapeExtentTypeCode>().Where(x => this[x] != null);
    }

    public IEnumerable<IExtent> GetSortedDimensions()
    {
        return GetDimensions().OrderBy(x => x);
    }

    public bool IsValidShapeComponentOf(IBaseShape baseShape)
    {
        return IsValidShapeComponentOf(baseShape, this);
    }

    public bool IsValidShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode)
    {
        return GetShapeExtentTypeCodes().Contains(shapeExtentTypeCode);
    }

    public bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode)
    {
        _ = NullChecked(shapeExtent, nameof(shapeExtent));

        for (int i = 0; i < GetShapeComponentCount(); i++)
        {
            if (GetShapeExtents().ElementAt(i).Equals(shapeExtent))
            {
                shapeExtentTypeCode = GetShapeExtentTypeCodes().ElementAt(i);

                return true;
            }
        }

        shapeExtentTypeCode = null;

        return false;
    }

    public void ValidateShapeExtentCount(int count, string name)
    {
        if (count == GetShapeComponentCount()) return;

        throw QuantityArgumentOutOfRangeException(name, count);
    }

    public void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName)
    {
        int count = NullChecked(shapeExtents, paramName).Count();

        ValidateShapeExtentCount(count, paramName);

        foreach (IExtent item in shapeExtents)
        {
            ValidateShapeComponent(item, paramName);
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

        if (other is not ISimpleShape simpleShape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            throw ArgumentTypeOutOfRangeException(paramName, other);
        }

        ValidateMeasureUnitTypeCode(simpleShape.MeasureUnitTypeCode, paramName);

        return Compare(this, simpleShape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IBaseShape? other)
    {
        return other is ISimpleShape simpleShape
            && base.Equals(simpleShape);
    }

    public override sealed IBaseSpread? ExchangeTo(Enum measureUnit)
    {
        if (measureUnit == null) return null;

        return measureUnit switch
        {
            AreaUnit areaUnit => exchangeToAreaUnit(areaUnit),
            ExtentUnit extentUnit => exchangeToExtentUnit(extentUnit),
            VolumeUnit volumeUnit => exchangeToVolumeUnit(volumeUnit),

            _ => null,
        };

        #region Local methods
        ISimpleShape? exchangeToExtentUnit(ExtentUnit extentUnit)
        {
            IEnumerable<IExtent> exchangedShapeExtents = getExchangedShapeExtents(extentUnit);

            if (exchangedShapeExtents.Count() != GetShapeExtents().Count()) return null;

            return GetSimpleShape(exchangedShapeExtents.ToArray());
        }

        IEnumerable<IExtent> getExchangedShapeExtents(ExtentUnit extentUnit)
        {
            foreach (IExtent item in GetShapeExtents())
            {
                IRateComponent? exchanged = item.ExchangeTo(extentUnit);

                if (exchanged is IExtent extent)
                {
                    yield return extent;
                }
            }
        }

        IBulkSurface? exchangeToAreaUnit(AreaUnit areaUnit)
        {
            if (getExchangedSpreadMeasure(areaUnit) is not IArea area) return null;

            return (IBulkSurface)GetFactory().SpreadFactory.CreateBaseSpread(area);
        }

        IBulkBody? exchangeToVolumeUnit(VolumeUnit volumeUnit)
        {
            if (getExchangedSpreadMeasure(volumeUnit) is not IVolume volume) return null;

            return (IBulkBody)GetFactory().SpreadFactory.CreateBaseSpread(volume);
        }

        IRateComponent? getExchangedSpreadMeasure(Enum measureUnit)
        {
            IRateComponent spreadMeasure = (IRateComponent)GetSpreadMeasure();

            return spreadMeasure.ExchangeTo(measureUnit);
        }
        #endregion
    }

    public override sealed bool? FitsIn(IBaseShape? other, LimitMode? limitMode)
    {
        if (other is not ISimpleShape simpleShape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            return null;
        }

        if (!simpleShape.IsExchangeableTo(MeasureUnitTypeCode)) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (simpleShape.GetShapeComponentCount() != GetShapeComponentCount())
        {
            simpleShape = (simpleShape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, simpleShape)?.FitsIn(limitMode);
    }

    public override sealed IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
    {
        return GetFactory().SpreadFactory.CreateBaseSpread(spreadMeasure);
    }

    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes();
    }

    public override sealed IEnumerable<IShapeComponent> GetShapeComponents()
    {
        return GetShapeExtents();
    }

    public override sealed IExtent? GetValidShapeComponent(IShapeComponent shapeComponent)
    {
        return shapeComponent is IExtent extent 
            && extent.DefaultQuantity > 0 ?
            extent
            : null;
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        object? converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

        ValidateDecimalQuantity((decimal)converted, paramName);
    }

    public override sealed void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName)
    {
        decimal defaultQuantity = NullChecked(shapeComponent, paramName).GetDefaultQuantity();

        if (shapeComponent is not IExtent) throw ArgumentTypeOutOfRangeException(paramName, shapeComponent);

        ValidateDecimalQuantity(defaultQuantity, paramName);
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

        foreach (ShapeExtentTypeCode item in simpleShape.GetShapeExtentTypeCodes())
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

    private static void ValidateDecimalQuantity(decimal quantity, string name)
    {
        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(name, quantity);
    }
    #endregion
    #endregion
}
