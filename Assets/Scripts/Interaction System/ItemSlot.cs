using Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // item slot Icon, name and button
    public Image xIcon;
    public TextMeshProUGUI xName;
    [SerializeField] private Button _xButton;

    // general item and description
    [HideInInspector] public Sprite xItemImage;
    [HideInInspector] public string sDescription;

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
