using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFade : MonoBehaviour
{
    private Renderer renderer;
    public float fadeSpeed = 1.0f;
    private Color materialColor;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        materialColor = renderer.material.color;
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * fadeSpeed, 1.0f);  // Calculate alpha value
        renderer.material.color = new Color(materialColor.r, materialColor.g, materialColor.b, alpha);
    }
}
