using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rush
{
    public partial class GameplayPanel : PanelView
    {
        [SerializeField]
        private Boss m_Boss;

        [SerializeField]
        private TextMeshProUGUI m_LevelNameText;
        [SerializeField]
        private TextMeshProUGUI m_TimeCountText;
        [SerializeField]
        private Slider m_KepuasanSlider;

        [SerializeField, ReadOnly]
        private int m_Minutes = 0;
        [SerializeField, ReadOnly]
        private int m_Seconds = 0;

        protected override void Start()
        {
            base.Start();
            m_LevelNameText.text = GameSingleton.Instance.GetPlayedLevelName();
            m_KepuasanSlider.value = GameSingleton.Instance.GetKepuasanRate();
        }

        public void Action()
        {
            m_Boss.Action();
        }
        public void HandleActionCount(int count)
        {
            m_Boss.HandleActionCount(count);
        }
        public void SetTimeCounText(float time)
        {
            m_Minutes = Mathf.FloorToInt(time / 60);
            m_Seconds = Mathf.FloorToInt(time % 60);

            string duration = string.Format("{0:00}:{1:00}", m_Minutes, m_Seconds);
            m_TimeCountText.text = duration;
        }

        public void SetKepuasanSlider(float kepuasanRate)
        {
            m_KepuasanSlider.value = kepuasanRate;
        }
    }
}

