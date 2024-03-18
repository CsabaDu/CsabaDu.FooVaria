using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

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

    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return IsValidMeasureUnitCode(this, measureUnitCode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode(measureUnitCode);

        return BaseMeasurement.IsValidMeasureUnit(context)
            && hasMeasureUnitCode(GetDefinedMeasureUnitCode(context));

        #region Local methods
        bool hasMeasureUnitCode(MeasureUnitCode measureUnitCode)
        {
            return GetMeasureUnitCodes().Contains(measureUnitCode);
        }
        #endregion
    }

    public bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IMass? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
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

    public void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        ValidateMeasureUnitCodes(this, baseQuantifiable, paramName);
    }

    #region Override methods
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
        return MeasureUnitCodes.Where(x => this[x] is not null);
    }

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

    public override bool? FitsIn(ILimiter? limiter)
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
    #endregion

    #region Abstract methods
    public abstract IBody GetBody();
    public abstract IBodyFactory GetBodyFactory();
    public IMass GetMass(IWeight weight, IBody body)
    {
        IMassFactory factory = GetMassFactory();

        return factory.Create(weight, body);
    }
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

        return (IWeight)volumeWeight.ExchangeTo(GetBaseMeasureUnit())!;
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