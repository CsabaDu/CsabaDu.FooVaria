namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class Measure : RateComponent<IMeasure>, IMeasure
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(this, other, SummingMode.Add);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

            decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

            return GetRateComponent(quantity);
        }

        public bool? FitsIn(ILimit? limit)
        {
            return FitsIn(limit, limit?.LimitMode);
        }

        public bool? FitsIn(IRateComponent? rateComponent, LimitMode? limitMode)
        {
            bool isLimitModeNull = limitMode == null;

            if (isRateComponentNull() && isLimitModeNull) return true;

            if (rateComponent?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) != true) return null;

            if (isLimitModeNull) return CompareTo(rateComponent) <= 0;

            IRateComponent ceilingRateComponent = rateComponent.Round(RoundingMode.Ceiling);
            rateComponent = getRoundedRateComponent();

            if (isRateComponentNull()) return null;

            int comparison = CompareTo(rateComponent);

            return Defined(limitMode!.Value, nameof(limitMode)) switch
            {
                LimitMode.BeEqual => comparison == 0 && ceilingRateComponent.Equals(rateComponent),

                _ => comparison.FitsIn(limitMode),
            };

            #region Local methods
            bool isRateComponentNull()
            {
                return rateComponent == null;
            }

            IRateComponent? getRoundedRateComponent()
            {
                return limitMode switch
                {
                    LimitMode.BeNotLess or
                    LimitMode.BeGreater => ceilingRateComponent,

                    LimitMode.BeNotGreater or
                    LimitMode.BeLess or
                    LimitMode.BeEqual => rateComponent!.Round(RoundingMode.Floor),

                    _ => null,
                };
            }
            #endregion
        }

        public IMeasure Multiply(decimal multiplier)
        {
            decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

            return GetRateComponent(quantity);
        }

        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(this, other, SummingMode.Subtract);
        }

        #region Override methods
        #region Sealed methods
        public override sealed bool Equals(IRateComponent? other)
        {
            return other is IMeasure
                && base.Equals(other);
        }

        public override sealed IMeasureFactory GetFactory()
        {
            return (IMeasureFactory)Factory;
        }

        public override sealed void Validate(IRootObject? rootObject, string paramName)
        {
            Validate(this, rootObject, validateMeasure, paramName);

            #region Local methods
            void validateMeasure()
            {
                ValidateBaseMeasure(this, rootObject!, paramName);
            }
            #endregion
        }
        #endregion
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static IMeasure GetSum(IMeasure measure, IMeasure? other, SummingMode summingMode)
        {
            Enum measureUnit = measure.Measurement.GetMeasureUnit();
            decimal quantity = measure.GetDecimalQuantity();

            if (other == null) return getMeasure();

            MeasureUnitTypeCode measureUnitTypeCode = other.MeasureUnitTypeCode;

            if (measure.IsExchangeableTo(measureUnitTypeCode))
            {
                quantity = getDefaultQuantitySum() / measure.GetExchangeRate();

                return getMeasure();
            }

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, nameof(other));

            #region Local methods
            decimal getDefaultQuantitySum()
            {
                quantity = measure.DefaultQuantity;
                decimal otherQuantity = other!.DefaultQuantity;

                return summingMode switch
                {
                    SummingMode.Add => decimal.Add(quantity, otherQuantity),
                    SummingMode.Subtract => decimal.Subtract(quantity, otherQuantity),

                    _ => throw new InvalidOperationException(null),
                };
            }

            IMeasure getMeasure()
            {
                return (IMeasure)measure.GetRateComponent(measureUnit, quantity);
            }
            #endregion
        }
        #endregion
        #endregion
    }

    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum> where TSelf : class, IMeasure, IDefaultRateComponent where TNum : struct
    {
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }

        public abstract TSelf? GetDefault(MeasureUnitTypeCode measureUnitTypeCode);

        #region Public methods

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
        public TSelf GetMeasure(TNum quantity, TEnum measureUnit)
        {
            return (TSelf)base.GetMeasure(quantity, measureUnit);
        }

        public TSelf GetMeasure(TSelf other)
        {
            return (TSelf)base.GetMeasure(other);
        }

        public TSelf GetMeasure(TEnum measureUnit)
        {
            return (TSelf)base.GetMeasure(measureUnit);
        }

        public TEnum GetMeasureUnit()
        {
            return (TEnum)Measurement.MeasureUnit;
        }

        #region Override methods
        #region Sealed methods
        public override sealed TSelf GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
        {
            throw new NotImplementedException();
        }

        public override sealed TSelf GetMeasure(IRateComponent baseMeasure)
        {
            return GetMeasure(getValidQuantity(), baseMeasure.Measurement);

            #region Local methods
            TNum getValidQuantity()
            {
                string paramName = nameof(baseMeasure);

                (this as IMeasurable).Validate(baseMeasure, paramName);

                object? quantity = GetValidQuantityOrNull(this, baseMeasure.Quantity);

                if (quantity != null) return (TNum)quantity;

                throw QuantityArgumentOutOfRangeException(paramName, baseMeasure.GetDecimalQuantity());
            }
            #endregion
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        protected X ConvertMeasure<X, Y>(ConvertMode convertMode) where X : class, IMeasure, IConvertMeasure where Y : struct, Enum // TODO Check!!!
        {
            decimal quantity = DefaultQuantity;
            decimal ratio = 1000; // Mindig?
            quantity = convertMode switch
            {
                ConvertMode.Multiply => quantity * ratio,
                ConvertMode.Divide => quantity / ratio,

                _ => throw new InvalidOperationException(null),
            };

            return (X)GetRateComponent(quantity, default(Y));
        }

        protected TSelf GetMeasure(TNum quantity, TEnum measureUnit, decimal exchangeRate, string customName)
        {
            return (TSelf)base.GetRateComponent(quantity, measureUnit, exchangeRate, customName);
        }

        protected TSelf GetMeasure(TNum quantity, string customName, decimal exchangeRate)
        {
            return (TSelf)base.GetMeasure(quantity, customName, exchangeRate);
        }

        protected void ValidateSpreadMeasure(string paramName, ISpreadMeasure? spreadMeasure)
        {
            MeasureUnitTypeCode measureUnitTypeCode = NullChecked(spreadMeasure, paramName).GetMeasureUnitTypeCode();

            ValidateMeasureUnitTypeCode(measureUnitTypeCode, paramName);

            decimal quantity = spreadMeasure!.GetDefaultQuantity();

            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
