using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHitBehavior : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void HitObject()
    {
        // Activate the particle system.
        particleSystem.Play();

        // Start a coroutine to wait for the particle system to finish.
        StartCoroutine(WaitForParticleCompletion());
    }

    private IEnumerator WaitForParticleCompletion()
    {
        // Wait for the duration of the particle system.
        yield return new WaitForSeconds(particleSystem.main.duration);

        // Destroy the object.
        Destroy(gameObject);
    }
}
