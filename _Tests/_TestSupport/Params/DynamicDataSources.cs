using CsabaDu.FooVaria.Measurables.Factories.Implementations;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private bool isTrue;
    private IFooVariaObject fooVariaObject;
    private MeasureUnitTypeCode measureUnitTypeCode;
    private Enum measureUnit;
    private object obj;
    private string name;
    private IFactory factory;
    private IBaseMeasurable baseMeasurable;
    private ICommonBase commonBase;
    private IMeasurable measurable;
    private IDictionary<object, string> nameCollection;
    private IBaseMeasurement baseMeasurement;

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
    protected class IFooVariaObject_arg : ObjectArray
    {
        internal IFooVariaObject FooVariaObject { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                FooVariaObject,
            };
        }
    }

    #region IFooVariaObject, string
    protected class IFooVariaObject_string_args : IFooVariaObject_arg
    {
        internal string Name { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                FooVariaObject,
                Name,
            };
        }
    }

    #region IFooVariaObject, string, MeasureUnitTypeCode
    protected class IFooVariaObject_string_MeasureUnitTypeCode_args : IFooVariaObject_string_args
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                FooVariaObject,
                Name,
                MeasureUnitTypeCode,
            };
        }
    }
    #endregion
    #endregion
    #endregion

    #region bool
    protected class Bool_arg : ObjectArray
    {
        internal bool IsTrue { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
            };
        }
    }

    #region bool, object
    protected class Bool_Object_args : Bool_arg
    {
        internal object Object { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                Object,
            };
        }
    }

    #region bool, object, MeasureUnitTypeCode
    protected class Bool_Object_MeasureUnitTypeCode_args : Bool_Object_args
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                Object,
                MeasureUnitTypeCode,
            };
        }
    }
    #endregion
    #endregion

    #region bool, MeasureUnitTypeCode
    protected class Bool_MeasureUnitTypeCode_args : Bool_arg
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                MeasureUnitTypeCode,
            };
        }
    }

    #region bool, MeasureUnitTypeCode, Enum
    protected class Bool_MeasureUnitTypeCode_Enum_args : Bool_MeasureUnitTypeCode_args
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                MeasureUnitTypeCode,
                MeasureUnit,
            };
        }
    }
    #endregion

    #region bool, MeasureUnitTypeCode, IBaseMeasurable
    protected class Bool_MeasureUnitTypeCode_IBaseMeasurable_args : Bool_MeasureUnitTypeCode_args
    {
        internal IBaseMeasurable BaseMeasurable { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                MeasureUnitTypeCode,
                BaseMeasurable,
            };
        }

    }

    #region bool, MeasureUnitTypeCode, IBaseMeasurable, Enum
    protected class Bool_MeasureUnitTypeCode_IBaseMeasurable_Enum_args : Bool_MeasureUnitTypeCode_IBaseMeasurable_args
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                MeasureUnitTypeCode,
                BaseMeasurable,
                MeasureUnit,
            };
        }
    }
    #endregion
    #endregion
    #endregion

    #region bool, Enum
    protected class Bool_Enum_args : Bool_arg
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                IsTrue,
                MeasureUnit,
            };
        }
    }
    #endregion
    #endregion

    #region Enum
    protected class Enum_arg : ObjectArray
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
            };
        }
    }

    #region Enum, MeasureUnitTypeCode
    protected class Enum_MeasureUnitTypeCode_args : Enum_arg
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
                MeasureUnitTypeCode,
            };
        }
    }

    #region Enum, MeasureUnitTypeCode, string
    protected class Enum_MeasureUnitTypeCode_String_args : Enum_MeasureUnitTypeCode_args
    {
        internal string Name { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
                MeasureUnitTypeCode,
                Name,
            };
        }
    }
    #endregion
    #endregion
    #endregion

    #region MeasureUnitTypeCode
    protected class MeasureUnitTypeCode_arg : ObjectArray
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
            };
        }
    }

    #region MeasureUnitTypeCode, IFooVariaObject
    protected class MeasureUnitTypeCode_IFooVariaObject_args : MeasureUnitTypeCode_arg
    {
        internal IFooVariaObject FooVariaObject { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                FooVariaObject,
            };
        }
    }
    #endregion

    #region MeasureUnitTypeCode, ICommonBase
    protected class MeasureUnitTypeCode_ICommonBase_args : MeasureUnitTypeCode_arg
    {
        internal ICommonBase CommonBase { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                CommonBase,
            };
        }
    }
    #endregion

    #region MeasureUnitTypeCode, IBaseMeasurable
    protected class MeasureUnitTypeCode_IBaseMeasurable_args : MeasureUnitTypeCode_arg
    {
        internal IBaseMeasurable BaseMeasurable { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                BaseMeasurable,
            };
        }
    }
    #endregion

    #region MeasureUnitTypeCode, IMeasurable
    protected class MeasureUnitTypeCode_IMeasurable_args : MeasureUnitTypeCode_arg
    {
        internal IMeasurable Measurable { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                Measurable,
            };
        }
    }
    #endregion

    #region MeasureUnitTypeCode, IDictionary<object, string>
    protected class MeasureUnitTypeCode_DictionaryObjectString_args : MeasureUnitTypeCode_arg
    {
        internal IDictionary<object, string> NameCollection { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                NameCollection,
            };
        }
    }
    #endregion
    #endregion

    #region IDictionary<object, string>
    protected class DictionaryObjectString_arg : ObjectArray
    {
        internal IDictionary<object, string> NameCollection { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                NameCollection,
            };
        }
    }
    #endregion
    #endregion

    #region Internal ArrayList methods
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        measureUnit = SampleParams.DefaultLimitMode;
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

    internal IEnumerable<object[]> GetInvalidBaseMeasurementEnumMeasureUnitArgArrayList()
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
            return new Enum_arg
            {
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }


    internal IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
    {
        measureUnit = null;
        yield return toObjectArray();

        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
        {
            yield return item;
        }

        measureUnit = RandomParams.GetRandomMeasureUnit();
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

    internal IEnumerable<object[]> GetBaseMeasurableEqualsArgsArrayList()
    {
        isTrue = false;
        obj = null;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        obj = new();
        yield return toObjectArray();

        isTrue = true;
        factory = new FactoryImplementation();
        obj = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        isTrue = false;
        obj = new BaseMeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Object_MeasureUnitTypeCode_args
            {
                IsTrue = isTrue,
                Object = obj,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList()
    {
        isTrue = true;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        baseMeasurable = new BaseMeasurableChild(new FactoryImplementation(), measureUnitTypeCode);
        yield return toObjectArray();

        isTrue = false;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitTypeCode_IBaseMeasurable_args
            {
                IsTrue = isTrue,
                MeasureUnitTypeCode = measureUnitTypeCode,
                BaseMeasurable = baseMeasurable,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList()
    {
        isTrue = true;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode, measureUnit);
        yield return toObjectArray();

        isTrue = false;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        measureUnit = null;
        yield return toObjectArray();

        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitTypeCode_Enum_args
            {
                IsTrue = isTrue,
                MeasureUnitTypeCode = measureUnitTypeCode,
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBoolEnumMeasureUnitArgArrayList()
    {
        isTrue = false;
        measureUnit = null;
        yield return toObjectArray();

        measureUnit = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        isTrue = true;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Enum_args
            {
                IsTrue = isTrue,
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        measureUnit = SampleParams.DefaultLimitMode;
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        name = ParamNames.measureUnit;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        name = ParamNames.measureUnitTypeCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Enum_MeasureUnitTypeCode_String_args
            {
                MeasureUnit = measureUnit,
                MeasureUnitTypeCode = measureUnitTypeCode,
                Name = name,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        isTrue = false;
        obj = null;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        obj = new();
        yield return toObjectArray();

        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        obj = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        isTrue = true;
        obj = new MeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        isTrue = false;
        obj = new MeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Object_MeasureUnitTypeCode_args
            {
                IsTrue = isTrue,
                Object = obj,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateInvalidArgArrayList()
    {
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        fooVariaObject = new FactoryImplementation();
        name = ParamNames.factory;
        yield return toObjectArray();

        fooVariaObject = new BaseMeasurableChild((IFactory)fooVariaObject, measureUnitTypeCode);
        name = ParamNames.other;
        yield return toObjectArray();

        fooVariaObject = new MeasurableChild(new MeasurableFactoryImplementation(), RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new IFooVariaObject_string_MeasureUnitTypeCode_args
            {
                FooVariaObject = fooVariaObject,
                Name = name,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateValidArgArrayList()
    {
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
        fooVariaObject = new MeasurableFactoryImplementation();
        yield return toObjectArray();

        fooVariaObject = new MeasurableChild((IMeasurableFactory)fooVariaObject, measureUnitTypeCode);
        yield return toObjectArray();

        fooVariaObject = new BaseMeasurementFactoryChild();
        yield return toObjectArray();

        fooVariaObject = new BaseMeasurementChild((IBaseMeasurementFactory)fooVariaObject, measureUnit);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_IFooVariaObject_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                FooVariaObject = fooVariaObject,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList()
    {
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measurable = new MeasurableChild(new MeasurableFactoryImplementation(), RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_IMeasurable_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                Measurable = measurable,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBoolMeasureUnitTypeCodeArgsArrayList()
    {
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        isTrue = false;
        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitTypeCode_args
            {
                IsTrue = isTrue,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        measureUnit = SampleParams.DefaultLimitMode;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Enum_MeasureUnitTypeCode_args
            {
                MeasureUnit = measureUnit,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableValidateInvalidArgArrayList()
    {
        factory = new FactoryImplementation();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        commonBase = new CommonBaseChild(factory);
        yield return toObjectArray();

        commonBase = new BaseMeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_ICommonBase_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                CommonBase = commonBase,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetCommonBaseValidateArgArrayList()
    {
        fooVariaObject = new FactoryImplementation();
        yield return toObjectArray();

        fooVariaObject = new CommonBaseChild((IFactory)fooVariaObject);
        yield return toObjectArray();

        fooVariaObject = new BaseSpreadFactoryImplementation();
        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        fooVariaObject = new BaseMeasurableChild((IFactory)fooVariaObject, measureUnitTypeCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new IFooVariaObject_arg
            {
                FooVariaObject = fooVariaObject,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableValidateValidArgArrayList()
    {
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        fooVariaObject = new FactoryImplementation();
        yield return toObjectArray();

        fooVariaObject = new BaseMeasurableChild((IFactory)fooVariaObject, measureUnitTypeCode);
        yield return toObjectArray();

        baseMeasurable = (IBaseMeasurable)fooVariaObject;
        fooVariaObject = new BaseSpreadFactoryImplementation();
        yield return toObjectArray();

        fooVariaObject = new BaseSpreadChild((IBaseSpreadFactory)fooVariaObject, baseMeasurable);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_IFooVariaObject_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                FooVariaObject = fooVariaObject,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetCustomNameCollectionArgArrayList()
    {
        nameCollection = new Dictionary<object, string>();
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        name = Guid.NewGuid().ToString();
        nameCollection.Add(measureUnit, name);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnit);
        name = Guid.NewGuid().ToString();
        nameCollection.Add(measureUnit, name);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new DictionaryObjectString_arg
            {
                NameCollection = nameCollection,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasureUnitTypeCodeCustomNameCollectionArgsArrayList()
    {
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(measureUnit);
        nameCollection = new Dictionary<object, string>();
        yield return toObjectArray();

        name = Guid.NewGuid().ToString();
        nameCollection.Add(measureUnit, name);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        name = Guid.NewGuid().ToString();
        nameCollection.Add(measureUnit, name);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_DictionaryObjectString_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                NameCollection = nameCollection,
            }
            .ToObjectArray();
        }
        #endregion
    }



    #endregion
}
