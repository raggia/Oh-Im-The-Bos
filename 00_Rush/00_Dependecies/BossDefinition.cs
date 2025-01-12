using System.Collections.Generic;
using UnityEngine;

namespace Rush
{
    [System.Serializable]
    public class BossIdle
    {
        public Sprite Sprite;
        public AudioClip Voice;

        public void PlayVoice()
        {
            SfxSingleton.Instance.PlaySound(Voice);
        }
    }
    [CreateAssetMenu(fileName = "New Boss", menuName = "Boss/Boss")]
    public class BossDefinition : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> m_ActionSprites = new();
        [SerializeField]
        private List<BossIdle> m_IdleSprites = new();
        public List<Sprite> ActionSprites => m_ActionSprites;
        public List<BossIdle> IdleSprites => m_IdleSprites;
        public Sprite GetActionSprite()
        {
            int r = Random.Range(0, m_ActionSprites.Count);
            return m_ActionSprites[r];
        }
        public Sprite GetIdleSprite(int index)
        {
            return m_IdleSprites[index].Sprite;
        }

        public void PlayVoice(int index)
        {
            m_IdleSprites[index].PlayVoice();
        }
    }
}

