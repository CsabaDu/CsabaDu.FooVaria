using CsabaDu.FooVaria.Proportions.Types.Implementations;
using CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;
using static CsabaDu.FooVaria.Common.Statics.MeasureUnitTypes;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public abstract class ProportionFactory : IProportionFactory
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

    public abstract IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    //{
    //    ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode));
    //    ValidateQuantity(defaultQuantity, nameof(defaultQuantity));
    //    ValidateMeasureUnitTypeCode(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));

    //    return denominatorMeasureUnitTypeCode switch
    //    {
    //        MeasureUnitTypeCode.TimePeriodUnit => getTimePeriodBasedProportion(),
    //        MeasureUnitTypeCode.VolumeUnit => getVolumedBasedProportion(),
    //        MeasureUnitTypeCode.WeightUnit => getWeightBasedProportion(),

    //        _ => throw exception(nameof(denominatorMeasureUnitTypeCode), denominatorMeasureUnitTypeCode),
    //    };

    //    #region Private methods
    //    IProportion getTimePeriodBasedProportion()
    //    {
    //        return numeratorMeasureUnitTypeCode switch
    //        {
    //            MeasureUnitTypeCode.Pieces => new Frequency(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode),

    //            _ => throw numeratorMeasureUnitTypeCodeException(),
    //        };
    //    }

    //    IProportion getVolumedBasedProportion()
    //    {
    //        return numeratorMeasureUnitTypeCode switch
    //        {
    //            MeasureUnitTypeCode.WeightUnit => new Density(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode),

    //            _ => throw numeratorMeasureUnitTypeCodeException(),
    //        };
    //    }

    //    IProportion getWeightBasedProportion()
    //    {
    //        return numeratorMeasureUnitTypeCode switch
    //        {
    //            MeasureUnitTypeCode.Currency => new Valuability(this, numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode),

    //            _ => throw numeratorMeasureUnitTypeCodeException(),
    //        };
    //    }

    //    ArgumentOutOfRangeException numeratorMeasureUnitTypeCodeException()
    //    {
    //        return exception(nameof(numeratorMeasureUnitTypeCode), numeratorMeasureUnitTypeCode);
    //    }

    //    ArgumentOutOfRangeException exception(string paramName, MeasureUnitTypeCode measureUnitTypeCode)
    //    {
    //        throw new ArgumentOutOfRangeException(paramName, measureUnitTypeCode, null);
    //    }
    //    #endregion
    //}

    public IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IBaseMeasurable baseMeasurable)
        {
            throw ArgumentTypeOutOfRangeException(name, numerator);
        }

       //ValidateMeasureUnitTypeCode(denominatorMeasureUnitTypeCode, name);

        return Create(baseMeasurable.MeasureUnitTypeCode, numerator.DefaultQuantity, denominatorMeasureUnitTypeCode);
    }

    //public IProportion Create(Enum numeratorMeasureUnit, decimal quantity, Enum denominatorMeasureUnit)
    //{
    //    validateNumeratorMeasureUnit();
    //    ValidateQuantity(quantity, nameof(quantity));
    //    ValidateMeasureUnit(denominatorMeasureUnit, nameof(denominatorMeasureUnit));

    //    MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetMeasureUnitTypeCode(numeratorMeasureUnit);
    //    MeasureUnitTypeCode denominatorMeasureUnitTypeCode = GetMeasureUnitTypeCode(denominatorMeasureUnit);
    //    quantity *= GetExchangeRate(numeratorMeasureUnit);
    //    quantity /= GetExchangeRate(denominatorMeasureUnit);

    //    return Create(numeratorMeasureUnitTypeCode, quantity, denominatorMeasureUnitTypeCode);

    //    #region Local methods
    //    void validateNumeratorMeasureUnit()
    //    {
    //        ValidateMeasureUnit(numeratorMeasureUnit, nameof(numeratorMeasureUnit));

    //        MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetMeasureUnitTypeCode(numeratorMeasureUnit);

    //        ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnit)); // TODO
    //    }
    //    #endregion
    //}

    //public IProportion Create(IMeasure numerator, Enum denominatorMeasureUnit)
    //{
    //    decimal quantity = NullChecked(numerator, nameof(numerator)).GetDecimalQuantity();
    //    Enum numeratorMeasureUnit = GetMeasureUnit(numerator);

    //    return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    //}

    public abstract IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit);
    //{
    //    if (NullChecked(numerator, nameof(numerator)) is not IMeasure measure)
    //    {
    //        throw ArgumentTypeOutOfRangeException(nameof(numerator), numerator);
    //    }

    //    decimal quantity = measure.GetDecimalQuantity();
    //    Enum numeratorMeasureUnit = GetMeasureUnit(measure);

    //    return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    //}
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
