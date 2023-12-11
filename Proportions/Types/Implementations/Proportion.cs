namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
            DefaultQuantity = defaultQuantity;
        }

        private protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            NumeratorMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();
            DefaultQuantity = baseRate.GetDefaultQuantity();
        }

        private protected Proportion(IProportion other) : base(other)
        {
            NumeratorMeasureUnitTypeCode = other.NumeratorMeasureUnitTypeCode;
            DefaultQuantity = other.DefaultQuantity;
        }
        #endregion

        #region Properties
        public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }
        public override decimal DefaultQuantity { get; init; }
        #endregion

        #region Public methods
        public IProportion GetProportion(IBaseRate baseRate)
        {
            return GetFactory().Create(baseRate);
        }

        public IProportion GetProportion(IRateComponent numerator, IRateComponent denominator)
        {
            return GetFactory().Create(numerator, denominator);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        public override sealed IProportion GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
        {
            return (IProportion)GetFactory().Create(numerator, denominatorMeasureUnit);
        }

        public override IProportionFactory GetFactory()
        {
            return (IProportionFactory)Factory;
        }
        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes();
        }

        public override sealed MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
        {
            return NumeratorMeasureUnitTypeCode;
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            object converted = NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal) ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!);

            if ((decimal)converted > 0) return;

            if (QuantityTypes.GetQuantityTypes().Contains(NullChecked(quantity, paramName).GetType())) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public override sealed IMeasure Multiply(IBaseMeasure multiplier)
        {
            MeasureUnitTypeCode measureUnitTypeCode = NullChecked(multiplier, nameof(multiplier)).MeasureUnitTypeCode;

            ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(multiplier));

            if (multiplier is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
            }

            decimal quantity = measure.DefaultQuantity * DefaultQuantity;
            Enum measureUnit = NumeratorMeasureUnitTypeCode.GetDefaultMeasureUnit();

            return measure.GetRateComponent(measureUnit, quantity);
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Proportion<T, U> : Proportion, IProportion<T, U> where T : class, IProportion, IMeasureProportion where U : struct, Enum
    {
        private protected Proportion(T other) : base(other)
        {
        }

        private protected Proportion(IProportionFactory<T, U> factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        private protected Proportion(IProportionFactory<T, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            ValidateMeasureUnitTypeCode(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));
        }

        public T GetProportion(IMeasure numerator, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override IProportionFactory<T, U> GetFactory()
        {
            return (IProportionFactory<T, U>)Factory;
        }

        public abstract T GetProportion(IRateComponent numerator, U denominatorMeasureUnit);
    }

    internal abstract class Proportion<T, W, U> : Proportion<T, U>, IProportion<T, W, U> where T : class, IProportion<T, W, U>, IMeasureProportion where U : struct, Enum where W : struct, Enum
    {
        private protected Proportion(T other) : base(other)
        {
        }

        private protected Proportion(IProportionFactory<T, W, U> factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        private protected Proportion(IProportionFactory<T, W, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
        }

        public T GetProportion(W numeratorMeasureUnit, ValueType quantity, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(W numeratorMeasureUnit, U denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override IProportionFactory<T, W, U> GetFactory()
        {
            return (IProportionFactory<T, W, U>)Factory;
        }
    }

}



    //internal abstract class Proportion<TNum, TContext, TEnum> : Proportion, IProportion<TNum, TContext, TEnum> where TNum : class, IProportion where TContext : struct, Enum where TEnum : struct, Enum
    //{
    //    #region Constructors
    //    private protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    //    {
    //    }

    //    private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
    //    {
    //    }
    //    #endregion

    //    #region Public methods
    //    public TNum GetProportion(TContext numeratorMeasureUnit, ValueType quantity, TEnum denominatorMeasureUnit)
    //    {
    //        ValidateQuantity(quantity, nameof(quantity));

    //        decimal decimalQuantity = (decimal?)quantity.ToQuantity(TypeCode.Decimal) ?? throw new InvalidOperationException(null);

    //        return (TNum)GetFactory().Create(numeratorMeasureUnit, decimalQuantity, denominatorMeasureUnit);
    //    }

    //    public TNum GetProportion(IMeasure numerator, TEnum denominatorMeasureUnit)
    //    {
    //        return (TNum)GetFactory().Create(numerator, denominatorMeasureUnit);
    //    }

    //    public decimal GetQuantity(TContext numeratorMeasureUnit, TEnum denominatorMeasureUnit)
    //    {
    //        return DefaultQuantity
    //            / GetExchangeRate(Defined(numeratorMeasureUnit, nameof(numeratorMeasureUnit)))
    //            * GetExchangeRate(Defined(denominatorMeasureUnit, nameof(denominatorMeasureUnit)));
    //    }

    //    #region Override methods
    //    #region Sealed methods
    //    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    //    {
    //        yield return MeasureUnitTypes.GetMeasureUnitTypeCode(typeof(TContext));
    //        yield return MeasureUnitTypes.GetMeasureUnitTypeCode(typeof(TEnum));
    //    }
    //    #endregion
    //    #endregion
    //    #endregion
    //}