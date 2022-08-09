using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	[Header("GUI Sounds")]
	[SerializeField] List<Sound> m_OnHoverSounds;
	[SerializeField] List<Sound> m_OnClickSounds;


	[Header("MC Sounds")]
	[SerializeField] List<Sound> m_McGrunts;
	[SerializeField] List<Sound> m_McDeath;

	public string currentMusicPlaying;

	void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;
			if(s.source.outputAudioMixerGroup == null)
            {
				s.source.outputAudioMixerGroup = mixerGroup;
			}			
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			print("Sound: " + name + " not found!");
			return;
		}

		s.source.Play();
	}

	public void PlaySound(string action)
    {
		switch(action)
        {
			case "MouseHover":
				//int mouseHoverIndex = UnityEngine.Random.Range(0, m_OnHoverSounds.Count);
				int mouseHoverIndex = 0;
				PlayShortSounds(m_OnHoverSounds[mouseHoverIndex].clip, m_OnHoverSounds[mouseHoverIndex].volume, m_OnHoverSounds[mouseHoverIndex].pitch);
				break;
			case "MouseClick":
				//int mouseClickIndex = UnityEngine.Random.Range(0, m_OnClickSounds.Count);
				int mouseClickIndex = 0;
				PlayShortSounds(m_OnClickSounds[mouseClickIndex].clip, m_OnClickSounds[mouseClickIndex].volume, m_OnClickSounds[mouseClickIndex].pitch);
				break;
			case "Jump":
				//int jumpIndex = UnityEngine.Random.Range(0, m_McGrunts.Count);
				int jumpIndex = 0;
				float volume = UnityEngine.Random.Range(m_McGrunts[jumpIndex].volume - m_McGrunts[jumpIndex].volumeVariance, m_McGrunts[jumpIndex].volume + m_McGrunts[jumpIndex].volumeVariance);
				float pitch = UnityEngine.Random.Range(m_McGrunts[jumpIndex].pitch - m_McGrunts[jumpIndex].pitchVariance, m_McGrunts[jumpIndex].pitch + m_McGrunts[jumpIndex].pitchVariance);
				PlayShortSounds(m_McGrunts[jumpIndex].clip, volume, pitch);
				break;
			case "Death":
                //int deathIndex = UnityEngine.Random.Range(0, m_McDeath.Count);
                //int deathIndex = 0;
				//float deathVolume = UnityEngine.Random.Range(m_McDeath[deathIndex].volume - m_McDeath[deathIndex].volumeVariance, m_McDeath[deathIndex].volume + m_McDeath[deathIndex].volumeVariance);
				//float deathPitch = UnityEngine.Random.Range(m_McDeath[deathIndex].pitch - m_McDeath[deathIndex].pitchVariance, m_McDeath[deathIndex].pitch + m_McDeath[deathIndex].pitchVariance);
				//PlayShortSounds(m_McDeath[deathIndex].clip, .2f, 1f);
				break;
		}
    }

	public void PlayShortSounds(AudioClip audioClip, float volume, float pitch)
	{
		try
		{
			AudioSource aS = gameObject.AddComponent<AudioSource>() as AudioSource;
			aS.pitch = pitch;
			aS.PlayOneShot(audioClip, volume);
			Destroy(aS, audioClip.length);
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	public void FadeAndPlayMusic(string musicName, float fadeTime)
    {
		FadeOutSound(currentMusicPlaying, fadeTime);
		StartCoroutine(PlayMusicAfterDelay(musicName, fadeTime));
    }

	public IEnumerator PlayMusicAfterDelay(string musicName, float delayTime)
    {
		yield return new WaitForSeconds(delayTime);
		PlayMusic(musicName);
    }

	public void PlayMusic(string musicName)
	{
		Sound s = Array.Find(sounds, item => item.name == musicName);
		if (s == null)
		{
			print("Music: " + name + " not found!");
			return;
		}
		currentMusicPlaying = musicName;
		s.source.Play();
	}

	public void FadeOutSound(string name, float fadeTime)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s != null)
		{
			StartCoroutine(FadeOut(s.source, fadeTime));
		}
	}

	public void FadeOutAll(float fadeTime)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
			StartCoroutine(FadeOut(sounds[i].source, fadeTime));
        }
    }

	public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
	{
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			float previousVolume = audioSource.volume;

			audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

			if (audioSource.volume != previousVolume)
			{
				yield return null;
			}
			else
			{
				audioSource.volume = 0;
			}
		}

		audioSource.Stop();
		audioSource.volume = startVolume;
    }

	public void StopFadeOut()
    {
		StopAllCoroutines();
		foreach (Sound s in sounds)
		{
			s.source.volume = s.volume;
		}
	}
}
