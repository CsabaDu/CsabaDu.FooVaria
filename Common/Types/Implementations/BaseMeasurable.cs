﻿using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseMeasurable : IBaseMeasurable
{
    #region Constructors
    protected BaseMeasurable(MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypeCode = GetValidMeasureUnitTypeCode(measureUnitTypeCode);
    }

    protected BaseMeasurable(Enum measureUnit)
    {
        MeasureUnitTypeCode = MeasureUnitTypes.GetValidMeasureUnitTypeCode(measureUnit);
    }

    protected BaseMeasurable(IBaseMeasurable other)
    {
        MeasureUnitTypeCode = NullChecked(other, nameof(other)).MeasureUnitTypeCode;
    }
    #endregion

    #region Properties
    public MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
    #endregion

    #region Public methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasurable other
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode;
    }

    public Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode)!;

        return MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitType);
    }

    public IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return MeasureUnitTypes.GetDefaultNames(measureUnitTypeCode);
    }

    public override int GetHashCode()
    {
        return MeasureUnitTypeCode.GetHashCode();
    }

    public Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode ?? MeasureUnitTypeCode);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null)
    {
        if (measureUnit == null) return hasMeasureUnitTypeCode();

        return IsDefinedMeasureUnit(measureUnit) && hasMeasureUnitTypeCode();

        #region Local methods
        bool hasMeasureUnitTypeCode()
        {
            return measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit);
        }
        #endregion
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit);
    }

    #region Virtual methods
    public virtual MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null)
    {
        if (measureUnit == null) return MeasureUnitTypeCode;

        return MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
    }

    public virtual IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return MeasureUnitTypes.GetMeasureUnitTypeCodes();
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
    }

    public virtual void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    #endregion
    #endregion

    #region Private methods
    private MeasureUnitTypeCode GetValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        return measureUnitTypeCode;
    }
    #endregion
}
