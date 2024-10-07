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
                    // Call the DisplayInfo method of the clicked item
                    // ** note this is a virtual method in the base class that can be overridden in derived classes
                    clickedItem.DisplayInfo();
                    
                    // Call the SayHello method of the clicked item
                    // ** note this is a non-virtual method in the base class that cannot be overridden in derived classes
                    clickedItem.SayHello();
                }
            }
        }
    }
}