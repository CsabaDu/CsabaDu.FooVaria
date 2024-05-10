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

    public static bool DoesNotThrowException(Action attempt)
    {
        try
        {
            attempt();
            return true;
        }
        catch (Exception ex)
        {
            logThrownException(attempt, ex);
            return false;
        }

        #region Local methods
        static void logThrownException(Action attempt, Exception ex)
        {
            const string logFileName = nameof(DoesNotThrowException) + "_failed";

            StartLog(BaseTypesLogDirectory, logFileName, attempt.Method.Name);
            LogVariable(BaseTypesLogDirectory, logFileName, ex.GetType().Name, ex.Message);
            EndLog(BaseTypesLogDirectory, logFileName);
        }
        #endregion
    }

    public static bool DoesSucceedAsExpected(bool success, object obj)
    {
        return obj is not null == success;
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
    #endregion
}
