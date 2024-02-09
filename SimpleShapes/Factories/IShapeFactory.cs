namespace CsabaDu.FooVaria.SimpleShapes.Factories
{
    public interface ISimpleShapeFactory : IShapeFactory
    {
        ISpreadFactory SpreadFactory { get; init; }
        ITangentShapeFactory TangentShapeFactory { get; init; }

        IMeasureFactory GetMeasureFactory();
        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
        IExtent CreateSimpleShapeExtent(ExtentUnit extentUnit, ValueType quantity);
    }

    public interface ISimpleShapeFactory<T, out TTangent> : ISimpleShapeFactory, IFactory<T>
        where T : class, ISimpleShape, ITangentShape
        where TTangent : class, ISimpleShape, ITangentShape
    {
        TTangent CreateTangentShape(T simpleShape, SideCode sideCode);
        TTangent CreateOuterTangentShape(T simpleShape);
        TTangent CreateInnerTangentShape(T simpleShape);
    }

}
