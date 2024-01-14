namespace CsabaDu.FooVaria.Quantifiables.Types;

public interface IQuantifiable : IMeasurable, IDefaultQuantity, IEqualityComparer<IQuantifiable>
{
    void ValidateQuantity(ValueType? quantity, string paramName);
}
