using UnityEngine.UI;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _map;

    [SerializeField] private float multiplier;
    private string _mapTag = "Map";

    void Start()
    {
        _map = GameObject.FindGameObjectWithTag(_mapTag).GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        float mapSize = _slider.value * multiplier;
        _map.localScale = new Vector2(mapSize, mapSize);
    }
}
