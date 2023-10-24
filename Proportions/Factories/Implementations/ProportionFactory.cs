using CsabaDu.FooVaria.Proportions.Types;
using CsabaDu.FooVaria.Proportions.Types.Implementations;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations
{
    public abstract class ProportionFactory : IBaseRateFactory, IProportionFactory
    {
        public abstract IProportion Create(IBaseRate baseRate);
        public abstract IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
        public abstract IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode);
        public abstract IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode measureUnitTypeCode);
    }

    public sealed class DensityFactory : ProportionFactory, IDensityFactory
    {
        public IDensity Create(WeightUnit numeratorMeasureUnit, decimal quantity, VolumeUnit denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public IDensity Create(IMeasure numerator, VolumeUnit denominatorMeasureUnit)
        {
            throw new NotImplementedException();
        }

        public override IDensity Create(IBaseRate baseRate)
        {
            throw new NotImplementedException();
        }

        public override IDensity Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement)
        {
            throw new NotImplementedException();
        }

        public override IDensity Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode)
        {
            throw new NotImplementedException();
        }

        public override IDensity Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            string name = nameof(numerator);

            if (NullChecked(numerator, name) is not IBaseMeasurable baseMeasurable)
            {
                throw ArgumentTypeOutOfRangeException(name, numerator);
            }

            return new Density(this, baseMeasurable.MeasureUnitTypeCode, numerator.DefaultQuantity, denominatorMeasureUnitTypeCode);
        }
    }
}
