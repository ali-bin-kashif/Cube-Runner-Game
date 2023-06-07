using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class GameManager : MonoBehaviour
{
    //Player Script
    public PlayerMovement Player;

    //Variables for score and it's, increment variable's value will increase with time
    public float Score;
    float scoreIncrement = 0.4f;

    //Game platform spawn script
    PlatformSpawnScript platformSpawner;

    //Variable to store time elapsed
    float timeElapsed;

    //This will help to decide wether to increase speed and score increment after specific platforms
    bool readyToExecute;

    // Difficulty States
    enum DifficulyLevel { Easy, Medium, Hard } 

    DifficulyLevel levelState;

    bool gameStarted ,inMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        //Getting Platform Script Component in the variable
        platformSpawner = GetComponent<PlatformSpawnScript>();

        //Initially the difficulty state will easy obviously
        levelState = DifficulyLevel.Easy;

        //Main Menu will be displayed at the start of the game
        inMainMenu = true;

        //Setting Main Menu camera movement animation
        Player.CameraMovements.SetBool("InMainMenu", inMainMenu);

        //Set time scale to 1, in case if the game is paused and returned to main menu
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(inMainMenu & !gameStarted)
        {
            Player.transform.Rotate(Vector3.up * 10f * Time.deltaTime, Space.World);
        }*/
        //Only execute when the player is alive and game has started(i.e after play button)
        if(gameStarted && Player.isAlive)
        {
            

            timeElapsed += Time.deltaTime; //Incrementing time elapsed with delta time
            Score += scoreIncrement;    //Incrementing score with score increment

            //On every 19th platform set bool to true so that the block below can be executed
            if (platformSpawner.PlatformCount % 19 == 0)
            {
                readyToExecute = true;
            }

            /*Condition block for incrementing player's speed and score increment speed after
              20th platform to make the game more challenging */
            if (platformSpawner.PlatformCount % 20 == 0 && readyToExecute)
            {
                Player.ForwardSpeed += 2;
                scoreIncrement += 0.02f;
                readyToExecute = false;
            }

            //Set Difficulty State to Medium after 50 Platforms and add start spawning medium planes
            if (platformSpawner.PlatformCount > 50 && levelState == DifficulyLevel.Easy)
            {
                platformSpawner.AddMediumPlanes();
                levelState = DifficulyLevel.Medium;
            }

            //Set Difficulty State to Hard after 120 Platforms and add start spawning hard planes also
            if (platformSpawner.PlatformCount > 120 && levelState == DifficulyLevel.Medium)
            {
                platformSpawner.AddHardPlanes();
                levelState = DifficulyLevel.Hard;
            }
        }

        //Set gamestarted to false after player die
        if(!Player.isAlive)
        {
            gameStarted = false;
        }
        
    }


    //Method for starting the game
    public IEnumerator StartGame()
    {
        //Set main menu bool to false
        inMainMenu = false;

        //Stop main menu camera movements
        Player.CameraMovements.SetBool("InMainMenu", inMainMenu);

        //Start Gameplay camera movement animations
        Player.CameraMovements.SetTrigger("GameStarted");

        

        yield return new WaitForSeconds(1f);

        Player.transform.rotation = new Quaternion { x = 0, y = 0, z = 0, w = 0 };

        //Set Start game to true
        gameStarted = true;

        //Player is alive now
        Player.isAlive = true;

        

         
    }
}
