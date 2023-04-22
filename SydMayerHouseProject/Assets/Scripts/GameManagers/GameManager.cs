using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public KeyCollect keyD, keyL;
    public DoorOpener dungeon, lab;

    public static bool dungeonKey = false;

    // Start is called before the first frame update
    void Start()
    {
        if (dungeonKey) {
            keyL.gameObject.SetActive(true);
        }
        else {
            keyL.gameObject.SetActive(false);
            Time.timeScale = 0;
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if (dungeon.isOpen) {
            SceneManager.LoadScene("Dungeon");
            dungeonKey = true;
        }
        
        else if (lab.isOpen && dungeonKey) {
            SceneManager.LoadScene("Lab");
        }
    }
}
