using UnityEngine;

namespace Rush
{
    public class LoadSceneAgent : MonoBehaviour
    {
        [SerializeField]
        private string m_SceneName;

        public void LoadScene()
        {
            SceneLoaderSingleton.Instance.LoadScene(m_SceneName);
        }
    }
}

