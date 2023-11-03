using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseMeasurable : CommonBase, IBaseMeasurable
{
    #region Constructors
    protected BaseMeasurable(IBaseMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory)
    {
        MeasureUnitTypeCode = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    protected BaseMeasurable(IBaseMeasurableFactory factory, Enum measureUnit) : base(factory)
    {
        MeasureUnitTypeCode = MeasureUnitTypes.GetValidMeasureUnitTypeCode(measureUnit);
    }

    protected BaseMeasurable(IBaseMeasurableFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
        MeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;
    }

    protected BaseMeasurable(IBaseMeasurable other) : base(other)
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

    public IEnumerable<string> GetDefaultNames()
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

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasurable other
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode;
    }

    public override IBaseMeasurableFactory GetFactory()
    {
        return (IBaseMeasurableFactory)Factory;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(IBaseMeasurable), MeasureUnitTypeCode);
    }

    public override void Validate(IFooVariaObject? fooVariaObject, string paramName)
    {
        ValidateCommonBaseAction = () => _ = GetValidBaseMeasurable(this, fooVariaObject!, paramName);

        Validate(this, fooVariaObject, paramName);
    }
    #endregion

    #region Virtual methods
    public virtual IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return MeasureUnitTypes.GetMeasureUnitTypeCodes();
    }

    public bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode);
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
    protected static T GetValidBaseMeasurable<T>(T commonBase, IFooVariaObject other, string paramName) where T : class, IBaseMeasurable
    {
        T baseMeasurable = GetValidCommonBase(commonBase, other, paramName);
        MeasureUnitTypeCode measureUnitTypeCode = commonBase.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;

        return GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode, paramName);
    }

    protected static T GetValidBaseMeasurable<T, U>(T other, U commonBaseProperty, U otherProperty, string paramName) where T : class, IBaseMeasurable where U : struct, Enum
    {
        if (commonBaseProperty.Equals(otherProperty)) return other;

        throw new ArgumentOutOfRangeException(paramName, otherProperty, null);
    }

    public bool IsExchangeableTo(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode == MeasureUnitTypeCode;
    }
    #endregion
    #endregion
}
