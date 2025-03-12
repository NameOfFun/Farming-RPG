using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetZ;
    public float smoothing;
    private Transform posPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player gameObject in the Scence and get its transform component
        posPlayer = FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // position the cammera shoudle be in
        Vector3 targetPosition = new Vector3(posPlayer.position.x, posPlayer.position.y + offsetZ, posPlayer.position.z - offsetZ) ;

        // set the position accordingly
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
