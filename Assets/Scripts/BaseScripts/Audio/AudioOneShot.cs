using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShot : MonoBehaviour
{

    protected AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        Invoke("DestroyAudio", audioSource.clip.length);
    }

    public void DestroyAudio()
    {
        Destroy(gameObject);
    }
    
}
