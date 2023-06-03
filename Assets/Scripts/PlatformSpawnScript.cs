using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class PlatformSpawnScript : MonoBehaviour
{

    public Transform StartPoint; // Point from where platforms start to spawn

    public GameObject Platform; // Platform Prefab

    public int PlatformCount; // Count for platforms


    //[Header("Obstacle Planes")]
    [SerializeField]
    GameObject[] EasyPlanes, MediumPlanes, HardPlanes; // Arrays for planes(obstacles)

    
    public List<GameObject> Obstacles; //Planes objects will be added in this list

    // Start is called before the first frame update
    void Start()
    {
        AddEasyPlanes();

        for (int i=0; i<5; i++) //Spawn few platforms in start
        {
            SpawnPlatforms();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnPlatforms()
    {
        GameObject _platform = Instantiate(Platform, StartPoint.position, Quaternion.identity); //Spawn
        StartPoint = _platform.transform.GetChild(0);  //Get endpoint of platform and set it as startpoint for next platform
        PlatformCount++; // Increase platform count
        Debug.Log("Platform : " + PlatformCount);
    }

    public void AddEasyPlanes()
    {
        foreach (GameObject _plane in EasyPlanes)  //Adding easy planes in obstacle list
        {
            Obstacles.Add(_plane);
        }
    }

    public void AddMediumPlanes()
    {
        foreach (GameObject _plane in MediumPlanes)  //Adding easy planes in obstacle list
        {
            Obstacles.Add(_plane);
        }
    }

    public void AddHardPlanes()
    {
        foreach (GameObject _plane in HardPlanes)  //Adding easy planes in obstacle list
        {
            Obstacles.Add(_plane);
        }
    }

}
