namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests;

internal class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        obj = GetBaseQuantifiableChild(RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        obj = GetBaseQuantifiableChild(measureUnit, RandomParams.GetRandomDecimal(defaultQuantity));
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        obj = GetBaseQuantifiableChild(measureUnit, defaultQuantity);
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_Decimal_args item = new(isTrue, obj, measureUnit, defaultQuantity);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInArgs()
    {
        // Not IBaseQuantifiable
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        limiter = GetLimiterBaseQuantifiableObject(null, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ILimiter_args item = new(measureUnit, limiter);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetInvalidQuantityTypeCodeArg()
    {
        TypeCode[] typeCodes = SampleParams.InvalidValueTypeCodes;

        return GetQuantityTypeCodeArg(typeCodes);
    }

    internal IEnumerable<object[]> GetValidQuantityTypeCodeArg()
    {
        TypeCode ulongTypeCode = TypeCode.UInt64;
        IEnumerable<TypeCode> typeCodes = QuantityTypeCodes
            .Where(x => x != ulongTypeCode)
            .Append(ulongTypeCode);

        return GetQuantityTypeCodeArg(typeCodes);
    }

    private static IEnumerable<object[]> GetQuantityTypeCodeArg(IEnumerable<TypeCode> typeCodes)
    {
        return typeCodes.Select(toObjectArray);

        #region Local methods
        static object[] toObjectArray(TypeCode typeCode)
        {
            TypeCode_arg item = new(typeCode);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
