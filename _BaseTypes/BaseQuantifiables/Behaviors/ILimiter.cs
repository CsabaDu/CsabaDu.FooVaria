namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors
{
    public interface ILimiter : ILimitMode;

    public interface ILimiter<TSelf, in TLimitable> : ILimiter, IEqualityComparer<TSelf>
        where TSelf : class, IBaseQuantifiable, ILimiter
        where TLimitable : class, IBaseQuantifiable, ILimitable
    {
        bool? Includes(TLimitable? limitable);
    }
}
