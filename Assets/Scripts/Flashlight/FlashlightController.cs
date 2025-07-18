using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Light flashlight;

    void Start()
    {
        // If flashlight not manually set in Inspector, auto-find it in children
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }

        // Flashlight starts OFF
        flashlight.enabled = false;
    }

    void Update()
    {
        // Press F to toggle flashlight
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
