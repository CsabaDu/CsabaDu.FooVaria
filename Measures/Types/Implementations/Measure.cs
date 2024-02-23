﻿namespace CsabaDu.FooVaria.Measures.Types.Implementations
{
    internal abstract class Measure : BaseMeasure, IMeasure
    {
        #region Enums
        protected enum MeasureOperationMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, Enum measureUnit, ValueType quantity) : base(factory)
        {
            Measurement = GetBaseMeasurementFactory().Create(measureUnit);
            Quantity = GetValidMeasureQuantity(quantity);
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }

        #region Override properties
        public override sealed object Quantity { get; init; }
        #endregion
        #endregion

        #region Public methods
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
        }

        public IMeasure ConvertToLimitable(ILimiter limiter)
        {
            return ConvertToLimitable(this, limiter);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw DecimalArgumentOutOfRangeException(nameof(divisor), divisor);

            return GetMeasure(divisor, MeasureOperationMode.Divide);
        }

        public IMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }

        public IMeasure GetBaseMeasure(string name, ValueType quantity)
        {
            return GetFactory().Create(name, quantity);
        }

        public IMeasure? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return GetFactory().Create(measureUnit, exchangeRate, quantity, customName);
        }

        public IMeasure? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        public IMeasure GetBaseMeasure(IBaseMeasure baseMeasure)
        {
            return GetFactory().Create(baseMeasure);
        }

        public IMeasure Multiply(decimal multiplier)
        {
            return GetMeasure(multiplier, MeasureOperationMode.Multiply);

        }

        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(other, SummingMode.Subtract);
        }

        #region Override methods
        #region Sealed methods
        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetFactory().MeasurementFactory;
        }

        public override sealed decimal GetDefaultQuantity()
        {
            return GetDefaultQuantity(Quantity, GetExchangeRate());
        }

        public override sealed IMeasureFactory GetFactory()
        {
            return (IMeasureFactory)Factory;
        }

        public override sealed LimitMode? GetLimitMode()
        {
            return base.GetLimitMode();
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            base.ValidateQuantity(quantity, paramName);
        }
        #endregion
        #endregion
        #endregion

        #region Private methods
        private IMeasure GetMeasure(decimal operand, MeasureOperationMode measureOperationMode)
        {
            Enum measureUnit = GetMeasureUnit();
            decimal quantity = getQuantity();

            return (IMeasure)GetBaseMeasure(quantity).ExchangeTo(measureUnit)!;

            #region Local methods
            decimal getQuantity()
            {
                decimal quantity = GetDecimalQuantity();

                return operand switch
                {
                    0 => 0,
                    1 => quantity,

                    _ => measureOperationMode switch
                    {
                        MeasureOperationMode.Multiply => decimal.Multiply(quantity, operand),
                        MeasureOperationMode.Divide => decimal.Divide(quantity, operand),

                        _ => throw new InvalidOperationException(null),
                    },
                };
            }
            #endregion
        }

        private IMeasure GetSum(IMeasure? other, SummingMode summingMode)
        {
            if (other == null) return GetBaseMeasure(this);

            if (other.IsExchangeableTo(GetMeasureUnitCode())) return getMeasure();

            throw InvalidMeasureUnitCodeEnumArgumentException(other.GetMeasureUnitCode(), nameof(other));

            #region Local methods
            decimal getDefaultQuantitySum()
            {
                decimal otherQuantity = other!.GetDefaultQuantity();

                return summingMode switch
                {
                    SummingMode.Add => decimal.Add(GetDefaultQuantity(), otherQuantity),
                    SummingMode.Subtract => decimal.Subtract(GetDefaultQuantity(), otherQuantity),

                    _ => throw new InvalidOperationException(null),
                };
            }

            IMeasure getMeasure()
            {
                Enum measureUnit = Measurement.GetMeasureUnit();
                decimal quantity = getDefaultQuantitySum() / GetExchangeRate();

                return GetBaseMeasure(measureUnit, quantity);
            }
            #endregion
        }

        private ValueType GetValidMeasureQuantity(ValueType quantity)
        {
            try
            {
                ValidateQuantity(quantity, GetQuantityTypeCode(), nameof(quantity));
            }
            catch (InvalidEnumArgumentException)
            {
                throw new InvalidOperationException(null);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }

            return quantity;
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum>(IMeasureFactory factory, Enum measureUnit, ValueType quantity)
        : Measure(factory, measureUnit, quantity), IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure
        where TNum : struct
    {
        #region Public methods
        public TSelf GetBaseMeasure(TNum quantity)
        {
            return GetMeasure(Measurement, quantity);
        }

        public TSelf GetMeasure(string name, TNum quantity)
        {
            return (TSelf)GetFactory().Create(name, quantity);
        }

        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            return (TSelf)GetFactory().CreateBaseMeasure(measurement, quantity);
        }

        public TSelf GetNew(TSelf other)
        {
            return (TSelf)GetFactory().CreateNew(other);
        }

        public TNum GetQuantity()
        {
            return (TNum)Quantity;
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum>(IMeasureFactory factory, TEnum measureUnit, ValueType quantity)
        : Measure<TSelf, TNum>(factory, measureUnit, quantity), IMeasure<TSelf, TNum, TEnum>
        where TSelf : class, IMeasure
        where TNum : struct
        where TEnum : struct, Enum
    {
        #region Constants
        private const decimal ConvertRatio = 1000m;
        #endregion

        #region Public methods
        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            return (TSelf)GetBaseMeasure(measureUnit, quantity);
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
        protected TOther ConvertMeasure<TOther>(MeasureOperationMode measureOperationMode)
            where TOther : IMeasure, IConvertMeasure
        {
            MeasureUnitCode measureUnitCode = GetMeasureUnitCode(typeof(TOther));
            Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
            decimal quantity = GetDefaultQuantity();
            quantity = measureOperationMode switch
            {
                MeasureOperationMode.Multiply => quantity * ConvertRatio,
                MeasureOperationMode.Divide => quantity / ConvertRatio,

                _ => throw new InvalidOperationException(null),
            };

            return (TOther)GetBaseMeasure(measureUnit, quantity);
        }

        protected void ValidateBulkSpreadQuantity(ValueType? quantity, string paramName)
        {
            if (GetValidQuantityOrNull(this, NullChecked(quantity, paramName)) is double bulkSpreadQuantity && bulkSpreadQuantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        protected static void ValidateSpreadMeasure(string paramName, ISpreadMeasure? spreadMeasure)
        {
            if (NullChecked(spreadMeasure, paramName).GetSpreadMeasure() is not IMeasure measure)
            {
                throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
            }

            ValidateMeasureUnitCodeByDefinition(measure.GetMeasureUnitCode(), paramName);

            decimal quantity = measure.GetDecimalQuantity();

            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }
        #endregion
    }
}
