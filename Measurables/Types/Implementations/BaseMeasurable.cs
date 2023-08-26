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
        MeasureUnitTypeCode = NullChecked(baseMeasurable, nameof(baseMeasurable)).MeasureUnitTypeCode;
    }

    private protected BaseMeasurable(IQuantifiable[] quantifiables)
    {
        MeasureUnitTypeCode = GetMeasureUnitTypeCode(quantifiables);
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

    public MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum? measureUnit = null)
    {
        if (measureUnit == null) return MeasureUnitTypeCode;

        ValidateMeasureUnit(measureUnit);

        string measureUnitTypeName = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
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
        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(NullChecked(measureUnit, nameof(measureUnit)));
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

        return Enum.IsDefined(measureUnitType, measureUnit);
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
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

    #region Private methods
    private static MeasureUnitTypeCode GetMeasureUnitTypeCode(IQuantifiable[] quantifiables)
    {
        int count = NullChecked(quantifiables, nameof(quantifiables)).Length;

        return count switch
        {
            1 => (quantifiables![0] as IMeasurable)!.MeasureUnitTypeCode,

            2 or
            3 => getRateMeasureUnitTypeCode(),

            _ => throw quantifiablesArgumentOutOfRangeException(),
        };

        #region Local methods
        MeasureUnitTypeCode getRateMeasureUnitTypeCode()
        {
            if (quantifiables!.Any(x => x is not IBaseMeasure))
            {
                throw quantifiablesArgumentOutOfRangeException();
            }

            return count switch
            {
                2 => getFlatRateMeasureUnitTypeCode(),
                3 => getLimitedRateMeasureUnitTypeCode(),

                _ => throw new InvalidOperationException(null),
            };
        }

        MeasureUnitTypeCode getFlatRateMeasureUnitTypeCode()
        {
            IEnumerable<IDenominator> denominators = quantifiables!.OfType<IDenominator>();

            if (denominators.Count() == 1 && quantifiables!.Any(x => x is IMeasure))
            {
                return denominators.First().MeasureUnitTypeCode;
            }

            throw quantifiablesArgumentOutOfRangeException();
        }

        MeasureUnitTypeCode getLimitedRateMeasureUnitTypeCode()
        {
            if (quantifiables!.Any(x => x is ILimit)) return getFlatRateMeasureUnitTypeCode();

            throw quantifiablesArgumentOutOfRangeException();
        }

        ArgumentOutOfRangeException quantifiablesArgumentOutOfRangeException()
        {
            return new ArgumentOutOfRangeException(nameof(quantifiables));
        }
        #endregion
    }
    #endregion
}
