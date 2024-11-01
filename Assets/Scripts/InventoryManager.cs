using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // Lista degli oggetti raccolti
    public Transform inventoryGrid; // Il contenitore della griglia
    public GameObject itemPrefab; // Prefab dell'icona dell'oggetto
    public Image artworkDisplay; // Mostra l'artwork selezionato
    public Text descriptionText; // Mostra la descrizione

    private InventoryItem selectedItem; // L'oggetto attualmente selezionato

    void Start()
    {
        UpdateInventoryUI();
    }

    // Aggiungi un oggetto all'inventario
    public void AddItem(InventoryItem newItem)
    {
        items.Add(newItem);
        UpdateInventoryUI();
    }

    // Aggiorna la UI dell'inventario
    void UpdateInventoryUI()
    {
        // Pulisci la griglia esistente
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject); // Usa Destroy per rimuovere gli oggetti UI precedenti
        }

        //setta numero fisso griglia per pagina, crei (es) 16 gameobkect vuoti e li riempi e non istanzi, cambio pagina pulisco e ri riempio.

        // Aggiungi nuovi oggetti alla griglia
        foreach (InventoryItem item in items)
        {
            GameObject newItem = Instantiate(itemPrefab, inventoryGrid); // Usa Instantiate per creare nuovi oggetti UI
            newItem.GetComponent<Image>().sprite = item.icon;

            // Aggiungi un listener per quando l'oggetto viene cliccato
            newItem.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));
        }
    }

    // Seleziona un oggetto nell'inventario
    void SelectItem(InventoryItem item)
    {
        selectedItem = item;
        descriptionText.text = item.description;
        artworkDisplay.sprite = item.artwork;
    }
}
