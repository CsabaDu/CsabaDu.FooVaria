namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private bool isTrue;
    //    private IRootObject rootObject;
    private MeasureUnitCode measureUnitCode;
    private Enum measureUnit;
    private object obj;
    //    private string name;
    //    private IFactory factory;
    //    private IBaseMeasurable baseMeasurable;
    //    private ICommonBase commonBase;
    //    private IMeasurable measurable;
    //    private IDictionary<object, string> nameCollection;
    //    //private IBaseMeasurement baseMeasurement;

    #region Readonly fileds
    private readonly RandomParams RandomParams = new();
    #endregion
    #endregion

    #region Protected types
    #region Abstract ObjectArray
    protected abstract class ObjectArray
    {
        public abstract object[] ToObjectArray();
    }
    #endregion

    #region IFooVariaObject
    //    protected class IFooVariaObject_arg : ObjectArray
    //    {
    //        internal IFooVariaObject FooVariaObject { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                FooVariaObject,
    //            };
    //        }
    //    }

    //    #region IFooVariaObject, string
    //    protected class IFooVariaObject_string_args : IFooVariaObject_arg
    //    {
    //        internal string Name { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                FooVariaObject,
    //                Name,
    //            };
    //        }
    //    }

    //    #region IFooVariaObject, string, MeasureUnitCode
    //    protected class IFooVariaObject_string_MeasureUnitCode_args : IFooVariaObject_string_args
    //    {
    //        internal MeasureUnitCode MeasureUnitCode { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                FooVariaObject,
    //                Name,
    //                MeasureUnitCode,
    //            };
    //        }
    //    }
    //    #endregion
    //    #endregion
    #endregion

    #region string
    //    protected class String_arg : ObjectArray
    //    {
    //        internal string Name { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                Name,
    //            };
    //        }
    //    }

    //    #region string, IMeasurementFactory
    //    protected class String_IMeasurementFactory_args : String_arg
    //    {
    //        internal IMeasurementFactory Factory { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                Name,
    //                Factory,
    //            };
    //        }
    //    }
    //    #endregion
    #endregion

    #region bool
    protected class Bool_arg : ObjectArray
    {
        internal bool IsTrue { get; init; }

        public override object[] ToObjectArray()
        {
            return
            [
                IsTrue,
            ];
        }
    }

    #region bool, object
    protected class Bool_Object_args : Bool_arg
    {
        internal object Object { get; init; }

        public override object[] ToObjectArray()
        {
            return
            [
                IsTrue,
                Object,
            ];
        }
    }

    #region bool, object, MeasureUnitCode
    protected class Bool_Object_MeasureUnitCode_args : Bool_Object_args
    {
        internal MeasureUnitCode MeasureUnitCode { get; init; }

        public override object[] ToObjectArray()
        {
            return
            [
                IsTrue,
                Object,
                MeasureUnitCode,
            ];
        }
    }
    #endregion
    #endregion

    #region bool, MeasureUnitCode
    protected class Bool_MeasureUnitCode_args : Bool_arg
    {
        internal MeasureUnitCode MeasureUnitCode { get; init; }

        public override object[] ToObjectArray()
        {
            return
            [
                IsTrue,
                MeasureUnitCode,
            ];
        }
    }

    //    #region bool, MeasureUnitCode, Enum
    //    protected class Bool_MeasureUnitCode_Enum_args : Bool_MeasureUnitCode_args
    //    {
    //        internal Enum MeasureUnit { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                IsTrue,
    //                MeasureUnitCode,
    //                MeasureUnit,
    //            };
    //        }
    //    }
    //    #endregion

    //    #region bool, MeasureUnitCode, IBaseMeasurable
    //    protected class Bool_MeasureUnitCode_IBaseMeasurable_args : Bool_MeasureUnitCode_args
    //    {
    //        internal IBaseMeasurable BaseMeasurable { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                IsTrue,
    //                MeasureUnitCode,
    //                BaseMeasurable,
    //            };
    //        }

    //    }

    //    #region bool, MeasureUnitCode, IBaseMeasurable, Enum
    //    protected class Bool_MeasureUnitCode_IBaseMeasurable_Enum_args : Bool_MeasureUnitCode_IBaseMeasurable_args
    //    {
    //        internal Enum MeasureUnit { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                IsTrue,
    //                MeasureUnitCode,
    //                BaseMeasurable,
    //                MeasureUnit,
    //            };
    //        }
    //    }
    //    #endregion
    //    #endregion
    #endregion

    //    #region bool, Enum
    //    protected class Bool_Enum_args : Bool_arg
    //    {
    //        internal Enum MeasureUnit { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                IsTrue,
    //                MeasureUnit,
    //            };
    //        }
    //    }
    #endregion

    #region Enum
    protected class Enum_arg : ObjectArray
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return
            [
                MeasureUnit,
            ];
        }
    }

    //    #region Enum, MeasureUnitCode
    //    protected class Enum_MeasureUnitCode_args : Enum_arg
    //    {
    //        internal MeasureUnitCode MeasureUnitCode { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnit,
    //                MeasureUnitCode,
    //            };
    //        }
    //    }

    //    #region Enum, MeasureUnitCode, string
    //    protected class Enum_MeasureUnitCode_String_args : Enum_MeasureUnitCode_args
    //    {
    //        internal string Name { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnit,
    //                MeasureUnitCode,
    //                Name,
    //            };
    //        }
    //    }
    //    #endregion
    //    #endregion

    //    #region Enum, string
    //    protected class Enum_string_args : Enum_arg
    //    {
    //        internal string Name { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnit,
    //                Name,
    //            };
    //        }
    //    }
    //    #endregion
    #endregion

    #region MeasureUnitCode
    //    protected class MeasureUnitCode_arg : ObjectArray
    //    {
    //        internal MeasureUnitCode MeasureUnitCode { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //            };
    //        }
    //    }

    //    #region MeasureUnitCode, IFooVariaObject
    //    protected class MeasureUnitCode_IFooVariaObject_args : MeasureUnitCode_arg
    //    {
    //        internal IFooVariaObject FooVariaObject { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //                FooVariaObject,
    //            };
    //        }
    //    }
    //    #endregion

    //    #region MeasureUnitCode, ICommonBase
    //    protected class MeasureUnitCode_ICommonBase_args : MeasureUnitCode_arg
    //    {
    //        internal ICommonBase CommonBase { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //                CommonBase,
    //            };
    //        }
    //    }
    //    #endregion

    //    #region MeasureUnitCode, IBaseMeasurable
    //    protected class MeasureUnitCode_IBaseMeasurable_args : MeasureUnitCode_arg
    //    {
    //        internal IBaseMeasurable BaseMeasurable { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //                BaseMeasurable,
    //            };
    //        }
    //    }
    //    #endregion

    //    #region MeasureUnitCode, IMeasurable
    //    protected class MeasureUnitCode_IMeasurable_args : MeasureUnitCode_arg
    //    {
    //        internal IMeasurable Measurable { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //                Measurable,
    //            };
    //        }
    //    }
    //    #endregion

    //    #region MeasureUnitCode, IDictionary<object, string>
    //    protected class MeasureUnitCode_DictionaryObjectString_args : MeasureUnitCode_arg
    //    {
    //        internal IDictionary<object, string> NameCollection { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                MeasureUnitCode,
    //                NameCollection,
    //            };
    //        }
    //    }
    //    #endregion
    #endregion

    #region IDictionary<object, string>
    //    protected class DictionaryObjectString_arg : ObjectArray
    //    {
    //        internal IDictionary<object, string> NameCollection { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                NameCollection,
    //            };
    //        }
    //    }

    //    #region IDictionary<object, string>, MeasureUnitCode
    //    protected class DictionaryObjectString_MeasureUnitCode_args : DictionaryObjectString_arg
    //    {
    //        internal MeasureUnitCode MeasureUnitCode { get; init; }

    //        public override object[] ToObjectArray()
    //        {
    //            return new object[]
    //            {
    //                NameCollection,
    //                MeasureUnitCode,
    //            };
    //        }
    //    }

    #endregion
    #endregion

    #region Internal ArrayList methods
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Enum_arg
            {
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        isTrue = false;
        obj = null;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        obj = new();
        yield return toObjectArray();

        isTrue = true;
        IMeasurableFactory factory = new MeasurableFactoryClass();
        obj = new MeasurableChild(factory, measureUnitCode);
        yield return toObjectArray();

        isTrue = false;
        obj = new MeasurableChild(factory, RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Object_MeasureUnitCode_args
            {
                IsTrue = isTrue,
                Object = obj,
                MeasureUnitCode = measureUnitCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBoolMeasureUnitCodeArgsArrayList()
    {
        measureUnitCode = NotDefinedMeasureUnitCode;
        isTrue = false;
        yield return toObjectArray();

        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitCode_args
            {
                IsTrue = isTrue,
                MeasureUnitCode = measureUnitCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    //    internal IEnumerable<object[]> GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList()
    //    {
    //        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
    //        {
    //            yield return item;
    //        }

    //        measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
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
    //            return new Bool_Object_MeasureUnitCode_args
    //            {
    //                IsTrue = isTrue,
    //                Object = obj,
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
    //                Name = name,
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
    //                Name = name,
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

    //        rootObject = new BaseMeasurementFactoryChild();
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

    //    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeArgsArrayList()
    //    {
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        measurable = new MeasurableChild(new MeasurableFactoryClass(), RandomParams.GetRandomMeasureUnitCode(measureUnitCode));
    //        yield return toObjectArray();

    //        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new MeasureUnitCode_IMeasurable_args
    //            {
    //                MeasureUnitCode = measureUnitCode,
    //                Measurable = measurable,
    //            }
    //            .ToObjectArray();
    //        }
    //        #endregion
    //    }

    //    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    //    {
    //        measureUnit = SampleParams.DefaultLimitMode;
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //        yield return toObjectArray();

    //        measureUnit = RandomParams.GetRandomMeasureUnit();
    //        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(MeasureUnitTypes.GetMeasureUnitCode(measureUnit));
    //        yield return toObjectArray();

    //        #region toObjectArray method
    //        object[] toObjectArray()
    //        {
    //            return new Enum_MeasureUnitCode_args
    //            {
    //                MeasureUnit = measureUnit,
    //                MeasureUnitCode = measureUnitCode,
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
    //                Name = name,
    //                Factory = (IMeasurementFactory)factory,
    //            }
    //            .ToObjectArray();
    //        }

    //        #endregion
    //    }
    #endregion
}
