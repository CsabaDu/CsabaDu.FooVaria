namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        #region Constructors
        private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = getValidParams().MeasureUnitTypeCode;
            DefaultQuantity = getValidParams().DefaultQuantity;

            #region Local methods
            (MeasureUnitTypeCode MeasureUnitTypeCode, decimal DefaultQuantity) getValidParams()
            {
                MeasureUnitTypeCode measureUnitTypeCode = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));

                ValidateQuantity(defaultQuantity, nameof(defaultQuantity));

                return (measureUnitTypeCode, defaultQuantity);
            }
            #endregion
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

        public IProportion GetProportion(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }
        #region Override methods
        #region Sealed methods
        public override sealed IProportion GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            return GetFactory().Create(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
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

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public override sealed IMeasure Multiply(IBaseMeasure multiplier) // Validate?
        {
            if (NullChecked(multiplier, nameof(multiplier)) is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
            }

            ValidateMeasureUnitTypeCode(measure.MeasureUnitTypeCode, nameof(multiplier));

            decimal quantity = measure.DefaultQuantity * DefaultQuantity;
            Enum measureUnit = NumeratorMeasureUnitTypeCode.GetDefaultMeasureUnit();

            return (IMeasure)measure.GetRateComponent(measureUnit, quantity);
        }

        #endregion
        #endregion
        #endregion
    }

    internal abstract class Proportion<TDEnum> : Proportion, IProportion<TDEnum>
        where TDEnum : struct, Enum
    {
        private protected Proportion(IProportion<TDEnum> other) : base(other)
        {
        }

        private protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        private protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }

        public IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        {
            return (IProportion<TDEnum>)GetFactory().Create(numerator, denominatorMeasureUnit);
        }
        public decimal GetQuantity(TDEnum denominatorMeasureUnit)
        {
            return DefaultQuantity / GetExchangeRate(denominatorMeasureUnit);
        }

        public IBaseMeasure Multiply(TDEnum measureUnit)
        {
            decimal quantity = GetQuantity(measureUnit);

            return GetFactory().CreateBaseMeasure(measureUnit, quantity);
        }
    }

    internal sealed class Proportion<TNEnum, TDEnum> : Proportion<TDEnum>, IProportion<TNEnum, TDEnum>
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        public Proportion(IProportion<TNEnum, TDEnum> other) : base(other)
        {
        }

        public Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        public Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }

        public override IBaseRate? ExchangeTo(IMeasurable context)
        {
            throw new NotImplementedException();
        }

        public IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override bool IsExchangeableTo(IMeasurable? context)
        {
            throw new NotImplementedException();
        }
    }
}

//    internal abstract class Proportion<T, U> : Proportion, IProportion<T, U>
//        where T : class, IProportion, IMeasureProportion
//        where U : struct, Enum
//    {
//        private protected Proportion(T other) : base(other)
//        {
//        }

//        private protected Proportion(IProportionFactory<T, U> factory, IBaseRate baseRate) : base(factory, baseRate)
//        {
//        }

//        private protected Proportion(IProportionFactory<T, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
//        {
//            ValidateMeasureUnitTypeCode(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));
//        }

//        public T GetProportion(IMeasure numerator, U denominatorMeasureUnit)
//        {
//            throw new NotImplementedException();
//        }

//        public decimal GetQuantity(U denominatorMeasureUnit)
//        {
//            throw new NotImplementedException();
//        }

//        public override IProportionFactory<T, U> GetFactory()
//        {
//            return (IProportionFactory<T, U>)Factory;
//        }

//        public abstract T GetProportion(IRateComponent numerator, U denominatorMeasureUnit);
//    }

//    internal abstract class Proportion<T, W, U> : Proportion<T, U>, IProportion<T, W, U>
//        where T : class, IProportion<T, W, U>, IMeasureProportion
//        where U : struct, Enum
//        where W : struct, Enum
//    {
//        private protected Proportion(T other) : base(other)
//        {
//        }

//        private protected Proportion(IProportionFactory<T, W, U> factory, IBaseRate baseRate) : base(factory, baseRate)
//        {
//        }

//        private protected Proportion(IProportionFactory<T, W, U> factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
//        {
//            ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
//        }

//        public T GetProportion(W numeratorMeasureUnit, ValueType quantity, U denominatorMeasureUnit)
//        {
//            throw new NotImplementedException();
//        }

//        public decimal GetQuantity(W numeratorMeasureUnit, U denominatorMeasureUnit)
//        {
//            throw new NotImplementedException();
//        }

//        public override IProportionFactory<T, W, U> GetFactory()
//        {
//            return (IProportionFactory<T, W, U>)Factory;
//        }
//    }

//}
