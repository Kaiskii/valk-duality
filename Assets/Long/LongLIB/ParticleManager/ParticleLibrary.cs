using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ParticleLibrary : ScriptableObject
{
#if UNITY_EDITOR
  [MenuItem("LongLib/Create Particle Library")]
  private static void TryGetLibrary()
  {
      if (!AssetDatabase.IsValidFolder("Assets/Resources"))
      {
          Debug.Log("No Resource Folder! Creating...");
          AssetDatabase.CreateFolder("Assets", "Resources");
      }
      
      var index = Resources.Load<ParticleLibrary>("ParticleLibrary");
      if (index == null) 
      {
          Debug.Log("Could not find ParticleLibrary! Creating...");
          index = CreateInstance<ParticleLibrary>();
          UnityEditor.AssetDatabase.CreateAsset(index, "Assets/Resources/ParticleLibrary.asset");
      }
  }
#endif

  [SerializeField]
  List<ParticleAsset> particles;
  
  static Dictionary<string, ParticleAsset> particleDictionary;

  [RuntimeInitializeOnLoadMethod]
  private static void Init(){
    var index = Resources.Load<ParticleLibrary>("ParticleLibrary");

    if(!index){
      Debug.LogWarning("Failed to load Particle Library! Is it created?");
      return;
    }

    particleDictionary = new Dictionary<string, ParticleAsset>();

    foreach(ParticleAsset p in index.particles)
    {
      //Check for duplicates
      if (particleDictionary.ContainsKey(p.name))
      {
          Debug.LogErrorFormat("Duplicate Particle name: "+p.name);
          continue;
      }
      particleDictionary.Add(p.name, p);
    }
  }

  public GameObject GetParticle(string particleName){
    ParticleAsset foundParticle;
    particleDictionary.TryGetValue(particleName,out foundParticle);
    return foundParticle.particle;
  }

  public float GetParticleRotationOffset(string particleName)
  {
    ParticleAsset foundParticle;
    particleDictionary.TryGetValue(particleName,out foundParticle);
    return foundParticle.rotationOffset;
  }

  [System.Serializable]
  public struct ParticleAsset
  {
      public string name;
      public GameObject particle;
      public float rotationOffset;
  }
}
