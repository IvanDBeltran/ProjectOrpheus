using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcesToPlay : MonoBehaviour
{
    [SerializeField] List<Sound> m_SoundsToPlayOnEnable;
    [SerializeField] List<Sound> m_SoundsToPlayOnDisable;
    [SerializeField] GameObject m_SFXPrefab;
    [SerializeField] AudioMixerGroup m_MixerGroup;

    [Header("Modifiers")]
    [SerializeField] bool InstatiateSfxPrefab = false;
    [SerializeField] bool PlaySoundOnDisable = false;

    private void OnEnable()
    {
        foreach(Sound s in m_SoundsToPlayOnEnable)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = m_MixerGroup;
            s.source.spatialBlend = 1;
            s.source.rolloffMode = AudioRolloffMode.Linear;
            s.source.maxDistance = 13;
            s.source.Play();
        }

        if(InstatiateSfxPrefab)
        {
            Instantiate(m_SFXPrefab, this.transform);
        }

        PlaySoundOnDisable = true;

    }

    private void OnDisable()
    {
        if(PlaySoundOnDisable && this.gameObject.CompareTag("PlayerAnchor"))
        {
            SoundManager.Instance.Play("Re-attachSoul");
            PlaySoundOnDisable = false;
        }
    }
}
