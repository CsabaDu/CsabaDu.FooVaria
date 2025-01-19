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
        return new ArgumentOutOfRangeException(paramName, arg.GetType().Name, $"The {paramName} argument's type is invalid in this context.");
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
    #region NullCheckers
    public static T NullChecked<T>(T? param, string? paramName)
    {
        ArgumentNullException.ThrowIfNull(param, paramName);

        if (param is string str)
        {
            ArgumentException.ThrowIfNullOrEmpty(str, paramName);
        }

        if (param is IEnumerable enumerable)
        {
            throwIfNullEnumeratorOrEmpty(enumerable);
        }

        return param!;

        void throwIfNullEnumeratorOrEmpty(IEnumerable enumerable) 
        {
            if (enumerable.GetEnumerator() is not IEnumerator enumerator)
            {
                throw new ArgumentException($"GetEnumerator() method of the {paramName} enumerable returns null.", paramName);
            }

            if (!enumerator.MoveNext())
            {
                throw new ArgumentException($"The {paramName} enumerable does not contain any element.", paramName);
            }
        }
    }
    #endregion

    #region TypeCheckers
    public static T TypeChecked<T>(T? param, [DisallowNull] string paramName, [DisallowNull] Type validType)
    {
        Type paramType = typeof(T);

        if (paramType == validType) return NullChecked(param, paramName);

        throw ArgumentTypeOutOfRangeException(paramName, paramType, validType);
    }

    public static T TypeChecked<T>(object? param, [DisallowNull] string paramName)
    {
        if (NullChecked(param, paramName) is T typeChecked) return typeChecked;

        throw ArgumentTypeOutOfRangeException(paramName, param!.GetType(), typeof(T));
    }
    #endregion

    #region EnumCheckers
    public static T Defined<T>(T param, string? paramName, Type validType)
        where T : Enum
    {
        if (Enum.IsDefined(validType, NullChecked(param, paramName))) return param;

        throw InvalidTypeEnumArgumentException(paramName, param, validType);
    }

    public static T Defined<T>(T param, string? paramName)
        where T : struct, Enum
    {
        if (param.IsDefined()) return param;

        throw InvalidTypeEnumArgumentException(paramName, param, typeof(T));
    }
    #endregion
    #endregion

    public static ArgumentOutOfRangeException ArgumentTypeOutOfRangeException(string paramName, object arg, Type validType)
    {
        return new ArgumentOutOfRangeException(paramName, arg.GetType().Name, $"The {paramName} argument's type is other than the valid {validType.Name} type.");
    }

    public static InvalidEnumArgumentException InvalidTypeEnumArgumentException(string? paramName, Enum param, Type enumType)
    {
        return new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }
}
