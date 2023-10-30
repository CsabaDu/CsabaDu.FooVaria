using CsabaDu.FooVaria.Common.Statics;
using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseSpread : BaseMeasurable, IBaseSpread
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

    protected BaseSpread(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IBaseMeasurable[] baseMeasurables) : base(factory, measureUnitTypeCode)
    {
        _ = NullChecked(baseMeasurables, nameof(baseMeasurables));
    }

    #endregion

    #region Properties
    public decimal DefaultQuantity => GetSpreadMeasure().DefaultQuantity;
    #endregion

    #region Public methods
    public int CompareTo(IBaseSpread? other)
    {
        if (other == null) return 1;

        return Compare(this, other) ?? throw ArgumentTypeOutOfRangeException(nameof(other), other);
    }

    public bool Equals(IBaseSpread? other)
    {
        return other?.MeasureUnitTypeCode == MeasureUnitTypeCode
            && other.DefaultQuantity == DefaultQuantity;
    }

    public IBaseSpread GetBaseSpread()
    {
        return this;
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is MeasureUnitTypeCode) return base.IsExchangeableTo(MeasureUnitTypeCode)   ;

        return MeasureUnitTypes.IsDefinedMeasureUnit(context)
            && MeasureUnitTypes.GetMeasureUnitTypeCode(context) == MeasureUnitTypeCode;
    }

    public decimal ProportionalTo(IBaseSpread other)
    {
        string name = nameof(other);

        _ = NullChecked(other, name);

        try
        {
            ValidateSpreadMeasure(other);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException(name, ex.InnerException);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex.InnerException);
        }

        return DefaultQuantity / other.DefaultQuantity;
    }

    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseSpread? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure)
    {
        string name = nameof(spreadMeasure);

        if (NullChecked(spreadMeasure, name).GetSpreadMeasure() is not IBaseMeasurable baseMeasurable
            || baseMeasurable.MeasureUnitTypeCode != MeasureUnitTypeCode)
        {
            throw ArgumentTypeOutOfRangeException(name, spreadMeasure!);
        }

        decimal quantity = spreadMeasure!.DefaultQuantity;

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(name, quantity);
    }

    #region Override methods
    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
    }

    #region Sealed methods
    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        yield return MeasureUnitTypeCode.AreaUnit;
        yield return MeasureUnitTypeCode.VolumeUnit;

    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        if (baseSpread == null && limitMode == null) return true;

        if (baseSpread?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

        if (limitMode == null) return CompareTo(baseSpread) <= 0;

        int? comparison = Compare(this, baseSpread);

        return comparison?.FitsIn(limitMode);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseSpread? ExchangeTo(Enum measureUnit);
    public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    public abstract void ValidateQuantity(ValueType? quantity);
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(IBaseSpread baseSpread, IBaseSpread other)
    {
        if (other == null) return null;

        if (other.MeasureUnitTypeCode != baseSpread.MeasureUnitTypeCode) return null;

        return baseSpread.DefaultQuantity.CompareTo(other.DefaultQuantity);
    }
    #endregion
    #endregion
}
