using UnityEngine;

public class CameraController : MonoBehaviour 
{
    [SerializeField] private Camera _mainCamera;
    //Camera uses Vector3
    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 _resultPosition;
    private bool _isDrag = false;
    private void LateUpdate() 
    {
        if (Input.GetMouseButton(0))
        {
            _difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - _mainCamera.transform.position;
            //check if this is the first click
            if (!_isDrag)
            {
                //record the original position if it is the firt click
                _isDrag = true;
                _origin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            _resultPosition =  _origin - _difference;
            _resultPosition.z = -10;
            _mainCamera.transform.position = _resultPosition;
        }
        else _isDrag = false;
    }
}