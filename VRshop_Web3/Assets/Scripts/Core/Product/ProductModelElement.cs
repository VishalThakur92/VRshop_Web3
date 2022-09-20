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

    [SerializeField]
    GameObject meshRef;


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
        ToggleUICanvas(false);
        Data.DataEvents.OnProductRepositionStart.Invoke(gameObject);
        Data.DataEvents.OnProductRepositionEnd += OnMoveEnd;
    }

    public void OnMoveEnd()    {
        GetComponent<BoxCollider>().enabled = true;
        transform.parent = null;
        Data.DataEvents.OnProductRepositionEnd -= OnMoveEnd;
    }


    public void RotateLeft()
    {
        //Rotate product to left by x degrees
        StartCoroutine(Rotate(Vector3.up));

    }
    public void RotateRight()
    {
        //Rotate product to right by x degrees
        StartCoroutine(Rotate(-Vector3.up));
    }


    public void IncreaseSize()
    {

        StartCoroutine(Resize(meshRef.transform.localScale.x, meshRef.transform.localScale.x +.3f ));
    }
    public void ReduceSize()
    {

        StartCoroutine(Resize(meshRef.transform.localScale.x, meshRef.transform.localScale.x- .3f));
    }

    public void OnDelete() {
        AppManager.Instance.DeleteLocalProduct(0);

        Destroy(gameObject);
    }

    IEnumerator Rotate(Vector3 rotation)
    {
        float startTime = Time.time;

        while (Time.time - startTime < .05f)
        {
            meshRef.transform.Rotate(rotation);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Resize(float curScale , float startScale)
    {
        float startTime = Time.time;

        while (Time.time - startTime < 2f)
        {
            curScale = Mathf.Clamp(Mathf.MoveTowards(curScale, startScale, Time.deltaTime * 10) , 0.5f , 3);
            meshRef.transform.localScale = new Vector3(curScale, curScale, curScale);
            yield return new WaitForEndOfFrame();
        }
    }
}
