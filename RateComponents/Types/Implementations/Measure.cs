//namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
//{
//    internal abstract class Measure : RateComponent<IMeasure>, IMeasure
//    {
//        #region Constructors
//        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
//        {
//        }
//        #endregion

//        #region Public methods
//        public IMeasure Add(IMeasure? other)
//        {
//            return GetSum(other, SummingMode.Add);
//        }

//        public IMeasure Divide(decimal divisor)
//        {
//            if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

//            decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

//            return GetBaseMeasure(quantity);
//        }

//        public bool? FitsIn(ILimit? limit)
//        {
//            return FitsIn(limit, limit?.LimitMode);
//        }

//        public bool? FitsIn(IRateComponent? rateComponent, LimitMode? limitMode)
//        {
//            bool isLimitModeNull = limitMode == null;

//            if (isRateComponentNull() && isLimitModeNull) return true;

//            if (rateComponent?.HasMeasureUnitCode(MeasureUnitCode) != true) return null;

//            if (isLimitModeNull) return CompareTo(rateComponent) <= 0;

//            IRateComponent ceilingRateComponent = rateComponent.Round(RoundingMode.Ceiling);
//            rateComponent = getRoundedRateComponent();

//            if (isRateComponentNull()) return null;

//            int comparison = CompareTo(rateComponent);

//            return Defined(limitMode!.Value, nameof(limitMode)) switch
//            {
//                LimitMode.BeEqual => comparison == 0 && ceilingRateComponent.Equals(rateComponent),

//                _ => comparison.FitsIn(limitMode),
//            };

//            #region Local methods
//            bool isRateComponentNull()
//            {
//                return rateComponent == null;
//            }

//            IRateComponent? getRoundedRateComponent()
//            {
//                return limitMode switch
//                {
//                    LimitMode.BeNotLess or
//                    LimitMode.BeGreater => ceilingRateComponent,

//                    LimitMode.BeNotGreater or
//                    LimitMode.BeLess or
//                    LimitMode.BeEqual => rateComponent!.Round(RoundingMode.Floor),

//                    _ => null,
//                };
//            }
//            #endregion
//        }

//        public IMeasure Multiply(decimal multiplier)
//        {
//            decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

//            return GetBaseMeasure(quantity);
//        }

//        public IMeasure Subtract(IMeasure? other)
//        {
//            return GetSum(other, SummingMode.Subtract);
//        }

//        #region Override methods
//        #region Sealed methods
//        public override sealed bool Equals(IRateComponent? other)
//        {
//            return other is IMeasure
//                && base.Equals(other);
//        }

//        public override sealed IMeasureFactory GetFactory()
//        {
//            return (IMeasureFactory)Factory;
//        }
//        #endregion
//        #endregion

//        #region Abstract methods
//        public abstract IMeasure GetMeasure(IRateComponent rateComponent);
//        #endregion
//        #endregion

//        #region Private methods
//        private IMeasure GetSum(IMeasure? other, SummingMode summingMode)
//        {
//            if (other == null) return GetMeasure(this);

//            if (other.IsExchangeableTo(MeasureUnitCode)) return getMeasure();

//            throw InvalidMeasureUnitCodeEnumArgumentException(other.MeasureUnitCode, nameof(other));

//            #region Local methods
//            decimal getDefaultQuantitySum()
//            {
//                decimal otherQuantity = other!.DefaultQuantity;

//                return summingMode switch
//                {
//                    SummingMode.Add => decimal.Add(DefaultQuantity, otherQuantity),
//                    SummingMode.Subtract => decimal.Subtract(DefaultQuantity, otherQuantity),

//                    _ => throw new InvalidOperationException(null),
//                };
//            }

//            IMeasure getMeasure()
//            {
//                Enum measureUnit = Measurement.GetMeasureUnit();
//                decimal quantity = getDefaultQuantitySum() / GetExchangeRate();

//                return (IMeasure)GetBaseMeasure(measureUnit, quantity);
//            }
//            #endregion
//        }
//        #endregion
//    }

//    internal abstract class Measure<T, TNum> : Measure, IMeasure<T, TNum>
//        where T : class, IMeasure, IDefaultBaseMeasure
//        where TNum : struct
//    {
//        #region Constructors
//        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
//        {
//        }
//        #endregion

//        #region Public methods
//        public T? GetDefault(MeasureUnitCode measureUnitCode)
//        {
//            return (T?)GetFactory().CreateDefault(measureUnitCode);
//        }

//        public T GetDefault()
//        {
//            return GetDefault(MeasureUnitCode)!;
//        }

