using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tooltipHeader;
    [SerializeField] TextMeshProUGUI _tooltipContent;
    [SerializeField] LayoutElement _tooltipLayout;
    [SerializeField] private int _characterWrapLimit;
    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            _tooltipHeader.gameObject.SetActive(false);
        }
        else
        {
            _tooltipHeader.text = header;
            _tooltipHeader.gameObject.SetActive(true);
        }
        _tooltipContent.text = content;
        int headerLength = _tooltipHeader.text.Length;
        int contentLength = _tooltipContent.text.Length;
        if (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) _tooltipLayout.enabled = true;
        else _tooltipLayout.enabled = false;
    }
    private void Update() 
    {
        if (Application.isEditor)
        {
            int headerLength = _tooltipHeader.text.Length;
            int contentLength = _tooltipContent.text.Length;
            if (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit) _tooltipLayout.enabled = true;
            else _tooltipLayout.enabled = false;
        }
        Vector2 position = Input.mousePosition;
        GetComponent<RectTransform>().pivot = new Vector2(position.x/Screen.width, position.y/Screen.height);
        GetComponent<RectTransform>().anchoredPosition = position;
    }
}
