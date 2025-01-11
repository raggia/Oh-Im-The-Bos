using UnityEngine;

namespace Rush
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Boss/Staff")]
    public class StaffDefinition : ScriptableObject
    {
        [SerializeField]
        private Sprite m_IdleStateSprite;
        [SerializeField]
        private Sprite m_ActionStateSprite;

        [SerializeField]
        private float m_KepuasanPoint;

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
        public float KepuasanPoint => m_KepuasanPoint;
    }
}

