using Microsoft.Extensions.Configuration;
using Synnduit;
using Synnduit.Deployment;
using Synnduit.Logging;
using Synnduit.Persistence.SqlServer;
using Synnduit.Properties;
using System.ComponentModel.Composition;
using System.Reflection;

try
{
    var runName = GetRunName(args);
    var secretId = GetSecretId(args);
    var configuration =
        new ConfigurationBuilder()
        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
        .AddUserSecrets(secretId, true)
        .Build();
    DeploymentExecutorFactory
        .CreateDeploymentExecutor(configuration, typeof(Repository).Assembly)
        .Deploy();
    RunnerFactory
        .CreateRunner(
            configuration,
            runName,
            typeof(Repository).Assembly,
            typeof(ConsoleLogger<>).Assembly)
        .Run();
    return 0;
}
catch (ReflectionTypeLoadException reflectionTypeLoadException)
{
    PrintMultiPartExceptionDetails(
        reflectionTypeLoadException, reflectionTypeLoadException.LoaderExceptions);
    return 1;
}
catch (CompositionException compositionException)
{
    PrintMultiPartExceptionDetails(compositionException, compositionException.RootCauses);
    return 1;
}
catch (Exception exception)
{
    Console.WriteLine(exception);
    return 1;
}

static string GetRunName(string[] args)
{
    string runName = null;
    if (args.Length >= 1)
    {
        runName = args.GetValue(0).ToString().Trim();
    }
    if (string.IsNullOrEmpty(runName))
    {
        throw new InvalidOperationException(Resources.RunNameExpected);
    }
    return runName;
}

static string GetSecretId(string[] args)
{
    const string defaultSecretId = "2ca80c47-9bbf-4b07-8fca-3912a7e23a12";
    string secretId = null;
    if (args.Length > 1)
    {
        secretId = args.GetValue(1).ToString().Trim();
    }
    if (string.IsNullOrEmpty(secretId))
    {
        return defaultSecretId;
    }
    if (Guid.TryParse(secretId, out _))
    {
        return secretId;
    }
    throw new InvalidOperationException(Resources.NotValidGuidForUserSecretId);
}

static void PrintMultiPartExceptionDetails(
    Exception rootException, IEnumerable<Exception> subordinateExceptions)
{
    PrintException(rootException);
    foreach (var subordinateException in subordinateExceptions)
    {
        PrintSeparator();
        PrintException(subordinateException);
    }
}

static void PrintException(Exception exception)
{
    try
    {
        Console.WriteLine(exception);
    }
    catch { }
}

static void PrintSeparator()
{
    Console.WriteLine();
    Console.WriteLine(" --- ");
    Console.WriteLine();
}
