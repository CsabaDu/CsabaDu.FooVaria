namespace CsabaDu.FooVaria.Spreads.Factories;

public interface ISpreadFactory : IFactory<ISpread>
{
    ISpread Create(ISpreadMeasure spreadMeasure);
    ISpread Create(params IExtent[] shapeExtents);
}
