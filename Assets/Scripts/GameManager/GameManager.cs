using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Collectible Tracking")]
    public int totalItems = 3;
    private int collectedItems = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ItemCollected()
    {
        collectedItems++;
        Debug.Log("USBs collected: " + collectedItems + " / " + totalItems);

        if (collectedItems >= totalItems)
        {
            Debug.Log("✅ All items collected! Trigger win logic here.");
            // TODO: Add win condition
        }
    }
}
