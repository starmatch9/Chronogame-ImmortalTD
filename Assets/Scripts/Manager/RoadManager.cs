using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoadManager : MonoBehaviour
{
    [Header("ËùÓÐÂ·¾¶")]
    public List<Tilemap> roads = new List<Tilemap>();

    // Start is called before the first frame update
    void Start()
    {
        GlobalData.globalRoads = roads;
    }
}
