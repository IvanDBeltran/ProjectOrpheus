using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelControl : MonoBehaviour
{

    private ActionController actionController;
    public GameObject restartAction;
    public string levelMusic;

    [Header("Game States")]
    public bool inInventory;
    public bool antiyGravityOn;

    [Header("GameObjects")]
    public GameObject playerHolder;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;
        actionController = FindObjectOfType<ActionController>();

        if(GameManager.Instance != null)
        {
            GameManager.Instance.isPaused = false;
            GameManager.Instance.inventoryActive = false;
            GameManager.Instance.pauseMenuActive = false;
            GameManager.Instance.dialogueActive = false;
        }
        
}

    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(levelMusic))
        {
            SoundManager.Instance.FadeOutSound(SoundManager.Instance.currentMusicPlaying, 1f);
        }
        else if (levelMusic != SoundManager.Instance.currentMusicPlaying)
        {
            SoundManager.Instance.FadeAndPlayMusic(levelMusic, 1f);
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        actionController.DeactivateAllActions();
        restartAction.SetActive(true);
    }
}
