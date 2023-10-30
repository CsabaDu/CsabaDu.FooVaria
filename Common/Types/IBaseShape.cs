namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseShape : IBaseSpread, IFit<IBaseShape>
{
    void ValidateShapeExtent(IQuantifiable shapeExtent, string name);
}

