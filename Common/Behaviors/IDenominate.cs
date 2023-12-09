namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IDenominate
    {
    }

    public interface IDenominate<in TOperand, out TSelf> : IDataErrorInfo, IMultiply<TOperand, TSelf> where TOperand : notnull where TSelf : class, IBaseMeasure
    {

    }
}
