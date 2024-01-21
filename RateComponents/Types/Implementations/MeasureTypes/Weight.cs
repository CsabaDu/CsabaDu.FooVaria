

//namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

//internal sealed class Weight : Measure<IWeight, double, WeightUnit>, IWeight
//{
//    #region Constructors
//    internal Weight(IMeasureFactory factory, WeightUnit weightUnit, ValueType quantity) : base(factory, weightUnit, quantity)
//    {
//    }
//    #endregion

//    #region Public methods
//    public IWeight ConvertFrom(IVolume volume)
//    {
//        return NullChecked(volume, nameof(volume)).ConvertMeasure();
//    }

//    public IVolume ConvertMeasure()
//    {
//        return ConvertMeasure<IVolume>(MeasureOperationMode.Multiply);
//    }
//    #endregion
//}
