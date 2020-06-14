using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasingEjector : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem casingEjector;

    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    // Start is called before the first frame update
    void Awake()
    {
        casingEjector = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Ejects a shell by playing the particlesystem
    /// </summary>
    public void EjectCasing()
    {
        casingEjector.Play();
    }

    //private void OnParticleCollision(GameObject other)
    //{

    //    int collCount = casingEjector.GetSafeCollisionEventSize();

    //    if (collCount > 8)
    //        CollisionEvents = new List<ParticleCollisionEvent>();

    //    int eventCount = casingEjector.GetCollisionEvents(other, CollisionEvents);

    //    for (int i = 0; i < eventCount; i++)
    //    {
    //        if (CollisionEvents.)

    //        //AudioSource.PlayClipAtPoint(audioClips.ArrayRandomValue<AudioClip>(), transform.position);
    //    }



    //}

    private void OnParticleTrigger()
    {
        

        // get the particles which matched the trigger conditions this frame
        int numEnter = casingEjector.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = casingEjector.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        print(numEnter);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = new Color32(255, 0, 0, 255);
            print(1);
            
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        if (numEnter > 0)
            AudioSource.PlayClipAtPoint(audioClips.ArrayRandomValue<AudioClip>(), transform.position);

        // re-assign the modified particles back into the particle system
        casingEjector.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        casingEjector.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);



        
    }
}
