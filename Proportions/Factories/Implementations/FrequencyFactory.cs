using CsabaDu.FooVaria.Proportions.Types.Implementations;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class FrequencyFactory : ProportionFactory<IFrequency, Pieces, TimePeriodUnit>, IFrequencyFactory
{
    #region Public methods
    #region Override methods
    public override IFrequency Create(IBaseRate baseRate)
    {
        if (baseRate is IFrequency other) return Create(other);

        return new Frequency(this, baseRate);
    }

    public override IFrequency Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        return new Frequency(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
    }

    public override IFrequency Create(IFrequency other)
    {
        return new Frequency(other);
    }
    #endregion
    #endregion
}
