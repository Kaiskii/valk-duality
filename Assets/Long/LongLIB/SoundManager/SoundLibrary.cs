using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SoundLibrary : ScriptableObject
{
  [Header("Sound Cap")]
  public float soundCapResetSpeed = 0.55f;
  public int maxSounds = 3;
  [Space]

  [SerializeField]
  List<SoundAsset> effectClips;

  [SerializeField]
   List<SoundAsset> musicClips;
   
#if UNITY_EDITOR
  [MenuItem("LongLib/Create Sound Library")]
  private static void TryGetLibrary()
  {
      if (!AssetDatabase.IsValidFolder("Assets/Resources"))
      {
          Debug.Log("No Resource Folder! Creating...");
          AssetDatabase.CreateFolder("Assets", "Resources");
      }
      
      var index = Resources.Load<SoundLibrary>("SoundLibrary");
      if (index == null) 
      {
          Debug.Log("Could not find SoundLibrary! Creating...");
          index = CreateInstance<SoundLibrary>();
          UnityEditor.AssetDatabase.CreateAsset(index, "Assets/Resources/SoundLibrary.asset");
      }
  }
#endif
  static Dictionary<string, SoundAsset> soundDictionary;

  [RuntimeInitializeOnLoadMethod]
  private static void Init(){
    var index = Resources.Load<SoundLibrary>("SoundLibrary");

    if(!index){
      Debug.LogWarning("Failed to load Sound Library! Is it created?");
      return;
    }

    soundDictionary = new Dictionary<string, SoundAsset>();

    foreach(SoundAsset fx in index.effectClips)
    {
      //Check for duplicates
      if (soundDictionary.ContainsKey(fx.name))
      {
          Debug.LogErrorFormat("Duplicate FX name: "+fx.name);
          continue;
      }
      soundDictionary.Add(fx.name, fx);
    }

    foreach(SoundAsset bgm in index.musicClips)
    {
      //Check for duplicates
      if (soundDictionary.ContainsKey(bgm.name))
      {
          Debug.LogErrorFormat("Duplicate BGM name: "+bgm.name);
          continue;
      }
      soundDictionary.Add(bgm.name, bgm);
    }
  }

  public SoundAsset GetSoundAsset(string clip){
    SoundAsset foundClip;
    soundDictionary.TryGetValue(clip,out foundClip);
    return foundClip;
  }

  [System.Serializable]
  public struct SoundAsset
  {
      public string name;
      public AudioClip clip;
      public float baseVolume;
      public Vector2 pitchVariance;
  }
}
