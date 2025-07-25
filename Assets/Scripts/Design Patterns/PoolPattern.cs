namespace Framework.Generics.Pattern.PoolPattern
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolPattern: MonoBehaviour
    {
        private List<GameObject> m_ObjectAvailable = new List<GameObject>();
        private List<GameObject> m_ObjectInUse = new List<GameObject>();
        [SerializeField] private int m_InitialAmount;
        [SerializeField] private GameObject m_ObjectToPool;
        
        private void Start()
        {
            InitialGeneration(m_InitialAmount);
        }

        /// <summary>
        /// Create an amount of Objects
        /// </summary>
        /// <param name="amount"></param>
        public virtual void InitialGeneration(int amount)
        {
            for (int i = 0; i <= amount; i++)
            {
                m_ObjectAvailable.Add(Instantiate(m_ObjectToPool, gameObject.transform));
                m_ObjectInUse[i].SetActive(false);
            }
        }

        /// <summary>
        /// Get a Object from the availables
        /// </summary>
        /// <returns></returns>
        public GameObject GetObject()
        {
            if (m_ObjectAvailable.Count != 0)
            {
                GameObject go = m_ObjectAvailable[0];
                m_ObjectInUse.Add(go);
                m_ObjectAvailable.RemoveAt(0);
                go.SetActive(true);
                return go;
            }
            else
            {
                GameObject go = Instantiate(m_ObjectToPool, gameObject.transform);
                m_ObjectInUse.Add(go);
                return go;
            }
        }

        /// <summary>
        /// Release the Object out of scene
        /// </summary>
        /// <param name="obj"></param>
        public void ReleaseObject(GameObject obj)
        {
            CleanUp(obj);
            m_ObjectAvailable.Add(obj);
            m_ObjectInUse.Remove(obj);
            obj.SetActive(false);
        }


        /// <summary>
        /// Reset to default values the bobble
        /// </summary>
        /// <param name="obj"></param>
        public virtual void CleanUp(GameObject obj)
        {

        }

        /// <summary>
        /// getter method for "bobble is use" list
        /// </summary>
        /// <returns></returns>
        public int GetIsUseCount()
        {
            return m_ObjectInUse.Count;
        }
    }

}

