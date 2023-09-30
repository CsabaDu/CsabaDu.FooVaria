using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

internal class DynamicDataSources
{
    #region Private fields
    private readonly RandomParams RandomParams = new();
    #endregion

    protected abstract class ObjectArray
    {
        public abstract object[] ToObjectArray();
    }
    #region Protected types
    #region bool
    protected class BoolArg : ObjectArray
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

    protected class BoolObjectArgs : BoolArg
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

    protected class BoolObjectMeasureUnitTypeCodeArgs : BoolObjectArgs
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

    protected class BoolMeasureUnitTypeCodeArgs : BoolArg
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

    protected class BoolMeasureUnitTypeCodeIBaseMeasurableArgs : BoolMeasureUnitTypeCodeArgs
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

    //protected class EqualMeasureUnitEnumsArgs : BoolArg
    //{
    //    internal Enum MeasureUnit { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            MeasureUnit,
    //        };
    //    }
    //}

    //protected class EqualsMeasureUnitTypeCodeArgs : BoolArg
    //{
    //    internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            MeasureUnitTypeCode,
    //        };
    //    }
    //}

    //protected class HasMeasureUnitTypeCodeArgs : EqualsMeasureUnitTypeCodeArgs
    //{
    //    internal IBaseMeasurable BaseMeasurable { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            MeasureUnitTypeCode,
    //            BaseMeasurable,
    //        };
    //    }
    //}

    //protected class HasMeasureUnitTypeCodeMeasureUnitArgs : HasMeasureUnitTypeCodeArgs
    //{
    //    internal Enum MeasureUnit { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            MeasureUnitTypeCode,
    //            BaseMeasurable,
    //            MeasureUnit,
    //        };
    //    }
    //}

    //protected class EqualsObjectArgs : BoolArg
    //{
    //    internal object Obj { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            Obj,
    //        };
    //    }
    //}

    //protected class BaseMeasurableEqualsObjectArgs : EqualsObjectArgs
    //{
    //    internal MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            Obj,
    //            MeasureUnitTypeCode,
    //        };
    //    }
    //}

    //protected class MeasurableEqualsObjectArgs : BaseMeasurableEqualsObjectArgs
    //{
    //    IMeasurableFactory MeasurableFactory { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            Obj,
    //            MeasureUnitTypeCode,
    //            MeasurableFactory,
    //        };
    //    }
    //}

    //protected class MeasurementEqualsObjectArgs : BaseMeasurableEqualsObjectArgs
    //{
    //    decimal ExchangeRate { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            IsTrue,
    //            Obj,
    //            MeasureUnitTypeCode,
    //            ExchangeRate,
    //        };
    //    }
    //}
    //#endregion

    //#region NullableMeasureUnitTypeCode
    //protected class NullableMeasureUnitTypeCode
    //{
    //    internal MeasureUnitTypeCode? MeasureUnitTypeCode { get; init; }
    //    public virtual object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            MeasureUnitTypeCode,
    //        };
    //    }
    //}

    //protected class NullableMeasureUnitTypeCodeEnum : NullableMeasureUnitTypeCode
    //{
    //    internal Enum MeasureUnit { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            MeasureUnitTypeCode,
    //            MeasureUnit,
    //        };
    //    }
    //}

    //protected class BaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgs : NullableMeasureUnitTypeCodeEnum
    //{
    //    internal IBaseMeasurable BaseMeasurable { get; init; }

