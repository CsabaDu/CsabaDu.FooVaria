namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public abstract class DynamicDataFields
{
    #region Readonly fields
    protected readonly RandomParams RandomParams = new();
    protected readonly RootObject RootObject = new();
    #endregion

    protected Enum _context;
    protected decimal _defaultQuantity;
    protected decimal _exchangeRate;
    protected bool _isTrue;
    protected Enum _measureUnit;
    protected MeasureUnitCode _measureUnitCode;
    protected object _obj;
}
