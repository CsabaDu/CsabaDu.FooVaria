﻿namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Fields
    private Enum context;
    private bool isTrue;
    private MeasureUnitCode measureUnitCode;
    private Enum measureUnit;
    private object obj;
    private IFactory factory;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion
    #endregion

    #region Records
    #region Abstract ObjectArray
    internal abstract record ObjectArray
    {
        internal abstract object[] ToObjectArray();
    }
    #endregion

    #region bool
    internal record Bool_arg(bool IsTrue) : ObjectArray
    {
        internal override object[] ToObjectArray() => [IsTrue];
    }

    #region bool, Enum
    internal record Bool_Enum_args(bool IsTrue, Enum MeasureUnit) : Bool_arg(IsTrue)
    {
        internal override object[] ToObjectArray() => [IsTrue, MeasureUnit];
    }

    #region bool, Enum, Enum
    internal record Bool_Enum_Enum_args(bool IsTrue, Enum MeasureUnit, Enum Context) : Bool_Enum_args(IsTrue, MeasureUnit)
    {
        internal override object[] ToObjectArray() => [IsTrue, MeasureUnit, Context];
    }
    #endregion

    #region bool, Enum, IBaseMeasurement
    internal record Bool_Enum_IBaseMeasurement_args(bool IsTrue, Enum MeasureUnit, IBaseMeasurement BaseMeasurement) : Bool_Enum_args(IsTrue, MeasureUnit)
    {
        internal override object[] ToObjectArray() => [IsTrue, MeasureUnit, BaseMeasurement];
    }
    #endregion
    #endregion

    #region bool, object
    internal record Bool_Object_args(bool IsTrue, object Obj) : Bool_arg(IsTrue)
    {
        internal override object[] ToObjectArray() => [IsTrue, Obj];
    }

    #region bool, object, Enum
    internal record Bool_Object_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit) : Bool_Object_args(IsTrue, Obj)
    {
        internal override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit];
    }

    #region bool, object, Enum, Enum
    internal record Bool_Object_Enum_Enum_args(bool IsTrue, object Obj, Enum MeasureUnit, Enum OtherMeasureUnit) : Bool_Object_Enum_args(IsTrue, Obj, MeasureUnit)
    {
        internal override object[] ToObjectArray() => [IsTrue, Obj, MeasureUnit, OtherMeasureUnit];
    }

    #endregion
    #endregion
    #endregion

    #region bool, MeasureUnitCode
    internal record Bool_MeasureUnitCode_args(bool IsTrue, MeasureUnitCode MeasureUnitCode) : Bool_arg(IsTrue)
    {
        internal override object[] ToObjectArray() => [IsTrue, MeasureUnitCode];
    }

    #region bool, MeasureUnitCode, object
    internal record Bool_MeasureUnitCode_Object_args(bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : Bool_MeasureUnitCode_args(IsTrue, MeasureUnitCode)
    {
        internal override object[] ToObjectArray() => [IsTrue];
    }
    #endregion
    #endregion
    #endregion

    #region Enum
    internal record Enum_arg(Enum MeasureUnit) : ObjectArray
    {
        internal override object[] ToObjectArray() => [MeasureUnit];
    }

    #region Enum, MeasureUnitCode
    internal record Enum_MeasureUnitCode_args(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode) : Enum_arg(MeasureUnit)
    {
        internal override object[] ToObjectArray() => [MeasureUnit, MeasureUnitCode];
    }

    #region Enum, MeasureUnitCode, bool
    internal record Enum_MeasureUnitCode_bool_args(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue) : Enum_MeasureUnitCode_args(MeasureUnit, MeasureUnitCode)
    {
        internal override object[] ToObjectArray() => [MeasureUnit, MeasureUnitCode, IsTrue];
    }
    #endregion
    #endregion
    #endregion

    #region MeasureUnitCode
    internal record MeasureUnitCode_arg(MeasureUnitCode MeasureUnitCode) : ObjectArray
    {
        internal override object[] ToObjectArray() => [MeasureUnitCode];
    }

    #region MeasureUnitCode, IMeasurable
    internal record MeasureUnitCode_IMeasurable_args(MeasureUnitCode MeasureUnitCode, IMeasurable Measurable) : MeasureUnitCode_arg(MeasureUnitCode)
    {
        internal override object[] ToObjectArray() => [MeasureUnitCode, Measurable];
    }

    #region MeasureUnitCode, IMeasurable, bool
    internal record MeasureUnitCode_IMeasurable_bool_args(MeasureUnitCode MeasureUnitCode, IMeasurable Measurable, bool IsTrue) : MeasureUnitCode_IMeasurable_args(MeasureUnitCode, Measurable)
    {
        internal override object[] ToObjectArray() => [MeasureUnitCode, Measurable, IsTrue];
    }
    #endregion
    #endregion
    #endregion
    #endregion

    #region ArrayList methods
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(measureUnit);
            
            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        // null
        isTrue = false;
        obj = null;
        measureUnit = null;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // IMeasure with same MeasureUnit
        isTrue = true;
        obj = new MeasurableChild(SampleParams.rootObject, string.Empty);
        measureUnit = RandomParams.GetRandomMeasureUnit();
        (obj as MeasurableChild).Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        yield return toObjectArray();

        // IMeasure different MeasureUnit with same MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        (obj as MeasurableChild).Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        yield return toObjectArray();

        // IMeasure with different MeasureUnitCode
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Object_Enum_args item = new(isTrue, obj, measureUnit);
            
            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableIsValidMeasureUnitCodeArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        isTrue = false;
        yield return toObjectArray();

        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = true;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_bool_args item = new(measureUnit, measureUnitCode, isTrue);
            
            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        isTrue = true;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_bool_args item = new(measureUnit, measureUnitCode, isTrue);
            
            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        // Not MeasureUnit type Not MeasureUnitCode enum
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        measureUnit = TypeCode.Empty;
        yield return toObjectArray();

        // Valid type not defined measureUnit
        measureUnit = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // Invalid type defined measureUnit
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        // Not defined MeasureUnitCode enum
        measureUnit = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);
            
            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        measureUnit = measureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);
            
            return item.ToObjectArray();
        }
        #endregion
    }
    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        yield return toObjectArray();

        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurementInvalidEnumMeasureUnitArgArrayList()
    {
        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
        {
            yield return item;
        }

        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurementEqualsObjectArgArrayList()
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
        obj = new BaseMeasurementChild(SampleParams.rootObject, null)
        {
            Returns = new()
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

    internal IEnumerable<object[]> GetBaseMeasurementEqualsBaseMeasurementArgArrayList()
    {
        // null
        obj = null;
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        obj = new BaseMeasurementChild(SampleParams.rootObject, null)
        {
            Returns = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit
        isTrue = true;
        obj = new BaseMeasurementChild(SampleParams.rootObject, null)
        {
            Returns = new()
            {
                GetBaseMeasureUnit = measureUnit,
            }
        };
        yield return toObjectArray();

        // Different MeasureUnit, same MeasureUnitCode and same ExhchangeRate
        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        _ = TrySetCustomMeasureUnit(measureUnit, decimal.One, RandomParams.GetRandomParamName());
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        obj = new BaseMeasurementChild(SampleParams.rootObject, null)
        {
            Returns = new()
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

    internal IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
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

    internal IEnumerable<object[]> GetBaseMeasurementIsExchangeableToArgArrayList()
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
        context = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }

    #endregion
}
