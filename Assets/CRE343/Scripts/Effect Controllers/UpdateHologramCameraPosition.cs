using UnityEngine;

[ExecuteAlways]
public class UpdateHologramCameraPosition : MonoBehaviour
{
    public Material hologramMaterial;

    void Update()
    {
        if (hologramMaterial != null)
        {
            // Pass the camera's world-space position to the shader
            hologramMaterial.SetVector("_CamPos", Camera.main.transform.position);
        }
    }
}