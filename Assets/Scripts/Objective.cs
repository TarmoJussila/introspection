using UnityEngine;

/// <summary>
/// Objective.
/// </summary>
public class Objective : MonoBehaviour
{
    public bool IsActivated = false;
    public bool IsFinal = false;

    public GameObject Light;

    // Start.
    private void Start()
    {
        if (Light != null)
            Light.SetActive(IsActivated);
    }

    // Disable objective when touched.
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (!IsActivated && otherCollider.CompareTag("Player"))
        {
            if (!IsFinal)
            {
                if (Light != null)
                    Light.SetActive(true);

                IsActivated = true;

                AudioController.Instance.PlaySound(SoundType.Objective);
            }
            else
            {
                
            }

        }
    }
}