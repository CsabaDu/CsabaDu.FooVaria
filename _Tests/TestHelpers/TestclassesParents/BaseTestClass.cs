using CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.TestDataRecords;
using CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

namespace CsabaDu.FooVaria.Tests.TestHelpers.TestclassesParents
{
    public abstract class BaseTestClass
    {
        protected static ArgsCode ArgsCode { get; set; }
        protected static DynamicDataSourcesBase DynamicDataSource { get; set; }
    }

    public abstract class BaseTestClass_xUnit<TDynamicDataSource> : BaseTestClass where TDynamicDataSource : DynamicDataSourcesBase, new()
    {
        static BaseTestClass_xUnit() => ArgsCode = ArgsCode.Instance;
    }
}
