using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public enum DoorState {
        Closed,
        Opening,
        Open,
        Closing
    }

    private Transform transform;
    private Transform hingeTrans;

    private float rotationAngle = 0;

    public DoorState State = DoorState.Closed;
    public bool SwitchOpenDirection = false;

    [Range(1.0f, 179.0f)]
    public float OpenRange = -90.0f;

    public float Speed = 1;
    public float Channel = 1;

    public KeyCollect doorKey;
    public bool isOpen = false;
    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        hingeTrans = transform.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider collider)
    {
        if(doorKey.isCollected && (State == DoorState.Opening || State == DoorState.Closing)) {
            float angle = Speed;
            angle = Mathf.Min(angle * Time.deltaTime * 60, OpenRange);
            rotationAngle += angle;

            if (rotationAngle >= OpenRange) {
                angle -= (rotationAngle - OpenRange);

                if (State == DoorState.Closing) {
                    State = DoorState.Closed;
                }
                if (State == DoorState.Opening) {
                    State = DoorState.Open;
                }
            }

            transform.RotateAround(hingeTrans.position, Vector3.up, angle);
            isOpen = true;
        }
    }
}
