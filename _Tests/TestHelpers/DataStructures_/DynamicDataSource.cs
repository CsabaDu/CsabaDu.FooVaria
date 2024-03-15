namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources;

public sealed class DynamicDataSource
{
    #region Fields
    private Enum _context;
    private decimal _exchangeRate;
    private bool _isTrue;
    private MeasureUnitCode _measureUnitCode;
    private Enum _measureUnit;
    private object _obj;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion
    #endregion

    #region Methods
    public IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnitCode();
        yield return toObjectArray();

        _measureUnit = RandomParams.GetRandomNotDefinedMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(_measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetMeasurableIsValidMeasureUnitCodeArgsArrayList()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        _isTrue = false;
        yield return toObjectArray();

        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _isTrue = true;
        yield return toObjectArray();

        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        _isTrue = false;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_bool_args item = new(_measureUnit, _measureUnitCode, _isTrue);

            return item.ToObjectArray();
        }
        #endregion
    }

    public IEnumerable<object[]> GetMeasurementInvalidEnumMeasureUnitArgArrayList()
    {
        foreach (object[] item in GetInvalidEnumMeasureUnitArgArrayList())
        {
            yield return item;
        }

        _measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_arg item = new(_measureUnit);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}