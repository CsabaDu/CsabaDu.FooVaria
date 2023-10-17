namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseSpread : BaseMeasurable, IBaseSpread
{
    #region Constructors
    protected BaseSpread(IBaseSpread other) : base(other)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
    }
    #endregion

    //#region Properties
    //public IEnumerable<MeasureUnitTypeCode> SpreadMeasureUnitTypeCodes
    //{
    //    get
    //    {
    //        yield return MeasureUnitTypeCode.AreaUnit;
    //        yield return MeasureUnitTypeCode.VolumeUnit;
    //    }
    //}
    //#endregion

    #region Public methods
    #region Override methods
    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
    }

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        //return SpreadMeasureUnitTypeCodes;
        yield return MeasureUnitTypeCode.AreaUnit;
        yield return MeasureUnitTypeCode.VolumeUnit;

    }
    #endregion

    #region Abstract methods
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion
}


