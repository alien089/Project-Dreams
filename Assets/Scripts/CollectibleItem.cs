
using UnityEditor;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public InventoryItem item; // L'oggetto da aggiungere all'inventario

    void OnMouseDown()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>(); //attach to singleton megamanager gamemanager.Inventory
        inventoryManager.AddItem(item);
        Destroy(gameObject); // Rimuovi l'oggetto dalla scena dopo il clic
    }
}

