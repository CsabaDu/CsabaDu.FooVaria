namespace CsabaDu.FooVaria.BaseTypes.Common.Types;

public interface ICommonBase : IRootObject
{
    IFactory Factory { get; init; }

    //ICreateNew GetFactory();
}
