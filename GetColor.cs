using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetColor : MonoBehaviour
{

  [SerializeField]
  private GameObject _greenSetter, _redSetter, _blueSetter;

  private float _red, _blue, _green;

  private DragMirror _mirrorB, _mirrorM, _mirrorT;


  private Image _image;
  private Color m_NewColor;

  void Start()
  {
    _mirrorT = _redSetter.GetComponent<DragMirror>();
    _mirrorM = _greenSetter.GetComponent<DragMirror>();
    _mirrorB = _blueSetter.GetComponent<DragMirror>();
    _image = GetComponent<Image>();
  }

  void Update()
  {
    _green = (_greenSetter.transform.position.x - _mirrorB.minX) * 0.04f;
    _red = (_redSetter.transform.position.x - _mirrorB.minX) * 0.04f;
    _blue = (_blueSetter.transform.position.x - _mirrorB.minX) * 0.04f;
    m_NewColor = new Color(_red, _green, _blue, 1f);
    _image.color = m_NewColor;
    Debug.Log(m_NewColor);
    Debug.Log("Blue " + _blue);
  }
}