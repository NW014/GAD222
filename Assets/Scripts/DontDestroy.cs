using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy Instance;
    
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
}
