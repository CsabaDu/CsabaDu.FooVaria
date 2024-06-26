﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null => false";
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return argsToObjectArray();

        testCase = "object => false";
        obj = new();
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        obj = GetBaseQuantifiableChild(RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return argsToObjectArray();

        testCase = "Same measureUnit, different defaultQuantity => false";
        obj = GetBaseQuantifiableChild(measureUnit, RandomParams.GetRandomDecimal(defaultQuantity));
        yield return argsToObjectArray();

        testCase = "Same measureUnit, same defaultQuantity => true";
        obj = GetBaseQuantifiableChild(this);
        isTrue = true;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_bool_object_Enum_decimal args = new(testCase, isTrue, obj, measureUnit, defaultQuantity);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInArgs()
    {
        testCase = "Not IBaseQuantifiable";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return argsToObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = GetMeasureUnitCode();
        limiter = GetLimiterBaseQuantifiableObject(null, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_ILimiter args = new(testCase, measureUnit, limiter);

            return args.ToObjectArray();
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

    #region Private methods
    private static IEnumerable<object[]> GetQuantityTypeCodeArg(IEnumerable<TypeCode> typeCodes)
    {
        return typeCodes.Select(argsToObjectArray);

        #region Local methods
        static object[] argsToObjectArray(TypeCode typeCode)
        {
            string testCase = GetEnumName(typeCode);
            TestCase_TypeCode item = new(testCase, typeCode);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
    #endregion
}
