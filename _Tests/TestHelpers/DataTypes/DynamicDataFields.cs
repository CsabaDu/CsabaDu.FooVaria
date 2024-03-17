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
    protected ILimiter _limiter;
    protected LimitMode? _limitMode;
    protected Enum _measureUnit;
    protected MeasureUnitCode _measureUnitCode;
    protected bool? _nullableBool;
    protected object _obj;
    protected TypeCode _typeCode;
}
