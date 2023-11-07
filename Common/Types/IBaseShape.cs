namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseShape : IBaseSpread, IShapeExtentCount, IFit<IBaseShape>
{
    void ValidateShapeExtent(IQuantifiable shapeExtent, string paramName);
}

