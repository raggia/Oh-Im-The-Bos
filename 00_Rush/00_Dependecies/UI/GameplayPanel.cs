using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rush
{
    public partial class GameplayPanel : PanelView
    {
        [SerializeField]
        private TextMeshProUGUI m_LevelNameText;
        [SerializeField]
        private TextMeshProUGUI m_TimeCountText;

        [SerializeField, ReadOnly]
        private int m_Minutes = 0;
        [SerializeField, ReadOnly]
        private int m_Seconds = 0;

        protected override void Start()
        {
            base.Start();
            m_LevelNameText.text = GameSingleton.Instance.GetPlayedLevelName();
        }
        public void SetTimeCounText(float time)
        {
            m_Minutes = Mathf.FloorToInt(time / 60);
            m_Seconds = Mathf.FloorToInt(time % 60);

            m_TimeCountText.text = $"{m_Minutes}:{m_Seconds}";
        }
    }
}

