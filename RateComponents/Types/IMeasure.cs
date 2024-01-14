//namespace CsabaDu.FooVaria.RateComponents.Types
//{
//    public interface IMeasure : IRateComponent, ILimitable, ICalculate<IMeasure, decimal>
//    {
//        IMeasure GetMeasure(IRateComponent rateComponent);
//    }

//    public interface IMeasure<T, TNum> : IMeasure, IRateComponent<T, TNum>
//        where T : class, IMeasure, IDefaultBaseMeasure
//        where TNum : struct
//    {
//        T GetMeasure(string name, TNum quantity);
//        T GetMeasure(IMeasurement measurement, TNum quantity);
//    }

//    public interface IMeasure<T, TNum, TEnum> : IMeasure<T, TNum>, IMeasureUnit<TEnum>
//        where T : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
//        where TNum : struct
//        where TEnum : struct, Enum
//    {
//        T GetMeasure(TEnum measureUnit, TNum quantity);
//        T GetMeasure(TEnum measureUnit);
//    }
//}
