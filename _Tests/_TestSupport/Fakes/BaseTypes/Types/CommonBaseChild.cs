
namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types
{
    internal sealed class CommonBaseChild(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName)
    {
        public override IFactory GetFactory()
        {
            return new FactoryObject();
        }
    }
}
