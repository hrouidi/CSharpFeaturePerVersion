using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp6.Feature
{
    class AutoPropertyInitializer
    {
        //Mutable property
        public string Adress { get; set; } = "SDF";


        //inline Read only property
        public Guid Id { get; } = Guid.NewGuid();

        //Constructor Read only property
        public Guid InternalId { get; }


        public AutoPropertyInitializer()
        {
            InternalId = Guid.NewGuid();
        }
    }
}
