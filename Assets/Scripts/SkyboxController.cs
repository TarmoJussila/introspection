using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skybox controller. Sets random skybox.
/// </summary>
public class SkyboxController : MonoBehaviour
{
    public List<Material> Skyboxes = new List<Material>();

	// Start.
	private void Start()
	{
        RenderSettings.skybox = Skyboxes[Random.Range(0, Skyboxes.Count)];

        DynamicGI.UpdateEnvironment();
    }
}