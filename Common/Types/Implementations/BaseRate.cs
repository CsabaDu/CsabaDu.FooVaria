namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseRate : BaseMeasure<IBaseRate, IMeasurable>, IBaseRate
    {
        #region Constructors
        public BaseRate(IBaseRate other) : base(other)
        {
        }

        public BaseRate(IBaseRateFactory factory, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
        {
        }

        public BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        public BaseRate(IBaseRateFactory factory, IBaseMeasure denominator) : base(factory, denominator)
        {
        }
        #endregion

        #region Public methods
        public override int CompareTo(IBaseRate? other)
        {
            if (other == null) return 1;

            if (AreExchangeables(this, other)) return DefaultQuantity.CompareTo(other.GetDefaultQuantity());

            throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
        }

        public override bool Equals(IBaseRate? other)
        {
            return AreExchangeables(this, other)
                && other!.DefaultQuantity == DefaultQuantity;
        }
    
        public decimal GetQuantity()
        {
            return DefaultQuantity;
        }

        public override decimal ProportionalTo(IBaseRate other)
        {
            string name = nameof(other);
            decimal quantity = NullChecked(other, name).GetDefaultQuantity();

            if (quantity == 0) throw QuantityArgumentOutOfRangeException(name, quantity);

            if (AreExchangeables(this, other)) return Math.Abs(DefaultQuantity / quantity);

            throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, name);
        }

        #region Override methods
        public override bool Equals(object? obj)
        {
            return obj is IBaseRate baseRate && Equals(baseRate);
        }

        public override IBaseRateFactory GetFactory()
        {
            return (IBaseRateFactory)Factory;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode, GetNumeratorMeasureUnitTypeCode());
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            return GetQuantityTypeCode(this);
        }

        public override sealed void Validate(IRootObject? rootObject, string paramName)
        {
            Validate(this, rootObject, validateBaseRate, paramName);

            #region Local methods
            void validateBaseRate()
            {
                _ = GetValidBaseRate(this, rootObject!, paramName);
            }
            #endregion
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            base.ValidateMeasureUnit(measureUnit, paramName);
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return GetNumeratorMeasureUnitTypeCode();
            yield return MeasureUnitTypeCode;
        }

        public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
        {
            if (GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);
        }
        #endregion

        #region Abstract methods
        public abstract IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator);
        public abstract IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit);
        public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
        public abstract IQuantifiable Multiply(IBaseMeasure multiplier);
        //public abstract void ValidateQuantity(ValueType? quantity, string paramName);
        #endregion

        #region Static methods
        public static bool AreExchangeables(IBaseRate baseRate, IMeasurable? baseMeasurable)
        {
            if (baseMeasurable is not IBaseRate other) return baseMeasurable?.HasMeasureUnitTypeCode(baseRate.MeasureUnitTypeCode) == true;

            return AreExchangeables(baseRate, other);
        }

        public static bool AreExchangeables(IBaseRate? baseRate, IBaseRate? other)
        {
            if (baseRate == null || other == null) return false;

            return baseRate.MeasureUnitTypeCode == other.MeasureUnitTypeCode
                && baseRate.GetNumeratorMeasureUnitTypeCode() == other.GetNumeratorMeasureUnitTypeCode();
        }

        public static int Compare(IBaseRate? baseRate, IBaseRate? other)
        {
            if (baseRate == null && other == null) return 0;

            if (baseRate == null) return -1;

            return baseRate.CompareTo(other);
        }

        public static bool Equals(IBaseRate baseRate, IBaseRate? other)
        {
            return baseRate?.Equals(other) == true;
        }

        public static decimal Proportionals(IBaseRate baseRate, IBaseRate other)
        {
            return NullChecked(baseRate, nameof(baseRate)).ProportionalTo(other);
        }
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static T GetValidBaseRate<T>(T commonBase, IRootObject other, string paramName)
            where T : class, IBaseRate
        {
            T baseRate = GetValidMeasurable(commonBase, other, paramName);

            commonBase.ValidateQuantity(baseRate.GetDefaultQuantity(), paramName);

            MeasureUnitTypeCode measureUnitTypeCode = commonBase.GetNumeratorMeasureUnitTypeCode();
            MeasureUnitTypeCode otherMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();

            _ = GetValidBaseMeasurable(baseRate, measureUnitTypeCode, otherMeasureUnitTypeCode, paramName);

            return baseRate;
        }
        #endregion
        #endregion

        #region Private methods
        private ArgumentOutOfRangeException BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(IBaseRate baseRate, string name)
        {
            MeasureUnitTypeCode measureUnitTypeCode = baseRate.MeasureUnitTypeCode;

            if (!HasMeasureUnitTypeCode(measureUnitTypeCode))
            {
                throw exception();
            }
            else
            {
                measureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();

                throw exception();
            }

            #region Local methods
            ArgumentOutOfRangeException exception()
            {
                return new ArgumentOutOfRangeException(name, measureUnitTypeCode, null);
            }
            #endregion
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnitTypeCode);
        }

        public abstract IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
        //{
        //    throw new NotImplementedException();
        //}

        public abstract IBaseRate GetBaseRate(IBaseMeasure numerator, IMeasurable denominator);
        #endregion
    }
}

