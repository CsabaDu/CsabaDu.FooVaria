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

    #endregion
    public decimal DefaultQuantity => GetSpreadMeasure().DefaultQuantity;

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
    public IBaseSpread GetBaseSpread()
    {
        return this;
    }
    public IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure)
    {
        return GetFactory().Create(spreadMeasure);
    }

    public abstract IBaseSpread? ExchangeTo(Enum measureUnit);
    public abstract ISpreadMeasure GetSpreadMeasure();
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
    public bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is MeasureUnitTypeCode) return base.IsExchangeableTo(MeasureUnitTypeCode)   ;

        return MeasureUnitTypes.IsDefinedMeasureUnit(context)
            && MeasureUnitTypes.GetMeasureUnitTypeCode(context) == MeasureUnitTypeCode;
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

    public virtual bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        if (baseSpread == null && limitMode == null) return true;

        if (baseSpread?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

        if (limitMode == null) return CompareTo(baseSpread) <= 0;

        int? comparison = Compare(this, baseSpread);

        return comparison?.FitsIn(limitMode);
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }
    #endregion

    private static int? Compare(IBaseSpread baseSpread, IBaseSpread other)
    {
        if (other == null) return null;

        if (other.MeasureUnitTypeCode != baseSpread.MeasureUnitTypeCode) return null;

        return baseSpread.DefaultQuantity.CompareTo(other.DefaultQuantity);
    }
    #endregion
}
