using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PlatformSpawnScript platformSpawner;
    public GameObject FuelPrefab;
    public GameObject CoinPrefab;

    private void Start()
    {
        //Assign Platform Spawn Object 
        platformSpawner = GameObject.FindObjectOfType<PlatformSpawnScript>();

        //Generate random number according to Obstacle array lenght
        int randomObstacleNumber = Random.Range(0, platformSpawner.Obstacles.Count);

        //Instantiate Obstacle GameObject
        GameObject obs = Instantiate(platformSpawner.Obstacles[randomObstacleNumber], transform.position, Quaternion.identity);

        //Set current platform as parent of obstacle object
        obs.transform.SetParent(transform,true);

        //Getting Transform child objects to spawn collectibles
        List<Transform> collectiblesPoints = GetChildsWithTag(obs.transform, "CollectiblesPoint");

        int randomNumber;

        //Spawn Fuel Prefab at random transform point in every 4th platform
        if(platformSpawner.PlatformCount % 4 == 0)
        {
            randomNumber = Random.Range(0, collectiblesPoints.Count); // Generate random number
            GameObject fuel = Instantiate(FuelPrefab, collectiblesPoints[randomNumber].position, Quaternion.identity);
            fuel.transform.SetParent(transform, true);
            collectiblesPoints.RemoveAt(randomNumber); // Removes the transforms at which fuel is instantiated
        }

        //Spawn Coin Prefab at random transform point
        randomNumber = Random.Range(0, collectiblesPoints.Count);
        GameObject coin = Instantiate(CoinPrefab, collectiblesPoints[randomNumber].position, Quaternion.identity);
        coin.transform.SetParent(transform, true);

    }


    private void OnTriggerEnter(Collider other)
    {
        //If player enters the platform's trigger then spawn more platforms and delete previous one

        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 1f);
            platformSpawner.SpawnPlatforms();

        }
    }


    //Method to get childs with specific tag in a transform
    private List<Transform> GetChildsWithTag(Transform _parent, string _tag)
    {
        List<Transform> list = new List<Transform>();
        foreach(Transform child in _parent)
        {
            if(child.CompareTag(_tag))
            {
                list.Add(child);
            }  
        }
        return list;
    }
}
