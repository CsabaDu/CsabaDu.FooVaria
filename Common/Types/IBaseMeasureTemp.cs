namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseMeasureTemp : IBaseMeasurable, IQuantifiable
    {
        decimal DefaultQuantity { get; }

        void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);
    }
}
