using CsabaDu.FooVaria.Common.Statics;
using System.Xml.Linq;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseMeasurable : CommonBase, IBaseMeasurable
{
    #region Constructors
    protected BaseMeasurable(IFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory)
    {
        MeasureUnitTypeCode = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));
    }

    protected BaseMeasurable(IFactory factory, Enum measureUnit) : base(factory)
    {
        MeasureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);
    }

    protected BaseMeasurable(IFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
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

    public Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        return MeasureUnitTypes.GetValidMeasureUnitTypeCode(measureUnit);
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode == MeasureUnitTypeCode;
    }

    public bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit)
    {
        return MeasureUnitTypes.HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit!);
    }

    public bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        return MeasureUnitTypes.IsDefinedMeasureUnit(measureUnit);
    }

    public void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseMeasurable other
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(IBaseMeasurable), MeasureUnitTypeCode);
    }

    //public override void Validate(ICommonBase? other)
    //{
    //    Validate(this, other);
    //}

    public override sealed void Validate(IFooVariaObject? fooVariaObject)
    {
        Validate(this, fooVariaObject);
    }
    #endregion

    #region Virtual methods
    public virtual void ValidateMeasureUnit(Enum measureUnit)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit);
    }

    public virtual void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    public abstract IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes();
    public abstract bool IsValidMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static void Validate<T>(T baseMeasurable, ICommonBase? other) where T : class, IBaseMeasurable
    {
        string name = nameof(other);

        if (NullChecked(other, name) is not T sameTypeBaseMeasurable)
        {
            throw new ArgumentOutOfRangeException(name, other!.GetType().Name, null);
        }

        MeasureUnitTypeCode measureUnitTypeCode = sameTypeBaseMeasurable.MeasureUnitTypeCode;

        if (measureUnitTypeCode == baseMeasurable.MeasureUnitTypeCode) return;

        throw new ArgumentOutOfRangeException(name, measureUnitTypeCode, null);
    }

    private new void Validate<T>(T baseMeasurable, IFooVariaObject? fooVariaObject) where T : class, IBaseMeasurable
    {
        _ = NullChecked(fooVariaObject, nameof(fooVariaObject));

        if (fooVariaObject is IFactory factory)
        {
            base.Validate(baseMeasurable, factory);
        }
        else if (fooVariaObject is ICommonBase other)
        {
            Validate(baseMeasurable, other, out T validbaseMeasurable);

            MeasureUnitTypeCode measureUnitTypeCode = validbaseMeasurable.MeasureUnitTypeCode;

            if (measureUnitTypeCode == baseMeasurable.MeasureUnitTypeCode) return;

            throw new ArgumentOutOfRangeException(nameof(other), measureUnitTypeCode, null);
        }
        else
        {
            throw new InvalidOperationException(null!);
        }
    }
    #endregion
    #endregion
}
