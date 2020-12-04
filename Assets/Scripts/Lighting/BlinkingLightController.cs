using UnityEngine;

public class BlinkingLightController : MonoBehaviour
{
    public float FadeOutTotalTime = 3f;
    private float _fadeOutDiffTime;

    private Light _light;
    private float _initialIntensity;

    void Start()
    {
        _light = GetComponent<Light>();
        _initialIntensity = _light.intensity;
    }

    void Update()
    {
        _fadeOutDiffTime += Time.deltaTime / FadeOutTotalTime;
        var factor = Mathf.Lerp(0, 1, _fadeOutDiffTime);

        if (factor >= 1f)
        {
            _fadeOutDiffTime = 0;
        }

        _light.intensity = _initialIntensity * factor;
    }
}
