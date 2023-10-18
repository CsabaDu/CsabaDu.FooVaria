using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseSpread : BaseMeasurable, IBaseSpread, IQuantifiable
{
    #region Constructors
    protected BaseSpread(IBaseSpread other) : base(other)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseSpread baseSpread) : base(factory, baseSpread)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
    }

    #endregion

    #region Public methods
    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseSpread? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    #region Override methods
    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
    }

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        yield return MeasureUnitTypeCode.AreaUnit;
        yield return MeasureUnitTypeCode.VolumeUnit;

    }
    #endregion

    #region Abstract methods
    public abstract IBaseSpread? ExchangeTo(Enum context);
    public abstract decimal GetDefaultQuantity();
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion
}
