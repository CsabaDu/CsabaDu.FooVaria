namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public abstract record DynamicData;

public record DynamicData<TSource>(TSource Source, ArgsCode ArgsCode) : DynamicData
    where TSource : DynamicDataSource, new();
