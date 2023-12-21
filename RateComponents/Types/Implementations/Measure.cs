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

    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure, IDefaultRateComponent
        where TNum : struct
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

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
            return (TSelf)GetFactory().Create(name, quantity);
        }

        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            return (TSelf)GetFactory().Create(measurement, quantity);
        }

        public TSelf GetNew(TSelf other)
        {
            return (TSelf)GetFactory().CreateNew(other);
        }

        public TNum GetQuantity()
        {
            return (TNum)Quantity;
        }

        public TSelf GetRateComponent(TNum quantity)
        {
            return GetMeasure(Measurement, quantity);
        }

        public TSelf GetRateComponent(IRateComponent rateComponent)
        {
            if (NullChecked(rateComponent, nameof(rateComponent)) is TSelf other) return GetNew(other);

            return (TSelf)GetRateComponent(rateComponent, GetFactory());
        }

        #region Override methods
        #region Sealed methods
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
        #endregion
        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure
        where TSelf : class, IMeasure, IDefaultRateComponent, IMeasureUnit
        where TNum : struct
        where TEnum : struct, Enum
    {
        #region Enums
        protected enum ConvertMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constants
        private const decimal ConvertRatio = 1000m;
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, TEnum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            return (TSelf)GetRateComponent(measureUnit, quantity);
        }

        public TSelf GetMeasure(TEnum measureUnit)
        {
            return (TSelf)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
        }

        public TEnum GetMeasureUnit()
        {
            return (TEnum)Measurement.MeasureUnit;
        }
        #endregion

        #region Protected methods
        protected TOther ConvertMeasure<TOther>(ConvertMode convertMode)
            where TOther : IMeasure, IConvertMeasure
        {
            MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(typeof(TOther));
            Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();
            decimal quantity = convertMode switch
            {
                ConvertMode.Multiply => DefaultQuantity * ConvertRatio,
                ConvertMode.Divide => DefaultQuantity / ConvertRatio,

                _ => throw new InvalidOperationException(null),
            };

            return (TOther)GetRateComponent(measureUnit, quantity);
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
