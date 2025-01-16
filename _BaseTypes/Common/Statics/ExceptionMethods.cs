namespace CsabaDu.FooVaria.BaseTypes.Common.Statics;

public static class ExceptionMethods
{
    #region InvalidEnumArgumentException
    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode)
    {
        return InvalidSideCodeEnumArgumentException(sideCode, nameof(sideCode));
    }

    public static InvalidEnumArgumentException InvalidSideCodeEnumArgumentException(SideCode sideCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode)
    {
        return InvalidComparisonCodeEnumArgumentException(comparisonCode, nameof(comparisonCode));
    }

    public static InvalidEnumArgumentException InvalidComparisonCodeEnumArgumentException(ComparisonCode comparisonCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)comparisonCode, comparisonCode.GetType());
    }

    public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode)
    {
        return InvalidSidenCodeEnumArgumentException(sideCode, nameof(sideCode));
    }

    public static InvalidEnumArgumentException InvalidSidenCodeEnumArgumentException(SideCode sideCode, string paramName)
    {
        return new InvalidEnumArgumentException(paramName, (int)sideCode, sideCode.GetType());
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static ArgumentOutOfRangeException ArgumentTypeOutOfRangeException(string paramName, object arg)
    {
        return new ArgumentOutOfRangeException(paramName, arg.GetType().Name, null);
    }

    public static ArgumentOutOfRangeException DecimalArgumentOutOfRangeException(decimal exchangeRate)
    {
        return DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);
    }

    public static ArgumentOutOfRangeException DecimalArgumentOutOfRangeException(string? paramName, decimal exchangeRate)
    {
        return new ArgumentOutOfRangeException(paramName, exchangeRate, null);
    }

    public static ArgumentOutOfRangeException NameArgumentOutOfRangeException(string name)
    {
        return new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public static ArgumentOutOfRangeException NameArgumentOutOfRangeException(string? paramName, string name)
    {
        return new ArgumentOutOfRangeException(paramName, name ?? string.Empty, null);
    }

    public static ArgumentOutOfRangeException CountArgumentOutOfRangeException(int count)
    {
        return CountArgumentOutOfRangeException(count, nameof(count));
    }

    public static ArgumentOutOfRangeException CountArgumentOutOfRangeException(int count, string paramName)
    {
        return new ArgumentOutOfRangeException(paramName, count, null);
    }

    #endregion

    #region Generic checkers
    #region ArgumentNullException
    public static T NullChecked<T>(T? param, string? paramName)
    {
        ArgumentNullException.ThrowIfNull(param, paramName);

        if (param is not IEnumerable enumerable) return param;

        if (enumerable.GetEnumerator() is not IEnumerator enumerator)
        {
            throw new InvalidOperationException($"The {paramName} enumerable's GetEnumerator() returned null.");
        }
            
        if (!enumerator.MoveNext())
        {
            throw new InvalidOperationException($"The {paramName} enumerable does not contain any elements.");
        }

        return param;
    }
    #endregion

    #region ArgumentOutOfRangeException
    public static T TypeChecked<T>(T? param, [DisallowNull] string paramName, [DisallowNull] Type type)
    {
        Type paramType = typeof(T);

        if (paramType == type) return NullChecked(param, paramName);

        throw ArgumentTypeOutOfRangeException(paramName, paramType);
    }

    public static T TypeChecked<T>(object? param, [DisallowNull] string paramName)
    {
        if (NullChecked(param, paramName) is T typeChecked) return typeChecked;

        throw ArgumentTypeOutOfRangeException(paramName, param!.GetType());
    }
    #endregion

    #region InvalidEnumArgumentException
    public static T Defined<T>(T param, string? paramName, Type enumType)
        where T : Enum
    {
        if (Enum.IsDefined(enumType, NullChecked(param, paramName))) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }

    public static T Defined<T>(T param, string? paramName)
        where T : struct, Enum
    {
        if (param.IsDefined()) return param;

        throw new InvalidEnumArgumentException(paramName, (int)(object)param, typeof(T));
    }
    #endregion
    #endregion
}
