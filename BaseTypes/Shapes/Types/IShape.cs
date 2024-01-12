using CsabaDu.FooVaria.Quantifiables.Behaviors;
using CsabaDu.FooVaria.Quantifiables.Types;
using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types;
{
    public interface IShape : IBaseSpread, IShapeComponentCount, IFit<IShape>, IShapeComponents
{
    IShape? GetBaseShape(params IShapeComponent[] shapeComponents);
    IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);

    void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
}

public interface IComplexShape : IShape
{
}
}


// Visszaállítani