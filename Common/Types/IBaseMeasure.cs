namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseMeasure : IMeasurable, IQuantifiable
    {
        decimal DefaultQuantity { get; }

        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }
}
