using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ItemCell : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject image;
    public GameObject text;
    public GameObject background;
    public int id; //클릭 식별용
    private Color originColor;

    public void Start()
    {
        originColor = background.GetComponent<Image>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        background.GetComponent<Image>().color = new Color(255, 0, 0);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        background.GetComponent<Image>().color = originColor;
    }



}
