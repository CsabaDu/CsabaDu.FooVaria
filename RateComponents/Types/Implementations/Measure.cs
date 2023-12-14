namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    //internal abstract class Measure : RateComponent<IMeasure>, IMeasure
    //{
    //    #region Constructors
    //    private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
    //    {
    //    }
    //    #endregion

    //    #region Public methods
    //    public IMeasure Add(IMeasure? other)
    //    {
    //        return GetSum(this, other, SummingMode.Add);
    //    }

    //    public IMeasure Divide(decimal divisor)
    //    {
    //        if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

    //        decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

    //        return GetRateComponent(quantity);
    //    }

    //    public bool? FitsIn(ILimit? limit)
    //    {
    //        return FitsIn(limit, limit?.LimitMode);
    //    }

    //    public bool? FitsIn(IRateComponent? rateComponent, LimitMode? limitMode)
    //    {
    //        bool isLimitModeNull = limitMode == null;

    //        if (isRateComponentNull() && isLimitModeNull) return true;

    //        if (rateComponent?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) != true) return null;

    //        if (isLimitModeNull) return CompareTo(rateComponent) <= 0;

    //        IRateComponent ceilingRateComponent = rateComponent.Round(RoundingMode.Ceiling);
    //        rateComponent = getRoundedRateComponent();

    //        if (isRateComponentNull()) return null;

    //        int comparison = CompareTo(rateComponent);

    //        return Defined(limitMode!.Value, nameof(limitMode)) switch
    //        {
    //            LimitMode.BeEqual => comparison == 0 && ceilingRateComponent.Equals(rateComponent),

    //            _ => comparison.FitsIn(limitMode),
    //        };

    //        #region Local methods
    //        bool isRateComponentNull()
    //        {
    //            return rateComponent == null;
    //        }

    //        IRateComponent? getRoundedRateComponent()
    //        {
    //            return limitMode switch
    //            {
    //                LimitMode.BeNotLess or
    //                LimitMode.BeGreater => ceilingRateComponent,

    //                LimitMode.BeNotGreater or
    //                LimitMode.BeLess or
    //                LimitMode.BeEqual => rateComponent!.Round(RoundingMode.Floor),

    //                _ => null,
    //            };
    //        }
    //        #endregion
    //    }

    //    public IMeasure Multiply(decimal multiplier)
    //    {
    //        decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

    //        return GetRateComponent(quantity);
    //    }

    //    public IMeasure Subtract(IMeasure? other)
    //    {
    //        return GetSum(this, other, SummingMode.Subtract);
    //    }

    //    #region Override methods
    //    #region Sealed methods
    //    public override sealed bool Equals(IRateComponent? other)
    //    {
    //        return other is IMeasure
    //            && base.Equals(other);
    //    }

    //    public override sealed IMeasureFactory GetFactory()
    //    {
    //        return (IMeasureFactory)Factory;
    //    }

    //    public override sealed void Validate(IRootObject? rootObject, string paramName)
    //    {
    //        Validate(this, rootObject, validateMeasure, paramName);

    //        #region Local methods
    //        void validateMeasure()
    //        {
    //            ValidateBaseMeasure(this, rootObject!, paramName);
    //        }
    //        #endregion
    //    }
    //    #endregion
    //    #endregion
    //    #endregion

    //    #region Private methods
    //    #region Static methods
    //    private static IMeasure GetSum(IMeasure measure, IMeasure? other, SummingMode summingMode)
    //    {
    //        Enum measureUnit = measure.Measurement.GetMeasureUnit();
    //        decimal quantity = measure.GetDecimalQuantity();

    //        if (other == null) return getMeasure();

    //        MeasureUnitTypeCode measureUnitTypeCode = other.MeasureUnitTypeCode;

    //        if (measure.IsExchangeableTo(measureUnitTypeCode))
    //        {
    //            quantity = getDefaultQuantitySum() / measure.GetExchangeRate();

    //            return getMeasure();
    //        }

    //        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, nameof(other));

    //        #region Local methods
    //        decimal getDefaultQuantitySum()
    //        {
    //            quantity = measure.DefaultQuantity;
    //            decimal otherQuantity = other!.DefaultQuantity;

    //            return summingMode switch
    //            {
    //                SummingMode.Add => decimal.Add(quantity, otherQuantity),
    //                SummingMode.Subtract => decimal.Subtract(quantity, otherQuantity),

    //                _ => throw new InvalidOperationException(null),
    //            };
    //        }

    //        IMeasure getMeasure()
    //        {
    //            return (IMeasure)measure.GetRateComponent(measureUnit, quantity);
    //        }
    //        #endregion
    //    }
    //    #endregion
    //    #endregion
    //}

    internal abstract class Measure<TSelf, TNum> : RateComponent<TSelf, TNum>, IMeasure<TSelf, TNum> where TSelf : class, IMeasure<TSelf, TNum>, IDefaultRateComponent where TNum : struct
    {
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }

        #region Public methods

        public TSelf? GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return (TSelf?)GetFactory().CreateDefault(measureUnitTypeCode);
        }

        public TSelf GetDefault()
        {
            return GetDefault(MeasureUnitTypeCode)!;
        }

        public TNum GetDefaultRateComponentQuantity()
        {
            return GetDefaultRateComponentQuantity<TNum>();
        }

        public TSelf GetMeasure(string name, TNum quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetMeasure(TNum quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetNew(TSelf other)
        {
            throw new NotImplementedException();
        }

        public TNum GetQuantity()
        {
            throw new NotImplementedException();
        }

        public TSelf GetRateComponent(TNum quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetRateComponent(IRateComponent rateComponent)
        {
            throw new NotImplementedException();
        }

        public override sealed TSelf GetRateComponent(ValueType quantity)
        {
            return (TSelf)base.GetRateComponent(quantity);
        }

        public override sealed TSelf GetRateComponent(string name, ValueType quantity)
        {
            return (TSelf)base.GetRateComponent(name, quantity);
        }

        public override sealed TSelf GetRateComponent(IMeasurement measurement, ValueType quantity)
        {
            return (TSelf)base.GetRateComponent(measurement, quantity);
        }

        public override sealed TSelf? GetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return (TSelf?)base.GetRateComponent(measureUnit, exchangeRate, quantity, customName);
        }

        public override sealed TSelf? GetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
        {
            return (TSelf?)base.GetRateComponent(customName, measureUnitTypeCode, exchangeRate, quantity);
        }

        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure<TSelf, TNum, TEnum> where TSelf : class, IMeasure<TSelf, TNum>, IDefaultRateComponent where TNum : struct where TEnum : struct, Enum
    {
        #region Enums
        protected enum ConvertMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, TEnum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetMeasure(TSelf other)
        {
            throw new NotImplementedException();
        }

        public TSelf GetMeasure(TEnum measureUnit)
        {
            throw new NotImplementedException();
        }

        public TEnum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }

        #region Override methods
        #region Sealed methods
        #endregion
        #endregion
        #endregion

        #region Protected methods
        protected TSelf ConvertMeasure<TSelf, TOther>(ConvertMode convertMode) where TSelf : class, IMeasure, IConvertMeasure where TOther : struct, Enum
        {
            decimal quantity = DefaultQuantity;
            decimal ratio = IConvertMeasure.ConvertRatio;
            quantity = convertMode switch
            {
                ConvertMode.Multiply => quantity * ratio,
                ConvertMode.Divide => quantity / ratio,

                _ => throw new InvalidOperationException(null),
            };

            return (TSelf)GetRateComponent(default(TOther), quantity);
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

            ValidateMeasureUnitTypeCode(measure.MeasureUnitTypeCode, paramName);

            decimal quantity = measure.GetDecimalQuantity();

            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
    }
}
