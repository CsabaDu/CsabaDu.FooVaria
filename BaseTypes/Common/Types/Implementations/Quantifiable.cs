﻿//namespace CsabaDu.FooVaria.Common.Types.Implementations;

//public abstract class Quantifiable : Measurable, IQuantifiable
//{
//    #region Constructors
//    protected Quantifiable(IQuantifiable other) : base(other)
//    {
//    }

//    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
//    {
//    }

//    protected Quantifiable(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
//    {
//    }

//    protected Quantifiable(IQuantifiableFactory factory, IBaseMeasurement baseMeasurement) : base(factory, baseMeasurement)
//    {
//    }

//    protected Quantifiable(IQuantifiableFactory factory, IQuantifiable quantifiable) : base(factory, quantifiable)
//    {
//    }

//    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitCode, quantifiables)
//    {
//    }
//    #endregion

//    #region Public methods
//    #region Override methods
//    public override bool Equals(object? obj)
//    {
//        return obj is IQuantifiable other
//            && base.Equals(other)
//            && GetDefaultQuantity() == other.GetDefaultQuantity();
//    }

//    public override int GetHashCode()
//    {
//        return HashCode.Combine(MeasureUnitCode, GetDefaultQuantity());
//    }

//    public override void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
//    {
//        if (GetMeasureUnitCodes().Contains(measureUnitCode)) return;
//    }

//    #endregion

//    #region Abstract methods
//    public abstract decimal GetDefaultQuantity();
//    public abstract void ValidateQuantity(ValueType? quantity, string paramName);
//    #endregion
//    #endregion

//    #region Protected methods
//    #region Static methods
//    protected static TypeCode? GetQuantityTypeCode(ValueType? quantity)
//    {
//        Type? quantityType = quantity?.GetType();

//        return GetQuantityTypes().Any(x => x == quantityType) ?
//            Type.GetTypeCode(quantityType)
//            : null;
//    }

//    protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> quantifiable)
//        where TNum : struct 
//    {
//        return Type.GetTypeCode(typeof(TNum));

//    }

//    protected static bool IsValidShapeComponentOf<T>(IShape baseShape, T shapeComponent) where T : class, IQuantifiable, IShapeComponent
//    {
//        return baseShape?.GetShapeComponents() is IEnumerable<T>;
//    }

//    #endregion
//    #endregion
//}

