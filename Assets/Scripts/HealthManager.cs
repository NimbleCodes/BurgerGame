using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float fDelayTime;
    public float fCurrentTime;
    public int iMaxX;
    private float fVertical;
    void Start()
    {
        iMaxX = (int)GetComponent<RectTransform>().rect.width;
        fVertical = transform.Find("SlidingArea").Find("Handle").gameObject.GetComponent<RectTransform>().offsetMax.y;

        fDelayTime = 10.0f;
        fCurrentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        fCurrentTime += Time.deltaTime;

        float fT = iMaxX * (fCurrentTime / fDelayTime);
        transform.Find("SlidingArea").Find("Handle").
            gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(fVertical + fT, 0);
    }
}
