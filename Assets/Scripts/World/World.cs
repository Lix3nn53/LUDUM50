using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class World : MonoBehaviour
{

  [SerializeField] private ParticleSystem starExplosion;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnCollideWithStar()
  {
    starExplosion.transform.parent = null;
    starExplosion.gameObject.SetActive(true); // Particle system will be destroyed after it has finished playing

    Destroy(gameObject);
  }
}
