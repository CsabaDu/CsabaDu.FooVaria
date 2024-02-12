namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal abstract class Mass : Quantifiable, IMass
{
    #region Constants
    private const MeasureUnitCode WeightMeasureUnitCode = MeasureUnitCode.WeightUnit;
    #endregion

    #region Constructors
    private protected Mass(IMass other) : base(other)
    {
        Weight = other.Weight;
    }

    private protected Mass(IMassFactory factory, IWeight weight, IBody body) : base(factory, weight)
    {
        validateParams();

        Weight = weight;

        #region Local methods
        void validateParams()
        {
            ValidateMassComponent(weight, nameof(weight));
            ValidateMassComponent(body, nameof(body));
        }
        #endregion
    }

    private protected Mass(IMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, WeightMeasureUnitCode, baseFace, height)
    {
        validateParams();

        Weight = weight;

        #region Local methods
        void validateParams()
        {
            ValidateMassComponent(weight, nameof(weight));
            _ = NullChecked(baseFace, nameof(baseFace));
            ValidateMassComponent(height, nameof(height));
        }
        #endregion
    }

    private protected Mass(IMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, WeightMeasureUnitCode, shapeExtents)
    {
        validateParams();

        Weight = weight;

        #region Local methods
        void validateParams()
        {
            ValidateMassComponent(weight, nameof(weight));
            foreach (IExtent item in NullChecked(shapeExtents, nameof(shapeExtents)))
            {
                ValidateMassComponent(item, nameof(shapeExtents));
            }
        }
        #endregion
    }
    #endregion

    #region Properties
    public IWeight Weight { get; init; }
    public abstract IBody Body { get; init; }
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

    public decimal GetDefaultQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetDefaultQuantity();
    }

    public IProportion<WeightUnit, VolumeUnit> GetDensity()
    {
        return GetFactory().CreateDensity(this);
    }

    public MeasureUnitCode GetMeasureUnitCode(decimal ratio)
    {
        bool isVolumeWeightGreater = GetVolumeWeight(ratio).CompareTo(Weight) < 0;

        return GetMeasureUnitCode(isVolumeWeightGreater);
    }

    public double GetQuantity()
    {
        return GetVolumeWeight().GetQuantity();
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
        return Body.GetSpreadMeasure();
    }

    public MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return Body.GetSpreadMeasureUnitCode();
    }

    public IVolume GetVolume()
    {
        return (IVolume)GetSpreadMeasure();
    }

    public IWeight GetVolumeWeight()
    {
        IWeight volumeWeight = GetVolumeWeight(this);

        return GetGreaterWeight(volumeWeight);
    }

    public IWeight GetVolumeWeight(decimal ratio)
    {
        ValidateQuantity(ratio, nameof(ratio));

        IWeight volumeWeight = GetVolumeWeight(this);
        volumeWeight = (IWeight)volumeWeight.Multiply(ratio);

        return GetGreaterWeight(volumeWeight);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode(measureUnitCode);

        return BaseMeasurement.IsValidMeasureUnit(context) && hasMeasureUnitCode(GetDefinedMeasureUnitCode(context!));

        #region Local methods
        bool hasMeasureUnitCode(MeasureUnitCode measureUnitCode)
        {
            return GetMeasureUnitCodes().Contains(measureUnitCode);
        }
        #endregion
    }

    public decimal ProportionalTo(IMass? mass)
    {
        return GetDefaultQuantity() / NullChecked(mass, nameof(mass)).GetVolumeWeight().GetDefaultQuantity();
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        Body.ValidateSpreadMeasure(spreadMeasure, paramName);
    }

    public void ValidateMassComponent(IQuantifiable? massComponent, string paramName)
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
        return MeasureUnitCodes.Where(x => this[x] != null);
    }

    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return GetVolumeWeight().GetDefaultQuantity();
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(Weight, Body);
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
        ValidateQuantity(this, quantity, paramName);
    }

    public override sealed void ValidateQuantity(IQuantifiable? quantifiable, string paramName)
    {
        base.ValidateQuantity(quantifiable, paramName);
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
            ISpread? exchanged = Body.ExchangeTo(measureUnit);

            if (exchanged is not IBody body) return null;

            return GetMass(Weight, body);
        }

        IMass? exchangeWeight()
        {
            IBaseMeasure? exchanged = Weight.ExchangeTo(measureUnit);

            if (exchanged is not IWeight weight) return null;

            return GetMass(weight, Body);
        }
    }

    public virtual bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        if (other == null || limitMode == null) return true;

        if (other == null) return null;

        bool? bodyFitsIn = Body.FitsIn(other.Body, limitMode);
        bool? weightFitsIn = Weight.FitsIn(other.Weight, limitMode);

        return BothFitIn(bodyFitsIn, weightFitsIn);
    }

    public virtual IBodyFactory GetBodyFactory()
    {
        return GetFactory().BodyFactory;
    }
    #endregion

    #region Abstract methods
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
        return volumeWeight.CompareTo(Weight) < 0 ?
            (IWeight)volumeWeight.ExchangeTo(GetMeasureUnit())!
            : Weight;
    }

    private MeasureUnitCode GetMeasureUnitCode(bool isVolumeWeightGreater)
    {
        return isVolumeWeightGreater ?
            Body.MeasureUnitCode
            : Weight.MeasureUnitCode;
    }

    #region Static methods
    private static IWeight GetVolumeWeight(IMass mass)
    {
        IVolume volume = mass.GetVolume();

        return volume.ConvertMeasure();
    }
    #endregion
    #endregion
}