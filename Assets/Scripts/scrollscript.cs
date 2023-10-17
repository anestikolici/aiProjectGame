using UnityEngine;
using UnityEngine.UI;

public class Scrollscript : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;

    public float extraSpace = 100f;

    void Start()
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + extraSpace);
    }
}
