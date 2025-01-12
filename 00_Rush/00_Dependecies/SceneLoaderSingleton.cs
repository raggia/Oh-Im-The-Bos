using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rush
{
    public partial class SceneLoaderSingleton : Singleton<SceneLoaderSingleton>
    {
        [SerializeField]
        private CanvasView m_CanvasView;

        public void LoadScene(string sceneName)
        {
            CoroutineUtility.BeginCoroutine($"{GetInstanceID()}/{nameof(LoadingScene)}", LoadingScene(sceneName));
        }

        public Coroutine LoadSceneAsync(string sceneName)
        {
            return CoroutineUtility.BeginCoroutineReturn($"{GetInstanceID()}/{nameof(LoadingScene)}", LoadingScene(sceneName));
        }

        private IEnumerator LoadingScene(string sceneName)
        {
            m_CanvasView.Show();
            var load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            yield return load;
            load.allowSceneActivation = false;
            yield return new WaitUntil(() => load.isDone);
            load.allowSceneActivation = true;
            yield return new WaitForSeconds(1f);
            m_CanvasView.Hide();
        }
    }
}

