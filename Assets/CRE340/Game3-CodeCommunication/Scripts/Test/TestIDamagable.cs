using System.Linq;
using UnityEngine;

public class TestIDamagable : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IDamagable[] damagables = FindObjectsOfType<MonoBehaviour>().OfType<IDamagable>().ToArray();

            foreach (IDamagable damagable in damagables)
            {
                Debug.Log(damagable);
                damagable.TakeDamage(1);
            }

            Debug.Log("Damagables: " + damagables.Length);
        }
    }
}
