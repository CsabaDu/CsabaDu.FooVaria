using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal abstract class Mass : BaseQuantifiable, IMass
{
    #region Constructors
    private protected Mass(IMass other) : base(other)
    {
        Weight = other.Weight;
    }

    private protected Mass(IMassFactory factory, IWeight weight) : base(factory)
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

    public IWeight GetChargeableWeight(decimal ratio, WeightUnit weightUnit, RoundingMode roundingMode)
    {
        ValidateMeasureUnit(weightUnit, nameof(weightUnit));

        return (IWeight)GetVolumeWeight(ratio)
            .ExchangeTo(weightUnit)!
            .Round(roundingMode);
    }

    public ValueType GetBaseQuantity()
    {
        return GetDefaultQuantity();
    }

    public decimal GetDefaultQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetDefaultQuantity();
    }

    public IProportion<WeightUnit, VolumeUnit> GetDensity()
    {
        return GetFactory().CreateDensity(this);
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

    public MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        IBody body = GetBody();

        return body.GetSpreadMeasureUnitCode();
    }

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

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode(measureUnitCode);

        return BaseMeasurement.IsValidMeasureUnit(context)
            && hasMeasureUnitCode(GetDefinedMeasureUnitCode(context!));

        #region Local methods
        bool hasMeasureUnitCode(MeasureUnitCode measureUnitCode)
        {
            return GetMeasureUnitCodes().Contains(measureUnitCode);
        }
        #endregion
    }

    public decimal ProportionalTo(IMass? mass)
    {
        decimal defaultQuantity = GetDefaultQuantity();
        decimal massDefaultQuantity = NullChecked(mass, nameof(mass)).GetVolumeWeight().GetDefaultQuantity();

        return defaultQuantity / massDefaultQuantity;
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBody body = GetBody();

        body.ValidateSpreadMeasure(spreadMeasure, paramName);
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

    #region Override methods
    public override IMassFactory GetFactory()
    {
        return (IMassFactory)Factory;
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        IBody body = GetBody();

        return base.GetMeasureUnitCodes().Append(body.GetMeasureUnitCode());
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

    public override sealed Enum GetMeasureUnit()
    {
        return Weight.GetMeasureUnit();
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCode(this, measureUnitCode, paramName);
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidatePositiveQuantity(quantity, paramName);
    }

    public override sealed void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        base.ValidateQuantity(baseQuantifiable, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual bool Equals(IMass? other)
    {
        return base.Equals(other)
            && other is IMass mass
            && Weight.Equals(mass.Weight);
    }

    public virtual IMass? ExchangeTo(Enum? measureUnit)
    {
        if (measureUnit == null) return null;

        MeasureUnitCode measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        if (!GetMeasureUnitCodes().Contains(measureUnitCode)) return null;

        return measureUnitCode switch
        {
            MeasureUnitCode.VolumeUnit => exchangeVolume(),
            MeasureUnitCode.WeightUnit => exchangeWeight(),

            _ => null,
        };

        IMass? exchangeVolume()
        {
            IQuantifiable? exchanged = GetBody().ExchangeTo(measureUnit);

            if (exchanged is not IBody body) return null;

            return GetMass(Weight, body);
        }

        IMass? exchangeWeight()
        {
            IQuantifiable? exchanged = Weight.ExchangeTo(measureUnit);

            if (exchanged is not IWeight weight) return null;

            return GetMass(weight, GetBody());
        }
    }

    public virtual bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        if (other == null || limitMode == null) return true;

        if (other == null) return null;

        bool? bodyFitsIn = GetBody().FitsIn(other.GetBody(), limitMode);
        bool? weightFitsIn = Weight.FitsIn(other.Weight, limitMode);

        return BothFitIn(bodyFitsIn, weightFitsIn);
    }

    public virtual bool? FitsIn(ILimiter? limiter)
    {
        if (limiter == null) return null;

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

    public virtual IBodyFactory GetBodyFactory()
    {
        return GetFactory().BodyFactory;
    }
    #endregion

    #region Abstract methods
    public abstract IBody GetBody();
    public abstract IMass GetMass(IWeight weight, IBody body);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool? BothFitIn(bool? bodyFitsIn, bool? weightFitsIn)
    {
        if (bodyFitsIn == null || weightFitsIn == null) return null;

        if (bodyFitsIn != weightFitsIn) return false;

        return bodyFitsIn;
    }
    #endregion
    #endregion

    #region Private methods
    private IWeight GetGreaterWeight(IWeight volumeWeight)
    {
        if (IsWeightNotLess(volumeWeight)) return Weight;

        return (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!;
    }

    private bool IsWeightNotLess(IWeight volumeWeight)
    {
        return volumeWeight.CompareTo(Weight) >= 0;
    }
    #endregion
}