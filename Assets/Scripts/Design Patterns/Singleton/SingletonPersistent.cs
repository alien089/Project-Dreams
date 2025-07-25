namespace Framework.Generics.Pattern.SingletonPattern
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        public static T instance;
        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                if (!TryGetComponent<T>(out instance))
                {
                    instance = gameObject.AddComponent<T>();
                }
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(instance);
        }
    }
}
