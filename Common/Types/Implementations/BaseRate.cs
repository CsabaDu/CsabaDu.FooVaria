namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseRate : BaseMeasure, IBaseRate
    {
        #region Constructors
        protected BaseRate(IBaseRate other) : base(other)
        {
        }

        protected BaseRate(IBaseRateFactory factory, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
        {
        }

        protected BaseRate(IBaseRateFactory factory, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
        {
        }

        protected BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        protected BaseRate(IBaseRateFactory factory, IBaseMeasure denominator) : base(factory, denominator)
        {
        }
        #endregion

        #region Public methods
        //public override int CompareTo(IBaseRate? other)
        //{
        //    if (other == null) return 1;

        //    if (IsExchangeableTo(other)) return DefaultQuantity.CompareTo(other.GetDefaultQuantity());

        //    throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
        //}

        //public override bool Equals(IBaseRate? other)
        //{
        //    return IsExchangeableTo(other)
        //        && other!.DefaultQuantity == DefaultQuantity;
        //}

        public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasure denominator)
        {
            return GetFactory().CreateBaseRate(numerator, denominator);
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorMeasurement);
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnit);
        }

        public decimal GetQuantity()
        {
            return DefaultQuantity;
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

        //public override sealed bool IsExchangeableTo(IMeasurable? measurable)
        //{
        //    if (measurable is not IBaseRate other) return measurable?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) == true;

        //    return MeasureUnitTypeCode == other.MeasureUnitTypeCode
        //        && GetNumeratorMeasureUnitTypeCode() == other.GetNumeratorMeasureUnitTypeCode();
        //}

        //public override decimal ProportionalTo(IBaseRate other)
        //{
        //    string name = nameof(other);
        //    decimal quantity = NullChecked(other, name).GetDefaultQuantity();

        //    if (quantity == 0) throw QuantityArgumentOutOfRangeException(name, quantity);

        //    if (IsExchangeableTo(other)) return Math.Abs(DefaultQuantity / quantity);

        //    throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, name);
        //}

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
        public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
        public abstract IQuantifiable Multiply(IBaseMeasure multiplier);
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

        public abstract IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures);
        #endregion
    }
}

