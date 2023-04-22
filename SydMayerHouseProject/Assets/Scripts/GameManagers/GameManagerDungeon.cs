using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerDungeon : MonoBehaviour
{

    public KeyCollect keyG;
    public DoorOpener guild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (guild.isOpen) {
            SceneManager.LoadScene("GuildHall");
        }
    }
}

