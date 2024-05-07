namespace CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

public sealed class TestSupport
{
    private const string LogFilePath = @"C:\Users\DudásCsaba\Source\Repos\Logs\";
    private const string Ext_txt = ".txt";

    internal static void RestoreConstantExchangeRates()
    {
        if (ExchangeRateCollection.Count != ConstantExchangeRateCount)
        {
            BaseMeasurement.RestoreConstantExchangeRates();
        }
    }

    public static bool DoesNotThrowException(Action attempt)
    {
        try
        {
            attempt();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool DoesSucceedAsExpected(bool success, object obj)
    {
        return obj is not null == success;
    }

    #region Logger
    private static string GetLogFilePath(string logFileName)
    {
        return LogFilePath + logFileName + Ext_txt;
    }

    public static void StartLog(string logFileName, string testMethodName)
    {
        using StreamWriter writer = new(GetLogFilePath(logFileName), true);

        writer.WriteLine($"Time: {DateTime.Now}, Test method: {testMethodName}");
    }

    public static void LogVariable(string logFileName, string variableName, object variableValue)
    {
        using StreamWriter writer = new(GetLogFilePath(logFileName), true);

        writer.WriteLine($"Variable {variableName} value: {variableValue}");
    }

    public static void EndLog(string logFileName)
    {
        using StreamWriter writer = new(GetLogFilePath(logFileName), true);

        writer.WriteLine($"---");
    }
    #endregion

}
