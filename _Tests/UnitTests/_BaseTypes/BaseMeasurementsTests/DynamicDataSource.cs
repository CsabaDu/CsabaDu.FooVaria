namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsObjectArg()
    {
        testCase = "null => false";
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        isTrue = false;
        yield return toObjectArray();

        testCase = "object => false";
        obj = new();
        yield return toObjectArray();

        testCase = "IBaseMeasurement => measureUnits equal";
        obj = GetBaseMeasurementChild(RandomParams.GetRandomValidMeasureUnit());
        isTrue = measureUnit.Equals((obj as IBaseMeasurement).GetBaseMeasureUnit());
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_object_Enum args = new(testCase, isTrue, obj, measureUnit);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsBaseMeasurementArg()
    {
        testCase = "null => false";
        IBaseMeasurement baseMeasurement = null;
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        baseMeasurement = GetBaseMeasurementChild(RandomParams.GetRandomMeasureUnit(measureUnitCode));
        yield return toObjectArray();

        testCase = "Same MeasureUnit => true";
        isTrue = true;
        baseMeasurement = GetBaseMeasurementChild(measureUnit);
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode, same ExhchangeRate, Different measureUnit => true";
        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, decimal.One, RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode();
        baseMeasurement = GetBaseMeasurementChild(measureUnitCode.GetDefaultMeasureUnit());
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_IBaseMeasurement args = new(testCase, isTrue, measureUnit, baseMeasurement);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetExchangeRateCollectionArg()
    {
        testCase = "measureUnit";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        yield return toObjectArray();

        testCase = "Custom measureUnit";
        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, RandomParams.GetRandomNotNegativeDecimal(), RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode();
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsExchangeableToArg()
    {
        testCase = "null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        context = null;
        yield return toObjectArray();

        testCase = "Not measureUnit not MeasureUnitCode Enum => false";
        context = TypeCode.Empty;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = GetMeasureUnitCode();
        context = RandomParams.GetRandomConstantMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        testCase = "Not defined measureUnit of same type => false";
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode => true";
        isTrue = true;
        context = measureUnitCode;
        yield return toObjectArray();

        testCase = "Same type valid measureUnit => true";
        context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Different type measureUnit => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same type invalid measureUnit => false";
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode();
        paramName = RandomParams.GetRandomParamName();
        SetCustomMeasureUnit(paramName, measureUnitCode, RandomParams.GetRandomPositiveDecimal());
        measureUnit = GetMeasureUnit(paramName);
        context = RandomParams.GetRandomNotUsedCustomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_Enum args = new(testCase, isTrue, measureUnit, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateExchangeRateArg()
    {
        testCase = "Zero ExchangeRate";
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        decimalQuantity = 0;
        yield return toObjectArray();

        testCase = "Negative ExchangeRate";
        decimalQuantity = RandomParams.GetRandomNegativeDecimal();
        yield return toObjectArray();

        testCase = "Different ExchangeRate";
        decimalQuantity = RandomParams.GetRandomNotNegativeDecimal(GetExchangeRate(measureUnit, null));
        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal args = new(testCase, measureUnit, decimalQuantity);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        testCase = "MeasureUnitCode";
        measureUnit = RandomParams.GetRandomConstantMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        context = measureUnitCode;
        yield return toObjectArray();

        testCase = "measureUnit";
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_Enum args = new(testCase, measureUnit, context);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
