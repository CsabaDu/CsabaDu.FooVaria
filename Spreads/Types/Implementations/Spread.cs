namespace CsabaDu.FooVaria.Spreads.Types.Implementations
{
    internal abstract class Spread : BaseSpread, ISpread
    {
        #region Constructors
        protected Spread(ISpread other) : base(other)
        {
        }

        protected Spread(ISpreadFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        #endregion

        #region Public methods
        public void ValidateShapeExtents(params IExtent[] shapeExtents)
        {
            SpreadMeasures.ValidateShapeExtents(MeasureUnitTypeCode, shapeExtents);
        }

        #region Override methods
        public override ISpreadFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
        }

        public override void ValidateMeasureUnit(Enum measureUnit)
        {
            MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);

            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        #region Sealed methods
        public override sealed ISpread? ExchangeTo(Enum measureUnit)
        {
            IBaseMeasure? exchanged = ((IBaseMeasure)GetSpreadMeasure()).ExchangeTo(measureUnit);

            if (exchanged == null) return null;

            return (ISpread)GetFactory().Create((ISpreadMeasure)exchanged);
        }

        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes();
        }

        public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
        public abstract ISpread GetSpread(params IExtent[] shapeExtents);
        public abstract ISpread GetSpread(IBaseSpread baseSpread);
        #endregion
        #endregion
    }

    internal abstract class Spread<T, U, V> : Spread, ISpread<T, U, V> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure where V : struct, Enum
    {
        #region Constructors
        protected Spread(T other) : base(other)
        {
            SpreadMeasure = (U)other.GetSpreadMeasure();
        }

        protected Spread(ISpreadFactory<T, U> factory, U spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = (U)SpreadMeasures.GetValidSpreadMeasure(spreadMeasure);
        }
        #endregion

        #region Properties
        public U SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public V GetMeasureUnit()
        {
            return (V)SpreadMeasure.Measurement.MeasureUnit;
        }
        public double GetQuantity()
        {
            return (double)SpreadMeasure.Quantity;
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        #region Override methods
        public override ISpreadFactory<T, U> GetFactory()
        {
            return (ISpreadFactory<T, U>)Factory;
        }

        #region Sealed methods
        public override sealed bool? FitsIn(IBaseSpread? other, LimitMode? limitMode)
        {
            if (other == null) return null;

            U spreadMeasure = (U)SpreadMeasures.GetValidSpreadMeasure(other);

            return SpreadMeasure.FitsIn(spreadMeasure, limitMode);
        }

        public override sealed T GetSpread(params IExtent[] shapeExtents)
        {
            return GetFactory().Create(shapeExtents);
        }

        public override sealed T GetSpread(ISpreadMeasure spreadMeasure)
        {
            if (spreadMeasure.GetSpreadMeasure() is U validSpreadMeasure) return GetFactory().Create(validSpreadMeasure);

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        public override sealed U GetSpreadMeasure()
        {
            return SpreadMeasure;
        }

        public override sealed bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return measureUnitTypeCode == MeasureUnitTypeCode;
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit)
        {
            if (NullChecked(measureUnit, nameof(measureUnit)) is U) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract T GetSpread(U spreadMeasure);
        public abstract T GetSpread(V measureUnit);
        #endregion
        #endregion
    }
}

