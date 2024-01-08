namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponents
    {
        IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents);
        IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);

        void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
    }

    public interface IComplexShape : IBaseShape
    {
    }
}


