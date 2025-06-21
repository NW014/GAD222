using UnityEngine;

public class EventSystemSingleton : MonoBehaviour
{
    private static EventSystemSingleton Instance;

    private void Awake()
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
