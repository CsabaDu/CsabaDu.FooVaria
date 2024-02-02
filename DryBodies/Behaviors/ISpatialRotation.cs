namespace CsabaDu.FooVaria.DryBodies.Behaviors
{
    public interface ISpatialRotation
    {
    }

    public interface ISpatialRotation<TSelf> : ISpatialRotation
        where TSelf : class, IDryBody, IRectangularShape
    {
        TSelf RotateSpatially();
        TSelf RotateTo(IDryBody other);
    }
}
