using System;
using System.Linq;
using UnityEngine;

public static class FeedbackEventManager
{
    public static void TriggerFeedbacks(GameObject source)
    {
        // Find all IFeedback components in the scene and trigger feedback
        foreach (var feedback in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IFeedback>())
        {
            feedback.TriggerFeedback(source);
            //Debug.Log($"Triggered feedback on {source.name}");
        }
    }
    public static void TriggerFeedbacks(GameObject source, float intensity)
    {
        // Find all IFeedback components in the scene and trigger feedback
        foreach (var feedback in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IFeedback>())
        {
            feedback.TriggerFeedback(source, intensity);
            //Debug.Log($"Triggered feedback on {source.name} with intensity {intensity}");
        }
    }
    public static void TriggerFeedbacks(GameObject source, float intensity, float duration)
    {
        // Find all IFeedback components in the scene and trigger feedback
        foreach (var feedback in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IFeedback>())
        {
            feedback.TriggerFeedback(source, intensity, duration);
            //Debug.Log($"Triggered feedback on {source.name} with intensity {intensity} and duration {duration}");
        }
    }
    public static void TriggerFeedbacks(GameObject source, float intensity, float duration, float cooldown)
    {
        // Find all IFeedback components in the scene and trigger feedback
        foreach (var feedback in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IFeedback>())
        {
            feedback.TriggerFeedback(source, intensity, duration, cooldown);
            //Debug.Log($"Triggered feedback on {source.name} with intensity {intensity} and duration {duration} and cooldown {cooldown}");
        }
    }
    public static void TriggerFeedbacks(GameObject source, float intensity, float duration, float cooldown, bool isPermanent)
    {
        // Find all IFeedback components in the scene and trigger feedback
        foreach (var feedback in GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IFeedback>())
        {
            feedback.TriggerFeedback(source, intensity, duration, cooldown, isPermanent);
            //Debug.Log($"Triggered feedback on {source.name} with intensity {intensity} and duration {duration} and cooldown {cooldown} and isPermanent {isPermanent}");
        }
    }
}
