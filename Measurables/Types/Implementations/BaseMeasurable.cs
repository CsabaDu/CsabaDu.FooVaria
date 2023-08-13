namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class BaseMeasurable : IBaseMeasurable
{
    #region Constructors
    private protected BaseMeasurable(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        MeasureUnitTypeCode = measureUnitTypeCode;
    }

    private protected BaseMeasurable(Enum measureUnit)
    {
       ValidateMeasureUnit(measureUnit);

        MeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
    }

    private protected BaseMeasurable(IBaseMeasurable baseMeasurable)
    {
        _ = NullChecked(baseMeasurable, nameof(baseMeasurable));

        MeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;
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
        if (measureUnitTypeCode == null)
        {
            IEnumerable<string> defaultNames = new List<string>();

            foreach (MeasureUnitTypeCode item in GetMeasureUnitTypeCodes())
            {
                Type measureUnitType = GetMeasureUnitType(item);
                IEnumerable<string> next = Enum.GetNames(measureUnitType);
                defaultNames = defaultNames.Union(next);
            }

            return defaultNames;
        }
        else
        {
            Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

            return Enum.GetNames(measureUnitType);
        }
    }

    public Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Type measureUnitType = GetMeasureUnit().GetType();

        if (measureUnitTypeCode == null) return measureUnitType;

        ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

        string nameSpace = measureUnitType.Namespace!;
        string name = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode.Value)!;

        return Type.GetType(nameSpace + "." + name)!;
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null)
    {
        if (measureUnit == null) return MeasureUnitTypeCode;

        ValidateMeasureUnit(measureUnit);

        string name = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), name);
    }

    public IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return Enum.GetValues<MeasureUnitTypeCode>();
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null)
    {
        return measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit);
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        _ = NullChecked(measureUnit, nameof(measureUnit));

        foreach (MeasureUnitTypeCode item in GetMeasureUnitTypeCodes())
        {
            Type measureUnitType = GetMeasureUnitType(item);

            if (Enum.IsDefined(measureUnitType, measureUnit)) return true;
        }

        return false;
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        _ = NullChecked(measureUnit, nameof(measureUnit));

        if (IsDefinedMeasureUnit(measureUnit))
        {
            if (measureUnitTypeCode == null) return;

            if (measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit)) return;
        }

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public virtual void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (Enum.IsDefined(typeof(MeasureUnitTypeCode), measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode); 
    }

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    #endregion
    #endregion
}
