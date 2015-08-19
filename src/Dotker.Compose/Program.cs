using Docker.DotNet;
using Microsoft.Framework.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dotker.Compose
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnv;

        public Program(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }

        public void Main(string[] args)
        {
            AnonymousCredentials creds = new AnonymousCredentials();

            DockerClient client = new DockerClientConfiguration(new Uri("http://127.0.0.1:4242")).CreateClient();

            var containers = client.Containers.ListContainersAsync(new Docker.DotNet.Models.ListContainersParameters() { All = true }).Result;

            foreach (var item in containers)
            {
                Console.WriteLine("Id: {0}", item.Id);
                Console.WriteLine("Names: {0}", string.Join(", ", item.Names));
                Console.WriteLine("Command: {0}", item.Command);
                Console.WriteLine("Created: {0}", item.Created);
                Console.WriteLine("Image: {0}", item.Image);
                Console.WriteLine("SizeRootFs: {0}", item.SizeRootFs);
                Console.WriteLine("SizeRw: {0}", item.SizeRw);
                Console.WriteLine("Status: {0}", item.Status);
            }

        }

        public void PrintEnvironmentInformation(string[] args)
        {
            Console.WriteLine("AppEnv");
            Console.WriteLine("\tApplicationName: {0}", _appEnv.ApplicationName);
            Console.WriteLine("\tVersion: {0}", _appEnv.Version);
            Console.WriteLine("\tApplicationBasePath: {0}", _appEnv.ApplicationBasePath);
            Console.WriteLine("\tRuntimeFramework: {0}", _appEnv.RuntimeFramework);
            Console.WriteLine("\tConfiguration: {0}", _appEnv.Configuration);

            Console.WriteLine();
            Console.WriteLine("Arguments:");
            foreach (var item in args)
            {
                Console.WriteLine("\t{0}", item);
            }
            Console.WriteLine();
            Console.WriteLine("Environment Properties:");
            var properties = typeof(Environment)
                .GetProperties(BindingFlags.Public | BindingFlags.Static);

            foreach (var prop in properties)
                Console.WriteLine("\t{0} : {1}", prop.Name, prop.GetValue(null));


            DirectoryInfo cd = new DirectoryInfo(_appEnv.ApplicationBasePath);

            var dirs = cd.GetDirectories();
            var files = cd.GetFiles();

            Console.WriteLine("CurrentDirectory:");
            Console.WriteLine("\tDirectories:");
            foreach (var item in dirs)
            {
                Console.WriteLine("\t\t{0}", item.FullName);
            }
            Console.WriteLine("\tFiles:");
            foreach (var item in files)
            {
                Console.WriteLine("\t\t{0}", item.FullName);
            }
        }
    }
}
