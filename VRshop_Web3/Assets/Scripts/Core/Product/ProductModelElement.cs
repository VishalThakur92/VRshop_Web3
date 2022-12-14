using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace VRshop_Web3
{
    public class ProductModelElement : MonoBehaviour, IInteractable
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

        [SerializeField]
        UnityEvent OnPlayBehaviour;


        void Start()
        {
            ToggleUICanvas(false);
            Data.Events.OnProductPurchased += OnMoveStart;
            //productNameText.text = value.ToString();
        }

        public void OnPointerEnter()
        {
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

        public void OnPointerClick()
        {
            GetComponent<BoxCollider>().enabled = false;
            //Show Interactable Canvas
            ToggleUICanvas(true);
        }

        void ToggleUICanvas(bool flag)
        {
            if (flag)
                UICanvas.transform.forward = Camera.main.transform.forward;
            UICanvas.SetActive(flag);
        }

        public void OnCloseInteractionMenu()
        {
            ToggleUICanvas(false);
            GetComponent<BoxCollider>().enabled = true;
        }

        //int value = 0;
        public void OnMoveStart()
        {
            //Debug.LogError("Product Move start");
            GetComponent<BoxCollider>().enabled = false;
            ToggleUICanvas(false);
            Data.Events.OnProductRepositionStart.Invoke(gameObject);
            Data.Events.OnProductRepositionEnd += OnMoveEnd;
            isPlaced = false;
        }

        public void OnMoveEnd()
        {
            //Debug.LogError("Product Move end");
            GetComponent<BoxCollider>().enabled = true;
            transform.parent = null;
            Data.Events.OnProductRepositionEnd -= OnMoveEnd;
            Data.Events.OnProductPurchased -= OnMoveStart;
            isPlaced = true;
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

        public void OnPlaySpecialStart()
        {
            ToggleUICanvas(false);
            GetComponent<BoxCollider>().enabled = false;
            OnPlayBehaviour.Invoke();
            Data.Events.OnProductPlaySpecialStart.Invoke(gameObject);
            Data.Events.OnProductPlaySpecialEnd += OnPlaySpecialEnd;
        }


        public void OnPlaySpecialEnd()
        {
            GetComponent<BoxCollider>().enabled = true;
            Data.Events.OnProductPlaySpecialEnd -= OnPlaySpecialEnd;
        }

        public void IncreaseSize()
        {

            StartCoroutine(Resize(meshRef.transform.localScale.x, meshRef.transform.localScale.x + .3f));
        }
        public void ReduceSize()
        {

            StartCoroutine(Resize(meshRef.transform.localScale.x, meshRef.transform.localScale.x - .3f));
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

        IEnumerator Resize(float curScale, float startScale)
        {
            float startTime = Time.time;

            while (Time.time - startTime < 2f)
            {
                curScale = Mathf.Clamp(Mathf.MoveTowards(curScale, startScale, Time.deltaTime * 10), 0.5f, 3);
                meshRef.transform.localScale = new Vector3(curScale, curScale, curScale);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
