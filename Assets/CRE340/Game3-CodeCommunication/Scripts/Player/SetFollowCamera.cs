using UnityEngine;
using Cinemachine;

public class SetFollowCamera : MonoBehaviour
{
    void Start()
    {
        // Try to find the CinemachineVirtualCamera component in the scene
        CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            // If not found, try to find the GameObject with the tag 'VirtualCamera'
            GameObject virtualCameraObject = GameObject.FindGameObjectWithTag("VirtualCamera");
            if (virtualCameraObject != null)
            {
                virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();
            }
        }

        if (virtualCamera != null)
        {
            // Set the Follow property to this transform
            virtualCamera.Follow = transform;
        }
        else
        {
            Debug.LogError("CinemachineVirtualCamera not found in the scene.");
        }
    }
}