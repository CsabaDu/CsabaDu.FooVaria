namespace CsabaDu.FooVaria.Common.Types;

public interface ICommonBase
{
    IFactory Factory { get; init; }

    IFactory GetFactory();

    void Validate(ICommonBase? other);
    void Validate(IFactory? factory);
}

