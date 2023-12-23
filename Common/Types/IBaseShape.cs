namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponents
    {
        IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);

        void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
    }

    //public interface IBaseShape<out TSelf, TSComponent> : IShapeComponents<TSComponent>
    //    where TSelf: class, IBaseShape, 
    //    where TSComponent : class, IQuantifiable, IShapeComponent
    //{

    //}

    public interface IComplexShape : IBaseShape
    {

    }
}


