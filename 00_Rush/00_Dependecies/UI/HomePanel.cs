using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rush
{
    public partial class HomePanel : PanelView
    {
        [SerializeField]
        private TextMeshProUGUI m_GameTitleText;
        [SerializeField]
        private Button m_PlayButton;
        [SerializeField]
        private Button m_CreditButton;
        [SerializeField]
        private Button m_QuitButton;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(StartPanel());
        }

        private IEnumerator StartPanel()
        {
            yield return new WaitUntil(() => CoroutineSingleton.Instance != null);
            ShowInternal();
        }
        protected override void ShowInternal(float overideDelay = 0)
        {
            base.ShowInternal(overideDelay);
            m_GameTitleText.text = GameSingleton.Instance.GameName;
        }
    }
}

