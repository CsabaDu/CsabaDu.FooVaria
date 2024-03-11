namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Fields
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
    #endregion
    #endregion

    #region bool, MeasureUnitCode
    internal record Bool_MeasureUnitCode_args(bool IsTrue, MeasureUnitCode MeasureUnitCode) : Bool_arg(IsTrue)
    {
        internal override object[] ToObjectArray() => [IsTrue, MeasureUnitCode];
    }
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

        // Invalid MeasureUnitCode enum
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

    //    internal IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
    //    {
    //        measureUnit = null;
    //        yield return toObjectArray();

    //        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
    //        {
    //            yield return item;
    //        }

    //        measureUnit = RandomParams.GetRandomMeasureUnit();
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Enum_arg
    //            {
    //                MeasureUnit = measureUnit,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBaseMeasurableEqualsArgsArrayList()
    //    {
    //        isTrue = false;
    //        obj = null;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        yield return toObjectArray();

    //        obj = new();
    //        yield return toObjectArray();

    //        isTrue = true;
    //        factory = new BaseMeasurableFactoryClass();
    //        obj = new BaseMeasurableChild((IBaseMeasurableFactory)factory, measureUnitCode);
    //        yield return toObjectArray();

    //        isTrue = false;
    //        obj = new BaseMeasurableChild((IBaseMeasurableFactory)factory, RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Bool_Object_Enum_args
    //            {
    //                IsTrue = isTrue,
    //                Obj = obj,
    //                MeasureUnitCode = measureUnitCode,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitCodeArgArrayList()
    //    {
    //        isTrue = true;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        baseMeasurable = new BaseMeasurableChild(new BaseMeasurableFactoryClass(), measureUnitCode);
    //        yield return toObjectArray();

    //        isTrue = false;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //        yield return toObjectArray();

    //        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Bool_MeasureUnitCode_IBaseMeasurable_args
    //            {
    //                IsTrue = isTrue,
    //                MeasureUnitCode = measureUnitCode,
    //                BaseMeasurable = baseMeasurable,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitCodeArgsArrayList()
    //    {
    //        isTrue = true;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
    //        yield return toObjectArray();

    //        isTrue = false;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //        yield return toObjectArray();

    //        measureUnit = null;
    //        yield return toObjectArray();

    //        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //        measureUnit = RandomParams.GetRandomMeasureUnit();
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Bool_MeasureUnitCode_Enum_args
    //            {
    //                IsTrue = isTrue,
    //                MeasureUnitCode = measureUnitCode,
    //                MeasureUnit = measureUnit,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBoolEnumMeasureUnitArgArrayList()
    //    {
    //        isTrue = false;
    //        measureUnit = null;
    //        yield return toObjectArray();

    //        measureUnit = SampleParams.DefaultLimitMode;
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //        yield return toObjectArray();

    //        isTrue = true;
    //        measureUnit = RandomParams.GetRandomMeasureUnit();
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Bool_Enum_args
    //            {
    //                IsTrue = isTrue,
    //                MeasureUnit = measureUnit,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitMeasureUnitCodeArgsArrayList()
    //    {
    //        measureUnit = SampleParams.DefaultLimitMode;
    //        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //        name = ParamNames.measureUnit;
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //        yield return toObjectArray();

    //        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //        name = ParamNames.measureUnitCode;
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Enum_MeasureUnitCode_String_args
    //            {
    //                MeasureUnit = measureUnit,
    //                MeasureUnitCode = measureUnitCode,
    //                IsTrue = name,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetMeasurableValidateInvalidArgArrayList()
    //    {
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        rootObject = new BaseMeasurableFactoryClass();
    //        name = ParamNames.factory;
    //        yield return toObjectArray();

    //        rootObject = new BaseMeasurableChild((IBaseMeasurableFactory)rootObject, measureUnitCode);
    //        name = ParamNames.other;
    //        yield return toObjectArray();

    //        rootObject = new MeasurableChild(new MeasurableFactoryClass(), RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new IFooVariaObject_string_MeasureUnitCode_args
    //            {
    //                FooVariaObject = rootObject,
    //                IsTrue = name,
    //                MeasureUnitCode = measureUnitCode,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetMeasurableValidateValidArgArrayList()
    //    {
    //        measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //        measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
    //        rootObject = new MeasurableFactoryClass();
    //        yield return toObjectArray();

    //        rootObject = new MeasurableChild((IMeasurableFactory)rootObject, measureUnitCode);
    //        yield return toObjectArray();

    //        rootObject = new BaseMeasurementFactoryClass();
    //        yield return toObjectArray();

    //        rootObject = new BaseMeasurementChild((IBaseMeasurementFactory)rootObject, measureUnit);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new MeasureUnitCode_IFooVariaObject_args
    //            {
    //                MeasureUnitCode = measureUnitCode,
    //                FooVariaObject = rootObject,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBaseMeasurableValidateInvalidArgArrayList()
    //    {
    //        factory = new BaseMeasurableFactoryClass();

    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        commonBase = new CommonBaseChild(factory);
    //        yield return toObjectArray();

    //        commonBase = new BaseMeasurableChild((IBaseMeasurableFactory)factory, RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new MeasureUnitCode_ICommonBase_args
    //            {
    //                MeasureUnitCode = measureUnitCode,
    //                CommonBase = commonBase,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetCommonBaseValidateArgArrayList()
    //    {
    //        rootObject = new BaseMeasurableFactoryClass();
    //        yield return toObjectArray();

    //        rootObject = new CommonBaseChild((IBaseMeasurableFactory)rootObject);
    //        yield return toObjectArray();

    //        rootObject = new BaseSpreadFactoryImplementation();
    //        yield return toObjectArray();

    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        rootObject = new BaseMeasurableChild((IBaseMeasurableFactory)rootObject, measureUnitCode);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new IFooVariaObject_arg
    //            {
    //                FooVariaObject = rootObject,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetBaseMeasurableValidateValidArgArrayList()
    //    {
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        rootObject = new BaseMeasurableFactoryClass();
    //        yield return toObjectArray();

    //        rootObject = new BaseMeasurableChild((IBaseMeasurableFactory)rootObject, measureUnitCode);
    //        yield return toObjectArray();

    //        baseMeasurable = (IBaseMeasurable)rootObject;
    //        rootObject = new BaseSpreadFactoryImplementation();
    //        yield return toObjectArray();

    //        rootObject = new BaseSpreadChild((IBaseSpreadFactory)rootObject, baseMeasurable);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new MeasureUnitCode_IFooVariaObject_args
    //            {
    //                MeasureUnitCode = measureUnitCode,
    //                FooVariaObject = rootObject,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetCustomNameCollectionArgArrayList()
    //    {
    //        nameCollection = new Dictionary<object, string>();
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //        name = Guid.NewGuid().ToString();
    //        nameCollection.Add(measureUnit, name);
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnit);
    //        name = Guid.NewGuid().ToString();
    //        nameCollection.Add(measureUnit, name);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new DictionaryObjectString_arg
    //            {
    //                NameCollection = nameCollection,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetMeasureUnitCodeCustomNameCollectionArgsArrayList()
    //    {
    //        measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //        measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
    //        nameCollection = new Dictionary<object, string>();
    //        yield return toObjectArray();

    //        name = Guid.NewGuid().ToString();
    //        nameCollection.Add(measureUnit, name);
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
    //        name = Guid.NewGuid().ToString();
    //        nameCollection.Add(measureUnit, name);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new MeasureUnitCode_DictionaryObjectString_args
    //            {
    //                MeasureUnitCode = measureUnitCode,
    //                NameCollection = nameCollection,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetCustomNameCollectionMeasureUnitCodeArgArrayList()
    //    {
    //        measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //        measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
    //        nameCollection = new Dictionary<object, string>();
    //        yield return toObjectArray();

    //        name = Guid.NewGuid().ToString();
    //        nameCollection.Add(measureUnit, name);
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new DictionaryObjectString_MeasureUnitCode_args
    //            {
    //                NameCollection = nameCollection,
    //                MeasureUnitCode = measureUnitCode,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetMeasurementFactoryNameArgsArrayList()
    //    {
    //        factory = null;
    //        name = ParamNames.factory;
    //        yield return toObjectArray();

    //        factory = new MeasurementFactory();
    //        name = ParamNames.measureUnit;
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new String_IMeasurementFactory_args
    //            {
    //                IsTrue = name,
    //                Factory = (IMeasurementFactory)factory,
    //            }
    //            .ToObjectArray();
    //        }

    //        #endregion
    //    }
    //#endregion
    #endregion
}
