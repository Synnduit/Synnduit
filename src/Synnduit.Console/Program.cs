using System;
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
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                return 1;
            }
        }
    }
}
