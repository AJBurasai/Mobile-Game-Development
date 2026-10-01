using UnityEngine;

public class boosterFlickerScript : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public float baseIntensity = 1f;
    public float flickerAmount = 0.05f;

    void Update()
    {
        light2D.intensity = baseIntensity + Random.Range(flickerAmount, 0.01f);
    }
}
