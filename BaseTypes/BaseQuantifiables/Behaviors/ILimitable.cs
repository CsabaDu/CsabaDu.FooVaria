namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors
{
    public interface ILimitable
    {
        bool? FitsIn(ILimiter? limiter);
    }

    //public interface ILimitable<out TSelf> : ILimitable
    //where TSelf : class, IBaseQuantifiable, ILimitable
    //{
    //    TSelf ConvertToLimitable(ILimiter limiter);
    //}
}

