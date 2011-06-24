using System;
using System.Collections.Generic;

namespace ChelasInjection
{
    public delegate object ResolverHandler(Binder sender, Type t);

    public abstract class Binder
    {
        internal Dictionary<Type,Type> binds = new Dictionary<Type,Type>(); 
        
        public void Configure()
        {
            InternalConfigure();
        }

        protected abstract void InternalConfigure();


        public event ResolverHandler CustomResolver;

        public ITypeBinder<Target> Bind<Source, Target>()
        {
            //var xx = new xxxx;
            //binds.Add(typeof(Source), xx);
            binds.Add(typeof(Source),typeof(Target)) ;
            //return xx
            return null;
        }


        public ITypeBinder<Source> Bind<Source>()
        {
            return Bind<Source, Source>();
        }

    }
}