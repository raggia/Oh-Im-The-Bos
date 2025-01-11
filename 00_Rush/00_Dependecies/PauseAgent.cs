using UnityEngine;

namespace Rush
{
    public class PauseAgent : MonoBehaviour
    {
        public void Pause()
        {
            GameSingleton.Instance.Pause();
        }
        public void UnPause()
        {
            GameSingleton.Instance.UnPause();
        }
    }
}

