using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimCovid.Core
{
    /// <summary>
    /// Controls the Camera Movement
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        //Camera uses Vector3
        private Vector3 _origin;
        private Vector3 _difference;
        private Vector3 _resultPosition;
        private bool _isDrag = false;
        [SerializeField] private LayerMask _uiLayerMask;
        private void LateUpdate()
        {
            HandlePointerDrag();
        }
        /*
        This function will move the camera when the mouse is down and moving (drag),
        resulting in a dragging effect
    */
        private void HandlePointerDrag()
        {
            if (Input.GetMouseButton(0))
            {
                if (!IsPointerOverUI())
                {
                    //Checks the difference between pointer and camera
                    _difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _mainCamera.transform.position;
                    //if it's not currently dragging, and left mouse is down, record the position 
                    if (!_isDrag)
                    {
                        _isDrag = true;
                        //Record the position of pointer to origin
                        _origin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
                _resultPosition = _origin - _difference;
                //Camera position is always -10
                _resultPosition.z = -10;
                _mainCamera.transform.position = _resultPosition;
            }
            else
            {
                _isDrag = false;
            }
        }
        //This function checks if the pointer is over an UI element by perofrming a raycast
        public bool IsPointerOverUI()
        {
            //Raycastall requires a pointerEventData, to record the pointer data
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            //Set the poimter positiom
            pointerEventData.position = Input.mousePosition;
            //Create a ;ist for results
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            //Perform the raycast, the result is returned to raycastResults
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            //Loop through all the elements in RaycasrResult
            for (int i = 0; i < raycastResults.Count; ++i)
            {
                //Check if the layer is equals to the UI layer
                if ((raycastResults[i].gameObject.layer & (1 << _uiLayerMask)) != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}