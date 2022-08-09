using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShotRandom : MonoBehaviour
{
    [SerializeField] List<Sound> m_SoundsToPlay;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool m_SelfDestroy;
    [SerializeField] float m_SpatialBlend = 0;
    float pitch;
    float volume;
    int audioClipIndex;

    private void Awake()
    {
        audioClipIndex = Random.Range(0, m_SoundsToPlay.Count);
        pitch = Random.Range(m_SoundsToPlay[audioClipIndex].pitch - m_SoundsToPlay[audioClipIndex].pitchVariance,
            m_SoundsToPlay[audioClipIndex].pitch + m_SoundsToPlay[audioClipIndex].pitchVariance);
        volume = Random.Range(m_SoundsToPlay[audioClipIndex].volume - m_SoundsToPlay[audioClipIndex].volumeVariance,
            m_SoundsToPlay[audioClipIndex].volume + m_SoundsToPlay[audioClipIndex].volumeVariance);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.spatialBlend = m_SpatialBlend;
        audioSource.PlayOneShot(m_SoundsToPlay[audioClipIndex].clip);
    }

    private void Start()
    {
        if(m_SelfDestroy)
        Invoke("DestroyAudio", m_SoundsToPlay[audioClipIndex].clip.length);
    }

    private void DestroyAudio()
    {
        Destroy(this.gameObject);
    }
}
