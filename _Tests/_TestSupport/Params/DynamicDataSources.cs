using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal static class DynamicDataSources
{
    #region Protected types
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

    #region Internal ArrayList methods
    internal static IEnumerable<object[]> GetBaseMeasurableEqualsObjectArgsArrayList()
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

    internal static IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgs()
    {
        MeasureUnitTypeCode? measureUnitTypeCode = null;
        Enum expected = getDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode());
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(expected);

        yield return toObjectArray();

        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(expected));
        expected = getDefaultMeasureUnit(measureUnitTypeCode.Value);

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

        Enum getDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
        {
            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitTypes().First(x => x.Name == measureUnitTypeCode.GetName());
            return (Enum)Enum.ToObject(measureUnitType, default(int));
        }
        #endregion
    }
    #endregion
}
