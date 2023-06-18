using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private float overallVolume = 0.6f;
    private string mainMenu = "MainMenu";
    private string mainScene = "MainScene";

    private void Awake() 
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume * overallVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) 
    {
        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == mainMenu)
        {
            StopSound("MainSceneSong");
            PlaySound("MainMenuSong");
        }
        else if (currentSceneName == mainScene)
        {
            StopSound("MainMenuSong");
            PlaySound("MainSceneSong");
        }   
    }

    // Change the game volume
    public void UpdateOverallVolume(float value)
    {
        overallVolume = value;

        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = s.volume * overallVolume;
            }
        }
    }

    public void PlaySound(string soundName) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopSound(string soundName) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
            return;
        }
        s.source.Stop();
    }

    
}
