namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations
{
    public abstract class BaseQuantifiable : Measurable, IBaseQuantifiable
    {
        #region Constructors
        #region Static constructor
        static BaseQuantifiable()
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

        protected BaseQuantifiable(IBaseQuantifiable other) : base(other)
        {
        }

        protected BaseQuantifiable(IBaseQuantifiableFactory factory) : base(factory)
        {
        }

        //protected BaseQuantifiable(IBaseQuantifiableFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
        //{
        //}

        //protected BaseQuantifiable(IBaseQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
        //{
        //}

        //protected BaseQuantifiable(IBaseQuantifiableFactory factory, IMeasurable measurable) : base(factory, measurable)
        //{
        //}

        //protected BaseQuantifiable(IBaseQuantifiableFactory factory, IBaseQuantifiable baseQuantifiable) : base(factory, baseQuantifiable)
        //{
        //}

        //protected BaseQuantifiable(IBaseQuantifiableFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode)
        //{
        //    _ = NullChecked(shapeComponents, nameof(shapeComponents));
        //}
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
                && obj is IBaseQuantifiable other
                && GetDefaultQuantity() == other.GetDefaultQuantity();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetMeasureUnitCode(), GetDefaultQuantity());
        }

        public override IBaseQuantifiableFactory GetFactory()
        {
            return (IBaseQuantifiableFactory)Factory;
        }
        #endregion

        #region Virtual methods
        public virtual void ValidateQuantity(ValueType? quantity, string paramName)
        {
            _ = ConvertQuantity(quantity, paramName, GetQuantityTypeCode());
        }

        public virtual void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
        {
            decimal quantity = TypeChecked(baseQuantifiable, paramName, GetType())!.GetDefaultQuantity();

            ValidatePositiveQuantity(quantity, paramName);
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

        public static decimal GetDefaultQuantitySquare(IBaseQuantifiable baseQuantifiable)
        {
            decimal quantity = NullChecked(baseQuantifiable, nameof(baseQuantifiable)).GetDefaultQuantity();

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
        protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> baseQuantifiable)
            where TNum : struct
        {
            return Type.GetTypeCode(typeof(TNum));

        }

        protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
        {
            decimal defaultQuantity = Convert.ToDecimal(quantity) * exchangeRate;

            return defaultQuantity.Round(RoundingMode.DoublePrecision);
        }

        protected static object GetQuantity<T, TNum>(T baseQuantifiable, TypeCode quantityTypeCode)
            where T : class, IBaseQuantifiable, IQuantity<TNum>
            where TNum : struct
        {
            ValueType quantity = (ValueType)(object)baseQuantifiable.GetQuantity();

            return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
        }

        protected static void ValidateMeasureUnitCode(IBaseQuantifiable baseQuantifiable, MeasureUnitCode measureUnitCode, string paramName)
        {
            if (baseQuantifiable.GetMeasureUnitCodes().Contains(measureUnitCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
        }

        protected static void ValidatePositiveQuantity(ValueType? quantity, string paramName)
        {
            _ = GetValidPositiveQuantity(quantity, paramName);
        }

        protected static decimal GetValidPositiveQuantity(ValueType? quantity, string paramName)
        {
            decimal converted = (decimal)ConvertQuantity(quantity, paramName, TypeCode.Decimal);

            if (converted > 0) return converted;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
        #endregion
    }

    //public abstract class Quantifiable<TSelf> : BaseQuantifiable, IQuantifiable<TSelf>
    //    where TSelf : class, IBaseQuantifiable
    //{
    //    #region Constructors
    //    protected Quantifiable(TSelf other) : base(other)
    //    {
    //        //MeasureUnitCode = other.GetMeasureUnitCode();
    //    }

    //    protected Quantifiable(IQuantifiableFactory<TSelf> factory) : base(factory)
    //    {
    //    }

    //    //protected BaseQuantifiable(IBaseQuantifiableFactory<TSelf> factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
    //    //{
    //    //    //MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
    //    //}

    //    //protected BaseQuantifiable(IBaseQuantifiableFactory<TSelf> factory, Enum measureUnit) : base(factory, measureUnit)
    //    //{
    //    //    //MeasureUnitCode = GetDefinedMeasureUnitCode(measureUnit);
    //    //}

    //    //protected BaseQuantifiable(IBaseQuantifiableFactory<TSelf> factory, IMeasurable measurable) : base(factory, measurable)
    //    //{
    //    //    //MeasureUnitCode = NullChecked(measurable, nameof(measurable)).GetMeasureUnitCode();
    //    //}

    //    //protected BaseQuantifiable(IBaseQuantifiableFactory<TSelf> factory, IBaseQuantifiable baseQuantifiable) : base(factory, baseQuantifiable)
    //    //{
    //    //    //MeasureUnitCode = NullChecked(baseQuantifiable, nameof(baseQuantifiable)).GetMeasureUnitCode();
    //    //}

    //    //protected BaseQuantifiable(IBaseQuantifiableFactory<TSelf> factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    //    //{
    //    //    //MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
    //    //}
    //    #endregion

    //    #region Properties
    //    //public MeasureUnitCode MeasureUnitCode { get; init; }
    //    #endregion

    //    #region Public methods
    //    public int CompareTo(TSelf? other)
    //    {
    //        if (other == null) return 1;

    //        ValidateMeasureUnitCode(other.GetMeasureUnitCode(), nameof(other));

    //        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    //    }

    //    public bool Equals(TSelf? other)
    //    {
    //        return base.Equals(other);
    //    }

    //    public TSelf GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    //    {
    //        return GetFactory().CreateQuantifiable(measureUnitCode, defaultQuantity);
    //    }

    //    public bool IsExchangeableTo(Enum? context)
    //    {
    //        if (context == null) return false;

    //        if (context is not MeasureUnitCode measureUnitCode)
    //        {
    //            if (!IsDefinedMeasureUnit(context)) return false;

    //            measureUnitCode = GetDefinedMeasureUnitCode(context);
    //        }

    //        return HasMeasureUnitCode(measureUnitCode);
    //    }

    //    public decimal ProportionalTo(TSelf? other)
    //    {
    //        string paramName = nameof(other);

    //        ValidateMeasureUnitCode(other, paramName);

    //        decimal defaultQuantity = other!.GetDefaultQuantity();

    //        if (defaultQuantity != 0) return Math.Abs(GetDefaultQuantity() / defaultQuantity);

    //        throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);
    //    }

    //    public void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable, string paramName)
    //    {
    //        ValidateMeasureUnitCode(baseQuantifiable, paramName);
    //        ValidateQuantity(baseQuantifiable, paramName);
    //    }

    //    #region Override methods
    //    public override IQuantifiableFactory<TSelf> GetFactory()
    //    {
    //        return (IQuantifiableFactory<TSelf>)Factory;
    //    }

    //    #region Sealed methods
    //    public override sealed MeasureUnitCode GetMeasureUnitCode()
    //    {
    //        return base.GetMeasureUnitCode();
    //    }

    //    public override sealed TypeCode GetQuantityTypeCode()
    //    {
    //        return base.GetQuantityTypeCode();
    //    }

    //    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    //    {
    //        base.ValidateMeasureUnitCode(measureUnitCode, paramName);
    //    }
    //    #endregion
    //    #endregion

    //    #region Abstract methods
    //    public abstract TSelf? ExchangeTo(Enum? context);
    //    public abstract bool? FitsIn(TSelf? other, LimitMode? limitMode);
    //    #endregion
    //    #endregion
    //}
}
