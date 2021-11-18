using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewChange : MonoBehaviour
{
    public GameObject scrollView;
    GameObject content;
    RectTransform contentRectTransform, scrollRecttransform;
    Text text;

    float enterSize, lineValue;

    private void Awake()
    {
        content = gameObject;
        text = content.GetComponent<Text>();
        contentRectTransform = content.GetComponent<RectTransform>();
        scrollRecttransform = scrollView.GetComponent<RectTransform>();

        lineValue = (scrollRecttransform.rect.height / text.fontSize / 1.15f);
        enterSize = scrollRecttransform.rect.height / lineValue;
    }

    void SizeChange()
    {
        Rect rect = contentRectTransform.rect;
        rect.height = text.text.Split('\n').Length * enterSize;
        contentRectTransform.sizeDelta = new Vector2(0, rect.height);

        //Debug.Log($"ScrollHeight : {scrollRecttransform.rect.height}, ContentHeight : {rect.height}, lineValue : {lineValue}");
    }


    [ContextMenu("스크롤올리기")]
    public void ScrollUp()
    {
        ScrollMove(-enterSize);
    }

    [ContextMenu("스크롤내리기")]
    public void ScrollDown()
    {
        ScrollMove(enterSize);
    }

    public void ScrollMove(float f)
    {
        Debug.Log(contentRectTransform);
        if(contentRectTransform == null)
        {
            return;
        }

        Vector2 position = contentRectTransform.anchoredPosition;
        position.y += f;
        contentRectTransform.anchoredPosition = position;
    }

    void Update()
    {
        SizeChange();
    }
}
