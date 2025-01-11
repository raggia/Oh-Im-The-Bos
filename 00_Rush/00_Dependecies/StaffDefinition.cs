using UnityEngine;

namespace Rush
{
    public class StaffDefinition : MonoBehaviour
    {
        [SerializeField]
        private Sprite m_IdleStateSprite;
        [SerializeField]
        private Sprite m_ActionStateSprite;

        [Header("Delay Idle to Action")]
        [SerializeField]
        private float m_MinActionDelayEnter;
        [SerializeField]
        private float m_MaxActionDelayEnter;
        public Sprite IdleStateSprite => m_IdleStateSprite;
        public Sprite ActionStateSprite => m_ActionStateSprite;

        public float GetActionDelayEnter()
        {
            return Random.Range(m_MinActionDelayEnter, m_MaxActionDelayEnter);
        }
    }
}

