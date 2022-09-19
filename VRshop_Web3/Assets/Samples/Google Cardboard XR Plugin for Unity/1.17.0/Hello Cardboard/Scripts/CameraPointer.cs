//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    [SerializeField]
    Image reticle;


    [SerializeField]
    Color nonInteractableColor, interactableColor;

    public Transform repositionHelperContainer;
    [SerializeField]
    GameObject repositionObj;

    Selectable currentSelectable;
    public static CameraPointer Instance { get; private set; }
    int layer_mask;

    private void Awake()
    {
#if UNITY_EDITOR
        rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

#endif
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        layer_mask = LayerMask.GetMask("Interactable", "UI");
    }
#if UNITY_EDITOR
    float rotY = 0;
    float rotX = 0;
    float mouseY = 0;
    float mouseX = 0;

    Vector3 rot;
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * 500  * Time.deltaTime;
        rotX += mouseY * 500 * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -80, 80);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation , localRotation , Time.deltaTime * 100);
    }
#endif


    private void OnGUI()
    {
        GUILayout.TextField($"Current Selectable = {currentSelectable}");
        GUILayout.TextField($"Current Raycast hit obj = {_gazedAtObject}");
    }
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance, layer_mask))
        {
            if (hit.transform.gameObject.layer == 3 && repositionObj != null)
                repositionHelperContainer.position = Vector3.Lerp(repositionHelperContainer.position, hit.point, Time.deltaTime * 10);
            //hit.
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                _gazedAtObject?.GetComponent<Interactable>()?.OnPointerExit();
                reticle.color = interactableColor;
                // New GameObject.
                //_gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;

                if (_gazedAtObject?.GetComponent<Selectable>())
                {
                    currentSelectable = _gazedAtObject.GetComponent<Selectable>();
                }
                //_gazedAtObject.SendMessage("OnPointerEnter");
                _gazedAtObject.GetComponent<Interactable>()?.OnPointerEnter();
                //if (Input.GetMouseButtonUp(0)) {
                //    Debug.LogError("On Tap" + Time.time);
                //    CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                //}
                //CurvedUIInputModule.CustomControllerButtonState = 

            }
        }
        else
        {
            //Vector3 newPos = Camera.main.transform.position + Camera.main.transform.forward;
            //repositionHelperContainer.position = newPos;
            //repositionHelperContainer.position = new Vector3(repositionHelperContainer.position.x, repositionHelperContainer.position.y, repositionHelperContainer.position.z +3);
            reticle.color = nonInteractableColor;
            //reticle.rectTransform.sizeDelta = Vector2.Lerp(reticle.rectTransform.sizeDelta, normalRect, Time.deltaTime * 6);
            // No GameObject detected in front of the camera.

            _gazedAtObject?.GetComponent<Interactable>()?.OnPointerExit();
            //CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject.GetComponent<Button>().OnPointerExit.
            //_gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
            currentSelectable = null;
        }

        // Checks for screen touches.
#if UNITY_EDITOR

        if (Input.GetMouseButtonUp(0))
#else

        if (Google.XR.Cardboard.Api.IsTriggerPressed)
#endif
        {
            if (repositionObj)
            {
                repositionObj.GetComponent<ProductModelElement>().OnMoveEnd();
                repositionObj = null;
            }
            else
            {

                _gazedAtObject?.GetComponent<Interactable>()?.OnPointerClick();
                _gazedAtObject?.GetComponent<Button>()?.onClick.Invoke();

                CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject?.GetComponent<Button>()?.onClick.Invoke();

                //Debug.LogError(EventSystem.current.gameObject?.name);
            }
        }
    }

    public void StartRepositioningBehaviour(GameObject obj)
    {
        repositionObj = obj;
        obj.transform.SetParent(repositionHelperContainer, true);
        obj.transform.localPosition = Vector3.zero;
    }

}
