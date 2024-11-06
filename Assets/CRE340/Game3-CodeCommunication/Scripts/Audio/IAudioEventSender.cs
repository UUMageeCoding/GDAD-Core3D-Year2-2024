public interface IAudioEventSender
{
    //TODO Add getters and setters for the  properties - currently not needed - future updates will require them
    // string EventName { get; set; }
    // bool PlayOnEnabled { get; set; }
    // float EventDelay { get; set; }
    //  more to come...

    void Play();
    void Stop();
    void Pause();
}