namespace CsabaDu.FooVaria.BaseTypes.Common.Behaviors
{
    public interface IGetNew<TSelf>/* : ICommonBase*/
        where TSelf : class, ICommonBase
    {
        TSelf GetNew(TSelf other);
    }
}
