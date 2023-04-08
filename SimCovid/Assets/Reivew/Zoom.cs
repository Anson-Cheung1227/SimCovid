using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Slider slider;
    public Transform map;

    public float multiplier;
    string mapTag = "Map";

    void Start()
    {
        map = GameObject.FindGameObjectWithTag(mapTag).GetComponent<Transform>();
    }

    public void FixedUpdate()
    {

        float mapSize = slider.value * multiplier;
        Debug.Log(mapSize);
        map.localScale = new Vector2(mapSize, mapSize);

    }

}
