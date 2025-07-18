using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string itemName = "USB Drive";

    private bool isPlayerNear = false;
    private Camera playerCamera;

    void Start()
    {
        // Try to find Main Camera if not manually set
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            Debug.LogError("❌ InteractableItem: No MainCamera found. Assign a camera or tag your camera as MainCamera.");
        }
    }

    void Update()
    {
        if (isPlayerNear)
        {
            Debug.Log("👣 Player near");

            if (IsLookingAtThis())
            {
                Debug.Log("🎯 Looking at item");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("🧲 Pressed E");
                    Pickup();
                }
            }
        }
    }


    private void Pickup()
    {
        Debug.Log($"✅ Picked up: {itemName}");

        if (GameManager.instance != null)
        {
            GameManager.instance.ItemCollected();
        }

        TooltipUI.instance.HideTooltip();
        Destroy(gameObject);
    }

    private bool IsLookingAtThis()
    {
        if (playerCamera == null)
        {
            Debug.LogError("❌ No playerCamera assigned");
            return false;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Create a layer mask that ignores the Player layer
        int mask = ~( (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("ProximityTrigger")) );

        if (Physics.Raycast(ray, out hit, 3f, mask))
        {
            Debug.Log("👀 Ray hit: " + hit.transform.name);
            return hit.transform.IsChildOf(transform); // assumes mesh/model is child
        }

        return false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("✅ Player entered trigger");

            TooltipUI.instance.ShowTooltip($"Press E to pick up {itemName}");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            TooltipUI.instance.HideTooltip();
        }
    }
}
