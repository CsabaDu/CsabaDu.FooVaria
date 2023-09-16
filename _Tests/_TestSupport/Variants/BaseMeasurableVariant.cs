using CsabaDu.FooVaria.Tests.TestSupport.Params;

namespace CsabaDu.FooVaria.Tests.TestSupport.Variants;

public class BaseMeasurableVariant
{
    #region Constructros
    internal BaseMeasurableVariant(RandomParams randomParams)
    {
        RandomParams = randomParams;
    }
    #endregion

    #region Properties
    private RandomParams RandomParams { get; init; }
    #endregion

    #region Public methods
    internal Enum GetDefaultMeasureUnit(out MeasureUnitTypeCode measureUnitTypeCode)
    {
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        return MeasureUnitTypes.GetMeasureUnit(measureUnitTypeCode, default);
    }

    internal IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        if (measureUnitTypeCode != null) return getDefaultNames(measureUnitTypeCode.Value);

        IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = MeasureUnitTypes.GetMeasureUnitTypeCodes();
        IEnumerable<string> defaultNames = getDefaultNames(measureUnitTypeCodes.First());

        for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
        {
            IEnumerable<string> next = getDefaultNames(measureUnitTypeCodes.ElementAt(i));
            defaultNames = defaultNames.Union(next);
        }

        return defaultNames;

        #region Local methods
        IEnumerable<string> getDefaultNames(MeasureUnitTypeCode measureUnitTypeCode)
        {
            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);

            foreach (string item in Enum.GetNames(measureUnitType))
            {
                yield return measureUnitType.Name + "." + item;
            }
        }
        #endregion
    }

    internal Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = MeasureUnitTypes.GetDefaultMeasureUnit(measureUnitTypeCode);

        return measureUnit.GetType();
    }

    internal MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        string measureUnitTypeName = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
    }
    #endregion
}
