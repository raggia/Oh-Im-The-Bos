using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rush
{
    public partial class ReadyPanel : PanelView
    {
        [SerializeField]
        private Button m_ReadyButton;
        [SerializeField]
        private LevelDefinition m_FirstLevel;
        private void Awake()
        {
            UnityAction action = new UnityAction(() => GameSingleton.Instance.SetReady(true));
            m_ReadyButton.onClick.AddListener(action);
        }
        private void OnDestroy()
        {
            UnityAction action = new UnityAction(() => GameSingleton.Instance.SetReady(true));
            m_ReadyButton.onClick.RemoveListener(action);
        }

        protected override void Start()
        {
            base.Start();
            ShowInternal();
            if (GameSingleton.Instance.PlayedLevel.Definition == m_FirstLevel)
            {
                //ShowInternal();
            }
            else
            {
                //HideInternal();
            }
        }
    }
}

