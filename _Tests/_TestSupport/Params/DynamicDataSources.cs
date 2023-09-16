using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Protected types
    #region bool
    protected class EqualsArg
    {
        internal bool AreEqual { get; init; }

        public virtual object[] ToObjectArray()
        {
            return new object[]
            {
                AreEqual,
            };
        }
    }

    protected class EqualsObjectArgs : EqualsArg
    {
        internal object Obj { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                AreEqual,
                Obj,
            };
        }
    }

    protected class BaseMeasurableEqualsObjectArgs : EqualsObjectArgs
    {
        internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                AreEqual,
                Obj,
                MeasureUnitTypeCode,
            };
        }
    }

    protected class MeasurableEqualsObjectArgs : BaseMeasurableEqualsObjectArgs
    {
        IMeasurableFactory MeasurableFactory { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                AreEqual,
                Obj,
                MeasureUnitTypeCode,
                MeasurableFactory,
            };
        }
    }

    protected class MeasurementEqualsObjectArgs : BaseMeasurableEqualsObjectArgs
    {
        decimal ExchangeRate { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                AreEqual,
                Obj,
                MeasureUnitTypeCode,
                ExchangeRate,
            };
        }
    }
    #endregion

    #region NullableMeasureUnitTypeCode
    protected class NullableMeasureUnitTypeCode
    {
        internal MeasureUnitTypeCode? MeasureUnitTypeCode { get; init; }

        public virtual object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
            };
        }
    }

    protected class NullableMeasureUnitTypeCodeEnum : NullableMeasureUnitTypeCode
    {
        internal Enum MeasureUnit { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                MeasureUnit,
            };
        }

    }

    protected class BaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgs : NullableMeasureUnitTypeCodeEnum
    {
        internal IBaseMeasurable BaseMeasurable { get; init; }

        public override object[] ToObjectArray()
        {
            return new object[]
            {
                MeasureUnitTypeCode,
                MeasureUnit,
                BaseMeasurable,
            };
        }
    }
    #endregion

    #region EnumMeasureUnit
    protected class EnumMeasureUnit
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
    #endregion

    #region Internal ArrayList methods
    #region General
    internal IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        Enum measureUnit = SampleParams.DefaultLimitMode;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region Local methods
        object[] toObjectArray()
        {
            return new EnumMeasureUnit
            {
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetNullableEnumMeasureUnitArgArrayList()
    {
        Enum measureUnit = null;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit();
        yield return toObjectArray();

        #region Local methods
        object[] toObjectArray()
        {
            return new EnumMeasureUnit
            {
                MeasureUnit = measureUnit,
            }
            .ToObjectArray();
        }
        #endregion
    }


    internal IEnumerable<object[]> GetNullableMeasureUnitTypeCodeArgArrayList()
    {
        MeasureUnitTypeCode? measureUnitTypeCode = null;
        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        #region Local methods
        object[] toObjectArray()
        {
            return new NullableMeasureUnitTypeCode
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion

    }
    #endregion

    #region BaseMeasurable
    internal IEnumerable<object[]> GetBaseMeasurableEqualsObjectArgsArrayList()
    {
        bool expected = false;
        object obj = null;
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        yield return toObjectArray();

        obj = new();
        yield return toObjectArray();

        expected = true;
        obj = new BaseMeasurableChild(measureUnitTypeCode);
        yield return toObjectArray();

        expected = false;
        obj = new BaseMeasurableChild(RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
        yield return toObjectArray();

        #region Local methods
        object[] toObjectArray()
        {
            return new BaseMeasurableEqualsObjectArgs
            {
                AreEqual = expected,
                Obj = obj,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        MeasureUnitTypeCode? measureUnitTypeCode = null;
        Enum expected = MeasureUnitTypes.GetDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode());
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(expected);
        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(expected));
        expected = MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitTypeCode.Value);
        yield return toObjectArray();

        #region Local methods
        object[] toObjectArray()
        {
            return new BaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgs
            {
                MeasureUnitTypeCode = measureUnitTypeCode,
                MeasureUnit = expected,
                BaseMeasurable = baseMeasurable,
            }
            .ToObjectArray();
        }
        #endregion
    }
    #endregion
    #endregion
}
