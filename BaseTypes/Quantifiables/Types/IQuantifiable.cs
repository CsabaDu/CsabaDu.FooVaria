namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

public interface IQuantifiable : IMeasurable, IDefaultQuantity
{
    void ValidateQuantity(ValueType? quantity, string paramName);
}
