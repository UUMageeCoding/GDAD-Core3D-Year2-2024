using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCycle : MonoBehaviour
{
    private Renderer renderer;
    public float speed = 0.25f;

    void Start(){
        renderer = GetComponent<Renderer>();
    }

    void Update(){
        float hue = Mathf.PingPong(Time.time * speed, 1); // Calculate hue value
        renderer.material.color = Color.HSVToRGB(hue, 1, 1); // Set material colour based on hue
    }
}