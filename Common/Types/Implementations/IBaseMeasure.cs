//namespace Common.Types.Implementations
//{
//    public interface IBaseMeasure : IQuantifiable/*, IProportional<IBaseMeasure>*/
//    {
//        decimal DefaultQuantity { get; init; }

//        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
//    }

//    public interface IBaseMeasure<TSelf, in TContext> : IBaseMeasure, IExchange<TSelf, TContext>
//        where TSelf : class, IBaseMeasure<TSelf, TContext>
//        where TContext : notnull
//    {
//        TSelf GetBaseMeasure(TContext context, decimal quantity);
//    }
//}
