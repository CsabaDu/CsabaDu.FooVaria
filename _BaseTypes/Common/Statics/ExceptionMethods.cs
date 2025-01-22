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
        if (param is string str)
        {
            ArgumentException.ThrowIfNullOrEmpty(str, paramName);
        }
        else if (param is IEnumerable enumerable)
        {
            return (T)NullChecked(enumerable, paramName, false);
        }

        ArgumentNullException.ThrowIfNull(param, paramName);

        return param!;
    }

    public static TEnumerable NullChecked<TEnumerable>(TEnumerable? enumerable, string? paramName, bool checkElements)
        where TEnumerable : IEnumerable
    {
        ArgumentNullException.ThrowIfNull(enumerable, paramName);

        if (enumerable.GetEnumerator() is not IEnumerator enumerator)
        {
            throw enumerableArgumentException("'s GetEnumerator() method returns null");
        }

        if (!enumeratorMoveNext())
        {
            throw enumerableArgumentException(" does not contain any element");
        }

        if (!checkElements) return enumerable;

        if (enumeratorCurrentIsNull())
        {
            throw enumerableNullValueElementsArgumentException();
        }

        while (enumeratorMoveNext())
        {
            if (enumeratorCurrentIsNull())
            {   
                throw enumerableNullValueElementsArgumentException();
            }
        }

        return enumerable;

        #region Local methods
        bool enumeratorMoveNext() => enumerator.MoveNext();

        bool enumeratorCurrentIsNull() => enumerator.Current is null;

        ArgumentException enumerableArgumentException(string messageEnd)
        => new($"The {paramName} enumerable{messageEnd}.", paramName);

        ArgumentException enumerableNullValueElementsArgumentException()
        => enumerableArgumentException(" contains null value elements");
        #endregion
    }
    #endregion

    #region TypeCheckers
    public static T TypeChecked<T>(T? param, string paramName, Type validType)
    {
        ArgumentNullException.ThrowIfNull(param, paramName);

        Type paramType = typeof(T);

        if (paramType == NullChecked(validType, nameof(validType))) return param;

        throw ArgumentTypeOutOfRangeException(paramName, paramType, validType);
    }

    public static T TypeChecked<T>(object? param, string paramName)
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
        string argTypeName = arg.GetType().Name;

        return new ArgumentOutOfRangeException(paramName, argTypeName, $"The {paramName} argument's type is other than the valid {validType.Name} type.");
    }

    public static InvalidEnumArgumentException InvalidTypeEnumArgumentException(string? paramName, Enum param, Type enumType)
    {
        return new InvalidEnumArgumentException(paramName, (int)(object)param, enumType);
    }
}
