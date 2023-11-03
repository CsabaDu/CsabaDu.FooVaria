namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IMeasureUnit<out T> where T : Enum
    {
        T GetMeasureUnit();
    }


    public interface IDenominate : IMultiply<IBaseMeasurable, IQuantifiable>
    {

    }

}
