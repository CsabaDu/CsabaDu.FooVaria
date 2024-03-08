namespace CsabaDu.FooVaria.BulkSpreads.Factories;

public interface IBulkBodyFactory : IBodyFactory, IBulkSpreadFactory<IBulkBody, IVolume, VolumeUnit>;
