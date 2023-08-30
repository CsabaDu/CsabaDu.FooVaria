namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IToVolumetricWeight
    {
        IWeight ToVolumetricWeight(decimal ratio, WeightUnit weightUnit = default);
    }
}
