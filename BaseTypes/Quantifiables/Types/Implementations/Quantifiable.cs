namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations
{
    public abstract class Quantifiable : Measurable, IQuantifiable
    {
        #region Constructors
        #region Static constructor
        static Quantifiable()
        {
            QuantityTypeSet =
            [
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(double),
                typeof(decimal),
            ];
        }
        #endregion

        protected Quantifiable(IQuantifiable other) : base(other)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, IMeasurable measurable) : base(factory, measurable)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, IQuantifiable quantifiable) : base(factory, quantifiable)
        {
        }

        protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode)
        {
            _ = NullChecked(shapeComponents, nameof(shapeComponents));
        }
        #endregion

        #region Properties
        #region Static properties
        public static HashSet<Type> QuantityTypeSet { get; }
        #endregion
        #endregion

        #region Public methods
        #region Override methods
        public override bool Equals(object? obj)
        {
            return base.Equals(obj)
                && obj is IQuantifiable other
                && GetDefaultQuantity() == other.GetDefaultQuantity();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MeasureUnitCode, GetDefaultQuantity());
        }

        public override IQuantifiableFactory GetFactory()
        {
            return (IQuantifiableFactory)Factory;
        }
        #endregion

        #region Virtual methods
        public virtual void ValidateQuantity(ValueType? quantity, string paramName)
        {
            _ = ConvertQuantity(quantity, paramName, GetQuantityTypeCode());
        }

        public virtual void ValidateQuantity(IQuantifiable? quantifiable, string paramName)
        {
            decimal quantity = TypeChecked(quantifiable, paramName, GetType())!.GetDefaultQuantity();

            ValidateQuantity(this, quantity, paramName);
        }
        #endregion

        #region Abstract methods
        public abstract decimal GetDefaultQuantity();
        #endregion

        #region Static methods
        public static object ConvertQuantity(ValueType? quantity, string paramName, TypeCode quantityTypeCode)
        {
            object? exchanged = NullChecked(quantity, paramName).ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

            return exchanged ?? throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public static decimal GetDefaultQuantitySquare(IQuantifiable quantifiable)
        {
            decimal quantity = NullChecked(quantifiable, nameof(quantifiable)).GetDefaultQuantity();

            return quantity * quantity;
        }

        public static IEnumerable<TypeCode> GetQuantityTypeCodes()
        {
            foreach (Type item in QuantityTypeSet)
            {
                yield return Type.GetTypeCode(item);
            }
        }
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> quantifiable)
            where TNum : struct
        {
            return Type.GetTypeCode(typeof(TNum));

        }

        protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
        {
            decimal defaultQuantity = Convert.ToDecimal(quantity) * exchangeRate;

            return defaultQuantity.Round(RoundingMode.DoublePrecision);
        }

        protected static object GetQuantity<T, TNum>(T quantifiable, TypeCode quantityTypeCode)
            where T : class, IQuantifiable, IQuantity<TNum>
            where TNum : struct
        {
            ValueType quantity = (ValueType)(object)quantifiable.GetQuantity();

            return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
        }

        protected static void ValidateMeasureUnitCode(IQuantifiable quantifiable, MeasureUnitCode measureUnitCode, string paramName)
        {
            if (quantifiable.GetMeasureUnitCodes().Contains(measureUnitCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        protected static void ValidateQuantity(IQuantifiable quantifiable, ValueType? quantity, string paramName)
        {
            decimal converted = (decimal)ConvertQuantity(quantity, paramName, TypeCode.Decimal);

            if (converted > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
        #endregion
    }

    public abstract class Quantifiable<TSelf> : Quantifiable, IQuantifiable<TSelf>
        where TSelf : class, IQuantifiable
    {
        #region Constructors
        protected Quantifiable(TSelf other) : base(other)
        {
        }

        protected Quantifiable(IQuantifiableFactory<TSelf> factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
        {
        }

        protected Quantifiable(IQuantifiableFactory<TSelf> factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected Quantifiable(IQuantifiableFactory<TSelf> factory, IMeasurable measurable) : base(factory, measurable)
        {
        }

        protected Quantifiable(IQuantifiableFactory<TSelf> factory, IQuantifiable quantifiable) : base(factory, quantifiable)
        {
        }

        protected Quantifiable(IQuantifiableFactory<TSelf> factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
        {
        }
        #endregion

        #region Public methods
        public int CompareTo(TSelf? other)
        {
            if (other == null) return 1;

            ValidateMeasureUnitCode(other.MeasureUnitCode, nameof(other));

            return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
        }

        public bool Equals(TSelf? other)
        {
            return base.Equals(other);
        }

        public TSelf GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
        {
            return GetFactory().CreateQuantifiable(measureUnitCode, defaultQuantity);
        }

        public bool IsExchangeableTo(Enum? context)
        {
            if (context == null) return false;

            if (context is not MeasureUnitCode measureUnitCode)
            {
                if (!IsDefinedMeasureUnit(context)) return false;

                measureUnitCode = GetDefinedMeasureUnitCode(context);
            }

            return HasMeasureUnitCode(measureUnitCode);
        }

        public decimal ProportionalTo(TSelf? other)
        {
            string paramName = nameof(other);

            ValidateMeasureUnitCode(other, paramName);

            decimal defaultQuantity = other!.GetDefaultQuantity();

            if (defaultQuantity != 0) return Math.Abs(GetDefaultQuantity() / defaultQuantity);

            throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);
        }

        public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
        {
            ValidateMeasureUnitCode(quantifiable, paramName);
            ValidateQuantity(quantifiable, paramName);
        }

        #region Override methods
        public override IQuantifiableFactory<TSelf> GetFactory()
        {
            return (IQuantifiableFactory<TSelf>)Factory;
        }

        #region Sealed methods
        public override sealed TypeCode GetQuantityTypeCode()
        {
            return base.GetQuantityTypeCode();
        }

        public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        {
            base.ValidateMeasureUnitCode(measureUnitCode, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract TSelf? ExchangeTo(Enum? context);
        public abstract bool? FitsIn(TSelf? other, LimitMode? limitMode);
        #endregion
        #endregion
    }
}

