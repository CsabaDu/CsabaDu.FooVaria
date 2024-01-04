using CsabaDu.FooVaria.Common.Types.Implementations;

namespace CsabaDu.FooVaria.Masses.Types.Implementations
{
    internal abstract class Mass : Quantifiable, IMass
    {
        protected Mass(IMass other) : base(other)
        {
        }

        protected Mass(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        protected Mass(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected Mass(IQuantifiableFactory factory, IBaseMeasurement baseMeasurement) : base(factory, baseMeasurement)
        {
        }

        protected Mass(IQuantifiableFactory factory, IQuantifiable quantifiable) : base(factory, quantifiable)
        {
        }

        protected Mass(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitTypeCode, quantifiables)
        {
        }

        public abstract IWeight Weight { get; init; }

        public abstract int CompareTo(IBaseSpread? other);
        public abstract bool Equals(IBaseSpread? other);
        public abstract IBaseSpread? ExchangeTo(Enum context);
        public abstract bool? FitsIn(IBaseSpread? comparable, LimitMode? limitMode);
        public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
        public abstract IBody GetBody();

        public override decimal GetDefaultQuantity()
        {
            throw new NotImplementedException();
        }

        public abstract IProportion<WeightUnit, VolumeUnit> GetDensity();
        public abstract IMass GetMass(IWeight weight, IBody body);

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }

        public abstract MeasureUnitTypeCode GetMeasureUnitTypeCode();
        public abstract double GetQuantity();
        public abstract ISpreadMeasure GetSpreadMeasure();
        public abstract IWeight GetVolumetricWeight(decimal ratio);
        public abstract bool IsExchangeableTo(Enum? context);
        public abstract decimal ProportionalTo(IBaseSpread comparable);
        public abstract void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName);

        public override void ValidateQuantity(ValueType? quantity, string paramName)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            yield return MeasureUnitTypeCode.WeightUnit;
            yield return MeasureUnitTypeCode.VolumeUnit;
        }
    }
}
