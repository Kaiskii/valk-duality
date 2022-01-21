using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
  ParticleLibrary particleLibrary;
  List<ParticleLibrary.ParticleAsset> particlePool;

  private void Start()
  {
    particleLibrary = Resources.Load<ParticleLibrary>("ParticleLibrary");
    if(!particleLibrary){
      Debug.LogWarning("Failed to load Particle Library! Is it created?");
    }

    particlePool = new List<ParticleLibrary.ParticleAsset>();
  }

  public void CreateParticle(string particleName, Vector2 position,Transform parent = null)
  {
    GameObject newParticle = GetParticle(particleName);
    if(!newParticle) return;

    newParticle.transform.position = position;
    newParticle.SetActive(true);
    newParticle.GetComponent<ParticleSystem>().Play();
  }

  public void CreateParticle(string particleName, Vector2 position,Vector3 direction,Transform parent = null)
  {
    GameObject newParticle = GetParticle(particleName,parent);
    if(!newParticle) return;

    newParticle.transform.position = position;

    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    newParticle.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle-particleLibrary.GetParticleRotationOffset(particleName)));

    newParticle.SetActive(true);
    newParticle.GetComponent<ParticleSystem>().Play();
  }

  GameObject GetParticle(string particleName, Transform parent = null)
  {
    //Search pool for object
    for (int i = 0;i<particlePool.Count;++i)
    {
      if (!particlePool[i].particle.activeInHierarchy && particlePool[i].name == particleName)
      {
        return particlePool[i].particle;
      }
    }

    if(particleLibrary.GetParticle(particleName) == null){
      Debug.LogWarning("Could not find particle "+particleName+"!");
      return null;
    }

    GameObject newParticleObj;

    if(!parent){
      newParticleObj = Instantiate(particleLibrary.GetParticle(particleName),this.transform);
    }else{
      newParticleObj = Instantiate(particleLibrary.GetParticle(particleName),parent);
    }

    particlePool.Add(new ParticleLibrary.ParticleAsset(){name = particleName,particle = newParticleObj});
    return newParticleObj;
  }
}
