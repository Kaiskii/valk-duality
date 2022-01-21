using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
  //Variable Declarations
  [SerializeField]
  AudioSource audioSource;
  SoundLibrary soundLibrary;

  void Start()
  {
    soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
    if(!soundLibrary){
      Debug.LogWarning("Failed to load Sound Library! Is it created?");
    }
  }
  
  // Play a single clip through the sound effects source.
  public void Play(string effect) {
    AudioClip clip = soundLibrary.GetClip(effect);
    if(!clip) {
      Debug.LogWarning("Could not find AudioClip " + effect);
      return;
    }

    audioSource.clip = clip;
    audioSource.PlayOneShot(clip);
  }

  // Play a single clip through the music source.
  public void PlayMusic(string bgm) {
    AudioClip clip = soundLibrary.GetClip(bgm);
    if(!clip) return;

    audioSource.clip = clip;
    audioSource.PlayOneShot(clip);
  }
}
