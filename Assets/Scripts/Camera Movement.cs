using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target; 
    
    private static Camera_Movement Instance;

    // private void Awake()
    // {
    //     if (Instance != null)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     
    //     Instance = this;
    //     DontDestroyOnLoad(gameObject);
    // }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
