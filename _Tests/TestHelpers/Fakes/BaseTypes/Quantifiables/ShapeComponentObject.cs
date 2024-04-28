namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

public sealed class ShapeComponentObject : MeasureUnitCodeObject, IShapeComponent
{ 
    public bool Equals(IShapeComponent x, IShapeComponent y)
    {
        return x?.GetMeasureUnitCode() == y?.GetMeasureUnitCode();
    }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        yield return this;
    }

    public decimal GetDefaultQuantity()
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return shapeComponent.GetMeasureUnitCode().GetHashCode();
    }
}
