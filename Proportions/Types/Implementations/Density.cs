namespace CsabaDu.FooVaria.Proportions.Types.Implementations
{
    internal sealed class Density : Proportion<IDensity, WeightUnit, VolumeUnit>, IDensity
    {
        #region Constructors
        public Density(IDensity other) : base(other)
        {
        }

        public Density(IDensityFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        public Density(IDensityFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }
        #endregion

        #region Public methods
        #region Override methods
        public override IDensityFactory GetFactory()
        {
            return (IDensityFactory)Factory;
        }
        #endregion
        #endregion
    }

    internal sealed class Frequency : Proportion<IFrequency, Pieces, TimePeriodUnit>, IFrequency
    {
        public Frequency(IFrequency other) : base(other)
        {
        }

        public Frequency(IFrequencyFactory factory, IBaseRate baseRate) : base(factory, baseRate)
        {
        }

        public Frequency(IFrequencyFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode)
        {
        }

        #region Public methods
        #region Override methods
        public override IFrequencyFactory GetFactory()
        {
            return (IFrequencyFactory)Factory;
        }
        #endregion
        #endregion
    }
}