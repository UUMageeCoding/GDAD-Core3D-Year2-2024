using UnityEngine;

public class ItemClick : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Try to get the Item component from the hit object
                Item clickedItem = hit.transform.GetComponent<Item>();

                // If an Item component is found, call its DisplayInfo method
                if (clickedItem != null)
                {
                    clickedItem.DisplayInfo();
                }
            }
        }
    }
}