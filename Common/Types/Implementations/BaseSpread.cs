﻿using CsabaDu.FooVaria.Common.Statics;
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

    protected BaseSpread(IBaseSpreadFactory factory, Enum measureUnit, params IBaseMeasurable[] baseMeasurables) : base(factory, measureUnit)
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

    public bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        if (baseSpread == null && limitMode == null) return true;

        if (baseSpread == null) return null;

        int? comparison = Compare(this, baseSpread);

        if (comparison == null) return null;

        limitMode ??= LimitMode.BeNotGreater;

        return comparison.Value.FitsIn(limitMode);
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
        ValidateSpreadMeasure(other, nameof(other));

        return DefaultQuantity / other.DefaultQuantity;
    }

    public bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseSpread? exchanged)
    {
        exchanged = ExchangeTo(measureUnit);

        return exchanged != null;
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        if (NullChecked(spreadMeasure, paramName).GetSpreadMeasure() is not IBaseMeasurable baseMeasurable
            || baseMeasurable.MeasureUnitTypeCode != MeasureUnitTypeCode)
        {
            throw ArgumentTypeOutOfRangeException(paramName, spreadMeasure!);
        }

        decimal quantity = spreadMeasure!.DefaultQuantity;

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
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
    public abstract IBaseSpread? ExchangeTo(Enum measureUnit);
    public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    public abstract void ValidateQuantity(ValueType? quantity, string paramName);
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
