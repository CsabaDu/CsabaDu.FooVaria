using CsabaDu.FooVaria.BaseTypes.Spreads.Types;
using CsabaDu.FooVaria.DryBodies.Types;
using CsabaDu.FooVaria.Masses.Behaviors;

namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal abstract class Mass : BaseQuantifiable, IMass
{
    #region Constructors
    private protected Mass(IMass other) : base(other, nameof(other))
    {
        Weight = other.Weight;
    }

    private protected Mass(IMassFactory factory, IWeight weight) : base(factory, nameof(factory))
    {
        ValidateMassComponent(weight, nameof(weight));

        Weight = weight;
    }
    #endregion

    #region Properties
    public IWeight Weight { get; init; }
    public IMeasure? this[MeasureUnitCode measureUnitCode] => measureUnitCode switch
    {
        MeasureUnitCode.VolumeUnit => GetVolume(),
        MeasureUnitCode.WeightUnit => Weight,

        _ => null,
    };
    #endregion

    #region Public methods
    public int CompareTo(IMass? mass)
    {
        return GetDefaultQuantity().CompareTo(mass?.GetDefaultQuantity());
    }

    public IMass? ExchangeTo(WeightUnit weightUnit)
    {
        IWeight? weight = Weight.ExchangeTo(weightUnit);

        if (weight is null) return null;

        return GetMass(weight, GetBody());
    }

    public IMass GetMass(IWeight weight, IBody body)
    {
        IMassFactory factory = GetMassFactory();

        return factory.Create(weight, body);
    }

    public ValueType GetBaseQuantity()
    {
        return GetDefaultQuantity();
    }

    public IWeight GetChargeableWeight(decimal ratio, WeightUnit weightUnit, RoundingMode roundingMode)
    {
        ValidateMeasureUnit(weightUnit, nameof(weightUnit));

        return (IWeight)GetVolumeWeight(ratio)
            .ExchangeTo(weightUnit)!
            .Round(roundingMode);
    }

    public decimal GetDefaultQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetDefaultQuantity();
    }

    public IProportion GetDensity()
    {
        IMassFactory factory = GetMassFactory();

        return factory.CreateDensity(this);
    }

    public override MeasureUnitCode GetMeasureUnitCode()
    {
        return Weight.GetMeasureUnitCode();
    }

    public MeasureUnitCode GetMeasureUnitCode(decimal ratio)
    {
        IWeight volumeWeight = GetVolumeWeight(ratio);

        return IsWeightNotLess(volumeWeight) ?
            Weight.GetMeasureUnitCode()
            : GetBody().GetMeasureUnitCode();
    }

    public double GetQuantity()
    {
        return Weight.GetQuantity();
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(quantityTypeCode);

        return quantity ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public double GetQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetQuantity();
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        IBody body = GetBody();

        return body.GetSpreadMeasure();
    }

    //public MeasureUnitCode GetSpreadMeasureUnitCode()
    //{
    //    IBody body = GetBody();

    //    return body.GetSpreadMeasureUnitCode();
    //}

    public IVolume GetVolume()
    {
        return (IVolume)GetSpreadMeasure();
    }

    public IWeight GetVolumeWeight()
    {
        return GetVolumeWeight(decimal.One);
    }

    public IWeight GetVolumeWeight(decimal ratio)
    {
        IVolume volume = GetVolume();
        IWeight volumeWeight = volume.ConvertMeasure();

        if (ratio == decimal.One) return GetGreaterWeight(volumeWeight);

        ValidateQuantity(ratio, nameof(ratio));

        volumeWeight = (IWeight)volumeWeight.Multiply(ratio);

        return GetGreaterWeight(volumeWeight);
    }

    //public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    //{
    //    return IsValidMeasureUnitCode(this, measureUnitCode);
    //}

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return HasMeasureUnitCode(measureUnitCode);

        return IsValidMeasureUnit(context)
            && HasMeasureUnitCode(GetMeasureUnitCode(context));
    }

    public decimal ProportionalTo(IMass? mass)
    {
        decimal defaultQuantity = GetDefaultQuantity();
        decimal massDefaultQuantity = NullChecked(mass, nameof(mass)).GetVolumeWeight().GetDefaultQuantity();

        return defaultQuantity / massDefaultQuantity;
    }

    public void ValidateDensity(IProportion density, string paramName)
    {
        foreach (MeasureUnitCode item in NullChecked(density, paramName).GetMeasureUnitCodes())
        {
            if (!HasMeasureUnitCode(item))
            {
                throw InvalidMeasureUnitCodeEnumArgumentException(item, paramName);
            }
        }
    }

    public void ValidateMassComponent(IBaseQuantifiable? massComponent, string paramName)
    {
        if (NullChecked(massComponent, paramName) is not IExtent or IWeight or IVolume or IBody)
        {
            throw ArgumentTypeOutOfRangeException(paramName, massComponent!);
        }

        decimal defaultQuantity = massComponent!.GetDefaultQuantity();

        ValidateQuantity(defaultQuantity, paramName);
    }

    public void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        ValidateMeasureUnitCodes(this, baseQuantifiable, paramName);
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBody body = GetBody();

        body.ValidateSpreadMeasure(spreadMeasure, paramName);
    }

    #region Override methods
    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        LimitMode? limitMode = limiter.GetLimitMode();

        if (limiter is IBaseRate baseRate) return GetDensity().FitsIn(baseRate, limitMode);

        if (limiter is not IQuantifiable quantifiable) return null;

        return quantifiable.GetMeasureUnitCode() switch
        {
            MeasureUnitCode.VolumeUnit => GetVolume().FitsIn(quantifiable, limitMode),
            MeasureUnitCode.WeightUnit => Weight.FitsIn(quantifiable, limitMode),

            _ => null,
        };
    }

    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return GetVolumeWeight().GetDefaultQuantity();
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(Weight, GetBody());
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return Weight.GetBaseMeasureUnit();
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCode(this, measureUnitCode, paramName);
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidatePositiveQuantity(quantity, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();
        yield return GetBody().GetMeasureUnitCode();
    }

    
    public virtual bool Equals(IMass? other)
    {
        return base.Equals(other)
            && other is IMass mass
            && Weight.Equals(mass.Weight);
    }

    public virtual bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        if (other is null || limitMode is null) return true;

        if (other is null) return null;

        bool? bodyFitsIn = GetBody().FitsIn(other.GetBody(), limitMode);
        bool? weightFitsIn = Weight.FitsIn(other.Weight, limitMode);

        return BothFitIn(bodyFitsIn, weightFitsIn);
    }

    public virtual bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IMass? exchanged)
    {
        exchanged = null;

        if (!Weight.IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
        exchanged = ExchangeTo((WeightUnit)measureUnitElements.MeasureUnit);

        return exchanged is not null;
    }
    #endregion

    #region Abstract methods
    public abstract IBody GetBody();
    public abstract IBodyFactory GetBodyFactory();
    public abstract IMass GetMass(IBody body, IProportion density);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool? BothFitIn(bool? bodyFitsIn, bool? weightFitsIn)
    {
        if (bodyFitsIn is null || weightFitsIn is null) return null;

        if (bodyFitsIn != weightFitsIn) return false;

        return bodyFitsIn;
    }

    protected static IMass GetMass(IMass mass, IBody body, IProportion density)
    {
        decimal defaultQuantity = NullChecked(body, nameof(body)).GetDefaultQuantity();

        mass.ValidateDensity(density, nameof(density));

        defaultQuantity *= density.DefaultQuantity;
        IWeight weight = (IWeight)mass.Weight.GetBaseMeasure(default(WeightUnit), defaultQuantity);

        return mass.GetMass(weight, body);
    }
    #endregion
    #endregion

    #region Private methods
    private IWeight GetGreaterWeight(IWeight volumeWeight)
    {
        if (IsWeightNotLess(volumeWeight)) return Weight;

        WeightUnit weightUnit = Weight.GetMeasureUnit();

        return volumeWeight.ExchangeTo(weightUnit)!;
    }

    private IMassFactory GetMassFactory()
    {
        return (IMassFactory)GetFactory();
    }

    private bool IsWeightNotLess(IWeight volumeWeight)
    {
        return volumeWeight.CompareTo(Weight) >= 0;
    }
    #endregion
}