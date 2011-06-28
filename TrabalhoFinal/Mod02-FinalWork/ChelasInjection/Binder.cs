using System;
using System.Collections.Generic;


namespace ChelasInjection
{
    public delegate object ResolverHandler(Binder sender, Type t);

    public abstract class Binder
    {
        internal Dictionary<Type,TypeConfig> binds = new Dictionary<Type,TypeConfig>(); 
        
        public void Configure()
        {
            InternalConfigure();
        }

        protected abstract void InternalConfigure();


        public event ResolverHandler CustomResolver;




        /// <summary>
        /// Guarda no dicionário binds a key do MyBinder e o tipo a executar aí definido.
        /// </summary>
        /// <typeparam name="Source"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <returns></returns>
        
        public ITypeBinder<Target> Bind<Source, Target>()
        {
            //var xx = new xxxx;
            //binds.Add(typeof(Source), xx);

            TypeConfig tC = new TypeConfig {Type = typeof (Target)};


            //binds.Add(typeof(Source),tC) ;
            binds[typeof (Source)] = tC;
            //return xx

            return new TypeBinder<Target>(tC);
        }


        public ITypeBinder<Source> Bind<Source>()
        {
            return Bind<Source, Source>();
        }

    }
}