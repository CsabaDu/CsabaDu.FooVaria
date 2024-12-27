namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal abstract class Mass : BaseQuantifiable, IMass
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Mass"/> class by copying another <see cref="IMass"/> instance.
    /// </summary>
    /// <param name="other">The other <see cref="IMass"/> instance to copy.</param>
    private protected Mass(IMass other) : base(other, nameof(other))
    {
        Weight = other.Weight;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Mass"/> class with the specified factory and weight.
    /// </summary>
    /// <param name="factory">The factory to create the mass.</param>
    /// <param name="weight">The weight of the mass.</param>
    private protected Mass(IMassFactory factory, IWeight weight) : base(factory, nameof(factory))
    {
        ValidateMassComponent(weight, nameof(weight));

        Weight = weight;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the weight of the mass.
    /// </summary>
    public IWeight Weight { get; init; }

    /// <summary>
    /// Gets the measure associated with the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>The measure associated with the specified measure unit code.</returns>
    public IMeasure? this[MeasureUnitCode measureUnitCode] => measureUnitCode switch
    {
        MeasureUnitCode.VolumeUnit => GetVolume(),
        MeasureUnitCode.WeightUnit => Weight,

        _ => null,
    };
    #endregion

    #region Public methods
    /// <summary>
    /// Compares the current mass to another mass.
    /// </summary>
    /// <param name="mass">The mass to compare to.</param>
    /// <returns>An integer that indicates the relative order of the masses being compared.</returns>
    public int CompareTo(IMass? mass)
    {
        return GetDefaultQuantity().CompareTo(mass?.GetDefaultQuantity());
    }

    /// <summary>
    /// Exchanges the current mass to the specified weight unit.
    /// </summary>
    /// <param name="weightUnit">The weight unit to exchange to.</param>
    /// <returns>The exchanged mass, or null if the exchange is not possible.</returns>
    public IMass? ExchangeTo(WeightUnit weightUnit)
    {
        IWeight? weight = Weight.ExchangeTo(weightUnit);

        if (weight is null) return null;

        return GetMass(weight, GetBody());
    }

    /// <summary>
    /// Gets a new mass with the specified weight and body.
    /// </summary>
    /// <param name="weight">The weight of the new mass.</param>
    /// <param name="body">The body of the new mass.</param>
    /// <returns>The new mass.</returns>
    public IMass GetMass(IWeight weight, IBody body)
    {
        IMassFactory factory = GetMassFactory();

        return factory.Create(weight, body);
    }

    /// <summary>
    /// Gets the base quantity of the mass.
    /// </summary>
    /// <returns>The base quantity of the mass.</returns>
    public ValueType GetBaseQuantity()
    {
        return GetDefaultQuantity();
    }

    /// <summary>
    /// Gets the chargeable weight of the mass based on the specified ratio, weight unit, and rounding mode.
    /// </summary>
    /// <param name="ratio">The ratio to apply.</param>
    /// <param name="weightUnit">The weight unit to use.</param>
    /// <param name="roundingMode">The rounding mode to apply.</param>
    /// <returns>The chargeable weight of the mass.</returns>
    public IWeight GetChargeableWeight(decimal ratio, WeightUnit weightUnit, RoundingMode roundingMode)
    {
        ValidateMeasureUnit(weightUnit, nameof(weightUnit));

        return (IWeight)GetVolumeWeight(ratio)
            .ExchangeTo(weightUnit)!
            .Round(roundingMode);
    }

    /// <summary>
    /// Gets the default quantity of the mass based on the specified ratio.
    /// </summary>
    /// <param name="ratio">The ratio to apply.</param>
    /// <returns>The default quantity of the mass.</returns>
    public decimal GetDefaultQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetDefaultQuantity();
    }

    /// <summary>
    /// Gets the density of the mass.
    /// </summary>
    /// <returns>The density of the mass.</returns>
    public IProportion GetDensity()
    {
        IMassFactory factory = GetMassFactory();

        return factory.CreateDensity(this);
    }

    /// <summary>
    /// Gets the measure unit code of the mass based on the specified ratio.
    /// </summary>
    /// <param name="ratio">The ratio to apply.</param>
    /// <returns>The measure unit code of the mass.</returns>
    public MeasureUnitCode GetMeasureUnitCode(decimal ratio)
    {
        IWeight volumeWeight = GetVolumeWeight(ratio);

        return IsWeightNotLess(volumeWeight) ?
            Weight.GetMeasureUnitCode()
            : GetBody().GetMeasureUnitCode();
    }

    /// <summary>
    /// Gets the quantity of the mass.
    /// </summary>
    /// <returns>The quantity of the mass.</returns>
    public double GetQuantity()
    {
        return Weight.GetQuantity();
    }

    /// <summary>
    /// Gets the quantity of the mass as the specified type code.
    /// </summary>
    /// <param name="quantityTypeCode">The type code to convert to.</param>
    /// <returns>The quantity of the mass as the specified type code.</returns>
    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(quantityTypeCode);

        return quantity ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    /// <summary>
    /// Gets the quantity of the mass based on the specified ratio.
    /// </summary>
    /// <param name="ratio">The ratio to apply.</param>
    /// <returns>The quantity of the mass.</returns>
    public double GetQuantity(decimal ratio)
    {
        return GetVolumeWeight(ratio).GetQuantity();
    }

    /// <summary>
    /// Gets the spread measure of the mass.
    /// </summary>
    /// <returns>The spread measure of the mass.</returns>
    public ISpreadMeasure GetSpreadMeasure()
    {
        IBody body = GetBody();

        return body.GetSpreadMeasure();
    }

    /// <summary>
    /// Gets the volume of the mass.
    /// </summary>
    /// <returns>The volume of the mass.</returns>
    public IVolume GetVolume()
    {
        return (IVolume)GetSpreadMeasure();
    }

    /// <summary>
    /// Gets the volume weight of the mass.
    /// </summary>
    /// <returns>The volume weight of the mass.</returns>
    public IWeight GetVolumeWeight()
    {
        return GetVolumeWeight(decimal.One);
    }

    /// <summary>
    /// Gets the volume weight of the mass based on the specified ratio.
    /// </summary>
    /// <param name="ratio">The ratio to apply.</param>
    /// <returns>The volume weight of the mass.</returns>
    public IWeight GetVolumeWeight(decimal ratio)
    {
        IVolume volume = GetVolume();
        IWeight volumeWeight = volume.ConvertMeasure();

        if (ratio == decimal.One) return GetGreaterWeight(volumeWeight);

        ValidateQuantity(ratio, nameof(ratio));

        volumeWeight = (IWeight)volumeWeight.Multiply(ratio);

        return GetGreaterWeight(volumeWeight);
    }

    /// <summary>
    /// Determines whether the mass is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context to check.</param>
    /// <returns>true if the mass is exchangeable to the context; otherwise, false.</returns>
    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return HasMeasureUnitCode(measureUnitCode);

        return IsValidMeasureUnit(context)
            && HasMeasureUnitCode(GetMeasureUnitCode(context));
    }

    /// <summary>
    /// Gets the proportion of the current mass to another mass.
    /// </summary>
    /// <param name="mass">The other mass to compare to.</param>
    /// <returns>The proportion of the current mass to the other mass.</returns>
    public decimal ProportionalTo(IMass? mass)
    {
        decimal defaultQuantity = GetDefaultQuantity();
        decimal massDefaultQuantity = NullChecked(mass, nameof(mass)).GetVolumeWeight().GetDefaultQuantity();

        return defaultQuantity / massDefaultQuantity;
    }

    /// <summary>
    /// Validates the density of the mass.
    /// </summary>
    /// <param name="density">The density to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
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

    /// <summary>
    /// Validates the mass component.
    /// </summary>
    /// <param name="massComponent">The mass component to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateMassComponent(IBaseQuantifiable? massComponent, string paramName)
    {
        if (NullChecked(massComponent, paramName) is not IExtent or IWeight or IVolume or IBody)
        {
            throw ArgumentTypeOutOfRangeException(paramName, massComponent!);
        }

        decimal defaultQuantity = massComponent!.GetDefaultQuantity();

        ValidateQuantity(defaultQuantity, paramName);
    }

    /// <summary>
    /// Validates the measure unit codes.
    /// </summary>
    /// <param name="measureUnitCodes">The measure unit codes to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName)
    {
        if (NullChecked(measureUnitCodes, paramName) is IMass mass)
        {
            ValidateMeasureUnitCodes(this, mass, paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, measureUnitCodes!);
    }

    /// <summary>
    /// Validates the spread measure.
    /// </summary>
    /// <param name="spreadMeasure">The spread measure to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBody body = GetBody();

        body.ValidateSpreadMeasure(spreadMeasure, paramName);
    }

    #region Override methods
    /// <summary>
    /// Determines whether the current mass fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>true if the current mass fits within the limiter; otherwise, null.</returns>
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
    /// <summary>
    /// Gets the default quantity of the mass.
    /// </summary>
    /// <returns>The default quantity of the mass.</returns>
    public override sealed decimal GetDefaultQuantity()
    {
        return GetVolumeWeight().GetDefaultQuantity();
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override sealed int GetHashCode()
    {
        return HashCode.Combine(Weight, GetBody());
    }

    /// <summary>
    /// Gets the base measure unit of the mass.
    /// </summary>
    /// <returns>The base measure unit of the mass.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        return Weight.GetBaseMeasureUnit();
    }

    /// <summary>
    /// Gets the quantity type code of the mass.
    /// </summary>
    /// <returns>The quantity type code of the mass.</returns>
    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    /// <summary>
    /// Determines whether the mass has the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to check.</param>
    /// <returns>true if the mass has the measure unit code; otherwise, false.</returns>
    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    /// <summary>
    /// Validates the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCode(this, measureUnitCode, paramName);
    }

    /// <summary>
    /// Validates the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidatePositiveQuantity(quantity, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    /// <summary>
    /// Gets the measure unit codes of the mass.
    /// </summary>
    /// <returns>An enumerable collection of measure unit codes.</returns>
    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return Weight.GetMeasureUnitCode();
        yield return GetBody().GetMeasureUnitCode();
    }

    /// <summary>
    /// Determines whether the current mass is equal to another mass.
    /// </summary>
    /// <param name="other">The other mass to compare to.</param>
    /// <returns>true if the current mass is equal to the other mass; otherwise, false.</returns>
    public virtual bool Equals(IMass? other)
    {
        return base.Equals(other)
            && other is IMass mass
            && Weight.Equals(mass.Weight);
    }

    /// <summary>
    /// Determines whether the current mass fits within another mass based on the specified limit mode.
    /// </summary>
    /// <param name="other">The other mass to compare to.</param>
    /// <param name="limitMode">The limit mode to apply.</param>
    /// <returns>true if the current mass fits within the other mass; otherwise, null.</returns>
    public virtual bool? FitsIn(IMass? other, LimitMode? limitMode)
    {
        if (other is null || limitMode is null) return true;

        if (other is null) return null;

        bool? bodyFitsIn = GetBody().FitsIn(other.GetBody(), limitMode);
        bool? weightFitsIn = Weight.FitsIn(other.Weight, limitMode);

        return BothFitIn(bodyFitsIn, weightFitsIn);
    }

    /// <summary>
    /// Tries to exchange the current mass to the specified context.
    /// </summary>
    /// <param name="context">The context to exchange to.</param>
    /// <param name="exchanged">The exchanged mass.</param>
    /// <returns>true if the exchange is successful; otherwise, false.</returns>
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
    /// <summary>
    /// Gets the body of the mass.
    /// </summary>
    /// <returns>The body of the mass.</returns>
    public abstract IBody GetBody();

    /// <summary>
    /// Gets the body factory of the mass.
    /// </summary>
    /// <returns>The body factory of the mass.</returns>
    public abstract IBodyFactory GetBodyFactory();

    /// <summary>
    /// Gets a new mass with the specified body and density.
    /// </summary>
    /// <param name="body">The body of the new mass.</param>
    /// <param name="density">The density of the new mass.</param>
    /// <returns>The new mass.</returns>
    public abstract IMass GetMass(IBody body, IProportion density);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    /// <summary>
    /// Determines whether both the body and weight fit within the specified limits.
    /// </summary>
    /// <param name="bodyFitsIn">The result of the body fit check.</param>
    /// <param name="weightFitsIn">The result of the weight fit check.</param>
    /// <returns>true if both fit within the limits; otherwise, null.</returns>
    protected static bool? BothFitIn(bool? bodyFitsIn, bool? weightFitsIn)
    {
        if (bodyFitsIn is null || weightFitsIn is null) return null;

        if (bodyFitsIn != weightFitsIn) return false;

        return bodyFitsIn;
    }

    /// <summary>
    /// Gets a new mass with the specified mass, body, and density.
    /// </summary>
    /// <param name="mass">The mass to use.</param>
    /// <param name="body">The body of the new mass.</param>
    /// <param name="density">The density of the new mass.</param>
    /// <returns>The new mass.</returns>
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
    /// <summary>
    /// Gets the greater weight between the current weight and the provided volume weight.
    /// </summary>
    /// <param name="volumeWeight">The volume weight to compare.</param>
    /// <returns>The greater weight.</returns>
    private IWeight GetGreaterWeight(IWeight volumeWeight)
    {
        if (IsWeightNotLess(volumeWeight)) return Weight;

        WeightUnit weightUnit = Weight.GetMeasureUnit();

        return volumeWeight.ExchangeTo(weightUnit)!;
    }

    /// <summary>
    /// Gets the mass factory associated with the current mass.
    /// </summary>
    /// <returns>The mass factory.</returns>
    private IMassFactory GetMassFactory()
    {
        return (IMassFactory)GetFactory();
    }

    /// <summary>
    /// Determines whether the provided volume weight is not less than the current weight.
    /// </summary>
    /// <param name="volumeWeight">The volume weight to compare.</param>
    /// <returns>true if the volume weight is not less than the current weight; otherwise, false.</returns>
    private bool IsWeightNotLess(IWeight volumeWeight)
    {
        return volumeWeight.CompareTo(Weight) >= 0;
    }
    #endregion
}