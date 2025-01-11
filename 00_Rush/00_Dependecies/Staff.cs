using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rush
{
    public enum StaffState
    {
        Idle = 0,
        Action = 1,
    }
    public class Staff : ButtonUIView
    {
        [SerializeField]
        private StaffState m_State = StaffState.Idle;
        [SerializeField, ReadOnly]
        private StaffDefinition m_Defi;
        [SerializeField]
        private Image m_CharImage;
        [SerializeField, ReadOnly]
        private float m_ActionDelayEnter;
        [SerializeField, ReadOnly]
        private float m_MaxActionDelayEnter;
        [SerializeField]
        private UnityEvent<Staff> m_OnIdleStateEnter = new();
        [SerializeField]
        private UnityEvent<Staff> m_OnActionStateEnter = new();
        private void Update()
        {
            HandleActionDelay();
        }
        private void HandleActionDelay()
        {
            if (!GameSingleton.Instance.Ready) return;
            if (GameSingleton.Instance.LevelOver) return;

            if (m_State == StaffState.Idle)
            {
                m_ActionDelayEnter -= Time.deltaTime;
                if (m_ActionDelayEnter <= 0f)
                {
                    SwitchState(StaffState.Action);
                    m_ActionDelayEnter = 0;
                }
            }
        }
        public void Init(StaffDefinition defi)
        {
            m_Defi = defi;
            SwitchState(StaffState.Idle);
            SetMaxActionDelay(m_Defi.GetActionDelayEnter());
            
        }
        private void SetMaxActionDelay(float set)
        {
            m_MaxActionDelayEnter = set;
        }
        private void SwitchState(StaffState state)
        {
            m_State = state;
            switch (m_State)
            {
                case StaffState.Idle:
                    OnSwitchIdle();
                    break;
                case StaffState.Action:
                    OnSwitchAction();
                    break;
            }
        }

        private void OnSwitchIdle()
        {
            m_CharImage.sprite = m_Defi.IdleStateSprite;
            SetMaxActionDelay(m_Defi.GetActionDelayEnter());
            m_ActionDelayEnter = m_MaxActionDelayEnter;
            SetInteractButtonInternal(false);
            m_OnIdleStateEnter?.Invoke(this);
        }
        private void OnSwitchAction()
        {
            m_CharImage.sprite = m_Defi.ActionStateSprite;
            SetInteractButtonInternal(true);
            m_OnActionStateEnter?.Invoke(this);
        }

        public void ApplyKepuasan()
        {
            GameSingleton.Instance.AddCurrentKepuasan(m_Defi.KepuasanPoint);
        }
        
    }
}

