namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IBaseRateFactory factory, MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
        {
            NumeratorMeasureUnitCode = Defined(numeratorMeasureUnitCode, nameof(numeratorMeasureUnitCode));
            DefaultQuantity = GetValidDecimalQuantity(defaultQuantity, nameof(defaultQuantity));
        }

        private protected Proportion(IBaseRateFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
        {
            NumeratorMeasureUnitCode = getValidMeasureUnitCode();
            DefaultQuantity = getValidDefaultQuantity();

            #region Local methods
            MeasureUnitCode getValidMeasureUnitCode()
            {
                string paramName = nameof(numeratorMeasureUnit);

                return IsValidMeasureUnit(NullChecked(numeratorMeasureUnit, paramName)) ?
                    GetDefinedMeasureUnitCode(numeratorMeasureUnit)
                    : throw InvalidMeasureUnitEnumArgumentException(numeratorMeasureUnit, paramName);
            }

            decimal getValidDefaultQuantity()
            {
                return GetValidDecimalQuantity(quantity, nameof(quantity))
                    * GetExchangeRate(numeratorMeasureUnit)
                    / GetExchangeRate(denominatorMeasureUnit);
            }
            #endregion
        }

        private protected Proportion(IBaseRateFactory factory, IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
        {
            NumeratorMeasureUnitCode = NullChecked(numerator, nameof(numerator)).MeasureUnitCode;
            DefaultQuantity = numerator.GetDefaultQuantity() / denominatorMeasurement.GetExchangeRate();

        }

        private protected Proportion(IBaseRateFactory factory, IBaseMeasure numerator, IBaseMeasure denominator) : base(factory, denominator)
        {
            NumeratorMeasureUnitCode = NullChecked(numerator, nameof(numerator)).MeasureUnitCode;
            DefaultQuantity = numerator.GetDefaultQuantity() / denominator.GetDefaultQuantity();
        }

        private protected Proportion(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            NumeratorMeasureUnitCode = baseRate.GetNumeratorMeasureUnitCode();
            DefaultQuantity = baseRate.GetDefaultQuantity();
        }

        private protected Proportion(IProportion other) : base(other)
        {
            NumeratorMeasureUnitCode = other.NumeratorMeasureUnitCode;
            DefaultQuantity = other.DefaultQuantity;
        }

        #endregion

        #region Properties
        public MeasureUnitCode NumeratorMeasureUnitCode { get; init; }
        public  decimal DefaultQuantity { get; init; }

        public override sealed Enum? this[RateComponentCode rateComponentCode] => (Enum?)base[rateComponentCode];
        #endregion

        #region Public methods
        public IProportion ConvertToLimitable(ILimiter limiter)
        {
            string paramName = nameof(limiter);

            if (NullChecked(limiter, paramName) is IBaseRate baseRate) return GetProportion(baseRate);

            throw ArgumentTypeOutOfRangeException(paramName, limiter);
        }

        #region Override methods
        #region Sealed methods
        public override sealed decimal GetDefaultQuantity()
        {
            return DefaultQuantity;
        }

        public override sealed Enum GetMeasureUnit()
        {
            return NumeratorMeasureUnitCode.GetDefaultMeasureUnit();
        }

        public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            return base.GetMeasureUnitCodes();
        }

        public override sealed MeasureUnitCode GetNumeratorMeasureUnitCode()
        {
            return NumeratorMeasureUnitCode;
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract IProportion GetProportion(IBaseRate baseRate);
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static decimal GetValidDecimalQuantity(ValueType? quantity, string paramName)
        {
            decimal converted = (decimal)ConvertQuantity(quantity, paramName, TypeCode.Decimal);

            if (converted > 0) return converted;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
        #endregion
    }

    internal abstract class Proportion<TDEnum> : Proportion, IProportion<TDEnum>
        where TDEnum : struct, Enum
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
        }
        #endregion

        #region Public methods
        public IMeasure Denominate(TDEnum measureUnit)
        {
            decimal quantity = GetQuantity(measureUnit);
            IMeasureFactory factory = GetFactory().MeasureFactory;

            return factory.Create(measureUnit, quantity);
        }

        public IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TDEnum denominatorMeasureUnit)
        {
            return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public override sealed IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
        {
            return GetFactory().Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode);
        }

        public override sealed IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }
        #endregion
        #endregion
        #endregion
    }

    internal sealed class Proportion<TNEnum, TDEnum> : Proportion<TDEnum>, IProportion<TNEnum, TDEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        #region Constructors
        internal Proportion(IProportionFactory factory, TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit) : base(factory, numeratorMeasureUnit, quantity, denominatorMeasureUnit)
        {
        }
        #endregion

        #region Public methods
        public TNEnum GetMeasureUnit(IMeasureUnit<TNEnum>? other)
        {
            if (other == null) return default;

            return (TNEnum)other.GetMeasureUnit();
        }

        public IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        {
            return GetFactory().Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit)
        {
            return GetQuantity(denominatorMeasureUnit) * GetExchangeRate(numeratorMeasureUnit);
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit)
        {
            return DefaultQuantity * GetExchangeRate(numeratorMeasureUnit);
        }
        #endregion
    }
}
