namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponent
{
    void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
}

