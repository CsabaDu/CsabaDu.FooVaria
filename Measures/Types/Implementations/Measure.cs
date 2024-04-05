using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;
using CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measures.Types.Implementations
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
        private protected Measure(IMeasureFactory factory) : base(factory, nameof(factory))
        {
            Factory = factory;
        }
        #endregion

        #region Properties
        public IMeasureFactory Factory { get; init; }
        #endregion

        #region Public methods
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw DecimalArgumentOutOfRangeException(nameof(divisor), divisor);

            return GetMeasure(divisor, MeasureOperationMode.Divide);
        }

        public IMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return Factory.Create(measureUnit, quantity);
        }

        public IMeasure GetBaseMeasure(string name, ValueType quantity)
        {
            return Factory.Create(name, quantity);
        }

        public IMeasure? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return Factory.Create(measureUnit, exchangeRate, quantity, customName);
        }

        public IMeasure? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return Factory.Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        public IMeasure GetBaseMeasure(IBaseMeasure baseMeasure)
        {
            return Factory.Create(baseMeasure);
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
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return Factory.MeasurementFactory;
        }

        public override sealed IMeasureFactory GetFactory()
        {
            return Factory;
        }

        public override sealed LimitMode? GetLimitMode()
        {
            return base.GetLimitMode();
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            return base.GetQuantityTypeCode();
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
            Enum measureUnit = GetBaseMeasureUnit();
            decimal quantity = getQuantity();

            return GetBaseMeasure(measureUnit, quantity);

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
            if (other is null) return GetBaseMeasure(this);

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
                Enum measureUnit = GetBaseMeasurement().GetBaseMeasureUnit();
                decimal quantity = getDefaultQuantitySum() / GetExchangeRate();

                return GetBaseMeasure(measureUnit, quantity);
            }
            #endregion
        }
        #endregion
    }

    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure
        where TNum : struct
    {
        #region Constructors
        private protected Measure(IMeasureFactory factory, TNum quantity) : base(factory)
        {
            Quantity = quantity;
        }
        #endregion

        #region Properties
        public TNum Quantity { get; init; }
        #endregion

        #region Public methods
        public TSelf GetBaseMeasure(TNum quantity)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return (TSelf)GetBaseMeasure(baseMeasurement, quantity);
        }

        public TSelf GetMeasure(string name, TNum quantity)
        {
            return (TSelf)Factory.Create(name, quantity);
        }

        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            return (TSelf)Factory.CreateBaseMeasure(measurement, quantity);
        }

        public TSelf GetNew(TSelf other)
        {
            return (TSelf)Factory.CreateNew(other);
        }

        public TNum GetQuantity()
        {
            return Quantity;
        }

        #region Override methods
        public override sealed ValueType GetBaseQuantity()
        {
            return Quantity;
        }
        #endregion
        #endregion
    }

    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure<TSelf, TNum, TEnum>
        where TSelf : class, IMeasure
        where TNum : struct
        where TEnum : struct, Enum
    {
        #region Constants
        private const decimal ConvertRatio = 1000m;
        #endregion

        #region Constructors
        private protected Measure(IMeasureFactory factory, TEnum measureUnit, TNum quantity) : base(factory, quantity)
        {
            Measurement = GetBaseMeasurementFactory().Create(measureUnit);
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        public TSelf? ExchangeTo(TEnum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            return GetMeasure(measureUnit);
        }

        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            return (TSelf)GetBaseMeasure(measureUnit, quantity);
        }

        public TSelf GetMeasure(TEnum measureUnit)
        {
            decimal defaultQuantity = GetDefaultQuantity();
            defaultQuantity /= BaseMeasurement.GetExchangeRate(measureUnit, nameof(measureUnit));
            TNum quantity = (TNum?)defaultQuantity.ToQuantity(typeof(TNum)) ?? throw new InvalidOperationException(null);

            return GetMeasure(measureUnit, quantity);
        }

        public TEnum GetMeasureUnit()
        {
            return (TEnum)GetBaseMeasureUnit();
        }

        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
        {
            exchanged = null;

            if (!IsExchangeableTo(context)) return false;

            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
            exchanged = ExchangeTo((TEnum)measureUnitElements.MeasureUnit);

            return exchanged is not null;
        }
        #endregion

        #region Protected methods
        protected TOther ConvertMeasure<TOther>(MeasureOperationMode measureOperationMode)
            where TOther : IMeasure, IConvertMeasure
        {
            MeasureUnitCode measureUnitCode = GetMeasureUnitCode(typeof(TOther));
            Enum? measureUnit = measureUnitCode.GetDefaultMeasureUnit();
            decimal quantity = GetDefaultQuantity();
            quantity = measureOperationMode switch
            {
                MeasureOperationMode.Multiply => quantity * ConvertRatio,
                MeasureOperationMode.Divide => quantity / ConvertRatio,

                _ => throw new InvalidOperationException(null),
            };

            return (TOther)GetBaseMeasure(measureUnit!, quantity);
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
