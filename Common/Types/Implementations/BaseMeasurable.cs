using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseMeasurable : IBaseMeasurable
{
    #region Constructors
    protected BaseMeasurable(MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypeCode = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    protected BaseMeasurable(Enum measureUnit)
    {
        MeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
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
    public Enum GetDefaultMeasureUnit()
    {
        return MeasureUnitTypes.GetDefaultMeasureUnit(MeasureUnitTypeCode);
    }

    public IEnumerable<string> GetDefaultNames()
    {
        return MeasureUnitTypes.GetDefaultNames(MeasureUnitTypeCode);
    }

    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypes.GetMeasureUnitType(MeasureUnitTypeCode);
    }

    public Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        return MeasureUnitTypes.GetValidMeasureUnitTypeCode(NullChecked(measureUnit, nameof(measureUnit)));
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit) && measureUnitTypeCode == MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit!);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return HasMeasureUnitTypeCode(measureUnitTypeCode, GetDefaultMeasureUnit());
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit);
    }

    public void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
    }

    #region Overriden methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasurable other
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode;
    }

    public override int GetHashCode()
    {
        return MeasureUnitTypeCode.GetHashCode();
    }
    #endregion

    #region Virtual methods
    public virtual IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return MeasureUnitTypes.GetMeasureUnitTypeCodes();
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit, MeasureUnitTypeCode);
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
}
