namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types.Implementations;

public abstract class BaseMeasure(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), IBaseMeasure
{
    #region Public methods
    #region Static methods
    public static void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
    {
        Type quantityType = NullChecked(quantity, paramName).GetType();

        ValidateQuantityTypeCode(quantityTypeCode, nameof(quantityTypeCode));

        if (quantityTypeCode == Type.GetTypeCode(quantityType)) return;

        throw ArgumentTypeOutOfRangeException(paramName, quantity!);
    }

    public static void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
    {
        if (GetQuantityTypeCode(quantityTypeCode) > TypeCode.Object) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
    }
    #endregion

    #region Override methods
    #region Sealed methods
    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return base.FitsIn(limiter);
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetBaseMeasureUnit();
    }

    public override sealed decimal GetDefaultQuantity()
    {
        object quantity = GetBaseQuantity();
        decimal exchangeRate = GetExchangeRate();

        return GetDefaultQuantity(quantity, exchangeRate);
    }

    public override sealed bool IsExchangeableTo(Enum? context)
    {
        if (!base.IsExchangeableTo(context)) return false;

        return IsValidMeasureUnit(getMeasureUnit());

        #region Local methods
        Enum getMeasureUnit()
        {
            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

            return measureUnitElements.MeasureUnit;
        }
        #endregion
    }

    public override sealed IBaseMeasure Round(RoundingMode roundingMode)
    {
        ValueType quantity = (ValueType)GetQuantity(roundingMode);
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return GetBaseMeasure(baseMeasurement, quantity);
    }

    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        GetBaseMeasurement().ValidateMeasureUnit(measureUnit, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IBaseMeasurement GetBaseMeasurement();
    public abstract IBaseMeasurementFactory GetBaseMeasurementFactory();
    public abstract LimitMode? GetLimitMode();
    #endregion

    public bool Equals(IBaseMeasure? x, IBaseMeasure? y)
    {
        return x is null == y is null
            && x?.GetRateComponentCode() == y?.GetRateComponentCode()
            && x?.GetLimitMode() == y?.GetLimitMode()
            && x?.Equals(y) != false;
    }

    public IBaseMeasure GetBaseMeasure(ValueType quantity)
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return GetBaseMeasure(baseMeasurement, quantity);
    }

    public IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        _ = NullChecked(baseMeasurement, nameof(baseMeasurement));
        ValidateQuantity(quantity, nameof(quantity));

        IBaseMeasureFactory factory = GetBaseMeasureFactory();

        return factory.CreateBaseMeasure(baseMeasurement, quantity);
    }

    public decimal GetExchangeRate()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetExchangeRate();
    }

    public int GetHashCode([DisallowNull] IBaseMeasure other)
    {
        return HashCode.Combine(other.GetRateComponentCode(), other.GetLimitMode(), other.GetHashCode());
    }

    public TypeCode GetQuantityTypeCode(object? quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetQuantityTypeCode(quantityTypeCode);
    }

    public RateComponentCode GetRateComponentCode()
    {
        return GetBaseMeasureFactory().RateComponentCode;
    }

    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        baseMeasurement.ValidateExchangeRate(exchangeRate, paramName);
    }
    #endregion

    #region Private methods
    private IBaseMeasureFactory GetBaseMeasureFactory()
    {
        return (IBaseMeasureFactory)GetFactory();
    }

    #region Static methods
    private static TypeCode GetQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (quantityTypeCode == TypeCode.Empty || QuantityTypeCodes.Contains(quantityTypeCode))
        {
            return quantityTypeCode;
        }

        if (quantityTypeCode.IsDefined()) return TypeCode.Object;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }
    #endregion
    #endregion
}