    //    public override object[] ToObjectArray()
    //    {
    //        return new object[]
    //        {
    //            MeasureUnitTypeCode,
    //            MeasureUnit,
    //            BaseMeasurable,
    //        };
    //    }
    //}
    //#endregion

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
    //#endregion

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
            return new BoolObjectMeasureUnitTypeCodeArgs
            {
                IsTrue = expected,
                Object = other,
                MeasureUnitTypeCode = measureUnitTypeCode,
            }
            .ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList()
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
            return new BoolMeasureUnitTypeCodeIBaseMeasurableArgs
            {
                IsTrue = expected,
                MeasureUnitTypeCode = measureUnitTypeCode,
                BaseMeasurable = baseMeasurable,
            }
            .ToObjectArray();
        }
        #endregion
    }

    //internal IEnumerable<object[]> GetNullableEnumMeasureUnitArgArrayList()
    //{
    //    Enum measureUnit = null;
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new EnumMeasureUnit
    //        {
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetNullableMeasureUnitTypeCodeArgArrayList()
    //{
    //    MeasureUnitTypeCode? measureUnitTypeCode = null;
    //    yield return toObjectArray();

    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new NullableMeasureUnitTypeCode
    //        {
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> ValidateMeasureUnitNullArgsArrayList()
    //{
    //    MeasureUnitTypeCode? measureUnitTypeCode = null;
    //    Enum measureUnit = null;
    //    yield return toObjectArray();

    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    yield return toObjectArray();

    //    measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new NullableMeasureUnitTypeCodeEnum
    //        {
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> ValidateMeasureUnitInvalidEnumArgArrayList()
    //{
    //    MeasureUnitTypeCode? measureUnitTypeCode = null;
    //    Enum measureUnit = SampleParams.DefaultLimitMode;
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //    yield return toObjectArray();

    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new NullableMeasureUnitTypeCodeEnum
    //        {
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> ValidateMeasureUnitValidMeasureUnitTypeCodeArgArrayList()
    //{
    //    MeasureUnitTypeCode? measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    Enum measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    measureUnitTypeCode = null;
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new NullableMeasureUnitTypeCodeEnum
    //        {
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> IsDefinedMeasureUnitArgsArrayList()
    //{
    //    bool expected = false;
    //    Enum measureUnit = null;
    //    yield return toObjectArray();

    //    measureUnit = SampleParams.DefaultLimitMode;
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomMeasureUnitTypeCode();
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
    //    yield return toObjectArray();

    //    expected = true;
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new EqualMeasureUnitEnumsArgs
    //        {
    //            IsTrue = expected,
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}
    #endregion
    #endregion
    #region BaseMeasurable
    //internal IEnumerable<object[]> BaseMeasurableEqualsObjectArgsArrayList()
    //{
    //    bool expected = false;
    //    object baseMeasurable = null;
    //    MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    yield return toObjectArray();

    //    baseMeasurable = new();
    //    yield return toObjectArray();

    //    expected = true;
    //    baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    expected = false;
    //    baseMeasurable = new BaseMeasurableChild(RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode));
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new BaseMeasurableEqualsObjectArgs
    //        {
    //            IsTrue = expected,
    //            Obj = baseMeasurable,
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetBaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    //{
    //    MeasureUnitTypeCode? measureUnitTypeCode = null;
    //    Enum expected = MeasureUnitTypes.GetDefaultMeasureUnit(RandomParams.GetRandomMeasureUnitTypeCode());
    //    IBaseMeasurable baseMeasurable = new BaseMeasurableChild(expected);
    //    yield return toObjectArray();

    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(MeasureUnitTypes.GetMeasureUnitTypeCode(expected));
    //    expected = MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitTypeCode.Value);
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new BaseMeasurableGetDefaultMeasureUnitMeasureUnitTypeCodeArgs
    //        {
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            MeasureUnit = expected,
    //            BaseMeasurable = baseMeasurable,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> HasMeasureUnitTypeCodeArgsArrayList()
    //{
    //    bool expected = true;
    //    MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    expected = false;
    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new HasMeasureUnitTypeCodeArgs
    //        {
    //            IsTrue = expected,
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            BaseMeasurable = baseMeasurable,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> HasMeasureUnitTypeCodeMeasureUnitArgsArrayList()
    //{
    //    bool expected = true;
    //    Enum measureUnit = null;
    //    MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
    //    IBaseMeasurable baseMeasurable = new BaseMeasurableChild(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode, measureUnit);
    //    yield return toObjectArray();

    //    expected = false;
    //    measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode(measureUnitTypeCode);
    //    yield return toObjectArray();

    //    measureUnit = SampleParams.DefaultLimitMode;
    //    yield return toObjectArray();

    //    measureUnit = SampleParams.NotDefinedMeasureUnitTypeCode;
    //    yield return toObjectArray();

    //    #region Local methods
    //    object[] toObjectArray()
    //    {
    //        return new HasMeasureUnitTypeCodeMeasureUnitArgs
    //        {
    //            IsTrue = expected,
    //            MeasureUnitTypeCode = measureUnitTypeCode,
    //            BaseMeasurable = baseMeasurable,
    //            MeasureUnit = measureUnit,
    //        }
    //        .ToObjectArray();
    //    }
    //    #endregion
    //}

    #endregion
    #endregion
    #endregion
}
