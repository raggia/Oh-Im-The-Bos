using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rush
{
    [System.Serializable]
    public partial class Level
    {
        [SerializeField]
        private int m_CurrentStarDone;
        [SerializeField]
        private bool m_Unlocked = false;
        [SerializeField]
        private LevelDefinition m_Definition;

        [SerializeField]
        private List<StaffDefinition> m_StaffDefinitions = new();

        public List<StaffDefinition> StaffDefinitions => m_StaffDefinitions;
        public int CurrentStarDone => m_CurrentStarDone;
        public LevelDefinition Definition => m_Definition;
        public bool Unlocked => m_Unlocked;

        public void Init()
        {
            m_CurrentStarDone = GameSingleton.Instance.LoadInt(m_Definition.name + "Star");
            m_Unlocked = GameSingleton.Instance.LoadBool(m_Definition.name + "Unlocked");
        }
        public Coroutine LoadLevel()
        {
            return m_Definition.LoadLevel();
        }
        public void AddStar(int add)
        {
            m_CurrentStarDone += add;
        }
        public void SetStar(int set)
        {
            m_CurrentStarDone = set;
        }
        public void SetUnlocked(bool set)
        {
            m_Unlocked = set;
            int i = m_Unlocked? 1 : 0;


            GameSingleton.Instance.SaveInt(m_Definition.name + "Unlocked", i);
        }
        public float GetKepuasan()
        {
            return m_Definition.Kepuasan;
        }
        public float GetDuration()
        {
            return m_Definition.Duration;
        }
    }
}
