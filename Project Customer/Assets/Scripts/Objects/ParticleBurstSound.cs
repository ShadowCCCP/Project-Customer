using UnityEngine;

public class ParticleBurstSound : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sparkSounds;

    private ParticleSystem pSystem;
    private ParticleSystem.EmissionModule emmisionModule;
    private AudioSource audioSource;

    bool playOnce;

    private void Start()
    {
        // Get references to the Particle System and AudioSource components.
        pSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        // Subscribe to the particle burst event.
        pSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 8) }); // Adjust the burst settings as needed.
        emmisionModule = pSystem.emission;
        emmisionModule.burstCount = 0; // Reset the burst count to prevent immediate playback.

        pSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, 8) }); // Set the burst count to 1 to trigger the event immediately.
        pSystem.Play(); // Start the Particle System.
    }

    private void Update()
    {
        // Check if the Particle System is emitting.
        if (pSystem.isPlaying)
        {
            // Check if a burst has occurred.
            if (pSystem.emission.burstCount > 0)
            {
                // Play the sound.
                if(!playOnce)
                {
                    int randomNumber = UnityEngine.Random.Range(0, 4);
                    audioSource.clip = sparkSounds[randomNumber];
                    audioSource.Play();
                    playOnce = true;
                }
                emmisionModule.burstCount = 0;
            }
        }

        // Reset the burst count to prevent immediate playback.
        if (!pSystem.isPlaying && !audioSource.isPlaying)
        {
            emmisionModule.burstCount = 0;
            playOnce = false;

            pSystem.Play();
            emmisionModule.burstCount = 8;
        }
    }
}