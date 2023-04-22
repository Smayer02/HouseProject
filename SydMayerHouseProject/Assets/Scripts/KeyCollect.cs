using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    public bool isCollected = false;

    void OnTriggerEnter (Collider collider) {
        isCollected = true;
        Destroy(gameObject);
    }
}
