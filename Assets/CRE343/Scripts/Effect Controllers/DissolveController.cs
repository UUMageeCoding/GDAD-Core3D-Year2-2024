using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Renderer renderer;
    public float dissolveSpeed = 1.0f;
    private float dissolveAmount = 0.0f;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Increment dissolve amount over time
        dissolveAmount += Time.deltaTime * dissolveSpeed;
        renderer.material.SetFloat("_DissolveAmount", dissolveAmount);

        // Reset dissolve amount to loop the effect
        if (dissolveAmount > 1.0f)
        {
            dissolveAmount = 0.0f;
        }
    }
}
