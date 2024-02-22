namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Factories
{
    public interface ISimpleShapeFactory : IShapeFactory
    {
        IMeasureFactory GetMeasureFactory();
        IBulkSpreadFactory GetBulkSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
        IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity);
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
