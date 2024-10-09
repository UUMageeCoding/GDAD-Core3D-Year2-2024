using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGlow : MonoBehaviour
{
    private Renderer renderer;
    public float pulseSpeed = 2.0f;
    public Color glowColor = Color.white;
    private Color baseColor;

    void Start(){
        renderer = GetComponent<Renderer>();
        baseColor = renderer.material.GetColor("_EmissionColor");
    }

    void Update(){
        float emission = Mathf.PingPong(Time.time * pulseSpeed, 1.0f);
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        renderer.material.SetColor("_EmissionColor", finalColor * glowColor);
    }
}