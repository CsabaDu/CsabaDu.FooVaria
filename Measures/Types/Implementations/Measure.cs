namespace CsabaDu.FooVaria.Measures.Types.Implementations
{
    internal abstract class Measure : BaseMeasure<IMeasure>, IMeasure
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit)
        {
            ValidateQuantity(quantity, nameof(quantity));

            Measurement = GetBaseMeasurementFactory().Create(measureUnit);
            Quantity = quantity;
        }
        #endregion

        public IMeasurement Measurement { get; init; }
        public override sealed object Quantity { get; init; }
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
        }

        public IMeasure ConvertLimiter(ILimiter limiter)
        {
            MeasureUnitCode measureUnitCode = NullChecked(limiter, nameof(limiter)).GetLimiterMeasureUnitCode();
            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
            decimal defaultQuantity = limiter.GetLimiterDefaultQuantity();

            return GetBaseMeasure(measureUnit, defaultQuantity);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw DecimalArgumentOutOfRangeException(nameof(divisor), divisor);

            decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

            return (IMeasure)GetBaseMeasure(quantity);
        }

        public bool? FitsIn(ILimiter? limiter)
        {
            if (limiter == null) return null;
            LimitMode limitMode = limiter.LimitMode;

            if (limiter is not IBaseMeasure baseMeasure)
            {
                baseMeasure = ConvertLimiter(limiter);
            }

            return FitsIn(baseMeasure, limitMode);
        }

        public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
        {
            bool isLimitModeNull = limitMode == null;

            if (isRateComponentNull() && isLimitModeNull) return true;

            if (baseMeasure?.HasMeasureUnitCode(MeasureUnitCode) != true) return null;

            if (isLimitModeNull) return CompareTo(baseMeasure) <= 0;

            IBaseMeasure ceilingBaseMeasure = baseMeasure.Round(RoundingMode.Ceiling);
            baseMeasure = getRoundedBaseMeasure();

            if (isRateComponentNull()) return null;

            int comparison = CompareTo(baseMeasure);

            return Defined(limitMode!.Value, nameof(limitMode)) switch
            {
                LimitMode.BeEqual => comparison == 0 && ceilingBaseMeasure.Equals(baseMeasure),

                _ => comparison.FitsIn(limitMode),
            };

            #region Local methods
            bool isRateComponentNull()
            {
                return baseMeasure == null;
            }

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

        public bool? FitsIn(IQuantifiable? comparable, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetFactory().MeasurementFactory;
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetDefaultQuantity(Quantity, GetExchangeRate());
        }

        public override IMeasureFactory GetFactory()
        {
            return (IMeasureFactory)Factory;
        }

        public override sealed RateComponentCode GetRateComponentCode()
        {
            return RateComponentCode.Numerator;
        }

        public IMeasure Multiply(decimal multiplier)
        {
            decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

            return (IMeasure)GetBaseMeasure(quantity);

        }
        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(other, SummingMode.Subtract);
        }

        public override void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme)
        {
            throw new NotImplementedException();
        }

        public override void ValidateQuantity(ValueType? quantity, string paramName)
        {
            throw new NotImplementedException();
        }

        #region Private methods
        private IMeasure GetSum(IMeasure? other, SummingMode summingMode)
        {
            if (other == null) return GetNew(this);

            if (other.IsExchangeableTo(MeasureUnitCode)) return getMeasure();

            throw InvalidMeasureUnitCodeEnumArgumentException(other.MeasureUnitCode, nameof(other));

            #region Local methods
            decimal getDefaultQuantitySum()
            {
                decimal otherQuantity = other!.GetDefaultQuantity();

                return summingMode switch
                {
                    SummingMode.Add => decimal.Add(GetDefaultQuantity(), otherQuantity),
                    SummingMode.Subtract => decimal.Subtract(GetDefaultQuantity(), otherQuantity),

                    _ => throw new InvalidOperationException(null),
                };
            }

            IMeasure getMeasure()
            {
                Enum measureUnit = Measurement.GetMeasureUnit();
                decimal quantity = getDefaultQuantitySum() / GetExchangeRate();

                return GetBaseMeasure(measureUnit, quantity);
            }
            #endregion
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public TSelf GetBaseMeasure(TNum quantity)
        {
            return GetMeasure(Measurement, quantity);
        }

        public TSelf? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return (TSelf?)GetFactory().CreateDefault(measureUnitCode);
        }

        public TSelf GetDefault()
        {
            return GetDefault(MeasureUnitCode)!;
        }

        public TSelf GetMeasure(string name, TNum quantity)
        {
            return (TSelf)GetFactory().Create(name, quantity);
        }

        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            return (TSelf)GetFactory().CreateBaseMeasure(measurement, quantity);
        }

        public TNum GetQuantity()
        {
            return (TNum)GetQuantity(GetQuantityTypeCode());
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure<TSelf, TNum, TEnum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
        where TNum : struct
        where TEnum : struct, Enum
    {
        #region Enums
        protected enum ConvertMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constants
        private const decimal ConvertRatio = 1000m;
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, TEnum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            return (TSelf)GetBaseMeasure(measureUnit, quantity);
        }

        public TSelf GetMeasure(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

        public TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other)
        {
            return (TEnum)(other ?? this).GetMeasureUnit();
        }
        #endregion

        #region Protected methods
        protected TOther ConvertMeasure<TOther>(ConvertMode convertMode)
            where TOther : IMeasure, IConvertMeasure
        {
            MeasureUnitCode measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(typeof(TOther));
            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
            decimal quantity = convertMode switch
            {
                ConvertMode.Multiply => GetDefaultQuantity() * ConvertRatio,
                ConvertMode.Divide => GetDefaultQuantity() / ConvertRatio,

                _ => throw new InvalidOperationException(null),
            };

            return (TOther)GetBaseMeasure(measureUnit, quantity);
        }

        protected void ValidateSpreadQuantity(ValueType? quantity, string paramName)
        {
            if (GetValidQuantityOrNull(this, NullChecked(quantity, paramName)) is double spreadQuantity && spreadQuantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        protected void ValidateSpreadMeasure(string paramName, ISpreadMeasure? spreadMeasure)
        {
            if (NullChecked(spreadMeasure, paramName).GetSpreadMeasure() is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
            }

            ValidateMeasureUnitCode(measure.MeasureUnitCode, paramName);

            decimal quantity = measure.GetDecimalQuantity();

            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
    }
}
