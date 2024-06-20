namespace CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

public class SupplementaryAssert
{
    public static void DoesNotThrowException(Action attempt)
    {
        Assert.IsNull(getCaughtExceptionOrNull());

        #region Local methods
        Exception? getCaughtExceptionOrNull()
        {
            try
            {
                attempt();

                return null;
            }
            catch (Exception caughtException)
            {
                Trace.WriteLine(caughtException.Message, caughtException.GetType().Name);
                logCaughtException(attempt, caughtException);

                return caughtException;
            }
        }

        static void logCaughtException(Action attempt, Exception ex)
        {
            const string logFileName = nameof(DoesNotThrowException) + "_failed";

            StartLog(BaseTypesLogDirectory, logFileName, attempt.Method.Name);
            LogVariable(BaseTypesLogDirectory, logFileName, ex.GetType().Name, ex.Message);
            EndLog(BaseTypesLogDirectory, logFileName);
        }
        #endregion
    }
}
