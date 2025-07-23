using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ItemEventManager ItemEvents;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        //else Destroy(gameObject);

        ItemEvents = GetComponentInChildren<ItemEventManager>();
    }

}
