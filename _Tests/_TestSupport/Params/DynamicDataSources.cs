using CsabaDu.FooVaria.Common;
using CsabaDu.FooVaria.Common.Behaviors;
using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private readonly RandomParams RandomParams = new();
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
    #endregion
    #endregion

    #region Internal ArrayList methods
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        Enum measureUnit = SampleParams.DefaultLimitMode;
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

    internal IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
    {
        Enum measureUnit = null;
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
        bool expected = false;
        object other = null;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        other = new();
        yield return toObjectArray();

        expected = true;
        IFactory factory = new FactoryImplementation();
        other = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        expected = false;
        other = new BaseMeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Object_MeasureUnitTypeCode_args
            {
                IsTrue = expected,
                Object = other,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList()
    {
        bool expected = true;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(new FactoryImplementation(), measureUnitTypeCode);
        yield return toObjectArray();

        expected = false;
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitTypeCode_IBaseMeasurable_args
            {
                IsTrue = expected,
                MeasureUnitTypeCode = measureUnitTypeCode,
                BaseMeasurable = baseMeasurable,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList()
    {
        bool expected = true;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode, measureUnit);
        yield return toObjectArray();

        expected = false;
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
                IsTrue = expected,
                MeasureUnitTypeCode = measureUnitTypeCode,
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBoolEnumMeasureUnitArgArrayList()
    {
        bool expected = false;
        Enum measureUnit = null;
        yield return toObjectArray();

        measureUnit = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        expected = true;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Enum_args
            {
                IsTrue = expected,
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        Enum measureUnit = SampleParams.DefaultLimitMode;
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        string name = ParamNames.measureUnit;
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
        bool expected = false;
        object other = null;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        other = new();
        yield return toObjectArray();

        IMeasurableFactory factory = new MeasurableFactoryImplementation();
        other = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        expected = true;
        other = new MeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        expected = false;
        other = new MeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_Object_MeasureUnitTypeCode_args
            {
                IsTrue = expected,
                Object = other,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateArgArrayList()
    {
        IMeasurableFactory factory = new MeasurableFactoryImplementation();

        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        ICommonBase other = new CommonBaseChild(factory);
        yield return toObjectArray();

        other = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        other = new MeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_ICommonBase_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                CommonBase = other,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList()
    {
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IMeasurable measurable = new MeasurableChild(new MeasurableFactoryImplementation(), RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
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
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
        bool expected = false;
        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        expected = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new Bool_MeasureUnitTypeCode_args
            {
                IsTrue = expected,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        Enum measureUnit = SampleParams.DefaultLimitMode;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
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

    internal IEnumerable<object[]> GetBaseMeasurableValidateArgArrayList()
    {
        IFactory factory = new FactoryImplementation();

        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        ICommonBase other = new CommonBaseChild(factory);
        yield return toObjectArray();

        other = new BaseMeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            return new MeasureUnitTypeCode_ICommonBase_args
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                CommonBase = other,
            }
            .ToObjectArray();
        }
        #endregion
    }

    #endregion
}
