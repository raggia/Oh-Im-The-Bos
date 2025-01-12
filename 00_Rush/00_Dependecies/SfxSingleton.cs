using UnityEngine;

namespace Rush
{
    public class SfxSingleton : Singleton<SfxSingleton>
    {
        [SerializeField]
        private AudioSource m_Audio;
        public void PlaySound(AudioClip audioClip)
        {
            m_Audio.PlayOneShot(audioClip);
        }
    }
}

