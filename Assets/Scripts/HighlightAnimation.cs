using UnityEngine;
using UnityEngine.UI;

public class HighlightAnimation : MonoBehaviour
{
    public GameObject highlightPrefab;
    public GameObject MainObjectPosition;
    public float duration = 0;
    public Image highlightImage;

    public bool higherThen1 = false;
    public bool lowerThen0 = false;

    public void Start()
    {
        highlightImage = highlightPrefab.GetComponent<Image>();
        highlightImage.transform.position = MainObjectPosition.transform.position;
    }
    public void Update()
    {
        if (duration >= 1)
        {
            higherThen1 = true;
            lowerThen0 = false;
        }
        else if (duration <= 0)
        {
            higherThen1 = false;
            lowerThen0 = true;
        }

        if (higherThen1)
        {
            duration -= Time.deltaTime;
        }
        else if (lowerThen0)
        {
            duration += Time.deltaTime;
        }

        highlightImage.color = new Color(highlightImage.color.r, highlightImage.color.g, highlightImage.color.b, duration);
    }
}
