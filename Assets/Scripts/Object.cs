using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Object : MonoBehaviour
{
    public SpriteRenderer thisAsset;
    public bool IsItem;
    

    private void OnEnable()
    {
        GameEventsManager.Instance.inventoryEvents.itemCollect += ItemCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.inventoryEvents.itemCollect -= ItemCollected;
    }

    void Start()
    {
        thisAsset = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        //thisAsset.sortingOrder = (int)(transform.position.y * -100);
        GetComponent<SortingGroup>().sortingOrder = (int)(transform.position.y * -100);
    }

    public void ItemCollected(float count)
    {
        // Destroy(gameObject);
    }

    
}
