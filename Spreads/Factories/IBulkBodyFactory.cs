namespace CsabaDu.FooVaria.Spreads.Factories;

public interface IBulkBodyFactory : ISpreadFactory<IBulkBody, IVolume>
{
    IBulkBody Create(IBody body);
}
