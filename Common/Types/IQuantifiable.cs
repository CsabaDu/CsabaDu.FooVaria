namespace CsabaDu.FooVaria.Common.Types;

public interface IQuantifiable : IMeasurable
{
    decimal GetDefaultQuantity();

    void ValidateQuantity(ValueType? quantity, string paramName); // TypeCode
}
