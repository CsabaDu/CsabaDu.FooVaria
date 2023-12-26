﻿namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IDenominate
    {
    }

    public interface IDenominate<out TSelf, in TOperand> : IDenominate, IMultiply<TSelf, TOperand>
        where TSelf : class, IBaseMeasure
        where TOperand : notnull
    {
    }
}
