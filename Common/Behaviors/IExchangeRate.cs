using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IExchangeRate
{
    decimal GetExchangeRate();

    void ValidateExchangeRate(decimal exchangeRate, string paramName);

    bool TryGetMeasureUnit(decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit);
}

