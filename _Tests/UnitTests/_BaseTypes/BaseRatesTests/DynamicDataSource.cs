using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseRatesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IBaseRate baseRate;
    IQuantifiable quantifiable;
    #endregion

    #region Methods
    internal IEnumerable<object[]> GetEqualsObjectArgs()
    {
        testCase = "null => false";
        isTrue = false;
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Different IBaseRate => false";
        obj = GetBaseRateChild(this);
        defaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity);
        yield return argsToObjectArray();

        testCase = "Equal IBaseRate => true";
        isTrue = true;
        obj = GetBaseRateChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(), measureUnit);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_bool_object_Enum_decimal_MeasureUnitCode args = new(testCase, isTrue, obj, measureUnit, defaultQuantity, denominatorCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsIBaseRateArgs()
    {
        testCase = "null => false";
        isTrue = false;
        baseRate = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Different DefaultQuantity => false";
        baseRate = GetBaseRateChild(this);
        defaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity);
        yield return argsToObjectArray();

        testCase = "Different DenominatorCode => false";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return argsToObjectArray();

        testCase = "Different type measureUnit => false";
        baseRate = GetBaseRateChild(this);
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Same baseRate => true";
        isTrue = true;
        baseRate = GetBaseRateChild(this);
        yield return argsToObjectArray();

        testCase = "Same measureUnit type, different measureUnit => true";
        measureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(), measureUnit);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_decimal_bool_MeasureUnitCode_IBaseRate args = new(testCase, measureUnit, defaultQuantity, isTrue, denominatorCode, baseRate);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIBaseRateLimitModeArgs()
    {
        testCase = "IBaseRate, Not defined LimitMode";
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        baseRate = GetBaseRateChild(this);
        limitMode = SampleParams.NotDefinedLimitMode;
        yield return argsToObjectArray();

        testCase = "Different NumeratotCode, valid LimitMode";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return argsToObjectArray();

        testCase = "Different DenominatorCode, valid LimitMode";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return argsToObjectArray();

        testCase = "null, valid LimitMode";
        baseRate = null;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_LimitMode_IBaseRate_MeasureUnitCode args = new(testCase, measureUnit, limitMode, baseRate, denominatorCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        testCase = "Not IBaseRatee";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Different type MeasureUnit";
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterBaseRateObject(this);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Different DenominatorCode";
        limiter = GetLimiterBaseRateObject(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_ILimiter_MeasureUnitCode args = new(testCase, measureUnit, limiter, denominatorCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseRateEnumNullArgs()
    {
        testCase = "null, null => numerator";
        quantifiable = null;
        measureUnit = null;
        paramName = ParamNames.numerator;
        yield return argsToObjectArray();

        testCase = "null, Enum => numerator";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return argsToObjectArray();

        testCase = "IQuantifiable, null => denominator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        measureUnit = null;
        paramName = ParamNames.measureUnit;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_Enum_string args = new(testCase, quantifiable, measureUnit, paramName);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseRateEnumInvalidArgs()
    {
        testCase = "Not MeasureUnit Enum";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        measureUnit = SampleParams.DefaultLimitMode;
        yield return argsToObjectArray();

        testCase = "Not defined MeasureUnit Enum";
        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return argsToObjectArray();

        testCase = "Not defined MeasureUnitCode";
        measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_Enum args = new(testCase, quantifiable, measureUnit);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseRateEnumValidArgs()
    {
        testCase = "MeasureUnit Enum";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return argsToObjectArray();

        testCase = "MeasureUnitCode";
        measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_Enum args = new(testCase, quantifiable, measureUnit);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseRateIMeasurableNullArgs()
    {
        testCase = "null, null => numerator";
        quantifiable = null;
        IMeasurable denominator = null;
        paramName = ParamNames.numerator;
        yield return argsToObjectArray();

        testCase = "null, IMeasurable => numerator";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominator = GetMeasurableChild(this);
        yield return argsToObjectArray();

        testCase = "IQuantifiable, null => denominator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        denominator = null;
        paramName = ParamNames.denominator;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_string_IMeasurable args = new(testCase, quantifiable, paramName, denominator);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetBaseRateIQuantifiableNullArgs()
    {
        testCase = "null, null => numerator";
        quantifiable = null;
        IQuantifiable denominator = null;
        paramName = ParamNames.numerator;
        yield return argsToObjectArray();

        testCase = "null, IQuantifiable => numerator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominator = GetQuantifiableChild(this);
        yield return argsToObjectArray();

        testCase = "IQuantifiable, null => denominator";
        quantifiable = GetQuantifiableChild(this);
        denominator = null;
        paramName = ParamNames.denominator;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IQuantifiable_string_IQuantifiable args = new(testCase, quantifiable, paramName, denominator);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetMeasureUnitCodeInvalidArgs()
    {
        testCase = "Not defined RateComponentCode";
        rateComponentCode = SampleParams.NotDefinedRateComponentCode;
        yield return argsToObjectArray();

        testCase = "RateComponentCode.Limit";
        rateComponentCode = RateComponentCode.Limit;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_RateComponentCode args = new(testCase, rateComponentCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetMeasureUnitCodeValidArgs()
    {
        testCase = "RateComponentCode.Numerator => Numerator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Numerator;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        MeasureUnitCode expected = GetMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "RateComponentCode.Denominator => Denominator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Denominator;
        expected = measureUnitCode;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_RateComponentCode_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode, rateComponentCode, expected);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityInvalidArgs()
    {
        testCase = "Not defined TypeCode";
        TestCase_TypeCode args = new(testCase, SampleParams.NotDefinedTypeCode);

        return GetGetQuantityArgs(SampleParams.InvalidValueTypeCodes)
            .Append(args.ToObjectArray());
    }

    internal IEnumerable<object[]> GetGetQuantityValidArgs()
    {
        return GetGetQuantityArgs(BaseQuantifiable.QuantityTypeCodes);
    }

    internal IEnumerable<object[]> GetGetRateComponentArgs()
    {
        testCase = "Not defined RateComponentCode => null";
        rateComponentCode = SampleParams.NotDefinedRateComponentCode;
        obj = null;
        yield return argsToObjectArray();

        testCase = "RateComponentCode.Numerator => Numerator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Numerator;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        obj = GetMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "RateComponentCode.Denominator => Denominator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Denominator;
        obj = measureUnitCode;
        yield return argsToObjectArray();

        testCase = "RateComponentCode.Limit => null";
        rateComponentCode = RateComponentCode.Limit;
        obj = null;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_RateComponentCode_object args = new(testCase, measureUnit, measureUnitCode, rateComponentCode, obj);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseRateHasMeasureUnitCodeArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();

        testCase = "Not defined MeasureUnitCode => false";
        isTrue = false;
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return argsToObjectArray();

        testCase = "Not contained MeasureUnitCode => false";
        measureUnitCode = getDifferentRandomMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Numerator MeasureUnitCode => true";
        isTrue = true;
        measureUnitCode = GetMeasureUnitCode();
        yield return argsToObjectArray();

        testCase = "Denominator MeasureUnitCode => true";
        measureUnitCode = denominatorCode;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_bool_MeasureUnitCode args = new(testCase, measureUnit, denominatorCode, isTrue, measureUnitCode);

            return args.ToObjectArray();
        }
        #endregion

        #region Local methods
        MeasureUnitCode getDifferentRandomMeasureUnitCode()
        {
            MeasureUnitCode[] measureUnitCodes = [GetMeasureUnitCode(), denominatorCode];

            return RandomParams.GetDifferentRandomMeasureUnitCode(measureUnitCodes);
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseRateIsExchangeableToArgs()
    {
        testCase = "null => false";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        baseRate = null;
        isTrue = false;
        yield return argsToObjectArray();

        testCase = "Different NumeratorCode => false";
        defaultQuantity = RandomParams.GetRandomDecimal();
        baseRate = GetBaseRateChild(this);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Different DenominatorCode => false";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return argsToObjectArray();

        testCase = "Same Numerator measureUnit, same DenominatorCode => true";
        baseRate = GetBaseRateChild(this);
        isTrue = true;
        yield return argsToObjectArray();

        testCase = "Same type different Numerator measureUnit, same DenominatorCode => true";
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        baseRate = GetBaseRateChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(), measureUnit);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_bool_IBaseRate args = new(testCase, measureUnit, denominatorCode, isTrue, baseRate);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsValidRateComponentArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());

        testCase = "null, RateComponentCode => false";
        rateComponentCode = RandomParams.GetRandomRateComponentCode();
        obj = null;
        isTrue = false;
        yield return argsToObjectArray();

        testCase = "object, RateComponentCode => false";
        obj = new();
        yield return argsToObjectArray();

        testCase = "NumeratorCode, RateComponentCode.Numerator => true";
        obj = GetMeasureUnitCode();
        rateComponentCode = RateComponentCode.Numerator;
        isTrue = true;
        yield return argsToObjectArray();

        testCase = "NumeratorCode, RateComponentCode.Denominator => false";
        rateComponentCode = RateComponentCode.Denominator;
        isTrue = false;
        yield return argsToObjectArray();

        testCase = "NumeratorCode, RateComponentCode.Limit => false";
        rateComponentCode = RateComponentCode.Limit;
        yield return argsToObjectArray();

        testCase = "NumeratorCode, not defined RateComponentCode => false";
        rateComponentCode = SampleParams.NotDefinedRateComponentCode;
        yield return argsToObjectArray();

        testCase = "DenominatorCode, not defined RateComponentCode => false";
        obj = denominatorCode;
        yield return argsToObjectArray();

        testCase = "DenominatorCode, RateComponentCode.Limit => false";
        rateComponentCode = RateComponentCode.Limit;
        yield return argsToObjectArray();

        testCase = "DenominatorCode, RateComponentCode.Numerator => false";
        rateComponentCode = RateComponentCode.Numerator;
        yield return argsToObjectArray();

        testCase = "DenominatorCode, RateComponentCode.Denominator => true";
        rateComponentCode = RateComponentCode.Denominator;
        isTrue = true;
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_RateComponentCode_object_bool args = new(testCase, measureUnit, denominatorCode, rateComponentCode, obj, isTrue);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetProportionalToArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();

        testCase = "Different NumeratorCode";
        defaultQuantity = RandomParams.GetRandomDecimal();
        baseRate = GetBaseRateChild(this);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return argsToObjectArray();

        testCase = "Different DenominatorCode";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_IBaseRate args = new(testCase, measureUnit, denominatorCode, baseRate);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseRateValidateMeasureUnitValidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();

        testCase = "Numerator-type MeasureUnit";
        context = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode());
        yield return argsToObjectArray();

        testCase = "Denominator-type MeasureUnit";
        context = RandomParams.GetRandomMeasureUnit(denominatorCode);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_Enum args = new(testCase, measureUnit, denominatorCode, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    #region Private methods
    private IEnumerable<object[]> GetGetQuantityArgs(IEnumerable<TypeCode> typeCodes)
    {
        foreach (var item in typeCodes)
        {
            testCase = GetEnumName(item);
            TestCase_TypeCode args = new(testCase, item);
            yield return args.ToObjectArray();
        }
    }
    #endregion
    #endregion
}
