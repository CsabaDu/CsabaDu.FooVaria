﻿using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Protected types
    #region Abstract types
    protected abstract class ObjectArray
    {
        public abstract object[] ToObjectArray();
    }
    #endregion

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

    protected class Enum_arg
    {
        internal Enum MeasureUnit { get; init; }

        public virtual object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnit,
            };
        }
    }
    #endregion

    #region Internal ArrayList methods
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        Enum measureUnit = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region Local methods
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

        IFactory factory = new FactoryImplementation();
        expected = true;
        other = new BaseMeasurableChild(factory, measureUnitTypeCode);
        yield return toObjectArray();

        expected = false;
        other = new BaseMeasurableChild(factory, RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region Local methods
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

        #region Local methods
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

        #region Local methods
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

    #endregion
}
