namespace Framework.Generics.Pattern.SingletonPattern
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance;
        // Start is called before the first frame update
        void Awake()
        {
            if (Instance == null)
            {
                if (!TryGetComponent<T>(out Instance))
                {
                    Instance = gameObject.AddComponent<T>();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}


