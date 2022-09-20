using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductModelElement : MonoBehaviour, Interactable
{
    [SerializeField]
    GameObject UICanvas;

    //[SerializeField]
    //Text productNameText;

    [SerializeField]
    bool isPlaced = true;

    [SerializeField]
    GameObject highlighter;


    void Start() {
        ToggleUICanvas(false);
        //productNameText.text = value.ToString();
    }

    public void OnPointerEnter() {
        highlighter.SetActive(true);
        //Show Highlighter
        //if(isPlaced)
        //    ToggleUICanvas(true);
    }
    public void OnPointerExit()
    {
        highlighter.SetActive(false);
        //Hide Highlighter
        //ToggleUICanvas(false);
    }

    public void OnPointerClick() {
        GetComponent<BoxCollider>().enabled = false;
        //Show Interactable Canvas
        ToggleUICanvas(true);
    }

    void ToggleUICanvas(bool flag) {
        if(flag)
            UICanvas.transform.forward = Camera.main.transform.forward;
        UICanvas.SetActive(flag);
    }


    //int value = 0;
    public void OnMoveStart()
    {
        //Debug.LogError("Enter move product mode");
        //Hide canvas
        ToggleUICanvas(false);
        //value++;
        //productNameText.text = value.ToString();
        CameraPointer.Instance.StartRepositioningBehaviour(this.gameObject);
    }

    public void OnMoveEnd()    {
        //Debug.LogError("Exit move product mode");
        GetComponent<BoxCollider>().enabled = true;
        transform.parent = null;
    }


    public void OnRotateLeft()
    {
        //Rotate product to left by x degrees

    }
    public void OnRotateRight()
    {
        //Rotate product to right by x degrees
    }

    public void OnDelete() {
        AppManager.Instance.DeleteLocalProduct(0);

        Destroy(gameObject);
    }


}
