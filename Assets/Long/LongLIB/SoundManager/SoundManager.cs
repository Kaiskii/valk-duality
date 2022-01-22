using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
  //Variable Declarations
  [SerializeField]
  AudioSource audioSource;
  [SerializeField]
  AudioSource bgmSource;

  SoundLibrary soundLibrary;

  [SerializeField]
  string startingBGM = "BGM1";
  bool bgmStarted = false;

  float fadeElapsedTime;
  float fadeTime;
  bool isFading;

  float soundCapElapsedTime;
  int soundsPlayed;

  void Start()
  {
    soundLibrary = Resources.Load<SoundLibrary>("SoundLibrary");
    if(!soundLibrary){
      Debug.LogWarning("Failed to load Sound Library! Is it created?");
    }

    if(!bgmStarted){
      PlayMusicWithFade(startingBGM,15f);
      bgmStarted = true;
    }
  }

  void Update()
  {
    soundCapElapsedTime+= Time.deltaTime;
    if(soundCapElapsedTime>=soundLibrary.soundCapResetSpeed)
    {
      soundsPlayed = 0;
      soundCapElapsedTime = 0;
    }

    if(isFading){
      fadeElapsedTime+=Time.deltaTime;
      bgmSource.volume = Mathf.Lerp(0, 1, fadeElapsedTime / fadeTime);
      if(bgmSource.volume >= 1){
        isFading = false;
        fadeElapsedTime = 0;
      }
    }
  }

  // Play a single clip through the sound effects source.
  public void Play(string effect, float volume = -1f) {
    SoundLibrary.SoundAsset soundAsset = soundLibrary.GetSoundAsset(effect);
    if(!soundAsset.clip || soundsPlayed>soundLibrary.maxSounds) return;

    audioSource.clip = soundAsset.clip;
    audioSource.pitch = Random.Range(soundAsset.pitchVariance.x,soundAsset.pitchVariance.y);
    if(volume != -1)
      audioSource.volume = volume;
    else
      audioSource.volume = soundAsset.baseVolume;

    audioSource.PlayOneShot(soundAsset.clip);
    soundsPlayed++;
  }

  public void PlayRandomPitch(string effect,float minPitch = 0.8f,float maxPitch = 1.2f) {
    SoundLibrary.SoundAsset soundAsset = soundLibrary.GetSoundAsset(effect);
    if(!soundAsset.clip || soundsPlayed>soundLibrary.maxSounds) return;

    audioSource.clip = soundAsset.clip;
    audioSource.pitch = Random.Range(minPitch, maxPitch);
    audioSource.PlayOneShot(soundAsset.clip);

    soundsPlayed++;
  }

  // Play a single clip through the music source.
  public void PlayMusic(string bgm) {
    SoundLibrary.SoundAsset soundAsset = soundLibrary.GetSoundAsset(bgm);
    if(!soundAsset.clip) return;

    bgmSource.clip = soundAsset.clip;
    bgmSource.PlayOneShot(soundAsset.clip);
  }

  public void PlayMusicWithFade(string bgm,float fadeInTime){
    SoundLibrary.SoundAsset soundAsset = soundLibrary.GetSoundAsset(bgm);
    if(!soundAsset.clip) return;

    fadeTime = fadeInTime;
    bgmSource.clip = soundAsset.clip;
    bgmSource.volume = 0;
    bgmSource.PlayOneShot(soundAsset.clip,0.5f);
    isFading = true;
  }
}
