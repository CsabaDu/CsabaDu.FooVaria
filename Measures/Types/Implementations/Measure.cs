namespace CsabaDu.FooVaria.Measures.Types.Implementations
{
    /// <summary>
    /// Represents an abstract base class for measures.
    /// </summary>
    internal abstract class Measure : BaseMeasure, IMeasure
    {
        #region Enums
        /// <summary>
        /// Defines the modes for measure operations.
        /// </summary>
        protected enum MeasureOperationMode
        {
            Multiply,
            Divide,
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Measure"/> class.
        /// </summary>
        /// <param name="factory">The measure factory.</param>
        private protected Measure(IMeasureFactory factory) : base(factory, nameof(factory))
        {
            Factory = factory;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the measure factory.
        /// </summary>
        public IMeasureFactory Factory { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds the specified measure to the current measure.
        /// </summary>
        /// <param name="other">The measure to add.</param>
        /// <returns>The sum of the measures.</returns>
        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
        }

        /// <summary>
        /// Divides the current measure by the specified divisor.
        /// </summary>
        /// <param name="divisor">The divisor.</param>
        /// <returns>The result of the division.</returns>
        /// <exception cref="DecimalArgumentOutOfRangeException">Thrown when the divisor is zero.</exception>
        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw DecimalArgumentOutOfRangeException(nameof(divisor), divisor);

            return GetMeasure(divisor, MeasureOperationMode.Divide);
        }

        /// <summary>
        /// Gets the base measure with the specified measure unit and quantity.
        /// </summary>
        /// <param name="measureUnit">The measure unit.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The base measure.</returns>
        public IMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return Factory.Create(measureUnit, quantity);
        }

        /// <summary>
        /// Gets the base measure with the specified name and quantity.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The base measure.</returns>
        public IMeasure GetBaseMeasure(string name, ValueType quantity)
        {
            return Factory.Create(name, quantity);
        }

        /// <summary>
        /// Gets the base measure with the specified measure unit, exchange rate, quantity, and custom name.
        /// </summary>
        /// <param name="measureUnit">The measure unit.</param>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="customName">The custom name.</param>
        /// <returns>The base measure.</returns>
        public IMeasure? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return Factory.Create(measureUnit, exchangeRate, quantity, customName);
        }

        /// <summary>
        /// Gets the base measure with the specified custom name, measure unit code, exchange rate, and quantity.
        /// </summary>
        /// <param name="customName">The custom name.</param>
        /// <param name="measureUnitCode">The measure unit code.</param>
        /// <param name="exchangeRate">The exchange rate.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The base measure.</returns>
        public IMeasure? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return Factory.Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        /// <summary>
        /// Gets the base measure with the specified base measure.
        /// </summary>
        /// <param name="baseMeasure">The base measure.</param>
        /// <returns>The base measure.</returns>
        public IMeasure GetBaseMeasure(IBaseMeasure baseMeasure)
        {
            return Factory.Create(baseMeasure);
        }

        /// <summary>
        /// Multiplies the current measure by the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>The result of the multiplication.</returns>
        public IMeasure Multiply(decimal multiplier)
        {
            return GetMeasure(multiplier, MeasureOperationMode.Multiply);
        }

        /// <summary>
        /// Subtracts the specified measure from the current measure.
        /// </summary>
        /// <param name="other">The measure to subtract.</param>
        /// <returns>The result of the subtraction.</returns>
        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(other, SummingMode.Subtract);
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the base measurement factory.
        /// </summary>
        /// <returns>The base measurement factory.</returns>
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return Factory.MeasurementFactory;
        }

        /// <summary>
        /// Gets the measure factory.
        /// </summary>
        /// <returns>The measure factory.</returns>
        public override sealed IMeasureFactory GetFactory()
        {
            return Factory;
        }

        /// <summary>
        /// Gets the limit mode.
        /// </summary>
        /// <returns>The limit mode.</returns>
        public override sealed LimitMode? GetLimitMode()
        {
            return default;
        }

        /// <summary>
        /// Gets the quantity type code.
        /// </summary>
        /// <returns>The quantity type code.</returns>
        public override sealed TypeCode GetQuantityTypeCode()
        {
            return base.GetQuantityTypeCode();
        }

        /// <summary>
        /// Validates the quantity.
        /// </summary>
        /// <param name="quantity">The quantity to validate.</param>
        /// <param name="paramName">The parameter name associated with the quantity.</param>
        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            base.ValidateQuantity(quantity, paramName);
        }
        #endregion
        #endregion
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the measure with the specified operand and operation mode.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <param name="measureOperationMode">The operation mode.</param>
        /// <returns>The measure.</returns>
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

        /// <summary>
        /// Gets the sum of the current measure and the specified measure.
        /// </summary>
        /// <param name="other">The measure to add or subtract.</param>
        /// <param name="summingMode">The summing mode.</param>
        /// <returns>The sum of the measures.</returns>
        /// <exception cref="InvalidMeasureUnitCodeEnumArgumentException">Thrown when the measure unit code is invalid.</exception>
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

    /// <summary>
    /// Represents a generic measure with a specific quantity type.
    /// </summary>
    /// <typeparam name="TSelf">The type of the measure.</typeparam>
    /// <typeparam name="TNum">The type of the quantity.</typeparam>
    internal abstract class Measure<TSelf, TNum> : Measure, IMeasure<TSelf, TNum>
        where TSelf : class, IMeasure
        where TNum : struct
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Measure{TSelf, TNum}"/> class.
        /// </summary>
        /// <param name="factory">The measure factory.</param>
        /// <param name="quantity">The quantity.</param>
        private protected Measure(IMeasureFactory factory, TNum quantity) : base(factory)
        {
            Quantity = quantity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the quantity.
        /// </summary>
        public TNum Quantity { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the strongly typed measure with the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The base measure.</returns>
        public TSelf GetBaseMeasure(TNum quantity)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return (TSelf)GetBaseMeasure(baseMeasurement, quantity);
        }

        /// <summary>
        /// Gets the strongly typed measure with the specified name and quantity.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The measure.</returns>
        public TSelf GetMeasure(string name, TNum quantity)
        {
            return (TSelf)Factory.Create(name, quantity);
        }

        /// <summary>
        /// Gets the strongly typed measure with the specified measurement and quantity.
        /// </summary>
        /// <param name="measurement">The measurement.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The measure.</returns>
        public TSelf GetMeasure(IMeasurement measurement, TNum quantity)
        {
            return (TSelf)Factory.CreateBaseMeasure(measurement, quantity);
        }

        /// <summary>
        /// Gets a new strongly typed measure based on the specified measure.
        /// </summary>
        /// <param name="other">The measure to base the new measure on.</param>
        /// <returns>The new measure.</returns>
        public TSelf GetNew(TSelf other)
        {
            return (TSelf)Factory.CreateNew(other);
        }

        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <returns>The quantity.</returns>
        public TNum GetQuantity()
        {
            return Quantity;
        }

        #region Override methods
        /// <summary>
        /// Gets the base quantity.
        /// </summary>
        /// <returns>The base quantity.</returns>
        public override sealed ValueType GetBaseQuantity()
        {
            return Quantity;
        }
        #endregion
        #endregion
    }

    /// <summary>
    /// Represents a generic measure with a specific quantity and measure unit type.
    /// </summary>
    /// <typeparam name="TSelf">The type of the measure.</typeparam>
    /// <typeparam name="TNum">The type of the quantity.</typeparam>
    /// <typeparam name="TEnum">The type of the measure unit.</typeparam>
    internal abstract class Measure<TSelf, TNum, TEnum> : Measure<TSelf, TNum>, IMeasure<TSelf, TNum, TEnum>
        where TSelf : class, IMeasure
        where TNum : struct
        where TEnum : struct, Enum
    {
        #region Constants
        private const decimal ConvertRatio = 1000m;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Measure{TSelf, TNum, TEnum}"/> class.
        /// </summary>
        /// <param name="factory">The measure factory.</param>
        /// <param name="measureUnit">The measure unit.</param>
        /// <param name="quantity">The quantity.</param>
        private protected Measure(IMeasureFactory factory, TEnum measureUnit, TNum quantity) : base(factory, quantity)
        {
            Measurement = GetBaseMeasurementFactory().Create(measureUnit);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the measurement.
        /// </summary>
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Exchanges the current strongly typed measure to the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit to exchange to.</param>
        /// <returns>The exchanged measure, or null if not exchangeable.</returns>
        public TSelf? ExchangeTo(TEnum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            return GetMeasure(measureUnit);
        }

        /// <summary>
        /// Gets the measure with the specified measure unit and quantity.
        /// </summary>
        /// <param name="measureUnit">The measure unit.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The measure.</returns>
        public TSelf GetMeasure(TEnum measureUnit, TNum quantity)
        {
            return (TSelf)GetBaseMeasure(measureUnit, quantity);
        }

        /// <summary>
        /// Gets the strongly typed measure with the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit.</param>
        /// <returns>The measure.</returns>
        public TSelf GetMeasure(TEnum measureUnit)
        {
            decimal defaultQuantity = GetDefaultQuantity();
            defaultQuantity /= BaseMeasurement.GetExchangeRate(measureUnit, nameof(measureUnit));
            TNum quantity = (TNum?)defaultQuantity.ToQuantity(typeof(TNum)) ?? throw new InvalidOperationException(null);

            return GetMeasure(measureUnit, quantity);
        }

        /// <summary>
        /// Gets the measure unit.
        /// </summary>
        /// <returns>The measure unit.</returns>
        public TEnum GetMeasureUnit()
        {
            return (TEnum)GetBaseMeasureUnit();
        }

        /// <summary>
        /// Gets the measurement.
        /// </summary>
        /// <returns>The measurement.</returns>
        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        /// <summary>
        /// Tries to exchange the current measure to the specified context.
        /// </summary>
        /// <param name="context">The context to exchange to.</param>
        /// <param name="exchanged">The exchanged measure, if successful.</param>
        /// <returns>True if exchangeable, otherwise false.</returns>
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
        /// <summary>
        /// Converts the measure to another measure type using the specified operation mode.
        /// </summary>
        /// <typeparam name="TOther">The type of the other measure.</typeparam>
        /// <param name="measureOperationMode">The operation mode.</param>
        /// <returns>The converted measure.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the operation mode is invalid.</exception>
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

        /// <summary>
        /// Validates the spread measure.
        /// </summary>
        /// <param name="paramName">The parameter name associated with the spread measure.</param>
        /// <param name="spreadMeasure">The spread measure to validate.</param>
        /// <exception cref="ArgumentTypeOutOfRangeException">Thrown when the spread measure is invalid.</exception>
        /// <exception cref="QuantityArgumentOutOfRangeException">Thrown when the quantity is out of range.</exception>
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
