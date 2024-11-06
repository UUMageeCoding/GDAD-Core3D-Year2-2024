using System.Collections;
using UnityEngine;

/// <summary>
/// to be attached to a GameObject that will send an audio event TO PLAY A SOUND EFFECT
/// USAGE:
///     attach this script to a GameObject and call the PlaySFX method from an event trigger or script
///     to play a sound effect with the parameters set in the inspector
///     ...
///     or call the PlaySFX method with a Transform parameter to attach the sound to a different GameObject
/// </summary>

public class AudioEventSender_SFX : MonoBehaviour, IAudioEventSender
{
    
    [Space(20)]
    ///  USE THIS AS A TAG TO DETERMINE WHICH EVENT TO SEND (Mutiple scripts can be attached to the same object)
    /// Loop through the AudioEventSender_SFX scripts on the object and send the event with the matching eventName
    public string eventName = "Custom SFX Event Name"; //for future use
    
    [Space(10)] 
    [Header("Sound FX Event Parameters (SFX)")] [Space(5)]
    [Space(20)]
    public string [] sfxName = new string[1]; // The name of the sound effects to play - can be multiple sounds to play randomly
    private string sfxNameToPlay; // The name of the sound effect to play
    
    [Space(20)]
    public bool playOnEnabled = true;
    public bool attachSoundToTransform = false;
    
    [Space(10)]
    [Range(0, 1f)] public float volume = 1.0f;
    [Range(0, 2f)] public float pitch = 1.0f;
    public bool randomisePitch = true;
    [Range(0, 1f)] public float pitchRange = 0.1f;
    [Range(0, 1f)] public float spatialBlend = 0.5f;

    [Space(10)] 
    public bool randomiseDelay = false;
    [Range(0,5f)]
    public float eventDelay = 0f;
    
    [Space(10)]
    [Range(0,100)]
    public int percentageChanceToPlay = 100;


    [Header("Collider Settings")] 
    public CollisionType collisionType = CollisionType.Trigger; // Use trigger or collision
    public string targetTag = "Player"; // Tag of the object that can trigger the event


    [Space(20)]
    [Header("TestMode : 'T' to play sound effect")]
    public bool testMode = false;
    
    private void OnEnable(){
        if (playOnEnabled)
        {   
            //CHECK THE TIME THE GAME HAS BEEN RUNNING - The audiomanager will not be ready to play music until the start method has run
            if (Time.timeSinceLevelLoad > 0.1f){
                Play();
            }
            else{
                StartCoroutine(PlaySFX_Delayed(eventDelay)); 
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (collisionType == CollisionType.Trigger && other.CompareTag(targetTag))
        {
            Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionType == CollisionType.Collision && collision.collider.CompareTag(targetTag))
        {
            Play();
        }
    }
    
    public void Play(){
        //check if the sound should play based on the percentage chance
        if (percentageChanceToPlay < 100){
            int random = Random.Range(0, 100);
            if (random > percentageChanceToPlay){
                return;
            }
        }
        
        sfxNameToPlay = sfxName[Random.Range(0, sfxName.Length)];
        
        if(eventDelay <= 0)
        {
            PlaySFX();
        }
        else
        {
            StartCoroutine(PlaySFX_Delayed(eventDelay));
        }
    }
    
    private void PlaySFX()
    {
        if (attachSoundToTransform){
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(this.transform, sfxNameToPlay, volume, pitch, randomisePitch, pitchRange, spatialBlend, eventName);
        }
        else{
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(null, sfxNameToPlay, volume, pitch, randomisePitch, pitchRange, spatialBlend, eventName);
        }
    }
    private IEnumerator PlaySFX_Delayed(float delay)
    {
        if(randomiseDelay){
            delay = Random.Range(0, eventDelay);
        }
        
        yield return new WaitForSeconds(delay);
        
        if (attachSoundToTransform){
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(this.transform, sfxNameToPlay, volume, pitch, randomisePitch, pitchRange, spatialBlend, eventName);
        }
        else{
            //send the PlaySFX Event with parameters from the inspector
            AudioEventManager.PlaySFX(null, sfxNameToPlay, volume, pitch, randomisePitch, pitchRange, spatialBlend, eventName);
        }
    }
    
    //we need thes methods to implement the interface - they are not used in this script but are required (used in the AudioEventSender_BGM script)
    public void Stop(){
        //to be implemented? - TODO - add a stop sound effect event (Unsure if this is needed)
    }
    public void Pause(){
        //to be implemented? - TODO - add a pause sound effect event (Unsure if this is needed)
    }

    #region Testing

    //----------------- EDITOR / TESTING-----------------
    // This section is only used in the editor to test the events - TODO ADD A CUSTOM EDITOR SCRIPT TO CALL THESE METHODS
    void Update()
    {
        if (testMode){
            if (Input.GetKeyDown(KeyCode.T))
            {
                Play();
            }
        }
    }
    #endregion
}