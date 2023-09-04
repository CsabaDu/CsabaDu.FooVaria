namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseMeasurable : IBaseMeasurable
{
    #region Constructors
    protected BaseMeasurable(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        MeasureUnitTypeCode = measureUnitTypeCode;
    }

    protected BaseMeasurable(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        MeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
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
        Type enumType = GetMeasureUnitType(measureUnitTypeCode)!;

        return (Enum)Enum.ToObject(enumType, 0);
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
        Type measureUnitType = GetMeasureUnit().GetType();

        if (measureUnitTypeCode == null) return measureUnitType;

        ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

        string measureUnitTypeName = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode.Value)!;
        measureUnitTypeName = measureUnitType.FullName!.Replace(measureUnitType.Name, measureUnitTypeName);

        return Type.GetType(measureUnitTypeName)!;
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null)
    {
        return measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit);
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(NullChecked(measureUnit, nameof(measureUnit)));
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

        return Enum.IsDefined(measureUnitType, measureUnit);
    }

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    public abstract MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null);
    public abstract IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    public abstract void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null);
    public abstract void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    #endregion
    #endregion
}
