using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rush
{
    public partial class LevelView : ButtonUIView
    {
        [SerializeField]
        private Level m_Level;
        [SerializeField]
        private List<Image> m_Stars = new();
        public LevelDefinition LevelDefinition => m_Level.Definition;

        private void Awake()
        {
            AddAction(Play);
        }
        public void Init(Level level)
        {
            m_Level = level;
            SetInteractButtonInternal(m_Level.Unlocked);
            foreach (var item in m_Stars)
            {
                m_ButtonText.text = m_Level.Definition.name;
                item.gameObject.SetActive(false);
            }

            if (m_Level.CurrentStarDone < 1) return;

            for (int i = 0; i < m_Level.CurrentStarDone; i++)
            {
                m_Stars[i].gameObject.SetActive(true);
            }
        }

        public void Play()
        {
            GameSingleton.Instance.PlayLevel(m_Level.Definition);
        }
    }
}

