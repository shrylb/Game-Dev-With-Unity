using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioSource musicSource;
   [SerializeField] AudioSource FXsource;

   public AudioClip bgm;
   public AudioClip whack;

   private void Start()
   {
    musicSource.clip = bgm;
    musicSource.Play();
   }

   public void WhackSFX(AudioClip clip)
   {
    FXsource.PlayOneShot(clip);
   }
}
