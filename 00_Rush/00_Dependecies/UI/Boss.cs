using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rush
{
    public enum BossState
    {
        Idle,
        Action,
    }
    [System.Serializable]
    public class Boss
    {
        [SerializeField]
        private BossDefinition m_Defi;
        [SerializeField]
        private Image m_BossImage;

        [SerializeField, ReadOnly]
        private BossState m_State = BossState.Idle;

        [SerializeField, ReadOnly]
        private int m_Count = 0;
        [SerializeField, ReadOnly]
        private bool m_Action = false;
        public void HandleActionCount(int actionCount)
        {
            m_Count = actionCount;
            
            if (m_Count < m_Defi.IdleSprites.Count)
            {
                SwitchState(BossState.Idle);
            }
        }

        private void SwitchState(BossState state)
        {
            m_State = state;
            switch(m_State)
            {
                case BossState.Idle:
                    m_BossImage.sprite = m_Defi.GetIdleSprite(m_Count);
                    m_Defi.PlayVoice(m_Count);
                    break;
                case BossState.Action:
                    m_BossImage.sprite = m_Defi.GetActionSprite();
                    break;
            }
        }
        public void Action()
        {
            CoroutineUtility.BeginCoroutine($"{m_Defi.GetInstanceID()}/{nameof(Actioning)}", Actioning());
        }
        private IEnumerator Actioning()
        {
            SwitchState(BossState.Action);
            yield return new WaitForSeconds(1f);
            GameSingleton.Instance.AddActionCount(-1);
        }
    }
}

