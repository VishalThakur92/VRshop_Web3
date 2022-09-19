using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductModelElement : MonoBehaviour, Interactable
{
    [SerializeField]
    CanvasGroup UICanvas;

    [SerializeField]
    bool isPlaced = true;


    void Start() {
        ToggleUICanvas(false);
    }

    public void OnPointerEnter() {
        if(isPlaced)
            ToggleUICanvas(true);
    }
    public void OnPointerExit() {
        ToggleUICanvas(false);
    }


    void ToggleUICanvas(bool flag) {
        UICanvas.interactable = flag;
        UICanvas.alpha = flag ? 1 : 0;
    }

    public void OnMoveSelected() {
        Debug.LogError("Enter move product mode");
    }
}
