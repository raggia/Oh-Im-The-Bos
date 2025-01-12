using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Rush
{
    public partial class GameSingleton : Singleton<GameSingleton>
    {
        [SerializeField]
        private string m_GameName;
        [SerializeField, ReadOnly]
        private Level m_PlayedLevel;

        [SerializeField, ReadOnly]
        private int m_StaffActionCount;
        [SerializeField, ReadOnly]
        private float m_CurrentKepuasan;
        [SerializeField, ReadOnly]
        private float m_CurrentDuration;
        [SerializeField, ReadOnly]
        private bool m_Ready = false;
        [SerializeField, ReadOnly]
        private bool m_LevelOver = false;
        [SerializeField]
        private int m_UnlockedLevel = 1;
        [SerializeField]
        private List<Level> m_Levels = new();

        [SerializeField]
        private UnityEvent<List<Level>> m_OnRefresh = new();
        [SerializeField]
        private UnityEvent<Level> m_OnLevelPlay = new();
        [SerializeField]
        private UnityEvent<Level> m_OnStarChanged = new();
        [SerializeField]
        private UnityEvent<float> m_OnMaxKepuasanChanged = new();
        [SerializeField]
        private UnityEvent<float> m_OnKepuasanChanged = new();
        [SerializeField]
        private UnityEvent<float> m_OnKepuasanRateChanged = new();
        [SerializeField]
        private UnityEvent<float> m_OnCurrentDurationChanged = new();
        [SerializeField]
        private UnityEvent<Level> m_OnLevelOverWin = new();
        [SerializeField]
        private UnityEvent<Level> m_OnLevelOverLose = new();
        [SerializeField]
        private UnityEvent<Level> m_OnReadyChanged = new();
        [SerializeField]
        private UnityEvent<int> m_OnActionCountChanged = new();
        public string GameName => m_GameName;

        public List<Level> Levels => m_Levels;
        private void Start()
        {
            Refresh();
            foreach (Level level in Levels)
            {
                level.Init();
            }
        }
        public void AddActionCount(int add)
        {
            m_StaffActionCount = add;
            m_StaffActionCount = Mathf.Clamp(m_StaffActionCount, 0, int.MaxValue);
            m_OnActionCountChanged?.Invoke(m_StaffActionCount);
        }
        private void Update()
        {
            HandlePlayingDuration();
        }

        private void HandlePlayingDuration()
        {
            if(!m_Ready) return;
            if (!m_LevelOver)
            {
                m_CurrentDuration -= Time.deltaTime;
                m_CurrentDuration = Mathf.Clamp(m_CurrentDuration, 0, GetDuration(m_PlayedLevel.Definition));
                m_OnCurrentDurationChanged?.Invoke(m_CurrentDuration);
                DetermineLose();
            }
        }

        private int StarUpdate()
        {
            int star = 0;
            if (GetKepuasanRateInternal() >= 1f)
            {
                star = 3;
                return star;
            }
            if (GetKepuasanRateInternal() > 0.66f)
            {
                star = 2;
                return star;
            }
            if (GetKepuasanRateInternal() > 0.33f)
            {
                star = 1;
                return star;
            }
            if (GetKepuasanRateInternal() < 0.33f)
            {
                star = 0;
                return star;
            }
            return star;
        }

        private IEnumerator PlayingLevel(LevelDefinition defi)
        {
            m_PlayedLevel = GetLevel(defi);

            RestartLevelInternal();
            yield return m_PlayedLevel.LoadLevel();
            m_OnLevelPlay?.Invoke(m_PlayedLevel);
        }
        public void RestartLevel()
        {
            RestartLevelInternal();
        }

        private void RestartLevelInternal()
        {
            m_Ready = false;
            m_LevelOver = false;
            SetCurrentKepuasanInternal(0);
            m_OnMaxKepuasanChanged?.Invoke(GetKepuasan(m_PlayedLevel.Definition));
            m_CurrentDuration = GetDuration(m_PlayedLevel.Definition);
        }
        private void SetCurrentKepuasanInternal(float set)
        {
            m_CurrentKepuasan = set;
            
            m_CurrentKepuasan = Mathf.Clamp(m_CurrentKepuasan, 0, GetKepuasan(m_PlayedLevel.Definition));
            float rate = GetKepuasanRateInternal();
            m_OnKepuasanChanged?.Invoke(m_CurrentKepuasan);
            m_OnKepuasanRateChanged?.Invoke(rate);
            DetermineWin();
        }
        public float GetKepuasanRate()
        {
            return GetKepuasanRateInternal();
        }
        private float GetKepuasanRateInternal()
        {
            float a = m_CurrentKepuasan;
            float b = GetKepuasan(m_PlayedLevel.Definition);
            float rate = a / b;
            return rate;
        }
        private void AddCurrentKepuasanInternal(float add)
        {
            m_CurrentKepuasan += add;
            m_CurrentKepuasan = Mathf.Clamp(m_CurrentKepuasan, 0, GetKepuasan(m_PlayedLevel.Definition));
            float rate = GetKepuasanRateInternal();
            m_OnKepuasanChanged?.Invoke(m_CurrentKepuasan);
            m_OnKepuasanRateChanged?.Invoke(rate);
            DetermineWin();
        }
        private float GetKepuasan(LevelDefinition defi)
        {
            return GetLevel(defi).GetKepuasan();
        }
        private float GetDuration(LevelDefinition defi)
        {
            return GetLevel(defi).GetDuration();
        }
        private void DetermineWin()
        {
            if (m_CurrentKepuasan >= GetKepuasan(m_PlayedLevel.Definition))
            {
                m_LevelOver = true;
                m_OnLevelOverWin?.Invoke(m_PlayedLevel);
                //SetUnlockedLevel(m_UnlockedLevel + 1);
                if (m_PlayedLevel.CurrentStarDone > StarUpdate()) return;
                SetStar(m_PlayedLevel.Definition, StarUpdate());
            }
        }
        private void DetermineLose()
        {
            if (m_CurrentDuration <= 0.1f)
            {
                m_LevelOver = true;
                m_OnLevelOverLose?.Invoke(m_PlayedLevel);
                if (m_PlayedLevel.CurrentStarDone > StarUpdate()) return;
                SetStar(m_PlayedLevel.Definition, StarUpdate());
            }
        }

        public void AddOnRefrech(UnityAction<List<Level>> action)
        {
            m_OnRefresh?.AddListener(action);
        }
        public void RemoveOnRefresh(UnityAction<List<Level>> action)
        {
            m_OnRefresh?.RemoveListener(action);
        }
        public void SetUnlockedLevel(int set)
        {
            m_UnlockedLevel = set;
            m_UnlockedLevel = Mathf.Clamp(m_UnlockedLevel, 1, m_Levels.Count + 1);
            if (m_UnlockedLevel < m_Levels.Count)
            {
                GetLevel(m_Levels[m_UnlockedLevel].Definition).SetUnlocked(true);
            }
            RefreshInternal();
        }
        public void AddUnlockedLevel(int add)
        {
            m_UnlockedLevel += add;
            m_UnlockedLevel = Mathf.Clamp(m_UnlockedLevel, 1, m_Levels.Count + 1);
            if (m_UnlockedLevel < m_Levels.Count)
            {
                GetLevel(m_Levels[m_UnlockedLevel].Definition).SetUnlocked(true);
            }
            RefreshInternal();
        }
        public void AddCurrentKepuasan(float add)
        {
            AddCurrentKepuasanInternal(add);
        }
        public void SetCurrentKepuasan(float set)
        {
            SetCurrentKepuasanInternal(set);
        }
        private void RefreshInternal()
        {
            if (m_UnlockedLevel > m_Levels.Count) return;
            for (int i = 0; i < m_UnlockedLevel; i++)
            {
                m_Levels[i].SetUnlocked(true);
            }
            m_OnRefresh?.Invoke(m_Levels);
        }
        public void SetUnlocked(LevelDefinition defi, bool set)
        {
            m_PlayedLevel = GetLevel(defi);
            m_PlayedLevel.SetUnlocked(set);
            GetLevel(defi).SetUnlocked(set);
            m_OnReadyChanged?.Invoke(m_PlayedLevel);
        }
        public void Refresh()
        {
            RefreshInternal();
        }
        public void QuitGame()
        {
            Application.Quit();
        }

        private Level GetLevel(LevelDefinition defi)
        {
            Level match = m_Levels.Find(x => x.Definition == defi);
            return match;
        }

        private void AddStar(LevelDefinition defi, int add)
        {
            m_PlayedLevel = GetLevel(defi);
            m_PlayedLevel.AddStar(add);
            GetLevel(defi).AddStar(add);
            m_OnStarChanged?.Invoke(m_PlayedLevel);

            SaveIntInternal(defi.name + "Star", GetLevel(defi).CurrentStarDone);
        }

        private void SetStar(LevelDefinition defi, int set)
        {
            m_PlayedLevel = GetLevel(defi);
            m_PlayedLevel.SetStar(set);
            GetLevel(defi).SetStar(set);
            m_OnStarChanged?.Invoke(m_PlayedLevel);

            SaveIntInternal(defi.name + "Star", GetLevel(defi).CurrentStarDone);
        }

        public void PlayLevel(LevelDefinition defi)
        {
            CoroutineSingleton.Instance.BeginCoroutine($"{GetInstanceID()}/{nameof(PlayingLevel)}", PlayingLevel(defi));
        }
        public void PlayLevel()
        {
            int index = m_UnlockedLevel - 1;
            if (m_UnlockedLevel > m_Levels.Count)
            {
                SceneLoaderSingleton.Instance.LoadScene("Main Menu");
                //return;
            }
            else
            {
                LevelDefinition defi = m_Levels[index].Definition;
                CoroutineSingleton.Instance.BeginCoroutine($"{GetInstanceID()}/{nameof(PlayingLevel)}", PlayingLevel(defi));
            }
            
        }

        public void SetReady(bool set)
        {
            m_Ready = set;
        }

        public string GetPlayedLevelName()
        {
            return m_PlayedLevel.Definition.name;
        }

        public Level PlayedLevel => m_PlayedLevel;
        public bool Ready => m_Ready;
        public bool LevelOver => m_LevelOver;
        public List<StaffDefinition> GetStaffDefinitions()
        {
            return GetLevel(m_PlayedLevel.Definition).StaffDefinitions;
        }
        public void Pause()
        {
            Time.timeScale = 0f;
        }
        public void UnPause()
        {
            Time.timeScale = 1f;
        }


        private void SaveIntInternal(string key, int val)
        {
            PlayerPrefs.SetInt(key, val);
            PlayerPrefs.Save();
        }
        public void SaveInt(string key, int val)
        {
            SaveIntInternal(key, val);
        }
        private int LoadIntInternal(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public int LoadInt(string key)
        {
            return LoadIntInternal(key);
        }

        private bool LoadBoolInternal(string key)
        {
            int val = PlayerPrefs.GetInt(key);
            bool match = false;
            if (val == 0)
            {
                match = false;
            }
            if (val == 1)
            {
                match = true;
            }
            return match;
        }
        public bool LoadBool(string key)
        {
            return LoadBoolInternal(key);
        }
    }
}

