using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lix.Core
{
  public class AudioManager : MonoBehaviour
  {
    public static AudioManager Instance;

    public Sound[] soundList;
    private static Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    // Start is called before the first frame update
    protected void Awake()
    {
      foreach (Sound s in soundList)
      {
        if (sources.ContainsKey(s.soundName)) continue;

        AudioSource source = s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;

        sources.Add(s.soundName, source);
      }
    }

    private void Start()
    {
      Play("Background");
    }

    public void Play(string name)
    {
      sources[name].Play();
    }

    public void SetVolume(string name, float v)
    {
      sources[name].volume = v;
    }
  }
}