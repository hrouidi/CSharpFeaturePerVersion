using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7.Feature
{
    class MoreExpressionBodyMembers
    {
        //Constructors
        public MoreExpressionBodyMembers(string label) => Label = label;

        // Expression-bodied finalizer
        ~MoreExpressionBodyMembers() => Console.Error.WriteLine("Finalized!");

        private string label;

        // Expression-bodied get / set accessors.
        public string Label
        {
            get => label;
            set => this.label = value ?? "Default label";
        }

        // Expression-bodied get / set indexer.
        public string this[int index]
        {
            get => $"{index}: {label}";
            set => label = value ?? "Default label";
        }
    }
}
