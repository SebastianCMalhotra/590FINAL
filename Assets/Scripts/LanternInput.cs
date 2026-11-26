
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public Light lanternLight;

    public float fadeSpeed = 3f;
    public float maxIntensity = 5f;

    private float targetIntensity;

    void Start()
    {
        targetIntensity = lanternLight.intensity;
    }

    void Update()
    {
        if (OVRInput.GetDown(
        OVRInput.RawButton.RIndexTrigger))
            {
                targetIntensity = targetIntensity > 0.01f ? 0f : maxIntensity;
            }

        lanternLight.intensity = Mathf.Lerp(
            lanternLight.intensity,
            targetIntensity,
            Time.deltaTime * fadeSpeed
        );
    }
}
