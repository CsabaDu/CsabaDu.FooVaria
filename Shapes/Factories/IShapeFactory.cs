using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IShapeFactory : IBaseShapeFactory
    {
        ISpreadFactory SpreadFactory { get; init; }
    }
}
