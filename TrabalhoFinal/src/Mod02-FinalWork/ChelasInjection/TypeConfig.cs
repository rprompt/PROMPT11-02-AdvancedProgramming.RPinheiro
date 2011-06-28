using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChelasInjection
{
    public class TypeConfig
    {
        public Type Type;
        public bool NoArgsCtor = false;
        public Type[] CArgs;
        public Func<object> CVals;
        public bool issingleton = false;
        public Action<Object> Initialize; 

    }
}
