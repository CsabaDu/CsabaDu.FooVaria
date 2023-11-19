namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IDenominate
    {
    }

    public interface IDenominate<in U, out T> : IDataErrorInfo, IMultiply<U, T> where U : notnull where T : class, IBaseMeasure
    {

    }
}
