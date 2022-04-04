using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lix.Core;

public class Moon : MonoBehaviour
{

  [SerializeField] private float rotateSpeed = 12.0f;
  [SerializeField] private ParticleSystem moonExplosion;
  [SerializeField] private Material deathStarMaterial;

  // Outer Dependencies
  private World world;
  private AudioManager audioManager;

  // Properties

  float duration = 10f;
  private float t = 0;

  private bool colorDirection = true;

  // Start is called before the first frame update
  void Start()
  {
    this.world = DIContainer.GetService<World>();
    this.audioManager = DIContainer.GetService<AudioManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (world != null)
    {
      // Rotate around world
      //   this.transform.RotateAround(world.transform.position, Vector3.forward, Time.deltaTime * rotateSpeed);

      this.transform.position = RotatePointAroundPivot(this.transform.position, Vector3.forward, Quaternion.Euler(0, 0, Time.deltaTime * rotateSpeed));
    }

    // float h = Mathf.Lerp(0, 1, t);
    Color color = Color.HSVToRGB(t, 1, 1);

    deathStarMaterial.SetColor("_EmissionColor", color);

    if (colorDirection)
    {
      t += Time.deltaTime / duration;
      if (t >= 1)
      {
        t = 1;
        colorDirection = !colorDirection;
      }
    }
    else
    {
      t -= Time.deltaTime / duration;
      if (t <= 0)
      {
        t = 0;
        colorDirection = !colorDirection;
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    audioManager.Play("StarExplosion");
    moonExplosion.transform.parent = null;
    moonExplosion.gameObject.SetActive(true); // Particle system will be destroyed after it has finished playing

    Destroy(gameObject);
  }

  public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle)
  {
    return angle * (point - pivot) + pivot;
  }
}
