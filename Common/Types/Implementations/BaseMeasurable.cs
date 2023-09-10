using CsabaDu.FooVaria.Common.Statics;

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
        MeasureUnitTypeCode = GetValidMeasureUnitTypeCode(measureUnit);
    }

    protected BaseMeasurable(IBaseMeasurable baseMeasurable)
    {
        MeasureUnitTypeCode = NullChecked(baseMeasurable, nameof(baseMeasurable)).MeasureUnitTypeCode;
    }
    #endregion

    #region Properties
    public MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
    #endregion

    #region Public methods
    public Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode)!;

        return (Enum)Enum.ToObject(measureUnitType, 0);
    }
    
    public string GetDefaultName(Enum? measureUnit = null)
    {
        if (measureUnit == null)
        {
            measureUnit = GetMeasureUnit();
        }
        else
        {
            ValidateMeasureUnit(measureUnit);
        }

        Type measureUnitType = measureUnit.GetType();

        return Enum.GetName(measureUnitType, measureUnit)!;
    }

    public IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode != null) return getDefaultNames(measureUnitTypeCode.Value);

        IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = GetMeasureUnitTypeCodes();
        IEnumerable<string> defaultNames = getDefaultNames(measureUnitTypeCodes.First());

        for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
        {
            IEnumerable<string> next = getDefaultNames(measureUnitTypeCodes.ElementAt(i));
            defaultNames = defaultNames.Union(next);
        }

        return defaultNames;

        #region Local methods
        IEnumerable<string> getDefaultNames(MeasureUnitTypeCode measureUnitTypeCode)
        {
            Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

            return Enum.GetNames(measureUnitType);
        }
        #endregion
    }

    public Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode == null) return GetMeasureUnit().GetType();

        measureUnitTypeCode = GetValidMeasureUnitTypeCode(measureUnitTypeCode);

        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode.Value);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null)
    {
        return measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit);
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit);
    }

    #region Virtual methods
    public virtual MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null)
    {
        if (measureUnit == null) return MeasureUnitTypeCode;

        ValidateMeasureUnit(measureUnit);

        return MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
    }

    public virtual IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return Enum.GetValues<MeasureUnitTypeCode>();
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

    #region Protected methods
    protected MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        string measureUnitTypeName = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
    }
    #endregion

    #region Private methods
    private MeasureUnitTypeCode GetValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        return measureUnitTypeCode;
    }
    #endregion
}
