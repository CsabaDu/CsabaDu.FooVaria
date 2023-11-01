using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;
using static CsabaDu.FooVaria.Common.Statics.MeasureUnitTypes;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class ProportionFactory : IProportionFactory
{
    #region Public methods
    public IProportion Create(IBaseRate baseRate)
    {
        _ = NullChecked(baseRate, nameof(baseRate));

        return Create(baseRate.GetNumeratorMeasureUnitTypeCode(), baseRate.DefaultQuantity, baseRate.MeasureUnitTypeCode);
    }

    public IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IQuantifiable quantifiable) throw ArgumentTypeOutOfRangeException(name, numerator);

        MeasureUnitTypeCode denominatorMeasureUnitTypeCode = NullChecked(denominatorMeasurement, nameof(denominatorMeasurement)).MeasureUnitTypeCode;

        return (IProportion)Create(quantifiable, denominatorMeasureUnitTypeCode);

    }

    public IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        ValidateNumeratorMeasureUnitTypeCode(numeratorMeasureUnitTypeCode);
        ValidateQuantity(defaultQuantity, nameof(defaultQuantity));
        _ = Defined(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));

        if (numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.Currency
            && denominatorMeasureUnitTypeCode == MeasureUnitTypeCode.WeightUnit)
        {
            return new Valuability(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        if (numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.Pieces
            && denominatorMeasureUnitTypeCode == MeasureUnitTypeCode.TimePeriodUnit)
        {
            return new Frequency(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        if (numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.WeightUnit
            && denominatorMeasureUnitTypeCode == MeasureUnitTypeCode.VolumeUnit)
        {
            return new Density(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        throw new ArgumentOutOfRangeException(nameof(denominatorMeasureUnitTypeCode), denominatorMeasureUnitTypeCode, null);
    }

    public IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IBaseMeasurable baseMeasurable)
        {
            throw ArgumentTypeOutOfRangeException(name, numerator);
        }

       _ = Defined(denominatorMeasureUnitTypeCode, name);

        return Create(baseMeasurable.MeasureUnitTypeCode, numerator.DefaultQuantity, denominatorMeasureUnitTypeCode);
    }

    public IProportion Create(Enum numeratorMeasureUnit, decimal quantity, Enum denominatorMeasureUnit)
    {
        validateNumeratorMeasureUnit();
        ValidateQuantity(quantity, nameof(quantity));
        _ = DefinedMeasureUnit(denominatorMeasureUnit, nameof(denominatorMeasureUnit));

        MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetMeasureUnitTypeCode(numeratorMeasureUnit);
        MeasureUnitTypeCode denominatorMeasureUnitTypeCode = GetMeasureUnitTypeCode(denominatorMeasureUnit);
        quantity *= GetExchangeRate(numeratorMeasureUnit);
        quantity /= GetExchangeRate(denominatorMeasureUnit);

        return Create(numeratorMeasureUnitTypeCode, quantity, denominatorMeasureUnitTypeCode);

        #region Local methods
        void validateNumeratorMeasureUnit()
        {
            _ = DefinedMeasureUnit(denominatorMeasureUnit, nameof(denominatorMeasureUnit));

            MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetMeasureUnitTypeCode(denominatorMeasureUnit);

            ValidateNumeratorMeasureUnitTypeCode(numeratorMeasureUnitTypeCode);
        }
        #endregion
    }

    public IProportion Create(IMeasure numerator, Enum denominatorMeasureUnit)
    {
        decimal quantity = NullChecked(numerator, nameof(numerator)).GetDecimalQuantity();
        Enum numeratorMeasureUnit = GetMeasureUnit(numerator);

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }

    public IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit)
    {
        if (NullChecked(numerator, nameof(numerator)) is not IMeasure measure)
        {
            throw ArgumentTypeOutOfRangeException(nameof(numerator), numerator);
        }

        decimal quantity = measure.GetDecimalQuantity();
        Enum numeratorMeasureUnit = GetMeasureUnit(measure);

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static Enum GetMeasureUnit(IMeasure measure)
    {
        return measure.Measurement.GetMeasureUnit();
    }

    private static void ValidateQuantity(decimal quantity, string name)
    {
        if (quantity >= 0) return;

        throw QuantityArgumentOutOfRangeException(name, quantity);
    }

    private static void ValidateNumeratorMeasureUnitTypeCode(MeasureUnitTypeCode numeratorMeasureUnitTypeCode)
    {
        if (numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.Currency
            || numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.Pieces
            || numeratorMeasureUnitTypeCode == MeasureUnitTypeCode.WeightUnit)
        {
            return;
        }

        throw new ArgumentOutOfRangeException(nameof(numeratorMeasureUnitTypeCode), numeratorMeasureUnitTypeCode, null);
    }
    #endregion
    #endregion
}
