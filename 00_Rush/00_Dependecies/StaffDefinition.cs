using System.Collections.Generic;
using UnityEngine;

namespace Rush
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Boss/Staff")]
    public class StaffDefinition : ScriptableObject
    {
        [SerializeField]
        private Sprite m_IdleStateSprite;
        [SerializeField]
        private List<Sprite> m_ActionStateSprites;

        [SerializeField]
        private float m_KepuasanPoint;

        [Header("Delay Idle to Action")]
        [SerializeField]
        private float m_MinActionDelayEnter;
        [SerializeField]
        private float m_MaxActionDelayEnter;
        public Sprite IdleStateSprite => m_IdleStateSprite;
        public Sprite GetActionStateSprite()
        {
            int r = Random.Range(0, m_ActionStateSprites.Count);
            return m_ActionStateSprites[r];
        }

        public float GetActionDelayEnter()
        {
            return Random.Range(m_MinActionDelayEnter, m_MaxActionDelayEnter);
        }
        public float KepuasanPoint => m_KepuasanPoint;
    }
}

