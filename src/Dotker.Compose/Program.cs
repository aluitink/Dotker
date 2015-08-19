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
