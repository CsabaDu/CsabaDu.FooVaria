﻿namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests;

internal class DynamicDataSource : DataFields
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsObjectArg()
    {
        // null
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = false;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // IBaseMeasurement
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(),
            }
        };
        isTrue = measureUnit.Equals((obj as IBaseMeasurement).GetBaseMeasureUnit());
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(isTrue, obj, measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsBaseMeasurementArg()
    {
        // null
        obj = null;
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit
        isTrue = true;
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
            }
        };
        yield return toObjectArray();

        // Different MeasureUnit, same MeasureUnitCode and same ExhchangeRate
        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, decimal.One, RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        obj = new BaseMeasurementChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
            }
        };
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasurement baseMeasurement = (IBaseMeasurement)obj;
            Bool_Enum_IBaseMeasurement_args item = new(isTrue, measureUnit, baseMeasurement);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetExchangeRateCollectionArg()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, RandomParams.GetRandomPositiveDecimal(), RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsExchangeableToArg()
    {
        // null -  false
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        context = null;
        yield return toObjectArray();

        // Not measureUnit not MeasureUnitCode Enum - false
        context = TypeCode.Empty;
        yield return toObjectArray();

        // other MeasureUnitCode - false
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        // same type not defined measureUnit - false
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same MeasureUnitCode - true
        isTrue = true;
        context = measureUnitCode;
        yield return toObjectArray();

        // same type valid measureUnit - true
        context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // other type measureUnit - false
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same type invalid measureUnit - false
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode();
        paramName = RandomParams.GetRandomParamName();
        SetCustomMeasureUnit(paramName, measureUnitCode, RandomParams.GetRandomPositiveDecimal());
        measureUnit = GetMeasureUnit(paramName);
        context = RandomParams.GetRandomNotUsedCustomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateExchangeRateArg()
    {
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        decimalQuantity = 0;
        yield return toObjectArray();

        decimalQuantity = RandomParams.GetRandomNegativeDecimal();
        yield return toObjectArray();

        decimalQuantity = RandomParams.GetRandomPositiveDecimal(GetExchangeRate(measureUnit, null));
        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_args item = new(measureUnit, decimalQuantity);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        // MeasureUnitCode
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        context = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        // measureUnit
        context = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Enum_args item = new(measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
