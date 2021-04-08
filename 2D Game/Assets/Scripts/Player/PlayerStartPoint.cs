using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private Player thePlayer;
    private CameraController theCamera;

    public Vector2 startDirection;

    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();

        if (thePlayer.startPoint == pointName)
        {
            thePlayer.transform.position = transform.position;
            thePlayer.lastMove = startDirection;

            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
