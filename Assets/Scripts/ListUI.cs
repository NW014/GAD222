using System;
using UnityEngine;
using UnityEngine.UI;

public class ListUI : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
    }
}
