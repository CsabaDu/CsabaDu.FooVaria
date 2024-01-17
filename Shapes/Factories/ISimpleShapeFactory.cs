namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface ISimpleShapeFactory : IShapeFactory
    {
        ISpreadFactory SpreadFactory { get; init; }
        ITangentShapeFactory TangentShapeFactory { get; init; }

        IMeasureFactory GetMeasureFactory();
        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
        IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity);
    }

    public interface ISimpleShapeFactory<T, out TTangent> : ISimpleShapeFactory, IFactory<T>
        where T : class, IShape, ITangentShape
        where TTangent : class, IShape, ITangentShape
    {
        TTangent CreateTangentShape(T shape, SideCode sideCode);
        TTangent CreateOuterTangentShape(T shape);
        TTangent CreateInnerTangentShape(T shape);
    }

}
