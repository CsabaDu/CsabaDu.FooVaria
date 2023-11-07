namespace CsabaDu.FooVaria.Common.Types;

public interface ICommonBase : IRootObject
{
    IFactory Factory { get; init; }

    IFactory GetFactory();

    void Validate(IRootObject? rootObject, string paramName);
}

