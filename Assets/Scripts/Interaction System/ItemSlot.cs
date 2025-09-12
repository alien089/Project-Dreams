using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // item slot Icon and name 
    [HideInInspector] public Image xIcon;
    [HideInInspector] public TextMeshProUGUI xName;

    // general item and description
    [HideInInspector] public Sprite xItemImage;
    [HideInInspector] public string sDescription;

    private Button _xButton;

    private void Awake()
    {
        xName = GetComponentInChildren<TextMeshProUGUI>();
        _xButton = GetComponentInChildren<Button>();

        xIcon = _xButton.gameObject.GetComponent<Image>();

    }

    private void OnEnable()
    {
        _xButton.onClick.AddListener(new(OnInteraction));
    }

    private void OnDisable()
    {
        _xButton.onClick.RemoveAllListeners();
    }


    public void OnInteraction()
    {
        GameManager.Instance.XInteractableEventBus.TriggerEvent(InteractEventList.ON_ITEMSLOT_PRESSED, this);
    }
}
