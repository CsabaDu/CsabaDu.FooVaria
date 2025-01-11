namespace CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

public sealed class TestSupport
{
    public const string BaseTypesLogDirectory = @"..\..\..\..\..\..\..\TestResults\Logs\";
    private const string Ext_txt = ".txt";

    internal static void RestoreConstantExchangeRates()
    {
        if (ExchangeRateCollection.Count != ConstantExchangeRateCount)
        {
            BaseMeasurement.RestoreConstantExchangeRates();
        }
    }

    public static bool SucceedsAsExpected(bool success, object obj)
    {
        return obj is not null == success;
    }

    public static bool? FitsIn<T>(T limitable, decimal defaultQuantity, LimitMode limitMode)
        where T : class, IDefaultQuantity, ILimitable
    {
        return limitable.GetDefaultQuantity().FitsIn(defaultQuantity, limitMode);
    }

    #region Logger
    public static void StartLog(string logDirectory, string logFileName, string testMethodName)
    {
        using StreamWriter writer = GetStreamWriter(logDirectory, logFileName);

        writer.WriteLine($"Time: {DateTime.Now}, Test method: {testMethodName}");
    }

    public static void LogVariable(string logDirectory, string logFileName, string variableName, object variableValue)
    {
        using StreamWriter writer = GetStreamWriter(logDirectory, logFileName);

        writer.WriteLine($"Variable {variableName} value: {variableValue}");
    }

    public static void EndLog(string logDirectory, string logFileName)
    {
        using StreamWriter writer = GetStreamWriter(logDirectory, logFileName);

        writer.WriteLine("---");
    }

    private static StreamWriter GetStreamWriter(string logDirectory, string logFileName)
    {
        string logFilePath = logDirectory + logFileName + Ext_txt;
        return new(logFilePath, true);
    }

    // const string methodName = nameof(TestMethod);
    // const string logFileName = $"testLogs_{methodName}";
    //
    // if (true)
    // {
    //     StartLog(BaseTypesLogDirectory, logFileName, methodName);
    //
    //     logVariable(nameof(variable), variable);
    //
    //     EndLog(BaseTypesLogDirectory, logFileName);
    // }
    //
    // #region Local methods
    // static void logVariable(string variableName, object variableValue)
    // {
    //     LogVariable(BaseTypesLogDirectory, logFileName, variableName, variableValue);
    // }
    // #endregion

    #endregion
}
