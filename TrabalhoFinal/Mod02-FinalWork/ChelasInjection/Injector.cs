using System;
using System.Collections.Generic;
using System.Reflection;
using ChelasInjection.Exceptions;
using System.Linq;

namespace ChelasInjection
{
    public class Injector
    {
        private Binder _myBinder;
        
        Dictionary<Type, Object> singletons = new Dictionary<Type, Object>();

        public Injector(Binder myBinder)
        {
            _myBinder = myBinder;
            _myBinder.Configure();
        }

        public T GetInstance<T>()
        {
            Dictionary<Type,Object> resolved = new Dictionary<Type, Object>();
            HashSet<Type> calltypes = new HashSet<Type>(); 
            return (T) GetInstance(typeof (T), calltypes, resolved);
        }

        public Object GetInstance(Type t, HashSet<Type> calltypes, Dictionary<Type,Object> resolved)
        {

            if (singletons.ContainsKey(t))
                return singletons[t];

            if (resolved.ContainsKey(t))
                return resolved[t];

            //verifica se estamos perante uma chamada circular => so chama uma vez cada tipo);

            if (calltypes.Contains(t))
                throw new CircularDependencyException();

            
            //calltypes.Add(t);            
            

            //retorna se existe o parametro associado ao tipo - ex: ISomeInterface7 ... SomeInterface7Default>();
            //
            TypeConfig bt;
            bool found = _myBinder.binds.TryGetValue(t, out bt);
            
            //se encontra 
            if (!found)
            {
                if (t.IsAbstract)
                {
                    throw new ChelasInjectionException(string.Format("Tipo {0} abstracto. Não existe constructor",
                                                                     t.FullName));
                }
                else
                {
                   _myBinder.binds[t] = new  TypeConfig{ Type = t};
                  found = _myBinder.binds.TryGetValue(t, out bt);
                }
            }


            if (found)
            {
                Console.WriteLine(bt.Type.FullName);


                //Se existir contructor sem parametros vamos devolver uma instancia criada com esse constructor)
                if (bt.NoArgsCtor)
                {
                    ConstructorInfo cix = bt.Type.GetConstructor(new Type[0]);
                    if (cix != null)
                    {
                        var res = cix.Invoke(new object[0]);

                        if (!resolved.ContainsKey(t))
                            resolved.Add(t,res);

                        if (bt.issingleton)
                        {
                            if (!singletons.ContainsKey(t))
                                singletons.Add(t, res);
                        }
                        return res;
                    }
                }


                //Se existir contructor com argumentos vamos devolver uma instancia criada com esse constructor)
                if (bt.CArgs != null)
                {
                    ConstructorInfo cix = bt.Type.GetConstructor(bt.CArgs);
                    if (cix != null)
                    {
                        object[] args = new object[bt.CArgs.Length];
                        object vals = null;
                        
                        if (bt.CVals != null)
                        {
                            vals = bt.CVals();
                        }


                        for (int i = 0; i < args.Length; ++i)
                        {
                            if (_myBinder.binds.ContainsKey(bt.CArgs[i]))
                            {
                                calltypes.Add(t);            
                                args[i] = GetInstance(bt.CArgs[i], calltypes,resolved);
                                calltypes.Remove(t);            
                            }
                            else
                            {
                             //obtem o valor do argumento cujo parametro é igual ao do constructorinfo
                             args[i] = vals.GetType().GetProperty(cix.GetParameters()[i].Name).GetValue(vals,null);
                            }
                        }

                        var res = cix.Invoke(args);

                        if (!resolved.ContainsKey(t))
                            resolved.Add(t, res);

                        if (bt.issingleton)
                        {
                          if (!singletons.ContainsKey(t))
                              singletons.Add(t, res);
                        }
                        return res;
                    }
                }



                //Outros contructores
                ConstructorInfo[] cia = bt.Type.GetConstructors();
                
                // verifica se estamos na presença de mais do que um constructor
                if (cia.Where(ci => ci.IsDefined(typeof(DefaultConstructorAttribute),false)).Count() > 1)
                {                    
                   throw new MultipleDefaultConstructorAttributesException("Foram encontrados múltiplos constructores")
                    ;
                }

                //Constructor com Default
                ConstructorInfo cDefault =
                    cia.SingleOrDefault(d => d.IsDefined(typeof (DefaultConstructorAttribute), false));
                
                if (cDefault != null)
                {
                    return cDefault;
                }
                
                
                //ordena os constructores por aqueles que têm maior numero de parametros

                Array.Sort<ConstructorInfo>(cia, (ci1, ci2) =>
                      ci2.GetParameters().Length - ci1.GetParameters().Length);
                

                
                //
                // Quero usar o constructor com o maior numero de parametros utilizáveis

                foreach (ConstructorInfo ci in cia)
                {
                    
                    ParameterInfo[] pia = ci.GetParameters();

                    bool cgood = true;
                    foreach (var pi in pia)
                    {
                        if (!_myBinder.binds.ContainsKey(pi.ParameterType))
                        {
                            cgood = false;
                            break;                            
                        }
                    }

                    if (cgood)
                    {                        
                        object[] args = new object[pia.Length];
                
                        for (int i = 0; i < args.Length; ++i )
                        {
                            calltypes.Add(t);            
                            args[i] = GetInstance(pia[i].ParameterType, calltypes,resolved);
                            calltypes.Remove(t);            
                        }
                
                        var res = ci.Invoke(args);
                        
                        if (bt.Initialize != null)
                        {
                            bt.Initialize(res);
                        }

                        if (!resolved.ContainsKey(t))
                            resolved.Add(t, res);
                        
                        if (bt.issingleton)
                        {
                            if (!singletons.ContainsKey(t))
                                singletons.Add(t,res);                            
                        }
                        return res;
                    }
                }
           
            }
            throw new ChelasInjectionException(string.Format("Bind não encontrado para o Type {0} ", t.FullName));
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