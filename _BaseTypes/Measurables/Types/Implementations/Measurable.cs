namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations;

public abstract class Measurable(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName), IMeasurable
{
    #region Enums
    protected enum SummingMode
    {
        Add,
        Subtract,
    }
    #endregion

    #region Constants
    public const string Default = nameof(Default);
    #endregion

    #region Constructors
    #region Static constructor
    static Measurable()
    {
        MeasureUnitTypeSet =
        [
            typeof(AreaUnit),
            typeof(Currency),
            typeof(DistanceUnit),
            typeof(ExtentUnit),
            typeof(TimePeriodUnit),
            typeof(Pieces),
            typeof(VolumeUnit),
            typeof(WeightUnit),
        ];

        MeasureUnitCodes = Enum.GetValues<MeasureUnitCode>();

        if (MeasureUnitCodes.Length != MeasureUnitTypeSet.Count) throw new InvalidOperationException(null);

        MeasureUnitTypeCollection = MeasureUnitCodes.ToDictionary
            (
                x => x,
                getMeasureUnitType
            );

        #region Local methods
        static Type getMeasureUnitType(MeasureUnitCode measureUnitCode)
        {
            string? measureUnitCodeName = Enum.GetName(measureUnitCode);

            return MeasureUnitTypeSet.First(x => x.Name == measureUnitCodeName);
        }
        #endregion
    }

    #endregion
    #endregion

    #region Properties
    #region Static properties
    public static Dictionary<MeasureUnitCode, Type> MeasureUnitTypeCollection { get; }
    public static HashSet<Type> MeasureUnitTypeSet { get; }
    public static MeasureUnitCode[] MeasureUnitCodes { get; }
    #endregion
    #endregion

    #region Public methods
    public Enum GetDefaultMeasureUnit()
    {
        return GetMeasureUnitCode().GetDefaultMeasureUnit()!;
    }

    public IEnumerable<string> GetDefaultMeasureUnitNames()
    {
        return GetDefaultNames(GetMeasureUnitCode());
    }

    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypeCollection[GetMeasureUnitCode()];
    }

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == GetMeasureUnitCode();
    }

    public void ValidateMeasureUnitCode(IMeasurable? measurable, [DisallowNull] string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(measurable, paramName).GetMeasureUnitCode();

        ValidateMeasureUnitCode(measureUnitCode, paramName);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && GetMeasureUnitCode().Equals(other.GetMeasureUnitCode());
    }

    //public override IMeasurableFactory GetFactory()
    //{
    //    return (IMeasurableFactory)Factory;
    //}

    public override int GetHashCode()
    {
        return GetMeasureUnitCode().GetHashCode();
    }
    #endregion

    #region Virtual methods
    public virtual MeasureUnitCode GetMeasureUnitCode()
    {
        return MeasurableHelpers.GetMeasureUnitCode(GetBaseMeasureUnit());
    }

    public virtual TypeCode GetQuantityTypeCode()
    {
        return GetMeasureUnitCode().GetQuantityTypeCode();
    }

    public virtual void ValidateMeasureUnit(Enum? measureUnit, [DisallowNull] string paramName)
    {
        if (measureUnit is MeasureUnitCode measureUnitCode)
        {
            ValidateMeasureUnitCode(measureUnitCode, paramName);
        }
        else
        {
            measureUnitCode = MeasurableHelpers.GetMeasureUnitCode(DefinedMeasureUnit(measureUnit, paramName));

            if (HasMeasureUnitCode(measureUnitCode)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
        }
    }

    public virtual void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetBaseMeasureUnit();
    #endregion
    #endregion
}
