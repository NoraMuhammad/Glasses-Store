using UnityEngine;
using SO.Events;

public class DuringCustomizationCM : MonoBehaviour
{
    [SerializeField] EventSO OnDuringCustomization;
    bool raised;
    static int counter;
    private void Awake()
    {
        counter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        counter++;
        if (other.CompareTag("MainCamera"))
        {
            if(gameObject.name == "ChoosenFrame CM" || counter > 1)
            {
                OnDuringCustomization.Raise();
                raised = true;
            }
        }
    }
}
