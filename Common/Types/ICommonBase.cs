namespace CsabaDu.FooVaria.Common.Types;

public interface ICommonBase : IFooVariaObject
{
    IFactory Factory { get; init; }

    IFactory GetFactory();

    void Validate(IFooVariaObject? fooVariaObject);

    //void Validate(ICommonBase? other);
    //void Validate(IFactory? factory);
}

