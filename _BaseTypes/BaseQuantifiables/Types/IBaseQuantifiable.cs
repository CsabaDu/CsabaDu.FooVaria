﻿namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types;

public interface IBaseQuantifiable : IMeasurable, IDefaultQuantity, ILimitable
{
    void ValidateQuantity(ValueType? quantity, string paramName);
    void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName);
}
