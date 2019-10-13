using System;
using System.Reflection;
using Synnduit.Deployment;

namespace Synnduit
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                DeploymentExecutorFactory.CreateDeploymentExecutor().Deploy();
                RunnerFactory.CreateRunner(args[0], args[1]).Run();
                return 0;
            }
            catch(ReflectionTypeLoadException reflectionTypeLoadException)
            {
                PrintReflectionTypeLoadExceptionDetails(reflectionTypeLoadException);
                return 1;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return 1;
            }
        }

        private static void PrintReflectionTypeLoadExceptionDetails(
            ReflectionTypeLoadException exception)
        {
            Console.WriteLine(exception);
            foreach (Exception loaderException in exception.LoaderExceptions)
            {
                PrintSeparator();
                Console.WriteLine(loaderException);
            }
        }

        private static void PrintSeparator()
        {
            Console.WriteLine();
            Console.WriteLine(" --- ");
            Console.WriteLine();
        }
    }
}
