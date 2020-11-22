using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0.2f, 0.0f, -10f);
    public float DampingTime = 0.3f;
    public Vector3 Velocity = Vector3.zero;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(true);
    }

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(
            Target.position.x -Offset.x,
            Offset.y,
            Offset.z);
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp( // Unity Method to create smooth camera follow
               this.transform.position,
               destination,
               ref Velocity,
               DampingTime
            );
        }
        else
        {
            this.transform.position = destination;
        }
    }


}
