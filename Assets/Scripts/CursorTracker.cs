using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTracker : MonoBehaviour
{
    private RectTransform myrRectTransform;
    [SerializeField] private GameObject idleCursor, tapCursor;
    
    void Start()
    {
        myrRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        myrRectTransform.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            idleCursor.SetActive(false);
            tapCursor.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            idleCursor.SetActive(true);
            tapCursor.SetActive(false);
        }
        
    }
}
