using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skybox controller. Sets random skybox. Persistent.
/// </summary>
public class SkyboxController : MonoBehaviour
{
    public static SkyboxController Instance { get; private set; }

    public List<Material> Skyboxes = new List<Material>();

    // Awake. Persistent.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start.
    private void Start()
	{
        RenderSettings.skybox = Skyboxes[Random.Range(0, Skyboxes.Count)];

        DynamicGI.UpdateEnvironment();
    }

    // Set random skybox.
    public void SetRandomSkybox()
    {

    }
}