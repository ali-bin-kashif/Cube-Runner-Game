using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{

    Animator playButton;
    MeshCollider collider;

    public GameUI GameUserInterface;
 
    // Start is called before the first frame update
    void Start()
    {
        playButton = GetComponent<Animator>();
        collider = GetComponent<MeshCollider>();
    }

    private void OnMouseUpAsButton()
    {
        collider.enabled = false; 
        playButton.SetTrigger("ButtonPress");
        StartCoroutine(GameUserInterface.PlayButton());
    }

}
