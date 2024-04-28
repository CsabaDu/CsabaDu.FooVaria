
namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

public sealed class ShapeComponentQuantifiableObject(IRootObject rootObject, string paramName) : QuantifiableChild(rootObject, paramName), IShapeComponent
{
    public static ShapeComponentQuantifiableObject GetShapeComponentQuantifiableObject(Enum measureUnit, decimal defaultQuantity, IQuantifiableFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = GetReturn(measureUnit, defaultQuantity, factory),
        };
    }

    public static ShapeComponentQuantifiableObject GetShapeComponentQuantifiableObject(DataFields fields, IQuantifiableFactory factory = null)
    {
        return GetShapeComponentQuantifiableObject(fields.measureUnit, fields.defaultQuantity, factory);
    }

    public bool Equals(IShapeComponent x, IShapeComponent y)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        yield return this;
    }

    public int GetHashCode([DisallowNull] IShapeComponent obj)
    {
        throw new NotImplementedException();
    }
}
