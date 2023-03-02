using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteLoop : MonoBehaviour
{
    [SerializeField] Volume volume;
    private Vignette vignette;
    public float minIntensity = 0.23f;
    public float maxIntensity = 0.4f;
    public float duration = 0.3f;
    public int loopCount = 5;


    public IEnumerator VignetteLoopRoutine()
    {
        for (int i = 0; i < loopCount; i++)
        {
            float timer = 0f;
            float startIntensity = vignette.intensity.value;
            float targetIntensity = Random.Range(minIntensity, maxIntensity);

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                float intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                vignette.intensity.value = intensity;
                yield return null;
            }

            timer = 0f;
            startIntensity = targetIntensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                float intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                vignette.intensity.value = intensity;
                yield return null;
            }
        }

        // Set the Vignette Intensity to 0 after the loop is completed
        vignette.intensity.value = 0f;
    }
}