//        public TNum GetDefaultRateComponentQuantity()
//        {
//            return GetDefaultRateComponentQuantity<TNum>();
//        }

//        public T GetMeasure(string name, TNum quantity)
//        {
//            return (T)GetFactory().Create(name, quantity);
//        }

//        public T GetMeasure(IMeasurement measurement, TNum quantity)
//        {
//            return (T)GetFactory().Create(measurement, quantity);
//        }

//        public T GetNew(T other)
//        {
//            return (T)GetFactory().CreateNew(other);
//        }

//        public TNum GetQuantity()
//        {
//            return (TNum)Quantity;
//        }

//        public T GetBaseMeasure(TNum quantity)
//        {
//            return GetMeasure(Measurement, quantity);
//        }

//        public T GetBaseMeasure(IRateComponent rateComponent)
//        {
//            if (NullChecked(rateComponent, nameof(rateComponent)) is T other) return GetNew(other);

//            return (T)GetBaseMeasure(rateComponent, GetFactory());
//        }

//        #region Override methods
//        #region Sealed methods
//        public override sealed T GetMeasure(IRateComponent rateComponent)
//        {
//            return GetBaseMeasure(rateComponent);
//        }

//        public override sealed T GetBaseMeasure(ValueType quantity)
//        {
//            return (T)base.GetBaseMeasure(quantity);
//        }

//        public override sealed T GetBaseMeasure(string name, ValueType quantity)
//        {
//            return (T)base.GetBaseMeasure(name, quantity);
//        }

//        public override sealed T GetBaseMeasure(IMeasurement measurement, ValueType quantity)
//        {
//            return (T)base.GetBaseMeasure(measurement, quantity);
//        }

//        public override sealed T? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
//        {
//            return (T?)base.GetBaseMeasure(measureUnit, exchangeRate, quantity, customName);
//        }

//        public override sealed T? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
//        {
//            return (T?)base.GetBaseMeasure(customName, measureUnitCode, exchangeRate, quantity);
//        }
//        #endregion
//        #endregion
//        #endregion
//    }

//    internal abstract class Measure<T, TNum, TEnum> : Measure<T, TNum>, IMeasure<T, TNum, TEnum>
//        where T : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
//        where TNum : struct
//        where TEnum : struct, Enum
//    {
//        #region Enums
//        protected enum ConvertMode
//        {
//            Multiply,
//            Divide,
//        }
//        #endregion

//        #region Constants
//        private const decimal ConvertRatio = 1000m;
//        #endregion

//        #region Constructors
//        private protected Measure(IMeasureFactory factory, TEnum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
//        {
//        }
//        #endregion

//        #region Public methods
//        public T GetMeasure(TEnum measureUnit, TNum quantity)
//        {
//            return (T)GetBaseMeasure(measureUnit, quantity);
//        }

//        public T GetMeasure(TEnum measureUnit)
//        {
//            return (T)(ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit));
//        }

//        public TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other)
//        {
//            return (TEnum)(other ?? this).GetMeasureUnit();
//        }
//        #endregion

//        #region Protected methods
//        protected TOther ConvertMeasure<TOther>(ConvertMode convertMode)
//            where TOther : IMeasure, IConvertMeasure
//        {
//            MeasureUnitCode measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(typeof(TOther));
//            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
//            decimal quantity = convertMode switch
//            {
//                ConvertMode.Multiply => DefaultQuantity * ConvertRatio,
//                ConvertMode.Divide => DefaultQuantity / ConvertRatio,

//                _ => throw new InvalidOperationException(null),
//            };

//            return (TOther)GetBaseMeasure(measureUnit, quantity);
//        }

//        protected void ValidateSpreadQuantity(ValueType? quantity, string paramName)
//        {
//            if (GetValidQuantityOrNull(this, NullChecked(quantity, paramName)) is double spreadQuantity && spreadQuantity > 0) return;

//            throw QuantityArgumentOutOfRangeException(paramName, quantity);
//        }

//        protected void ValidateSpreadMeasure(string paramName, ISpreadMeasure? spreadMeasure)
//        {
//            if (NullChecked(spreadMeasure, paramName).GetSpreadMeasure() is not IMeasure measure)
//            {
//                throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
//            }

//            ValidateMeasureUnitCode(measure.MeasureUnitCode, paramName);

//            decimal quantity = measure.GetDecimalQuantity();

//            if (quantity > 0) return;

//            throw QuantityArgumentOutOfRangeException(paramName, quantity);
//        }
//        #endregion
//    }
//}
