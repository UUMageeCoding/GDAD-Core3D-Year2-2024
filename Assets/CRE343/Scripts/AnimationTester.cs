using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    public Animator animator;
    public string[] boolParameterNames = new string[0];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boolParameterNames.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                ToggleBoolParameter(boolParameterNames[i]);
            }
        }
    }

    private void ToggleBoolParameter(string parameterName)
    {
        if (animator != null && !string.IsNullOrEmpty(parameterName))
        {
            bool currentValue = animator.GetBool(parameterName);
            animator.SetBool(parameterName, !currentValue);
        }
    }
}