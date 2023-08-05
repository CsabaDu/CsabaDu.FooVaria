namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class MeasureUnit : IMeasureUnit
{
    private protected MeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        MeasureUnitTypeCode = measureUnitTypeCode;
    }

    private protected MeasureUnit(Enum measureUnit)
    {
       ValidateMeasureUnit(measureUnit);

        MeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
    }

    private protected MeasureUnit(IMeasurable measurable)
    {
        _ = NullChecked(measurable, nameof(measurable));

        MeasureUnitTypeCode = measurable.MeasureUnitTypeCode;
    }

    public MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    public Enum GetDefaultMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Type enumType = GetMeasureUnitType(measureUnitTypeCode)!;

        return (Enum)Enum.ToObject(enumType, 0);
    }

    public Enum? GetDefinedMeasureUnit(Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit) ? measureUnit : null;
    }

    public Type GetMeasureUnitType(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        Type measureUnitType = GetMeasureUnit().GetType();

        if (measureUnitTypeCode == null) return measureUnitType;

        string nameSpace = measureUnitType.Namespace! + ".";
        string name = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode)!;

        return Type.GetType(nameSpace + name)!;
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null)
    {
        if (measureUnit == null) return MeasureUnitTypeCode;

        ValidateMeasureUnit(measureUnit);

        string name = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), name);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum? measureUnit = null)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        MeasureUnitTypeCode otherMeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

        return otherMeasureUnitTypeCode == measureUnitTypeCode;
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        _ = NullChecked(measureUnit, nameof(measureUnit));

        return Enum.IsDefined(measureUnit.GetType(), measureUnit);
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

    public abstract Enum GetMeasureUnit();
}
