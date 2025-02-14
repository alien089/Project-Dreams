using System;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    public Action<Sprite> OnItemPopUp;
    public Action<Condition> OnItemClicked;

    [SerializeField] private GameObject popUpPannel;
    private SpriteRenderer popUpRenderer;

    private void Awake()
    {
        popUpRenderer = popUpPannel.GetComponent<SpriteRenderer>();

        foreach (Item item in FindObjectsOfType<Item>())
        {

        }
    }
}
