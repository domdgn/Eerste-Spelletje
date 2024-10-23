using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffects : MonoBehaviour
{
    private PlayerEffects effects;
    private Camera mainCamera;
    private float lerpDuration = 0.75f;
    private float lerpTimer = 0.0f;
    private bool isRapidActive = false;

    private Volume volume;
    private LensDistortion lensDistortion;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;

    private float originalOrthoSize = 8.5f;
    private float rapidOrthoSize = 7.5f;
    private float originalDistortionIntensity = -0.05f;
    private float rapidDistortionIntensity = -0.15f;
    private float originalBloomIntensity = 2.5f;
    private float rapidBloomIntensity = 5f;
    private float originalChromaticAberrationIntensity = 0.08f;
    private float rapidChromaticAberrationIntensity = 0.5f;
    private float originalContrast = 1f;
    private float rapidContrast = 20f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            effects = player.GetComponent<PlayerEffects>();
        }
        else
        {
            Debug.LogWarning("Player object not found!");
        }

        mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            originalOrthoSize = mainCamera.orthographicSize;
        }
        else
        {
            Debug.LogWarning("Main Camera not found or is not orthographic!");
        }

        volume = FindObjectOfType<Volume>();
        if (volume == null)
        {
            Debug.LogWarning("Volume not found! Ensure a Volume is present in the scene.");
            return;
        }

        if (!volume.profile.TryGet(out lensDistortion))
        {
            Debug.LogWarning("Lens Distortion not found in Volume profile.");
        }
        else
        {
            lensDistortion.intensity.value = originalDistortionIntensity;
        }

        if (!volume.profile.TryGet(out bloom))
        {
            Debug.LogWarning("Bloom not found in Volume profile.");
        }
        else
        {
            bloom.intensity.value = originalBloomIntensity;
        }

        if (!volume.profile.TryGet(out chromaticAberration))
        {
            Debug.LogWarning("Chromatic Aberration not found in Volume profile.");
        }
        else
        {
            chromaticAberration.intensity.value = originalChromaticAberrationIntensity;
        }

        if (!volume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogWarning("Color Adjustments not found in Volume profile.");
        }
        else
        {
            colorAdjustments.contrast.value = originalContrast;
        }
    }

    void Update()
    {
        if (effects != null && mainCamera != null && mainCamera.orthographic)
        {
            float targetOrthoSize = originalOrthoSize;
            float targetDistortionIntensity = originalDistortionIntensity;
            float targetBloomIntensity = originalBloomIntensity;
            float targetChromaticAberrationIntensity = originalChromaticAberrationIntensity;
            float targetContrast = originalContrast;

            if (effects.isRapid)
            {
                if (!isRapidActive)
                {
                    isRapidActive = true;
                    lerpTimer = 0.0f;
                }
                targetOrthoSize = rapidOrthoSize;
                targetDistortionIntensity = rapidDistortionIntensity;
                targetBloomIntensity = rapidBloomIntensity;
                targetChromaticAberrationIntensity = rapidChromaticAberrationIntensity;
                targetContrast = rapidContrast;
            }
            else
            {
                if (isRapidActive)
                {
                    isRapidActive = false;
                    lerpTimer = 0.0f;
                }
            }

            if (lerpTimer <= lerpDuration)
            {
                lerpTimer += Time.deltaTime;
                float t = lerpTimer / lerpDuration;
                mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrthoSize, t);

                if (lensDistortion != null)
                {
                    lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, targetDistortionIntensity, t);
                }

                if (bloom != null)
                {
                    bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, targetBloomIntensity, t);
                }

                if (chromaticAberration != null)
                {
                    chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, targetChromaticAberrationIntensity, t);
                }

                if (colorAdjustments != null)
                {
                    colorAdjustments.contrast.value = Mathf.Lerp(colorAdjustments.contrast.value, targetContrast, t);
                }
            }
        }
    }
}
