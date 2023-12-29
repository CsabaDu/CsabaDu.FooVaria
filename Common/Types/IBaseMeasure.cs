namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseMeasure : /*IMeasurable, */IQuantifiable
    {
        decimal DefaultQuantity { get; init; }

        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }

    public interface IBaseMeasure<TSelf, in TContext> : IBaseMeasure, IExchange<TSelf, TContext>
        where TSelf : class, IBaseMeasure<TSelf, TContext>
        where TContext : notnull
    {
    }
}
