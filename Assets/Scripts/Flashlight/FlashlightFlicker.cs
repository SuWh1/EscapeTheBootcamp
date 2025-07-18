using UnityEngine;

public class FlashlightFlicker : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private float flickerChance = 0.01f; // 1% per frame
    [SerializeField] private float flickerDuration = 0.05f; // duration of flicker

    private bool isFlickering = false;

    void Start()
    {
        if (flashlight == null)
            flashlight = GetComponent<Light>();
    }

    void Update()
    {
        if (!isFlickering && flashlight.enabled)
        {
            if (Random.value < flickerChance)
            {
                StartCoroutine(Flicker());
            }
        }
    }

    System.Collections.IEnumerator Flicker()
    {
        isFlickering = true;
        flashlight.enabled = false;
        yield return new WaitForSeconds(flickerDuration);
        flashlight.enabled = true;
        isFlickering = false;
    }
}
