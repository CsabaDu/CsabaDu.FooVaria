﻿namespace CsabaDu.FooVaria.Quantifiables.Types.Implementations;

//public abstract class BaseSpread : Quantifiable, IBaseSpread
//{
//    public decimal DefaultQuantity { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }
//    #region Constructors
//    protected BaseSpread(IBaseSpread other) : base(other)
//    {
//    }

//    protected BaseSpread(IBaseSpreadFactory factory, IBaseSpread baseSpread) : base(factory, baseSpread)
//    {
//    }

//    protected BaseSpread(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitCode, quantifiables)
//    {
//    }

//    protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
//    {
//    }

//    protected BaseSpread(IBaseSpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
//    {
//    }
//    #endregion

//    #region Public methods
//    public int CompareTo(IBaseSpread? other)
//    {
//        if (other == null) return 1;

//        return Compare(this, other) ?? throw ArgumentTypeOutOfRangeException(nameof(other), other);
//    }

//    public bool Equals(IBaseSpread? other)
//    {
//        return base.Equals(other);
//    }

//    public bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
//    {
//        if (baseSpread == null && limitMode == null) return true;

//        if (baseSpread == null) return null;

//        int? comparison = Compare(this, baseSpread);

//        if (comparison == null) return null;

//        limitMode ??= LimitMode.BeNotGreater;

//        return comparison.Value.FitsIn(limitMode);
//    }

//    public override sealed decimal GetDefaultQuantity()
//    {
//        return (GetSpreadMeasure() as IBaseMeasure ?? throw new InvalidOperationException(null)).GetDefaultQuantity();
//    }

//    public override sealed Enum GetMeasureUnit()
//    {
//        return (GetSpreadMeasure() as IMeasurable)!.GetMeasureUnit();
//    }

//    public MeasureUnitCode GetMeasureUnitCode()
//    {
//        return MeasureUnitCode;
//    }

//    public double GetQuantity()
//    {
//        return GetSpreadMeasure().GetQuantity();
//    }

//    public bool IsExchangeableTo(Enum? context)
//    {
//        if (context == null) return false;

//        if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode();

//        if (!IsDefinedMeasureUnit(context)) return false;

//        measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(context);

//        return hasMeasureUnitCode();

//        #region Local methods
//        bool hasMeasureUnitCode()
//        {
//            return HasMeasureUnitCode(measureUnitCode);
//        }
//        #endregion
//    }

//    public decimal ProportionalTo(IBaseSpread other)
//    {
//        ValidateSpreadMeasure(other, nameof(other));

//        return GetDefaultQuantity() / other.GetDefaultQuantity();
//    }

//    public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
//    {
//        throw new NotImplementedException();
//    }

//    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
//    {
//        IBaseMeasure baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure ?? throw new InvalidOperationException(null);

//        MeasureUnitCode measureUnitCode = baseMeasure.MeasureUnitCode;

//        if (measureUnitCode != MeasureUnitCode) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

//        decimal quantity = baseMeasure.GetDefaultQuantity();

//        if (quantity > 0) return;

//        throw QuantityArgumentOutOfRangeException(paramName, quantity);
//    }

//    #region Override methods
//    public override IBaseSpreadFactory GetFactory()
//    {
//        return (IBaseSpreadFactory)Factory;
//    }

//    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    {
//        yield return MeasureUnitCode.AreaUnit;
//        yield return MeasureUnitCode.VolumeUnit;
//    }

//    #region Sealed methods
//    public override sealed TypeCode GetQuantityTypeCode()
//    {
//        return GetQuantityTypeCode(this);
//    }
//    #endregion
//    #endregion

//    #region Abstract methods
//    public abstract IBaseSpread? ExchangeTo(Enum measureUnit);
//    public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
//    public abstract ISpreadMeasure GetSpreadMeasure();
//    #endregion
//    #endregion

//    #region Private methods
//    #region Static methods
//    private static int? Compare(IBaseSpread baseSpread, IBaseSpread? other)
//    {
//        if (other?.HasMeasureUnitCode(baseSpread.MeasureUnitCode) != true) return null;

//        return baseSpread.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
//    }

//    public object GetQuantity(RoundingMode roundingMode)
//    {
//        throw new NotImplementedException();
//    }

//    public object GetQuantity(TypeCode quantityTypeCode)
//    {
//        throw new NotImplementedException();
//    }

//    public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//    #endregion
//}
