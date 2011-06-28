using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChelasInjection;

namespace ChelasInjection
{
    public class TypeBinder<T> : ITypeBinder<T>, IConstructorBinder<T>, IActivationBinder<T>
    {
        private TypeConfig _tc;

        public TypeBinder(TypeConfig tc)
        {
            _tc = tc;
        }

        public ITypeBinder<T> WithNoArgumentsConstructor()
        {

            _tc.NoArgsCtor = true;
            return this;
        }

        public IConstructorBinder<T> WithConstructor(params Type[] constructorArguments)
        {
            _tc.NoArgsCtor = false;
            _tc.CArgs = constructorArguments;
            return this;
            
        }


        public ITypeBinder<T> WithValues(Func<object> values)
        {
            _tc.CVals = values;
            return this;
        }


        public IActivationBinder<T> WithActivation
        {
            get { return this; }
        }

        public ITypeBinder<T> PerRequest()
        {
            _tc.issingleton = false;
            return this;

        }

        public ITypeBinder<T> Singleton()
        {
            _tc.issingleton = true;
            return this;

        }

        public ITypeBinder<T> InitializeObjectWith(Action<T> initialization)
        {
            _tc.Initialize = o => initialization((T) o);
            return this;
        }


    }
}
