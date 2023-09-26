using System.ComponentModel;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Measure : BaseMeasure, IMeasure
{
    #region Enums
    protected enum ConvertMode
    {
        Multiply,
        Divide,
    }
    #endregion

    #region Constants
    private const int DefaultMeasureQuantity = 0;
    #endregion

    #region Constructors
    private protected Measure(IMeasure other) : base(other)
    {
    }

    private protected Measure(IMeasureFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IMeasure Add(IMeasure? other)
    {
        return GetSum(this, other, SummingMode.Add);
    }

    public IMeasure Divide(decimal divisor)
    {
        if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

        decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

        return GetMeasure(quantity);
    }

    public bool? FitsIn(ILimit? limit)
    {
        return FitsIn(limit, limit?.LimitMode);
    }

    public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
    {
        if (baseMeasure == null && limitMode == null) return true;

        if (baseMeasure?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

        limitMode ??= default;
        IBaseMeasure ceilingBaseMeasure = baseMeasure.Round(RoundingMode.Ceiling);
        baseMeasure = getRoundedBaseMeasure();

        if (baseMeasure == null) return null;

        int comparison = CompareTo(baseMeasure);

        return limitMode switch
        {
            LimitMode.BeEqual => comparison == 0 && ceilingBaseMeasure.Equals(baseMeasure),

            _ => comparison.FitsIn(limitMode),
        };

        #region Local methods
        IBaseMeasure? getRoundedBaseMeasure()
        {
            return limitMode switch
            {
                LimitMode.BeNotLess or
                LimitMode.BeGreater => ceilingBaseMeasure,

                LimitMode.BeNotGreater or
                LimitMode.BeLess or
                LimitMode.BeEqual => baseMeasure!.Round(RoundingMode.Floor),

                _ => null,
            };
        }
        #endregion
    }

    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetMeasureFactory().Create(quantity, measureUnit);
    }

    public IMeasure GetMeasure(ValueType quantity, string name)
    {
        return GetMeasureFactory().Create(quantity, name);
    }

    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasureFactory().Create(quantity, measureUnit, exchangeRate, customName);
    }

    public IMeasure GetMeasure(ValueType quantity, string customName, decimal exchangeRate)
    {
        return GetMeasureFactory().Create(quantity, customName, MeasureUnitTypeCode, exchangeRate);
    }

    public IMeasure GetMeasure(ValueType quantity, IMeasurement measurement)
    {
        return GetMeasureFactory().Create(quantity, measurement);
    }

    public IMeasure GetMeasure(IMeasure other)
    {
        return GetMeasureFactory().Create(other);
    }

    public IMeasure GetMeasure(ValueType quantity)
    {
        if (quantity is Enum measureUnit) return GetMeasure(measureUnit);

        return GetMeasure(quantity, Measurement);
    }

    public IMeasure GetMeasure(Enum measureUnit)
    {
        IBaseMeasure excchanged = ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        return GetMeasure(excchanged);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return MeasurableFactory as IMeasureFactory ?? throw new InvalidOperationException(null);
    }

    #region Overriden methods
    public override sealed LimitMode? GetLimitMode()
    {
        return null;
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public IMeasure Multiply(decimal multiplier)
    {
        decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

        return GetMeasure(quantity);
    }

    public IMeasure Subtract(IMeasure? other)
    {
        return GetSum(this, other, SummingMode.Subtract);
    }

    public override sealed bool Equals(IBaseMeasure? other)
    {
        return Equals(this, other);
    }

    public override sealed bool Equals(object? obj)
    {
        return obj is IMeasure measure && Equals(measure);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetMeasure(quantity, measureUnit);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measurement)
    {
        return GetMeasure(quantity, measurement);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetMeasure(quantity, measureUnitTypeCode, exchangeRate, customName);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetMeasure(quantity, name);
    }

    public override sealed ValueType GetDefaultRateComponentQuantity()
    {
        return DefaultMeasureQuantity;
    }

    public override sealed int GetHashCode()
    {
        return GetHashCode(this);
    }
    #endregion

    #region Abstract methods
    public abstract IMeasure GetMeasure(IBaseMeasure baseMeasure);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static T GetMeasure<T, U>(T measure, U quantity, string name) where T : class, IMeasure where U : struct
    {
        validateName();

        return (T)measure.GetMeasure(quantity, name);

        #region Local methods
        void validateName()
        {
            if (MeasureUnitTypes.GetDefaultNames(measure.MeasureUnitTypeCode).Contains(NullChecked(name, nameof(name)))) return;

            if (measure.Measurement.GetCustomNameCollection(measure.MeasureUnitTypeCode).Values.Contains(name)) return;

            throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
        #endregion
    }

    protected static T GetMeasure<T, U>(T measure, U quantity, IMeasurement measurement) where T : class, IMeasure where U : struct
    {
        ValidateMeasurabe(measurement, nameof(measurement));

        return (T)measure.GetMeasure(quantity, measurement);
    }

    protected static T GetMeasure<T>(T measure, IBaseMeasure baseMeasure) where T : class, IMeasure
    {
        ValidateMeasurabe(baseMeasure, nameof(baseMeasure));

        return (T)measure.GetMeasure(baseMeasure);
    }

    protected static T GetMeasure<T>(T measure, T other) where T : class, IMeasure
    {
        return (T)measure.GetMeasure(other);
    }

    protected static T GetMeasure<T, U, V>(T measure, U quantity, V measureUnit) where T : class, IMeasure where U : struct where V : struct, Enum
    {
        return (T)measure.GetMeasure(quantity, measureUnit);
    }

    protected static T GetMeasure<T, U, V>(T measure, U quantity, V measureUnit, decimal exchangeRate, string customName) where T : class, IMeasure where U : struct where V : struct, Enum
    {
        return (T)measure.GetMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    protected static T GetMeasure<T, U>(T measure, U quantity, string customName, decimal exchangeRate) where T : class, IMeasure where U : struct
    {
        return (T)measure.GetMeasure(quantity, customName, exchangeRate);
    }

    protected static T GetMeasure<T, U>(T measure, U quantity) where T : class, IMeasure where U : struct
    {
        return (T)measure.GetMeasure(quantity);
    }

    protected static T ConvertMeasure<T, V>(IMeasure measure, ConvertMode convertMode) where T : class, IMeasure, IConvertMeasure where V : struct, Enum
    {
        decimal quantity = measure.DefaultQuantity;
        decimal ratio = 1000;
        quantity = convertMode switch
        {
            ConvertMode.Multiply => quantity * ratio,
            ConvertMode.Divide => quantity / ratio,

            _ => throw new InvalidOperationException(null),
        };

        return (T)measure.GetMeasure(quantity, default(V));
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static void ValidateMeasurabe<T>(T measurable, string measurableName) where T : class, IMeasurable, IRateComponent
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurable, measurableName).MeasureUnitTypeCode;

        try
        {
            measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
        }
        catch (InvalidEnumArgumentException)
        {
            throw new ArgumentOutOfRangeException(measurableName, measureUnitTypeCode, null);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex.InnerException);
        }
    }
    #endregion
    #endregion
}
