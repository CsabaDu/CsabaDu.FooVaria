using CsabaDu.FooVaria.Quantifiables.Types.Implementations;

namespace CsabaDu.FooVaria.BaseMeasures.Types.Implementations
{
    public abstract class BaseMeasure : Quantifiable, IBaseMeasure
    {
        #region Constructors
        protected BaseMeasure(IBaseMeasureFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, IBaseMeasurement baseMeasurement) : base(factory, baseMeasurement)
        {
        }
        #endregion

        #region Properties
        #region Abstract properties
        public abstract object Quantity { get; init; }
        #endregion
        #endregion

        #region Public methods
        public int CompareTo(IBaseMeasure? other)
        {
            if (other == null) return 1;

            ValidateMeasureUnitCode(other.MeasureUnitCode, nameof(other));

            return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
        }

        public bool Equals(IBaseMeasure? other)
        {
            return base.Equals(other);
        }

        public IBaseMeasure? ExchangeTo(Enum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            decimal exchangeRate = GetExchangeRate(measureUnit);

            if (GetRateComponentCode() == RateComponentCode.Limit && GetDefaultQuantity() % exchangeRate > 0) return null; // ???

            IBaseMeasurementFactory factory = GetBaseMeasurementFactory();
            IBaseMeasurement baseMeasurement = factory.CreateBaseMeasurement(measureUnit)!;
            decimal quantity = GetDefaultQuantity() / exchangeRate;

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public IBaseMeasure GetBaseMeasure(ValueType quantity)
        {
            Enum measureUnit = GetMeasureUnit();
            IBaseMeasurementFactory factory = GetBaseMeasurementFactory();
            IBaseMeasurement baseMeasurement = factory.CreateBaseMeasurement(measureUnit)!;

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
        {
            return GetFactory().CreateBaseMeasure(baseMeasurement, quantity);
        }

        public decimal GetDecimalQuantity()
        {
            return (decimal)GetQuantity(TypeCode.Decimal);
        }

        public decimal GetExchangeRate()
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.GetExchangeRate();
        }

        public decimal GetExchangeRate(Enum measureUnit)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.GetExchangeRate(measureUnit);
        }

        public object GetQuantity(RoundingMode roundingMode)
        {
            decimal quantity = roundDecimalQuantity();

            return quantity.ToQuantity(GetQuantityTypeCode()) ?? throw new InvalidOperationException(null);

            #region Local methods
            decimal roundDecimalQuantity()
            {
                quantity = GetDecimalQuantity();

                return roundingMode switch
                {
                    RoundingMode.General => decimal.Round(quantity),
                    RoundingMode.Ceiling => decimal.Ceiling(quantity),
                    RoundingMode.Floor => decimal.Floor(quantity),
                    RoundingMode.Half => getHalfQuantity(),

                    _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
                };
            }

            decimal getHalfQuantity()
            {
                decimal halfQuantity = decimal.Floor(quantity);
                decimal half = 0.5m;

                if (quantity == halfQuantity) return quantity;

                halfQuantity += half;

                if (quantity <= halfQuantity) return halfQuantity;

                return halfQuantity + half;
            }
            #endregion
        }

        public object GetQuantity(TypeCode quantityTypeCode)
        {
            ValueType quantity = (ValueType)Quantity;

            return quantity.ToQuantity(quantityTypeCode) ?? throw new InvalidOperationException(null);
        }

        public TypeCode? GetQuantityTypeCode(object quantity)
        {
            TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

            return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
        }

        public bool IsExchangeableTo(Enum? context)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.IsExchangeableTo(context);
        }

        public decimal ProportionalTo(IBaseMeasure baseMeasure)
        {
            MeasureUnitCode measureUnitCode = NullChecked(baseMeasure, nameof(baseMeasure)).MeasureUnitCode;

            if (IsExchangeableTo(measureUnitCode)) return GetDefaultQuantity() / baseMeasure.GetDefaultQuantity();

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, nameof(baseMeasure));
        }

        public IBaseMeasure Round(RoundingMode roundingMode)
        {
            ValueType quantity = (ValueType)GetQuantity(roundingMode);
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public void ValidateExchangeRate(decimal exchangeRate, string paramName)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            baseMeasurement.ValidateExchangeRate(exchangeRate, paramName);
        }

        public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
        {
            ValidateQuantity(NullChecked(quantifiable, paramName).GetDefaultQuantity(), paramName);
        }

        public void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
        {
            if (GetValidQuantityTypeCodeOrNull(quantityTypeCode) != null) return;

            throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
        }

        #region Override methods
        public override IBaseMeasureFactory GetFactory()
        {
            return (IBaseMeasureFactory)Factory;
        }

        public override Enum GetMeasureUnit()
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.GetMeasureUnit();
        }

        #region Sealed methods
        #endregion
        #endregion

        #region Virtual methods
        public virtual LimitMode? GetLimitMode()
        {
            return null;
        }
        #endregion

        #region Abstract methods
        public abstract IBaseMeasurement GetBaseMeasurement();
        public abstract IBaseMeasurementFactory GetBaseMeasurementFactory();
        public abstract RateComponentCode GetRateComponentCode();
        public abstract void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme);
        #endregion
        #endregion

        #region Protected methods
        public bool Equals(IBaseMeasure? x, IBaseMeasure? y)
        {
            if (x == null && y == null) return true;

            if (x == null || y == null) return false;

            return x.GetLimitMode() == y.GetLimitMode()
                && x.GetRateComponentCode() == y.GetRateComponentCode()
                && x.Equals(y);
        }

        public int GetHashCode([DisallowNull] IBaseMeasure baseMeasure)
        {
            return HashCode.Combine(baseMeasure.GetLimitMode(), baseMeasure.GetRateComponentCode(), baseMeasure.GetHashCode());
        }

        #region Static methods
        protected static object? GetValidQuantityOrNull(IBaseMeasure baseMeasure, object? quantity)
        {
            quantity = ((ValueType?)quantity)?.ToQuantity(baseMeasure.GetQuantityTypeCode());

            return baseMeasure.GetRateComponentCode() switch
            {
                RateComponentCode.Denominator => getValidDenominatorQuantity(),
                RateComponentCode.Numerator or
                RateComponentCode.Limit => quantity,

                _ => throw new InvalidOperationException(null),
            };

            #region Local methods
            object? getValidDenominatorQuantity()
            {
                if (quantity == null || (decimal)quantity <= 0) return null;

                return quantity;
            }
            #endregion
        }
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
        {
            if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

            return null;
        }
        #endregion
        #endregion
    }

    public abstract class BaseMeasure<TSeff> : BaseMeasure, IBaseMeasure<TSeff>
        where TSeff : class, IBaseMeasure
    {
        #region Constructors
        protected BaseMeasure(IBaseMeasureFactory<TSeff> factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }
        #endregion

        #region Public methods
        public TSeff GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }

        public TSeff GetBaseMeasure(string name, ValueType quantity)
        {
            return GetFactory().Create(name, quantity);
        }

        public TSeff? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return GetFactory().Create(measureUnit, exchangeRate, quantity, customName);
        }

        public TSeff? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        #region Override methods
        public override IBaseMeasureFactory<TSeff> GetFactory()
        {
            return (IBaseMeasureFactory<TSeff>)Factory;
        }

        public TSeff GetNew(TSeff other)
        {
            return GetFactory().CreateNew(other);
        }
        #endregion
        #endregion
    }
}
