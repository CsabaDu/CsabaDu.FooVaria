using CsabaDu.FooVaria.Proportions.Factories;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal abstract class Proportion : BaseRate, IProportion
    {
        public Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
        }

        protected Proportion(IProportion other) : base(other)
        {
            NumeratorMeasureUnitTypeCode = other.NumeratorMeasureUnitTypeCode;
        }

        protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
            NumeratorMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numerator, denominatorMeasureUnitTypeCode)
        {
            NumeratorMeasureUnitTypeCode = numerator.MeasureUnitTypeCode;
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, IMeasurement denominator) : base(factory, numerator, denominator)
        {
            NumeratorMeasureUnitTypeCode = numerator.MeasureUnitTypeCode;
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, Enum denominatorMeasureUnit) : base(factory, numerator, denominatorMeasureUnit)
        {
            NumeratorMeasureUnitTypeCode = numerator.MeasureUnitTypeCode;
        }

        public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }

        public override sealed MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
        {
            return NumeratorMeasureUnitTypeCode;
        }

        public abstract IProportion GetProportion(IBaseRate baseRate);
        public abstract IProportion GetProportion(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
    }

    internal abstract class Proportion<T, U, W> : Proportion, IProportion<T, U, W> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    {
        protected Proportion(T other) : base(other)
        {
        }

        protected Proportion(IProportionFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numerator, denominatorMeasureUnitTypeCode)
        {
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, IMeasurement denominator) : base(factory, numerator, denominator)
        {
        }

        protected Proportion(IProportionFactory factory, IMeasure numerator, Enum denominatorMeasureUnit) : base(factory, numerator, denominatorMeasureUnit)
        {
        }

        protected Proportion(IProportionFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }

        public override T GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            throw new NotImplementedException();
        }

        public override T GetProportion(IBaseRate baseRate)
        {
            throw new NotImplementedException();
        }

        public override T GetProportion(IBaseMeasure numerator, IMeasurement denominatorMeasurement)
        {
            throw new NotImplementedException();
        }

        public T GetProportion(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public T GetProportion(IMeasure numerator, W denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public decimal GetQuantity(U numeratorMeasureUnit, W denominatorMeasureUnit)
        {
            return DefaultQuantity
                / GetExchangeRate(numeratorMeasureUnit)
                * GetExchangeRate(denominatorMeasureUnit);
        }
    }

    internal sealed class Density : Proportion<IDensity, WeightUnit, VolumeUnit>, IDensity
    {
        public Density(IDensity other) : base(other)
        {
        }

        public Density(IDensityFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        public Density(IDensityFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numerator, denominatorMeasureUnitTypeCode)
        {
        }

        public Density(IDensityFactory factory, IMeasure numerator, IMeasurement denominator) : base(factory, numerator, denominator)
        {
        }

        public Density(IDensityFactory factory, IMeasure numerator, Enum denominatorMeasureUnit) : base(factory, numerator, denominatorMeasureUnit)
        {
        }

        public Density(IDensityFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }

        public override IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator)
        {
            throw new NotImplementedException();
        }

        public override void ValidateQuantity(decimal quantity)
        {
            if (quantity > 0) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }
    }
}
