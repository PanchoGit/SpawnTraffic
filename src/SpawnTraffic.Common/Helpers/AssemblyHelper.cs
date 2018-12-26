using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace SpawnTraffic.Common.Helpers
{
    public class AssemblyHelper
    {
        public static Assembly[] GetReferencingAssemblies(string assemblyName)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (!IsCandidateLibrary(library, assemblyName)) continue;
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
            return assemblies.ToArray();
        }

        private static bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == assemblyName
                   || library.Name.StartsWith(assemblyName)
                   || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }
    }
}
