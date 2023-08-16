using System.Reflection.Metadata;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measurable : BaseMeasurable, IMeasurable
    {
        #region Constructors
        private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = measurable.MeasurableFactory;
        }

        //private protected Measurable(IMeasurableFactory measurableFactory, params IBaseMeasure[] baseMeasures) : this(measurableFactory)
        //{
        //    _ = NullChecked(baseMeasures, nameof(baseMeasures));

        //    MeasureUnitTypeCode = GetMeasureUnitTypeCode(measurableFactory, baseMeasures);
        //}
        #endregion

        #region Properties
        public IMeasurableFactory MeasurableFactory { get; init; }
        #endregion

        #region Public methods
        public IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null)
        {
            return MeasurableFactory.CreateDefault(measurable ?? this);
        }

        public IMeasurable GetMeasurable(IMeasurable measurable)
        {
            return MeasurableFactory.Create(measurable);
        }

        public virtual IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
        {
            return NullChecked(measurableFactory, nameof(measurableFactory)).Create(measurable);
        }

        public virtual TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            TypeCode? quantityTypeCode = (measureUnitTypeCode ?? MeasureUnitTypeCode).GetQuantityTypeCode();

            return quantityTypeCode ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode!.Value);
        }
        #endregion
    }

    internal sealed class Denominator : BaseMeasure, IDenominator
    {
        private const decimal DefaultDenominatorQuantity = decimal.One;

        public Denominator(IDenominator denominator) : base(denominator)
        {
            Quantity = denominator.Quantity;
        }

        public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit)
        {
            Quantity = GetDenominatorQuantity(quantity);
        }

        public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, IMeasurement measurement) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measurement)
        {
            Quantity = GetDenominatorQuantity(quantity);
        }

        public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, customMeasureUnitTypeCode, exchangeRate, customName)
        {
            Quantity = GetDenominatorQuantity(quantity);
        }

        public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit, decimal exchangeRate, string? customName) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit, exchangeRate, customName)
        {
            Quantity = GetDenominatorQuantity(quantity);
        }

        public override object Quantity { get; init; }

        public override TypeCode QuantityTypeCode => TypeCode.Decimal;

        public override RateComponentCode RateComponentCode => RateComponentCode.Denominator;

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
        {
            return GetDenominator(name, quantity);
        }

        public IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = null)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(IDenominator? other = null)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = null)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string? customName = null, ValueType? quantity = null)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null, ValueType? quantity = null)
        {
            throw new NotImplementedException();
        }

        public IDenominator GetDenominator(string name, ValueType? quantity = null)
        {
            throw new NotImplementedException();
        }

        public IDenominatorFactory GetDenominatorFactory()
        {
            return MeasurableFactory as IDenominatorFactory ?? throw new InvalidOperationException(null);
        }

        public override ValueType GetQuantity(ValueType? quantity = null)
        {
            quantity = base.GetQuantity(quantity);

            if ((decimal)quantity <= 0) throw QuantityArgumentOutOfRangeException(quantity);

            return quantity;
        }

        #region Private methods
        private ValueType GetDenominatorQuantity(ValueType? quantity)
        {
            return GetQuantity(quantity ?? DefaultDenominatorQuantity);
        }
        #endregion
    }
}
