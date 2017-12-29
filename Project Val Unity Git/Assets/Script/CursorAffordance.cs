using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance: MonoBehaviour {

    [SerializeField]
    Vector2 cursourHotspot = new Vector2(2f,2f);
    [SerializeField]
    Texture2D WalkCoursor = null;
    [SerializeField]
    Texture2D Enemy = null;
    [SerializeField]
    Texture2D CantDoAnything = null;

    CameraRaycaster cameraRaycaster;



	// Use this for initialization
	void Start () {

        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.onLayerChange += OnLayerChange;
    }
	
    void OnLayerChange(Layer newLayer)
    {
        switch (newLayer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(WalkCoursor, cursourHotspot, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(Enemy, cursourHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(CantDoAnything, cursourHotspot, CursorMode.Auto);
                return;
        }
    }
}
