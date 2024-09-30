using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    // Properties
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }

    // Additional property for Health
    public int Health { get; set; }

    // Additional property for Hunger
    public int Hunger { get; set; }

    // Use Awake or Start to initialize values instead of constructors
    void Awake()
    {
        Name = "Generic Animal";
        Species = "Unknown";
        Age = 1;
        Health = 100;
        Hunger = 0;
    }

    // Method to make a sound
    public virtual void MakeSound()
    {
        Debug.Log($"{Name} the {Species} makes a sound!");
    }

    // Method to describe the animal
    public virtual void Describe()
    {
        Debug.Log($"{Name} is a {Age} year old {Species}.");
    }

    // Method to eat with a variable parameter
    public void Eat(string food)
    {
        Debug.Log($"{Name} the {Species} eats {food}.");
        Hunger = Mathf.Max(0, Hunger - 20); // Reduces hunger level
    }

    // Method to sleep with a time parameter
    public void Sleep(int hours)
    {
        Debug.Log($"{Name} the {Species} sleeps for {hours} hours.");
        Health = Mathf.Min(100, Health + hours * 5); // Increases health
    }

    // Method to simulate movement with a distance parameter
    public void Move(float distance)
    {
        Debug.Log($"{Name} the {Species} moves {distance} meters.");
    }

    // Method to check status with conditional behaviour
    public void CheckStatus()
    {
        if (Hunger >= 50)
        {
            Debug.Log($"{Name} the {Species} is very hungry and needs food.");
        }
        else if (Health < 50)
        {
            Debug.Log($"{Name} the {Species} is not feeling well and needs rest.");
        }
        else
        {
            Debug.Log($"{Name} the {Species} is in good health and not hungry.");
        }
    }
}
