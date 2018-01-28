using UnityEngine;

/// <summary>
/// Objective.
/// </summary>
public class Objective : MonoBehaviour
{
    public bool IsActivated = false;

    public GameObject Light;

    // Start.
    private void Start()
    {
        Light.SetActive(IsActivated);
    }

    // Disable objective when touched.
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (!IsActivated && otherCollider.CompareTag("Player"))
        {
            Light.SetActive(true);

            IsActivated = true;

            AudioController.Instance.PlaySound(SoundType.Objective);
        }
    }
}