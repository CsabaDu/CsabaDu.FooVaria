using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Shapes.Types.Implementations;

internal abstract class Shape : BaseShape, IShape
{
    #region Constructors
    private protected Shape(IShape other) : base(other)
    {
    }

    private protected Shape(IShapeFactory factory, IBaseShape baseShape) : base(factory, baseShape)
    {
    }

    private protected Shape(IShapeFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IExtent[] shapeExtents) : base(factory, measureUnitTypeCode, shapeExtents)
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
        foreach (ShapeExtentTypeCode item in Enum.GetValues<ShapeExtentTypeCode>())
        {
            if (this[item] != null)
            {
                yield return item;
            }
        }
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

        if (other is not IShape shape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            throw ArgumentTypeOutOfRangeException(paramName, other);
        }

        ValidateMeasureUnitTypeCode(shape.MeasureUnitTypeCode, paramName);

        return Compare(this, shape) ?? throw new ArgumentOutOfRangeException(paramName);
    }

    public override sealed bool Equals(IBaseShape? other)
    {
        return other is IShape shape
            && base.Equals(shape);
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
        IShape? exchangeToExtentUnit(ExtentUnit extentUnit)
        {
            IEnumerable<IExtent> exchangedShapeExtents = getExchangedShapeExtents(extentUnit);

            if (exchangedShapeExtents.Count() != GetShapeExtents().Count()) return null;

            return GetShape(exchangedShapeExtents.ToArray());
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
        if (other is not IShape shape)
        {
            if (other is IComplexShape complexShape)
            {
                throw new NotImplementedException();
            }

            return null;
        }

        if (!shape.IsExchangeableTo(MeasureUnitTypeCode)) return null;

        limitMode ??= LimitMode.BeNotGreater;

        SideCode sideCode = limitMode == LimitMode.BeNotLess || limitMode == LimitMode.BeGreater ?
            SideCode.Outer
            : SideCode.Inner;

        if (shape.GetShapeComponentCount() != GetShapeComponentCount())
        {
            shape = (shape as ITangentShape)!.GetTangentShape(sideCode);
        }

        return Compare(this, shape)?.FitsIn(limitMode);
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
        return GetShapeComponents();
    }

    public override sealed IExtent? GetValidShapeComponent(IShapeComponent shapeComponent)
    {
        return shapeComponent is IExtent extent 
            && extent.DefaultQuantity > 0 ?
            extent
            : null;
    }

    public override sealed void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateShape, paramName);

        #region Local methods
        void validateShape()
        {
            base.Validate(rootObject, paramName);

            int shapeExtentCount = GetShapeExtents().Count();

            if (rootObject is IShape shape && shape.GetShapeComponentCount() == shapeExtentCount) return;

            throw ArgumentTypeOutOfRangeException(paramName, rootObject!);
        }
        #endregion
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
    private static int? Compare(IShape shape, IShape? other)
    {
        if (other == null) return null;

        int comparison = 0;

        foreach (ShapeExtentTypeCode item in shape.GetShapeExtentTypeCodes())
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

    private static void ValidateDecimalQuantity(decimal quantity, string name)
    {
        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(name, quantity);
    }
    #endregion
    #endregion
}
