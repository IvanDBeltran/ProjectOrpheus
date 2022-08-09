using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayOneShotFmod : MonoBehaviour
{ 
    [SerializeField] OneShotActions m_ActionOnAwake = new OneShotActions();

    private void Awake()
    {

        PlaySoundOnAwake();
    }

    public void PlaySoundOnAwake()
    {
        switch(m_ActionOnAwake)
        {
            case OneShotActions.None:
                return; 
            case OneShotActions.Fire:
                FMODManager.instance.PlayShortSounds("Fire");
                break;
            case OneShotActions.Laser:
                FMODManager.instance.PlayShortSounds("Laser");
                break;
            case OneShotActions.Explosion:
                FMODManager.instance.PlayShortSounds("Explosion");
                break;
        }
    }

    #region FMOD_Methods
    public void PlayFootsteps()
    {
        FMODManager.instance.PlayShortSounds("Footsteps");
    }
    public void PlayJumpSound()
    {
        FMODManager.instance.PlayShortSounds("Jump");
    }
    public void PlaySwitchSound()
    {
        FMODManager.instance.PlayShortSounds("Switch"); 
    }
    public void PlayDeathSound()
    {
        FMODManager.instance.PlayShortSounds("Death");
    }
    #endregion


}
