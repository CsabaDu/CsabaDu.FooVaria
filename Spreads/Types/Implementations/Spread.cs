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

        protected Spread(ISpreadFactory factory, ISpreadMeasure spreadMeasure) : base(factory, (IMeasure)spreadMeasure)
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

        //public override void GetValidBaseMeasurable(ICommonBase? other)
        //{
        //    GetValidBaseMeasurable(this, other);
        //}

        //public override void GetValidBaseMeasurable(IFactory? factory)
        //{
        //    GetValidBaseMeasurable(this, factory);
        //}

        public override void ValidateMeasureUnit(Enum measureUnit)
        {
            MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

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
            ValidateSpreadMeasure(spreadMeasure);

            SpreadMeasure = spreadMeasure;
        }
        #endregion

        #region Properties
        public T SpreadMeasure { get; init; }
        #endregion

        #region Public methods
        public int CompareTo(T? other)
        {
            return SpreadMeasure.CompareTo(other);
        }

        public bool Equals(T? other)
        {
            return SpreadMeasure.Equals(other);
        }

        public T? ExchangeTo(U measureUnit)
        {
            IBaseMeasure excchanged = SpreadMeasure.ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);

            return (T)SpreadMeasure.GetMeasure(excchanged);
        }

        public bool? FitsIn(T? other, LimitMode? limitMode)
        {
            return SpreadMeasure.FitsIn(other, limitMode);
        }

        public decimal GetDefaultQuantity()
        {
            return SpreadMeasure.DefaultQuantity;
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

        public bool TryExchangeTo(U measureUnit, [NotNullWhen(true)] out T? exchanged)
        {
            exchanged = ExchangeTo(measureUnit);

            return exchanged != null;
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public void ValidateSpreadMeasure(IMeasure measure)
        {
            string name = nameof(measure);

            if (NullChecked(measure, name) is not T spreadMeasure)
            {
                throw new ArgumentOutOfRangeException(name, measure.GetType().Name, null);
            }

            double quantity = (double)spreadMeasure.Quantity;

            try
            {
                ValidateQuantity(quantity);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException(name, quantity, null);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
        }

        #region Override methods
        #region Sealed methods
        public override sealed Enum GetMeasureUnit()
        {
            return SpreadMeasure.GetMeasureUnit();
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

