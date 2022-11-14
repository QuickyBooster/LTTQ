using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManagerLobby : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image _img;
    [SerializeField] Sprite _default, _pressed;
    [SerializeField] AudioClip _compressClip, _uncompressClip;
    [SerializeField] AudioSource _audioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default; 
    }
}
