using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    public int musicTrackNumber = 0;
    
    public string sfxName = "name";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //playe background music  when a number key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioManager.Instance.PlayMusic(musicTrackNumber);
        }
        
        //play sound effect when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySoundEffect(this.transform, sfxName, 1.0f, 1.0f, true);
        }
    }
}
