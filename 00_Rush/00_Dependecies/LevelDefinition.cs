using UnityEngine;

namespace Rush
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Boss/Level")]
    public partial class LevelDefinition : ScriptableObject
    {
        [SerializeField]
        private string m_SceneName;
        [SerializeField]
        private int m_Kepuasan;
        [SerializeField]
        private float m_Duration;

        public int Kepuasan => m_Kepuasan;
        public float Duration => m_Duration;

        public Coroutine LoadLevel()
        {
            return SceneLoaderSingleton.Instance.LoadSceneAsync(m_SceneName);

        }
    }
}

