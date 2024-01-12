//namespace CsabaDu.FooVaria.Common.Types.Implementations;

//public abstract class Measurable : CommonBase, IMeasurable
//{
//    #region Enums
//    protected enum SummingMode
//    {
//        Add,
//        Subtract,
//    }
//    #endregion

//    #region Constructors
//    protected Measurable(IMeasurableFactory factory, MeasureUnitCode measureUnitCode) : base(factory)
//    {
//        MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
//    }

//    protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(factory)
//    {
//        MeasureUnitCode = GetValidMeasureUnitCode(measureUnit);
//    }

//    protected Measurable(IMeasurableFactory factory, IMeasurable measurable) : base(factory, measurable)
//    {
//        MeasureUnitCode = measurable.MeasureUnitCode;
//    }

//    protected Measurable(IMeasurableFactory factory, MeasureUnitCode measureUnitCode, params IMeasurable[] measurables) : base(factory, measurables)
//    {
//        MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
//    }

//    protected Measurable(IMeasurable other) : base(other)
//    {
//        MeasureUnitCode = other.MeasureUnitCode;
//    }
//    #endregion

//    #region Properties
//    public MeasureUnitCode MeasureUnitCode { get; init; }
//    #endregion

//    #region Public methods
//    public Enum GetDefaultMeasureUnit()
//    {
//        return MeasureUnitCode.GetDefaultMeasureUnit();
//    }

//    public IEnumerable<string> GetDefaultMeasureUnitNames()
//    {
//        return GetDefaultNames(MeasureUnitCode);
//    }

//    public Type GetMeasureUnitType()
//    {
//        return MeasureUnitTypes.GetMeasureUnitType(MeasureUnitCode);
//    }

//    public virtual TypeCode GetQuantityTypeCode()
//    {
//        return MeasureUnitCode.GetQuantityTypeCode();
//    }

//    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
//    {
//        return measureUnitCode == MeasureUnitCode;
//    }

//    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
//    {
//        return GetMeasureUnitCodes().Contains(measureUnitCode);
//    }

//    #region Override methods
//    public override bool Equals(object? obj)
//    {
//        return obj is IMeasurable other
//            && MeasureUnitCode.Equals(other?.MeasureUnitCode);
//    }

//    public override IMeasurableFactory GetFactory()
//    {
//        return (IMeasurableFactory)Factory;
//    }

//    public override int GetHashCode()
//    {
//        return MeasureUnitCode.GetHashCode();
//    }
//    #endregion

//    #region Virtual methods
//    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    {
//        return MeasureUnitTypes.GetMeasureUnitCodes();
//    }

//    public virtual void ValidateMeasureUnit(Enum measureUnit, string paramName)
//    {
//        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, paramName);
//    }

//    public virtual void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
//    {
//        MeasureUnitTypes.ValidateMeasureUnitCode(measureUnitCode, paramName);
//    }
//    #endregion

//    #region Abstract methods
//    public abstract Enum GetMeasureUnit();
//    #endregion
//    #endregion
//}
