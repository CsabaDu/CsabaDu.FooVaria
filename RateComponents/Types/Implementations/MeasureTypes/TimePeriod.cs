//namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

//internal sealed class TimePeriod : Measure<ITimePeriod, double, TimePeriodUnit>, ITimePeriod
//{
//    #region Constructors
//    internal TimePeriod(IMeasureFactory factory, TimePeriodUnit timePeriodUnit, ValueType quantity) : base(factory, timePeriodUnit, quantity)
//    {
//    }
//    #endregion

//    #region Public methods
//    public ITimePeriod ConvertFrom(TimeSpan timeSpan)
//    {
//        double quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

//        return GetMeasure(default, quantity);
//    }

//    public TimeSpan ConvertMeasure()
//    {
//        long ticks = (long)DefaultQuantity.ToQuantity(TypeCode.Int64)! * TimeSpan.TicksPerMinute;

//        return new TimeSpan(ticks);
//    }
//    #endregion
//}
