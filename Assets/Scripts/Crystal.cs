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

    private int initialCrystalCount;

    [Header("Sound Settings")]
    public float ConsumeSoundPitchGrowth = 0.15f;
    public float ConsumeSoundBasePitch = 1.0f;

    public float ConsumeSoundVolume = 0.75f;

    // Start.
    private void Start()
	{
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        var baseMeshRenderer = meshRenderers[0];

        MeshRenderers = meshRenderers.ToList();

        // Remove base renderer from list (will remain unchanged).
        MeshRenderers.Remove(baseMeshRenderer);

        initialCrystalCount = MeshRenderers.Count;
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
                int currentCrystalCount = MeshRenderers.Count;

                foreach (var meshRenderer in MeshRenderers)
                {
                    selectedMeshRenderer = meshRenderer;
                    break;
                }
                
                if (selectedMeshRenderer != null && MeshRenderers.Contains(selectedMeshRenderer))
                {
                    selectedMeshRenderer.material = ConsumedMaterial;
                    MeshRenderers.Remove(selectedMeshRenderer);
                }

                EnergyController.Instance.AddEnergy();
                EnergyHandler.Instance.ShowPlusSign(true);
                AudioController.Instance.PlaySound(SoundType.Collect, ConsumeSoundBasePitch + (initialCrystalCount - currentCrystalCount) * ConsumeSoundPitchGrowth, ConsumeSoundVolume);
            }
            else
            {
                EnergyHandler.Instance.ShowPlusSign(false);
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

            StopAllCoroutines();

            StartCoroutine(CheckConsumeState());
        }
    }

    // Stop consuming crystal when not touching.
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            IsConsuming = false;

            EnergyHandler.Instance.ShowPlusSign(false);

            StopAllCoroutines();
        }
    }
}