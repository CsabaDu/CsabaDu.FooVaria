//namespace CsabaDu.FooVaria.RateComponents.Types
//{
//    public interface IMeasure : IRateComponent, ILimitable, ICalculate<IMeasure, decimal>
//    {
//        IMeasure GetMeasure(IRateComponent rateComponent);
//    }

//    public interface IMeasure<TSelf, TNum> : IMeasure, IRateComponent<TSelf, TNum>
//        where TSelf : class, IMeasure, IDefaultBaseMeasure
//        where TNum : struct
//    {
//        TSelf GetMeasure(string name, TNum quantity);
//        TSelf GetMeasure(IMeasurement measurement, TNum quantity);
//    }

//    public interface IMeasure<TSelf, TNum, TEnum> : IMeasure<TSelf, TNum>, IMeasureUnit<TEnum>
//        where TSelf : class, IMeasure, IDefaultBaseMeasure, IMeasureUnit
//        where TNum : struct
//        where TEnum : struct, Enum
//    {
//        TSelf GetMeasure(TEnum measureUnit, TNum quantity);
//        TSelf GetMeasure(TEnum measureUnit);
//    }
//}
