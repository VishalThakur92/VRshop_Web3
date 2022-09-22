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
using VRshop_Web3;

    /// <summary>
    /// Sends messages to gazed GameObject.
    /// </summary>
    public class CameraPointer : MonoBehaviour
    {
        [SerializeField]
        AudioSource buttonClickAudio;
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
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }
        private void Start()
        {
            layer_mask = LayerMask.GetMask("Interactable", "UI", "Floor");

            Data.Events.OnProductRepositionStart += StartRepositioningBehaviour;

        }

        private void OnDestroy()
        {
            Data.Events.OnProductRepositionStart -= StartRepositioningBehaviour;
        }

        //private void OnGUI()
        //{
        //    GUILayout.TextField($"Current Selectable = {currentSelectable}");
        //    GUILayout.TextField($"Current Raycast hit obj = {_gazedAtObject}");
        //}
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
                //Reposition obj at Ground point where player is looking
                if ((hit.transform.gameObject.layer == 3 || hit.transform.gameObject.layer == 6) && repositionObj != null)
                {
                    repositionHelperContainer.position = Vector3.Lerp(repositionHelperContainer.position, hit.point, Time.deltaTime * 10);

                    //Place Obj only is is placed on Ground not otherwise
                    if (Input.GetMouseButtonUp(0))
                    {
                        buttonClickAudio.Play();
                        Data.Events.OnProductRepositionEnd.Invoke();
                        //repositionObj.GetComponent<ProductModelElement>().OnMoveEnd();
                        repositionObj = null;
                    }
                }

                // A different GameObject detected in front of the camera.
                if (_gazedAtObject != hit.transform.gameObject)
                {

                    reticle.color = interactableColor;

                    _gazedAtObject?.GetComponent<IInteractable>()?.OnPointerExit();
                    // New GameObject.
                    //_gazedAtObject?.SendMessage("OnPointerExit");
                    _gazedAtObject = hit.transform.gameObject;

                    if (_gazedAtObject?.GetComponent<Selectable>())
                    {
                        currentSelectable = _gazedAtObject.GetComponent<Selectable>();
                    }
                    //_gazedAtObject.SendMessage("OnPointerEnter");
                    //if (Input.GetMouseButtonUp(0)) {
                    //    Debug.LogError("On Tap" + Time.time);
                    //    CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                    //}
                    //CurvedUIInputModule.CustomControllerButtonState = 

                }

                hit.transform.gameObject.GetComponent<IInteractable>()?.OnPointerEnter();
            }
            else
            {
                //cant place obj, Float obj in air
                if (repositionObj != null)
                    repositionHelperContainer.transform.position = Vector3.Lerp(repositionHelperContainer.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 3.5f, Time.deltaTime * 10);

                //Vector3 newPos = Camera.main.transform.position + Camera.main.transform.forward;
                //repositionHelperContainer.position = newPos;
                //repositionHelperContainer.position = new Vector3(repositionHelperContainer.position.x, repositionHelperContainer.position.y, repositionHelperContainer.position.z +3);
                reticle.color = nonInteractableColor;
                //reticle.rectTransform.sizeDelta = Vector2.Lerp(reticle.rectTransform.sizeDelta, normalRect, Time.deltaTime * 6);
                // No GameObject detected in front of the camera.

                _gazedAtObject?.GetComponent<IInteractable>()?.OnPointerExit();
                //CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject.GetComponent<Button>().OnPointerExit.
                //_gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = null;
                currentSelectable = null;
            }

            // Checks for screen touches.

            //if (Google.XR.Cardboard.Api.IsTriggerPressed)
            if (Input.GetMouseButtonUp(0))
            {
                _gazedAtObject?.GetComponent<IInteractable>()?.OnPointerClick();

                _gazedAtObject?.GetComponent<Button>()?.onClick.Invoke();

                CurvedUI.CurvedUIEventSystem.instance.currentSelectedGameObject?.GetComponent<Button>()?.onClick.Invoke();
                CurvedUI.CurvedUIEventSystem.instance.SetSelectedGameObject(null, null);
                buttonClickAudio.Play();
            }
        }

        void StartRepositioningBehaviour(GameObject obj)
        {
            repositionObj = obj;
            obj.transform.SetParent(repositionHelperContainer, true);
            obj.transform.localPosition = Vector3.zero;
        }

    }
