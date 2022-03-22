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
    var configuration =
        new ConfigurationBuilder()
        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
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
    if (args.Length == 1)
    {
        runName = args.Single().Trim();
    }
    if (string.IsNullOrEmpty(runName))
    {
        throw new InvalidOperationException(Resources.RunNameExpected);
    }
    return runName;
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
