namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseMeasure : IMeasurable, IQuantifiable/*, IProportional<IBaseMeasure>*/
    {
        decimal DefaultQuantity { get; }

        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }

    public interface IBaseMeasure<T, in U> : IBaseMeasure, IExchange<T, U> where T : class, IBaseMeasure<T, U> where U : notnull
    {

    } 
}
