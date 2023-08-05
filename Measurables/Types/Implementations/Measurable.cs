namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measurable : MeasureUnit, IMeasurable
    {
        private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }
        private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }

        //private protected Measurable(IMeasurableFactory measurableFactory, params IBaseMeasure[] baseMeasures) : this(measurableFactory)
        //{
        //    _ = NullChecked(baseMeasures, nameof(baseMeasures));

        //    MeasureUnitTypeCode = GetMeasureUnitTypeCode(measurableFactory, baseMeasures);
        //}

        private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }

        private protected Measurable(IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = measurable.MeasurableFactory;
        }

        public IMeasurableFactory MeasurableFactory { get; init; }

        public IMeasurable GetMeasurable(IMeasurable measurable)
        {
            return MeasurableFactory.Create(measurable);
        }

        public virtual TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            measureUnitTypeCode ??= MeasureUnitTypeCode;

            return measureUnitTypeCode switch
            {
                MeasureUnitTypeCode.AreaUnit or
                MeasureUnitTypeCode.DistanceUnit or
                MeasureUnitTypeCode.ExtentUnit or
                MeasureUnitTypeCode.TimePeriodUnit or
                MeasureUnitTypeCode.VolumeUnit or
                MeasureUnitTypeCode.WeightUnit => TypeCode.Double,
                MeasureUnitTypeCode.Currency => TypeCode.Decimal,
                MeasureUnitTypeCode.Pieces => TypeCode.Int64,

               _ => throw InvalidMeasureUnitTypeCodeEnumArgumentException((MeasureUnitTypeCode)measureUnitTypeCode),
            };
        }

        public abstract IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null);
    }

    internal class Measurement : Measurable, IMeasurement
    {
        public const decimal DefaultExchangeRate = decimal.One;

        static Measurement()
        {
            ExchangeRateCollection = new SortedList<Enum, decimal>();

            InitiateConstantExchangeRates();
        }

        internal Measurement(IMeasurementFactory measurementFactory, Enum measureUnit, decimal? exchangeRate = null) : base(measurementFactory, measureUnit)
        {
            if (IsValidMeasureUnit(measureUnit))
            {
                exchangeRate ??= GetExchangeRate(measureUnit);

                ValidateExchangeRate(exchangeRate, measureUnit);

            }
            else
            {
                ValidateExchangeRate(exchangeRate);
            }

            MeasureUnit = measureUnit;
            ExchangeRate = (decimal)exchangeRate!;
        }

        internal Measurement(IMeasurementFactory measurementFactory, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(measurementFactory, measureUnitTypeCode)
        {
            ValidateExchangeRate(exchangeRate);

            MeasureUnit = GetNextCustomMeasureUnit(measureUnitTypeCode);
            ExchangeRate = exchangeRate;
        }

        public object MeasureUnit { get; init; }
        public decimal ExchangeRate { get; init; }
        private static IDictionary<Enum, decimal> ExchangeRateCollection { get; }

        public int CompareTo(IMeasurement? other)
        {
            if (other == null) return 1;

            ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode);

            return ExchangeRate.CompareTo(other.ExchangeRate);
        }

        public bool Equals(IMeasurement? other)
        {
            return other?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) == true
                && CompareTo(other) == 0;
        }

        public override bool Equals(object? obj)
        {
            return obj is IMeasurement other && Equals(other);
        }

        public override IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<Enum, decimal> GetExchangeRateCollection(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            if (measureUnitTypeCode == null) return new SortedList<Enum, decimal>(ExchangeRateCollection);

            Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

            return ExchangeRateCollection
                .Where(x => x.Key.GetType() == measureUnitType)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        public decimal GetExchangeRate(Enum measureUnit)
        {
            decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key == measureUnit).Value;

            if (exchangeRate > 0) return exchangeRate;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
        }
        public IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null)
        {
            throw new NotImplementedException();
        }

        public IMeasurement GetMeasurement(IMeasurement? other = null)
        {
            throw new NotImplementedException();
        }

        public IMeasurement GetMeasurement(IBaseMeasure baseMeasure)
        {
            _ = NullChecked(baseMeasure, nameof(baseMeasure));

            return baseMeasure.Measurement;
        }

        public override Enum GetMeasureUnit()
        {
            return (Enum)MeasureUnit;
        }

        public ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            ValidateExchangeRate(exchangeRate);

            return GetMeasurement(GetNextCustomMeasureUnit(measureUnitTypeCode), exchangeRate);
        }

        public IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode)
        {
            throw new NotImplementedException();
        }

        public void InitiateCustomExchangeRates(MeasureUnitTypeCode measurementUnitTypeCode, params decimal[] exchangeRates)
        {
            ValidateCustomMeasureUnitTypeCode(measurementUnitTypeCode);
            _ = NullChecked(exchangeRates, nameof(exchangeRates));

            int count = exchangeRates.Length;

            if (count == 0) throw new ArgumentOutOfRangeException(nameof(exchangeRates), count, null);

            Type measureUnitType = GetMeasureUnitType(measurementUnitTypeCode);
            string[] names = Enum.GetNames(measureUnitType);

            for (int i = 0; i < count; i++)
            {
                AddExchangeRate(measureUnitType, names, exchangeRates, i);
            }
        }

        public bool IsCustomMeasureUnit(Enum measureUnit)
        {
            if (measureUnit == null) return false;

            return HasMeasureUnitTypeCode(MeasureUnitTypeCode.Currency, measureUnit)
                || HasMeasureUnitTypeCode(MeasureUnitTypeCode.Pieces, measureUnit);
        }

        public bool IsExchangeableTo(Enum measureUnit)
        {
            return IsExchangeableTo(measureUnit, MeasureUnitTypeCode);
        }

        public bool IsValidMeasureUnit(Enum measureUnit)
        {
            return ExchangeRateCollection.ContainsKey(measureUnit);
        }

        public decimal ProportionalTo(IMeasurement measurement)
        {
            _ = NullChecked(measurement, nameof(measurement));

            Enum measureUnit = measurement.GetMeasureUnit();

            ValidateMeasureUnit(measureUnit);

            return ExchangeRate / GetExchangeRate(measureUnit);
        }

        public void RestoreConstantMeasureUnits()
        {
            ExchangeRateCollection.Clear();
            InitiateConstantExchangeRates();
        }

        public bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate)
        {
            return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
        }

        public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
        {
            throw new NotImplementedException();
        }

        public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode)
        {
            if (measurementUnitTypeCode == MeasureUnitTypeCode.Currency) return;
            if (measurementUnitTypeCode == MeasureUnitTypeCode.Pieces) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measurementUnitTypeCode);
        }

        public void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null)
        {
            _ = NullChecked(exchangeRate, nameof(exchangeRate));

            if (exchangeRate > 0)
            {
                if (measureUnit == null) return;

                if (GetExchangeRate(measureUnit) == exchangeRate) return;
            }

            throw ExchangeRateArgumentOutOfRangeException(exchangeRate);
        }

        public override void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            if (IsExchangeableTo(measureUnit, measureUnitTypeCode ?? MeasureUnitTypeCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        public override void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            if (measureUnitTypeCode == MeasureUnitTypeCode) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
        }

        #region Private methods
        private static void AddConstantExchangeRates<T>(params decimal[] exchangeRates) where T : struct, Enum
        {
            Type measureUnitType = typeof(T);
            string[] names = Enum.GetNames(measureUnitType);
            int namesCount = names.Length;
            int exchangeRatesCount = exchangeRates?.Length ?? 0;

            if (exchangeRatesCount != 0 && exchangeRatesCount != namesCount - 1) throw new InvalidOperationException(null);

            ExchangeRateCollection.Add(default(T), DefaultExchangeRate);

            if (exchangeRatesCount > 0)
            {
                for (int i = 0; i < exchangeRatesCount; i++)
                {
                    AddExchangeRate(measureUnitType, names, exchangeRates!, i);
                }
            }
        }

        private static void AddExchangeRate(Type measureUnitType, string[] names, decimal[] exchangeRates, int i)
        {
            Enum measureUnit = (Enum)Enum.Parse(measureUnitType, names[i + 1]);
            ExchangeRateCollection.Add(measureUnit, exchangeRates[i]);
        }

        private Enum GetNextCustomMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
        {
            ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

            IDictionary<Enum, decimal> exchangeRateCollection = GetExchangeRateCollection(measureUnitTypeCode);
            int count = exchangeRateCollection.Count;

            for (int i = 0; i < count; i++)
            {
                Enum measureUnit = exchangeRateCollection.ElementAt(i).Key;
                int measureUnitValue = (int)(object)measureUnit;

                if (measureUnitValue > i) return measureUnit;
            }

            Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);
            return (Enum)Enum.ToObject(measureUnitType, count);
        }

        private static void InitiateConstantExchangeRates()
        {
            AddConstantExchangeRates<AreaUnit>(100, 10000, 1000000);
            AddConstantExchangeRates<Currency>();
            AddConstantExchangeRates<DistanceUnit>(1000);
            AddConstantExchangeRates<Pieces>();
            AddConstantExchangeRates<ExtentUnit>(10, 100, 1000);
            AddConstantExchangeRates<TimePeriodUnit>(60, 1440, 10080, 14400);
            AddConstantExchangeRates<VolumeUnit>(1000, 1000000, 1000000000);
            AddConstantExchangeRates<WeightUnit>(1000, 1000000);
        }

        private bool IsExchangeableTo(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
        {
            return IsValidMeasureUnit(measureUnit) && HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);
        }
        #endregion
    }
}
