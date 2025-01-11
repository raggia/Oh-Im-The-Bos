using UnityEngine;

namespace Rush
{
    public class QuitGameAgent : MonoBehaviour
    {
        public void QuitGame()
        {
            GameSingleton.Instance.QuitGame();
        }
    }
}

