using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7
{
    class ThrowExpressions
    {
        private string _name;


        // throw expression on property setter
        public string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value), "New name must not be null");
        }

        // throw expression on field initialization
        private string loadedConfig = LoadConfigResourceOrDefault() ?? throw new InvalidOperationException("Could not load config");

        private static string LoadConfigResourceOrDefault()
        {
            return null;
        }
    }
}
