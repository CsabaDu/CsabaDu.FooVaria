using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface IBody : ISpread, IExchange<IBody, VolumeUnit>
{
    IBody GetBody();
}
