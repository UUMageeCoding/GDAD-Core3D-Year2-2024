using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColourCycleText : MonoBehaviour
{
    private TextMeshPro tm;
    public float speed = 0.25f;

    void Start(){
        tm = GetComponent<TextMeshPro>();
    }

    void Update(){
        float hue = Mathf.PingPong(Time.time * speed, 1); // Calculate hue value
        tm.color = Color.HSVToRGB(hue, 1, 1); // Set material colour based on hue
    }
}
