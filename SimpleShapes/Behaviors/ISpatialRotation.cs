namespace CsabaDu.FooVaria.SimpleShapes.Behaviors
{
    public interface ISpatialRotation
    {
        IDryBody RotateTo(IDryBody other);
    }

    public interface ISpatialRotation<TSelf> : ISpatialRotation
        where TSelf : class, IDryBody, IRectangularShape
    {
        TSelf RotateSpatially();
        //TSelf RotateSpatiallyWith(TSelf other);
    }
}
