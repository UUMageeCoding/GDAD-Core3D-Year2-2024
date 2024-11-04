using UnityEngine;
using TMPro;

public class State_UI : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;   // Reference to the TextMeshPro component
    private Enemy enemy;                             // Reference to the Enemy script

    private void Awake()
    {
        // Attempt to find the Enemy component in the parent or the same GameObject
        enemy = GetComponentInParent<Enemy>();

        // Get TextMeshPro component if not assigned
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        if (textMesh == null)
        {
            Debug.LogError("TextMeshPro component not found on State_UI!");
        }
    }

    private void Start()
    {
        // Update the display initially
        UpdateStateText();
    }

    private void Update()
    {
        // Keep the text updated to the current state
        UpdateStateText();
    }

    public void UpdateStateText()
    {
        if (enemy != null && textMesh != null)
        {
            textMesh.text = enemy.GetCurrentStateName(); // Display the enemy's current state
        }
    }
}