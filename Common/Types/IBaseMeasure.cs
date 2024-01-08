namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseMeasure : /*IMeasurable, */IQuantifiable, IExchangeRate, IBaseMeasureQuantity, IQuantityTypeCode
    {
        decimal DefaultQuantity { get; init; }

        IBaseMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity);
        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }

    //public interface IBaseMeasure<TSelf, in TContext> : IBaseMeasure, IExchange<TSelf, TContext>
    //    where TSelf : class, IBaseMeasure<TSelf, TContext>
    //    where TContext : notnull
    //{
    //}
}
