using System;

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