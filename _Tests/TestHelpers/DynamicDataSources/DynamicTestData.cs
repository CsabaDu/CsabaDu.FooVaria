namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public record DynamicTestData<TSource>(TSource Source) where TSource : DynamicDataSource;
