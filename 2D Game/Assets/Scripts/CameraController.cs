using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    private float movementSpeed = 5f;

    // To stop creating duplicates when camera moves around scenes
    private static bool cameraExists;

    // Start is called before the first frame update
    void Start()
    {
        // Check if player already exist in the new scene
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
}
