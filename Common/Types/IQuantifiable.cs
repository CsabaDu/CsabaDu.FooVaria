namespace CsabaDu.FooVaria.Common.Types;

public interface IQuantifiable : IMeasurable, IDefaultQuantity
{
    void ValidateQuantity(ValueType? quantity, string paramName);
}
