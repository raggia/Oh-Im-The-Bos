using UnityEngine;

namespace Rush
{
    public partial class GameSingleton : Singleton<GameSingleton>
    {
        [SerializeField]
        private string m_GameName;

        public string GameName => m_GameName;
    }
}

