namespace Framework.Generics.Pattern.FactoryPattern 
{
    using UnityEngine;

    public static class Factory
    {
        public static T CreateTObject<T>(T obj) where T : Component
        {
            T rtn = obj;
            return rtn; 
        }
    }
}
