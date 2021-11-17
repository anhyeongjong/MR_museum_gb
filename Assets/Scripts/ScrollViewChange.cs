using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewChange : MonoBehaviour
{
    public GameObject scrollView;
    GameObject content;
    RectTransform rectTransform;
    Text text;

    float enterSize;

    private void Start()
    {
        content = gameObject;
        text = content.GetComponent<Text>();
        rectTransform = content.GetComponent<RectTransform>();
        enterSize = scrollView.GetComponent<RectTransform>().rect.height / (100f / text.fontSize);
    }


    void Update()
    {
        Rect rect = rectTransform.rect;
        rect.height = text.text.Split('\n').Length * enterSize;
        rectTransform.sizeDelta = new Vector2(0, rect.height);
    }
}
