using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Crystal.
/// </summary>
public class Crystal : MonoBehaviour
{
    public bool IsConsuming = false;

    public float ConsumeDelayTime = 0.5f;

    public Material ConsumedMaterial;

    public List<MeshRenderer> MeshRenderers = new List<MeshRenderer>();

	// Start.
	private void Start()
	{
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        var baseMeshRenderer = meshRenderers[0];

        MeshRenderers = meshRenderers.ToList();

        // Remove base renderer from list (will remain unchanged).
        MeshRenderers.Remove(baseMeshRenderer);
	}

    // Check crystal consume state. Disable renderers with delay.
    private IEnumerator CheckConsumeState()
    {
        if (IsConsuming)
        {
            yield return new WaitForSeconds(ConsumeDelayTime);

            MeshRenderer selectedMeshRenderer = null;

            if (MeshRenderers.Count > 0)
            {
                foreach (var meshRenderer in MeshRenderers)
                {
                    selectedMeshRenderer = meshRenderer;
                    //selectedMeshRenderer.enabled = false;
                    break;
                }
                
                if (selectedMeshRenderer != null && MeshRenderers.Contains(selectedMeshRenderer))
                {
                    selectedMeshRenderer.material = ConsumedMaterial;
                    MeshRenderers.Remove(selectedMeshRenderer);
                }

                EnergyController.Instance.AddEnergy();

                AudioController.Instance.PlaySound(SoundType.Collect);
            }
            else
            {
                IsConsuming = false;
                yield break;
            }

            yield return CheckConsumeState();
        }
    }

    // Consume crystal when on touched.
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            IsConsuming = true;

            StartCoroutine(CheckConsumeState());
        }
    }

    // Stop consuming crystal when not touching.
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            IsConsuming = false;

            StopAllCoroutines();
        }
    }
}