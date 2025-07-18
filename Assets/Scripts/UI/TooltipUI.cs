using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI instance;

    [SerializeField] private Text tooltipText;
    [SerializeField] private GameObject tooltipPanel;

    void Awake()
    {
        instance = this;
        HideTooltip();
    }

    public void ShowTooltip(string message)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = message;
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}
