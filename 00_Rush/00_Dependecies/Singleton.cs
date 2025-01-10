using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rush
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        /// whether or not this singleton already has an instance 
		public static bool HasInstance => m_Instance != null;
        public static T Current => m_Instance;

        protected static T m_Instance;
        /// <summary>
        /// Singleton design pattern
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindAnyObjectByType<T>();
                    if (m_Instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        obj.name = typeof(T).Name + "_AutoCreated";
                        m_Instance = obj.AddComponent<T>();
                    }
                }
                return m_Instance;
            }
        }

        /// <summary>
        /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        /// <summary>
        /// Initializes the singleton.
        /// </summary>
        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            DontDestroyOnLoad(this.gameObject);
            // we check for existing objects of the same type
            T[] check = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T searched in check)
            {
                if (searched != this)
                {
                    Destroy(searched.gameObject);
                }
            }

            if (m_Instance == null)
            {
                m_Instance = this as T;
            }
        }
    }
}

