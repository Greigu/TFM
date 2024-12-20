using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    public Material targetMaterial; // Assign your material in the Inspector
    public Color emissionColor = Color.white; // Color of the emission
    public float intensity = 1.0f; // Intensity of the emission

    void Start()
    {
        // Set the initial emission
        targetMaterial = GetComponent<Renderer>().material;
        SetEmission(intensity);
    }

    public void SetEmission(float newIntensity)
    {
        // Calculate the new emission color
        Color finalColor = emissionColor * newIntensity;

        // Apply the emission color to the material
        targetMaterial.SetColor("_EmissionColor", finalColor);

        // Enable the emission property
        DynamicGI.SetEmissive(GetComponent<Renderer>(), finalColor);
    }

    // Example method to change intensity over time
    void Update()
    {
        // Example: Increase intensity over time
        intensity += Time.deltaTime * 0.1f;
        SetEmission(intensity);
    }
}
