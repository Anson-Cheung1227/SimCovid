using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
        if (Input.GetMouseButton(0))
        {
            if (!isPointerOverUI())
            {
                _difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _mainCamera.transform.position;
                if (!_isDrag)
                {
                    _isDrag = true;
                    _origin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            _resultPosition =  _origin - _difference;
            _resultPosition.z = -10;
            _mainCamera.transform.position = _resultPosition;
        }
        else _isDrag = false;
    }
    private bool isPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for (int i = 0; i < raycastResults.Count; ++i)
        {
            if ((raycastResults[i].gameObject.layer & (1 << _uiLayerMask)) != 0)
            {
                return true;
            }
        }
        return false;
    }
}