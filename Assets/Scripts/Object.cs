using UnityEngine;
using UnityEngine.Rendering;

public class Object : MonoBehaviour
{
    public SpriteRenderer thisAsset;
    
    void Start()
    {
        //thisAsset = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        //thisAsset.sortingOrder = (int)(transform.position.y * -100);
        GetComponent<SortingGroup>().sortingOrder = (int)(transform.position.y * -100);
    }
}
