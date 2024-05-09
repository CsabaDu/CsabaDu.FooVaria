namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseRatesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IBaseRate baseRate;
    #endregion

    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null => false";
        isTrue = false;
        baseRate = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        testCase = "different DefaultQuantity => false";
        baseRate = GetBaseRateChild(this);
        defaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity);
        yield return toObjectArray();

        testCase = "Different DenominatorCode => false";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return toObjectArray();

        testCase = "Different type measureUnit => false";
        baseRate = GetBaseRateChild(this);
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same baseRate => true";
        isTrue = true;
        baseRate = GetBaseRateChild(this);
        yield return toObjectArray();

        testCase = "Same measureUnit type, different measureUnit => true";
        measureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(), measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_bool_MeasureUnitCode_IBaseRate args = new(testCase, measureUnit, defaultQuantity, isTrue, denominatorCode, baseRate);

            return args.ToObjectArray();
        }
        #endregion
    }

    //internal IEnumerable<object[]> GetFitsInArgs()
    //{
    //    testCase = "Not IBaseQuantifiable";
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    limiter = new LimiterObject();
    //    yield return toObjectArray();

    //    testCase = "Different MeasureUnitCode";
    //    measureUnitCode = GetMeasureUnitCode();
    //    limiter = GetLimiterBaseQuantifiableObject(null, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        TestCase_Enum_ILimiter args = new(testCase, measureUnit, limiter);

    //        return args.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetInvalidQuantityTypeCodeArg()
    //{
    //    TypeCode[] typeCodes = SampleParams.InvalidValueTypeCodes;

    //    return GetQuantityTypeCodeArg(typeCodes);
    //}

    //internal IEnumerable<object[]> GetValidQuantityTypeCodeArg()
    //{
    //    TypeCode ulongTypeCode = TypeCode.UInt64;
    //    IEnumerable<TypeCode> typeCodes = QuantityTypeCodes
    //        .Where(x => x != ulongTypeCode)
    //        .Append(ulongTypeCode);

    //    return GetQuantityTypeCodeArg(typeCodes);
    //}

    //private static IEnumerable<object[]> GetQuantityTypeCodeArg(IEnumerable<TypeCode> typeCodes)
    //{
    //    return typeCodes.Select(toObjectArray);

    //    #region Local methods
    //    static object[] toObjectArray(TypeCode typeCode)
    //    {
    //        string testCase = GetEnumName(typeCode);
    //        TestCase_TypeCode item = new(testCase, typeCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}
    #endregion
}
