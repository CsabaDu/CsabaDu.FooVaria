namespace CsabaDu.FooVaria.Tests.TestHelpers.Params
{
    public sealed class TestSupport
    {
        public static bool Returned(Action validator)
        {
            try
            {
                validator();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
