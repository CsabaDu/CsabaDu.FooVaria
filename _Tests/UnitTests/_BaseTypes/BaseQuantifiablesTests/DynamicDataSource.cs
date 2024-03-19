namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests;

internal class DynamicDataSource : DataFields
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
        obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
                GetDefaultQuantity = defaultQuantity,
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = defaultQuantity,
            }
        };
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
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        limiter = new LimiterBaseQuantifiableOblect(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(),
            }
        };

        #region toObjectArray method
        object[] toObjectArray()
        {
            ILimiter_arg item = new(limiter);

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
        IEnumerable<TypeCode> typeCodes = GetQuantityTypeCodes();

        return GetQuantityTypeCodeArg(typeCodes);
    }

    private static IEnumerable<object[]> GetQuantityTypeCodeArg(IEnumerable<TypeCode> typeCodes)
    {
        return typeCodes.Select(typeCodeToObjectArray);

        #region Local methods
        static object[] typeCodeToObjectArray(TypeCode typeCode)
        {
            TypeCode_arg item = new(typeCode);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
