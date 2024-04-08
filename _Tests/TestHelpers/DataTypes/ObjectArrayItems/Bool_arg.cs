using CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Bool_arg(bool IsTrue) : ObjectArray
    {
        public override object[] ToObjectArray() => [IsTrue];
    }

    public record Bool_Enum_ValueType_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity) : Bool_Enum_args(IsTrue, MeasureUnit)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity];
    }

    public record Bool_Enum_ValueType_RateComponentCode_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity, RateComponentCode RateComponentCode) : Bool_Enum_ValueType_args(IsTrue, MeasureUnit, Quantity)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity, RateComponentCode];
    }


    public record Bool_Enum_ValueType_RateComponentCode_IBaseMeasure_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity, RateComponentCode RateComponentCode, IBaseMeasure BaseMeasure) : Bool_Enum_ValueType_RateComponentCode_args(IsTrue, MeasureUnit, Quantity, RateComponentCode)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity, RateComponentCode, BaseMeasure];
    }

    public record Bool_Enum_ValueType_RateComponentCode_IBaseMeasure_NullableLimitMode_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity, RateComponentCode RateComponentCode, IBaseMeasure BaseMeasure, LimitMode? LimitMode) : Bool_Enum_ValueType_RateComponentCode_IBaseMeasure_args(IsTrue, MeasureUnit, Quantity, RateComponentCode, BaseMeasure)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity, RateComponentCode, BaseMeasure, LimitMode];
    }
}
