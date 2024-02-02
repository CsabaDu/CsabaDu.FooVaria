namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface ILimitable
    {
        bool? FitsIn(ILimiter? limiter);
    }

    public interface ILimitable<out TSelf> : ILimitable
    where TSelf : class, IQuantifiable, ILimitable
    {
        TSelf ConvertToLimitable(ILimiter limiter);
    }
}

