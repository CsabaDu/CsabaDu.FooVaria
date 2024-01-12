﻿namespace CsabaDu.FooVaria.Measures.Types.Implementations
{
    internal abstract class Measure : BaseMeasure, IMeasure
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
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

            if (rateComponent?.HasMeasureUnitCode(MeasureUnitCode) != true) return null;

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
            return GetSum(other, SummingMode.Subtract);
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
        #endregion
        #endregion

        #region Abstract methods
        public abstract IMeasure GetMeasure(IRateComponent rateComponent);
        #endregion
        #endregion

        #region Private methods
        private IMeasure GetSum(IMeasure? other, SummingMode summingMode)
        {
            if (other == null) return GetMeasure(this);

            if (other.IsExchangeableTo(MeasureUnitCode)) return getMeasure();

            throw InvalidMeasureUnitCodeEnumArgumentException(other.MeasureUnitCode, nameof(other));

            #region Local methods
            decimal getDefaultQuantitySum()
            {
                decimal otherQuantity = other!.DefaultQuantity;

                return summingMode switch
                {
                    SummingMode.Add => decimal.Add(DefaultQuantity, otherQuantity),
                    SummingMode.Subtract => decimal.Subtract(DefaultQuantity, otherQuantity),

                    _ => throw new InvalidOperationException(null),
                };
            }

            IMeasure getMeasure()
            {
                Enum measureUnit = Measurement.GetMeasureUnit();
                decimal quantity = getDefaultQuantitySum() / GetExchangeRate();

                return (IMeasure)GetRateComponent(measureUnit, quantity);
            }
            #endregion
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }
        #endregion

        #region Public methods
        public TSelf? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return (TSelf?)GetFactory().CreateDefault(measureUnitCode);
        }

        public TSelf GetDefault()
        {
            return GetDefault(MeasureUnitCode)!;
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
        public override sealed TSelf GetMeasure(IRateComponent rateComponent)
        {
            return GetRateComponent(rateComponent);
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

        public override sealed TSelf? GetRateComponent(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return (TSelf?)base.GetRateComponent(customName, measureUnitCode, exchangeRate, quantity);
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure<TSelf, TNum, TEnum>
        where TSelf : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
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

        public TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other)
        {
            return (TEnum)(other ?? this).GetMeasureUnit();
        }
        #endregion

        #region Protected methods
        protected TOther ConvertMeasure<TOther>(ConvertMode convertMode)
            where TOther : IMeasure, IConvertMeasure
        {
            MeasureUnitCode measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(typeof(TOther));
            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
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

            ValidateMeasureUnitCode(measure.MeasureUnitCode, paramName);

            decimal quantity = measure.GetDecimalQuantity();

            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
    }
}
