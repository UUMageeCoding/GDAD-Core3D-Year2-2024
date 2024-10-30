using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackEventManager : MonoBehaviour
{
    //define a delegate for camera shake events
    public delegate void CameraEvent_Shaker(float frequency, float amplitude, float duration);
    
    //define a delegate for chromatic aberration events
    public delegate void ChromaticAberrationEvent(float intensity, float duration);
    
    // Multi-delegate for triggering camera shake events
    public static CameraEvent_Shaker ShakeCamera;
    
    // Multi-delegate for triggering chromatic aberration events
    public static ChromaticAberrationEvent ChromaticAberration;
}
