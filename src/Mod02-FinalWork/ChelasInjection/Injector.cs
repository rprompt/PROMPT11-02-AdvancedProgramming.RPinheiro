using System;
using System.Collections.Generic;
using System.Reflection;
using ChelasInjection.Exceptions;

namespace ChelasInjection
{
    public class Injector
    {
        private Binder _myBinder;

        public Injector(Binder myBinder)
        {
            _myBinder = myBinder;
            _myBinder.Configure();
        }

        public T GetInstance<T>()
        {
            HashSet<Type> calltypes = new HashSet<Type>(); 
            return (T) GetInstance(typeof (T), calltypes);
        }

        public Object GetInstance(Type t, HashSet<Type> calltypes)
        {

            //verifica se estamos perante uma chamada circular => so chama uma vez cada tipo

            if (calltypes.Contains(t))
                throw new CircularDependencyException();

            calltypes.Add(t);            
            

            //retorna se existe o parametro associado ao ISomeInterface7 ... SomeInterface7Default>();
            //
            Type bt;
            bool found = _myBinder.binds.TryGetValue(t, out bt);
            
            //se encontra 
            if (found)
            {
                Console.WriteLine(bt.FullName);

                //Vamos buscar a informação do constructor sem parametros [0] do SomeInterface7Default>()
                //ConstructorInfo ci = t.GetConstructor(new Type[0]);

                //Se existir contructor sem parametros vamos devolver uma instancia criada com esse constructor)
                //if (ci != null)
                //{
                //    return (T)ci.Invoke(new object[0]);
                //}

                
                ConstructorInfo[] cia = bt.GetConstructors();
                
                
                //ordena os constructores por aqueles que têm maior numero de parametros

                Array.Sort<ConstructorInfo>(cia, (ci1, ci2) =>
                      ci2.GetParameters().Length - ci1.GetParameters().Length);
                
                
                // Quero usar o constructor com o maior numero de parametros utilizáveis

                foreach (ConstructorInfo ci in cia)
                {
                    
                    ParameterInfo[] pia = ci.GetParameters();

                    bool cgood = true;
                    foreach (var pi in pia)
                    {
                        if (!_myBinder.binds.ContainsKey(pi.ParameterType))
                            cgood = false;
                            break
                        ;
                    }

                    if (cgood)
                    {                        
                        object[] args = new object[pia.Length];
                
                        for (int i = 0; i < args.Length; ++i )
                        {
                            args[i] = GetInstance(pia[i].ParameterType, calltypes);
                        }
                
                        return ci.Invoke(args);
                    }
                }

            }
            throw new NotImplementedException();
        }

        public T GetInstance<T, TA>() where TA : Attribute
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>(string name) 
        {
            throw new NotImplementedException();
        }
    }
}