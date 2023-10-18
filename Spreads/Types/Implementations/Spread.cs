using CsabaDu.FooVaria.Common.Statics;
using CsabaDu.FooVaria.Common.Types.Implementations;
using CsabaDu.FooVaria.Spreads.Statics;
using System.Diagnostics.CodeAnalysis;

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
        public override IBaseSpreadFactory GetFactory()
        {
            return (IBaseSpreadFactory)Factory;
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
        public abstract ISpread GetSpread(IBaseSpread baseSppread);
        #endregion
        #endregion
    }

    internal abstract class Spread<T, U> : Spread, ISpread<T, U> where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        #region Constructors
        protected Spread(ISpread<T, U> other) : base(other)
        {
            SpreadMeasure = other.SpreadMeasure;
        }

        protected Spread(ISpreadFactory factory, T spreadMeasure) : base(factory, spreadMeasure)
        {
            SpreadMeasure = (T)SpreadMeasures.GetValidSpreadMeasure(spreadMeasure);
        }
        #endregion

        #region Properties
        public T SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public int CompareTo(T? spreadMeasure)
        {
            return SpreadMeasure.CompareTo(spreadMeasure);
        }

        public bool Equals(T? spreadMeasure)
        {
            return SpreadMeasure.Equals(spreadMeasure);
        }

        public bool? FitsIn(T? spreadMeasure, LimitMode? limitMode)
        {
            return SpreadMeasure.FitsIn(spreadMeasure, limitMode);
        }

        public U GetMeasureUnit()
        {
            return (U)SpreadMeasure.Measurement.MeasureUnit;
        }
        public double GetQuantity()
        {
            return (double)SpreadMeasure.Quantity;
        }

        public decimal ProportionalTo(T spreadMeaasure)
        {
            return SpreadMeasure.ProportionalTo(spreadMeaasure);
        }

        public bool IsExchangeableTo(U context)
        {
            return SpreadMeasure.IsExchangeableTo(context);
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure)
        {
            if (SpreadMeasures.GetValidSpreadMeasure(spreadMeasure) is T) return;

            throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure!);
        }

        #region Override methods
        #region Sealed methods
        public override sealed decimal GetDefaultQuantity()
        {
            return SpreadMeasure.DefaultQuantity;
        }

        public override sealed T GetSpreadMeasure()
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
        public abstract ISpread<T, U> GetSpread(T spreadMeasure);
        public abstract ISpread<T, U> GetSpread(U measureUnit);
        #endregion
        #endregion
    }
}

