using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AV_Testing : MonoBehaviour
{
    public GameObject directorPrefab; // Assign the PlayableDirector prefab in the Inspector
    public GameObject particlePrefab; // Assign the particle prefab in the Inspector
    public Transform spawnLocation; // Optional: assign a specific spawn location

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Instantiate and play the PlayableDirector
            if (directorPrefab != null)
            {
                GameObject directorInstance = Instantiate(directorPrefab, spawnLocation ? spawnLocation.position : transform.position, Quaternion.identity);
                PlayableDirector playableDirector = directorInstance.GetComponent<PlayableDirector>();
                if (playableDirector != null)
                {
                    playableDirector.Play();
                }
            }

            // Instantiate the particle prefab
            if (particlePrefab != null)
            {
                Instantiate(particlePrefab, spawnLocation ? spawnLocation.position : transform.position, transform.rotation);
            }
        }
        
        //EXAMPLES OF HOW TO USE THE AUDIO MANAGER EVENTS
        
        // press the x key to call the audio manager event to play a sfx
        if (Input.GetKeyDown(KeyCode.X))
        {
            //AudioEventManager.PlaySFX(null, "Cat meows", 1.0f, 1.0f, true, 0.1f, 0f, "null");
        }
        //press the y key to call the audio manager event to play a music
        if (Input.GetKeyDown(KeyCode.Y)){
            //udioEventManager.PlayBGM(0, "name here", 1F, FadeType.FadeInOut, 2f, true, "null");
        }
        
        //press the z key to call the audio manager event to play ambient audio
        if (Input.GetKeyDown(KeyCode.Z)){
            //AudioEventManager.PlayAmbientAudio(null, 0, "name here", 1F, 1F, 1F, FadeType.FadeInOut, 2f, true, "null");
        }
        
        // press the m key to get and call the AudioEventSender_SFX component Play() to play a sfx
        if (Input.GetKeyDown(KeyCode.M))
        {
            //AudioEventSender_SFX audioEventSender = GetComponent<AudioEventSender_SFX>();
            //audioEventSender?.Play();
        }
    }
    
}