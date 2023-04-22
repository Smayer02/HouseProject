using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCommands : MonoBehaviour
{
    public bool buttonPressed = false;
    public Button buttonObject;

    public void OnButtonPress() {
        Time.timeScale = 1;
        buttonPressed = true;
        buttonObject.gameObject.SetActive(false);
    }
}
