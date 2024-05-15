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
        yield return toObjectArray();

        testCase = "Different IBaseRate => false";
        obj = GetBaseRateChild(this);
        defaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity);
        yield return toObjectArray();

        testCase = "Equal IBaseRate => true";
        isTrue = true;
        obj = GetBaseRateChild(this);
        measureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(), measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "Different DefaultQuantity => false";
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

    internal IEnumerable<object[]> GetFitsInIBaseRateLimitModeArgs()
    {
        testCase = "IBaseRate, Not defined LimitMode";
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        denominatorCode = RandomParams.GetRandomMeasureUnitCode();
        baseRate = GetBaseRateChild(this);
        limitMode = SampleParams.NotDefinedLimitMode;
        yield return toObjectArray();

        testCase = "Different NumeratotCode, valid LimitMode";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        testCase = "Different DenominatorCode, valid LimitMode";
        baseRate = GetBaseRateChild(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return toObjectArray();

        testCase = "null, valid LimitMode";
        baseRate = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "Different type MeasureUnit";
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterBaseRateObject(this);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Different DenominatorCode";
        limiter = GetLimiterBaseRateObject(this);
        denominatorCode = RandomParams.GetRandomMeasureUnitCode(denominatorCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "null, Enum => numerator";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return toObjectArray();

        testCase = "IQuantifiable, null => denominator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        measureUnit = null;
        paramName = ParamNames.measureUnit; 
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "Not defined MeasureUnit Enum";
        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        testCase = "Not defined MeasureUnitCode";
        measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "MeasureUnitCode";
        measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "null, IMeasurable => numerator";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominator = GetMeasurableChild(this);
        yield return toObjectArray();

        testCase = "IQuantifiable, null => denominator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        quantifiable = GetQuantifiableChild(this);
        denominator = null;
        paramName = ParamNames.denominator;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "null, IQuantifiable => numerator";
        defaultQuantity = RandomParams.GetRandomDecimal();
        measureUnit = RandomParams.GetRandomMeasureUnit();
        denominator = GetQuantifiableChild(this);
        yield return toObjectArray();

        testCase = "IQuantifiable, null => denominator";
        quantifiable = GetQuantifiableChild(this);
        denominator = null;
        paramName = ParamNames.denominator;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "RateComponentCode.Limit";
        rateComponentCode = RateComponentCode.Limit;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "RateComponentCode.Denominator => Denominator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Denominator;
        expected = measureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
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
        yield return toObjectArray();

        testCase = "RateComponentCode.Numerator => Numerator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Numerator;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        obj = GetMeasureUnitCode();
        yield return toObjectArray();

        testCase = "RateComponentCode.Denominator => Denominator MeasureUnitCode";
        rateComponentCode = RateComponentCode.Denominator;
        obj = measureUnitCode;
        yield return toObjectArray();

        testCase = "RateComponentCode.Limit => null";
        rateComponentCode = RateComponentCode.Limit;
        obj = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode_RateComponentCode_object args = new(testCase, measureUnit, measureUnitCode, rateComponentCode, obj);

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
