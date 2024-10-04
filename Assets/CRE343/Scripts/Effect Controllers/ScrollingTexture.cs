using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URPScrollingTexture : MonoBehaviour
{
    public float scrollSpeedX = 0.5f; // Speed of scrolling in X direction
    public float scrollSpeedY = 0.5f; // Speed of scrolling in Y direction

    private Renderer rend;
    private Vector2 savedOffset; // To store the initial offset

    void Start()
    {
        rend = GetComponent<Renderer>();
        savedOffset = rend.material.GetTextureOffset("_BaseMap"); // Use _BaseMap for URP
    }

    void Update()
    {
        // Calculate the new offset based on time and scroll speed
        float newOffsetX = savedOffset.x + Time.time * scrollSpeedX;
        float newOffsetY = savedOffset.y + Time.time * scrollSpeedY;

        // Set the new offset to the _BaseMap texture
        rend.material.SetTextureOffset("_BaseMap", new Vector2(newOffsetX, newOffsetY));
    }
}
