using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IShapeFactory : IBaseShapeFactory, IFactory<IShape>
    {
        ISpreadFactory SpreadFactory { get; init; }
    }
}
