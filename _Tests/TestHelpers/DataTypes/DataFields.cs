namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes;

public class DataFields
{
#region Readonly fields
public readonly RandomParams RandomParams = new();
public readonly RootObject RootObject = new();
#endregion

public Enum context;
public decimal defaultQuantity;
public decimal exchangeRate;
public bool isTrue;
public ILimiter limiter;
public LimitMode? limitMode;
public Enum measureUnit;
public MeasureUnitCode measureUnitCode;
public object obj;
public TypeCode typeCode;
public Type measureUnitType;
public string paramName;

}
