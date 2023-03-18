using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handler for ModalWindow
/// </summary>
public class ModalWindowController : MonoBehaviour
{
    [SerializeField] private GameObject _modalWindow;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Image _image;
    public void Hide()
    {
        _modalWindow.SetActive(false);
    }
    public void SetContent(string header, Sprite image, string contentText, string buttonText)
    {
        if (header == null)
        {
            _headerText.enabled = false;
        }
        else
        {
            _headerText.text = header;
            _headerText.enabled = true;
        }
        if (_contentText == null)
        {
            _contentText.enabled = false;
        }
        else
        {
            _contentText.text = contentText;
            _contentText.enabled = true;
        }
        if (image == null)
        {
            _image.gameObject.SetActive(false);
        }
        else
        {
            _image.sprite = image;
            _image.gameObject.SetActive(true);
        }
        _buttonText.text = buttonText;
    }
}
