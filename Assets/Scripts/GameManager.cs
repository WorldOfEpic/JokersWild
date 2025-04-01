using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public AudioSource bgMusic;
    public DeckManager gm;

    void Start()
    {
        bgMusic = GetComponent<AudioSource>();

        gm.startGame();
        bgMusic.Play();
        
    }
    void Update()
    {
        if(gm.pHealth == 0)
        {
            //load scene death
            SceneManager.LoadScene("DeathScreen");
        }
        if(gm.npcHealth == 0)
        {
            SceneManager.LoadScene("WinScreen");
            //load win screen
        }
    }

 
}