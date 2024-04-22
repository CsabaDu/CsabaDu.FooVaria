﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_decimal_object_RoundingMode_args(string Case, Enum MeasureUnit, decimal DefaultQuantity, object Obj, RoundingMode RoundingMode) : Enum_decimal_object_args(Case, MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Obj, RoundingMode];
}
