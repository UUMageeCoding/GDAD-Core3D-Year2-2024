using UnityEngine;

public interface IFeedback
{
    void TriggerFeedback(GameObject source);
    void TriggerFeedback(GameObject source, float intensity);
    void TriggerFeedback(GameObject source, float intensity, float duration);
    void TriggerFeedback(GameObject source, float intensity, float duration, float cooldown);
    void TriggerFeedback(GameObject source, float intensity, float duration, float cooldown, bool isPermanent);
}

