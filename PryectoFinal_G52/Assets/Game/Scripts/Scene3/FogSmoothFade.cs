using UnityEngine;
using System.Collections;

public class FogSmoothFade : MonoBehaviour
{
    public ParticleSystem fogSystem;
    public float fadeDuration = 2f;    // duración del fade-out
    public string playerTag = "Player";

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    private void Start()
    {
        mainModule = fogSystem.main;
        emissionModule = fogSystem.emission;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartCoroutine(FadeOutFog());
        }
    }

    IEnumerator FadeOutFog()
    {
        float startRate = emissionModule.rateOverTime.constant;
        float startAlpha = mainModule.startColor.color.a;
        float startSize = mainModule.startSize.constant;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;

            // 1. Reducir emisión
            emissionModule.rateOverTime = Mathf.Lerp(startRate, 0, progress);

            // 2. Reducir la opacidad (alpha)
            Color c = mainModule.startColor.color;
            c.a = Mathf.Lerp(startAlpha, 0, progress);
            mainModule.startColor = c;

            // 3. Reducir el tamaño
            mainModule.startSize = Mathf.Lerp(startSize, 0.1f, progress);

            yield return null;
        }

        fogSystem.Stop();
    }
}