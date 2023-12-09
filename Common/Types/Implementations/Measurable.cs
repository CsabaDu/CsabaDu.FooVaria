using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class Measurable : CommonBase, IMeasurable
{
    protected enum SummingMode
    {
        Add,
        Subtract,
    }

    #region Constructors
    protected Measurable(IMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory)
    {
        MeasureUnitTypeCode = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(factory)
    {
        MeasureUnitTypeCode = GetValidMeasureUnitTypeCode(measureUnit);
    }

    protected Measurable(IMeasurableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
        MeasureUnitTypeCode = measurable.MeasureUnitTypeCode;
    }

    protected Measurable(IMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IMeasurable[] measurables) : base(factory, measurables)
    {
        MeasureUnitTypeCode = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    protected Measurable(IMeasurable other) : base(other)
    {
        MeasureUnitTypeCode = other.MeasureUnitTypeCode;
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

    public IEnumerable<string> GetDefaultMeasureUnitNames()
    {
        return MeasureUnitTypes.GetDefaultNames(MeasureUnitTypeCode);
    }

    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypes.GetMeasureUnitType(MeasureUnitTypeCode);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode == MeasureUnitTypeCode;
    }

    public bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode;
    }

    public override IMeasurableFactory GetFactory()
    {
        return (IMeasurableFactory)Factory;
    }

    public override int GetHashCode()
    {
        return MeasureUnitTypeCode.GetHashCode();
    }

    public override void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateMeasurable, paramName);

        #region Local methods
        void validateMeasurable()
        {
            _ = GetValidMeasurable(this, rootObject!, paramName);
        }
        #endregion
    }
    #endregion

    #region Virtual methods
    public virtual IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return MeasureUnitTypes.GetMeasureUnitTypeCodes();
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, paramName);
    }

    public virtual void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode, paramName);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static TSelf GetValidMeasurable<TSelf>(TSelf commonBase, IRootObject other, string paramName) where TSelf : class, IMeasurable
    {
        TSelf baseMeasurable = GetValidCommonBase(commonBase, other, paramName);
        MeasureUnitTypeCode measureUnitTypeCode = commonBase.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;

        return GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode, paramName);
    }

    protected static TSelf GetValidBaseMeasurable<TSelf, TEnum>(TSelf other, TEnum commonBaseProperty, TEnum otherProperty, string paramName) where TSelf : class, IMeasurable where TEnum : struct, Enum
    {
        if (commonBaseProperty.Equals(otherProperty)) return other;

        throw new ArgumentOutOfRangeException(paramName, otherProperty, null);
    }
    #endregion
    #endregion
}
