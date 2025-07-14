using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public class ItemSpawner : MonoBehaviour
{
    private static ItemSpawner Instance;
    
    [SerializeField] List<GameObject> startingItems;
    [SerializeField] List<string> startingItemNames;
    [SerializeField] List<Vector2> startItemPositions;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void OnEnable()
    {
        GameEventsManager.Instance.inventoryEvents.addItemName += AddToNameList;
        
        
        for (int i = 0; i < startingItems.Count; i++) 
        {
            if (!startingItemNames.Contains(startingItems[i].name)) 
            {
                GameObject item = Instantiate(startingItems[i]);
                item.transform.position = startItemPositions[i];
                item.name = startingItems[i].name;
            }
        }
    }

    void OnDisable()
    {
        GameEventsManager.Instance.inventoryEvents.addItemName -= AddToNameList;
    }

    public void AddToNameList(string itemName)
    {
        startingItemNames.Add(itemName);
    }
}
