using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IShapeFactory : IBaseShapeFactory
    {
        ISpreadFactory SpreadFactory { get; init; }
        ITangentShapeFactory TangentShapeFactory { get; init; }

        IMeasureFactory GetMeasureFactory();
        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
        IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity);
    }

    public interface IShapeFactory<T, out TTangent> : IShapeFactory, IFactory<T>
        where T : class, IShape, ITangentShape
        where TTangent : class, IShape, ITangentShape
    {
        TTangent CreateTangentShape(T shape, SideCode sideCode);
        TTangent CreateOuterTangentShape(T shape);
        TTangent CreateInnerTangentShape(T shape);
    }

}
