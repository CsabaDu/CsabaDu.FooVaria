namespace CsabaDu.FooVaria.Common.Types.Implementations
{
    public abstract class BaseRate : BaseMeasure, IBaseRate
    {
        #region Constructors
        //public decimal DefaultQuantity { get; }

        public BaseRate(IBaseRate other) : base(other)
        {
            //DefaultQuantity = other.DefaultQuantity;
        }

        public BaseRate(IBaseRateFactory factory, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            //DefaultQuantity = defaultQuantity;
        }

        public BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            //DefaultQuantity = baseRate.DefaultQuantity;
        }

        public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numerator, denominatorMeasureUnitTypeCode)
        {
            //DefaultQuantity = NullChecked(numerator, nameof(numerator)).GetDefaultQuantity();
        }

        public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, IBaseMeasure denominator) : base(factory, denominator)
        {
            //DefaultQuantity = NullChecked(numerator, nameof(numerator)).GetDefaultQuantity();
        }

        public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, Enum denominatorMeasureUnit) : base(factory, numerator, denominatorMeasureUnit)
        {
            //DefaultQuantity = NullChecked(numerator, nameof(numerator)).GetDefaultQuantity();
        }
        #endregion

        #region Public methods
        public int CompareTo(IBaseRate? other)
        {
            if (other == null) return 1;

            if (AreExchangeables(this, other)) return DefaultQuantity.CompareTo(other.GetDefaultQuantity());

            throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
        }

        public bool Equals(IBaseRate? other)
        {
            return AreExchangeables(this, other)
                && other!.GetDefaultQuantity() == DefaultQuantity;
        }
    
        public decimal GetQuantity()
        {
            return DefaultQuantity;
        }

        public decimal ProportionalTo(IBaseRate other)
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
            return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode/*, NumeratorMeasureUnitTypeCode*/);
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
            if (baseMeasurable is not IBaseRate other) return baseMeasurable?.IsExchangeableTo(baseRate.MeasureUnitTypeCode) == true;

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
        protected static T GetValidBaseRate<T>(T commonBase, IRootObject other, string paramName) where T : class, IBaseRate
        {
            T baseRate = GetValidBaseMeasurable(commonBase, other, paramName);

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

            if (!IsExchangeableTo(measureUnitTypeCode))
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
            return GetFactory().Create(numerator, denominatorMeasureUnitTypeCode);
        }

        public abstract IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit);
        //{
        //    throw new NotImplementedException();
        //}

        public IBaseRate GetBaseRate(IBaseMeasure numerator, IMeasurable denominator)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public abstract class BaseMeasure : Measurable, IBaseMeasure
    {
        #region Enums
        protected enum SummingMode
        {
            Add,
            Subtract,
        }
        #endregion

        protected BaseMeasure(IBaseMeasure other) : base(other)
        {
            DefaultQuantity = other.DefaultQuantity;
        }

        protected BaseMeasure(IMeasurableFactory factory, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
            ValidateQuantity(defaultQuantity, nameof(defaultQuantity));

            DefaultQuantity = defaultQuantity;
        }

        protected BaseMeasure(IMeasurableFactory factory, IQuantifiable quantifiable, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
            DefaultQuantity = GetValidDefaultQuantity(quantifiable, nameof(quantifiable));
        }

        protected BaseMeasure(IMeasurableFactory factory, IQuantifiable quantifiable, Enum measureUnit) : base(factory, measureUnit)
        {
            DefaultQuantity = GetValidDefaultQuantity(quantifiable, nameof(quantifiable));
        }

        protected BaseMeasure(IMeasurableFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
        {
            DefaultQuantity = GetValidDefaultQuantity(baseMeasure, nameof(baseMeasure));
        }

        public decimal DefaultQuantity { get; init; }

        public decimal GetDefaultQuantity()
        {
            return DefaultQuantity;
        }

        public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
        {
            ValidateQuantity(NullChecked(quantifiable, paramName).GetDefaultQuantity(), paramName);
        }

        public abstract void ValidateQuantity(ValueType? quantity, string paramName);

        private decimal GetValidDefaultQuantity(IQuantifiable? quantifiable, string paramName)
        {
            ValidateQuantifiable(quantifiable, paramName);

            return quantifiable!.GetDefaultQuantity();
        }
    }
}

