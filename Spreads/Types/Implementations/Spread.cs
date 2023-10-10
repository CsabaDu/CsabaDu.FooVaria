using CsabaDu.FooVaria.Common.Types.Implementations;
using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Spreads.Types
{
    internal abstract class Spread : BaseMeasurable, ISpread
    {
        #region Constructors
        protected Spread(ISpread other) : base(other)
        {
        }

        protected Spread(ISpreadFactory factory, IMeasure spreadMeasure) : base(factory, spreadMeasure)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        public override ISpreadFactory GetFactory()
        {
            return (ISpreadFactory)Factory;
        }

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }

        public override bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
        }

        public override void Validate(ICommonBase? other)
        {
            Validate(this, other);
        }

        public override void Validate(IFactory? factory)
        {
            Validate(this, factory);
        }

        public override void ValidateMeasureUnit(Enum measureUnit)
        {
            MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

            if (IsValidMeasureUnitTypeCode(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        #region Sealed methods
        public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.AreaUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
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
        public abstract ISpread GetSpread(ISpread other);
        public abstract ISpreadMeasure GetSpreadMeasure();
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

        public T GetSpreadMeasure(U measureUnit)
        {
            IBaseMeasure exchanged = SpreadMeasure.ExchangeTo(Defined(measureUnit, nameof(measureUnit)))!;

            return (T)SpreadMeasure.GetMeasure(exchanged);
        }

        public bool IsExchangeableTo(Enum? context)
        {
            return SpreadMeasure.IsExchangeableTo(context);
        }

        public decimal ProportionalTo(T spreadMeaasure)
        {
            return SpreadMeasure.ProportionalTo(spreadMeaasure);
        }

        public void ValidateQuantity(double quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public void ValidateSpreadMeasure(IMeasure spreadMeasure)
        {
            if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is T) return;

            throw new ArgumentOutOfRangeException(nameof(spreadMeasure), spreadMeasure.GetType().Name, null);
        }

        #region Override methods
        #region Sealed methods
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

        public sealed override void Validate(ICommonBase? other)
        {
            Validate(this, other);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract ISpread<T, U> GetSpread(T spreadMeasure);
        public abstract ISpread<T, U> GetSpread(U measureUnit);
        #endregion
        #endregion
    }

    internal sealed class Surface : Spread<IArea, AreaUnit>, ISurface
    {
        public Surface(ISurface other) : base(other)
        {
        }

        public Surface(ISurfaceFactory factory, IArea area) : base(factory, area)
        {
        }

        public override ISurfaceFactory GetFactory()
        {
            return (ISurfaceFactory)Factory;
        }

        public override ISurface GetSpread(IArea area)
        {
            throw new NotImplementedException();
        }

        public override ISurface GetSpread(AreaUnit areaUnit)
        {
            throw new NotImplementedException();
        }

        public override ISurface GetSpread(ISpreadMeasure spreadMeasure)
        {
            throw new NotImplementedException();
        }

        public override ISurface GetSpread(ISpread other)
        {
            throw new NotImplementedException();
        }

        public override void Validate(IFactory? factory)
        {
            Validate(this, factory);
        }
    }

    internal sealed class Body : Spread<IVolume, VolumeUnit>, IBody
    {
        public Body(IBody other) : base(other)
        {
        }

        public Body(IBodyFactory factory, IVolume volume) : base(factory, volume)
        {
        }

        public override IBodyFactory GetFactory()
        {
            return (IBodyFactory)Factory;
        }

        public override IBody GetSpread(IVolume volume)
        {
            throw new NotImplementedException();
        }

        public override IBody GetSpread(VolumeUnit volumeUnit)
        {
            throw new NotImplementedException();
        }

        public override IBody GetSpread(ISpreadMeasure spreadMeasure)
        {
            throw new NotImplementedException();
        }

        public override IBody GetSpread(ISpread other)
        {
            throw new NotImplementedException();
        }

        public override void Validate(IFactory? factory)
        {
            Validate(this, factory);
        }
    }
}
