using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform player;
    public Transform mirrorCamera;
    public Transform mirrorPlane;

    private Vector2 distance;

    float cameraRotation;
    float planeRotation;
    void Start()
    {
        cameraRotation = mirrorCamera.eulerAngles.y;
        planeRotation = mirrorPlane.eulerAngles.y;
    }

    void Update()
    {
        distance = new Vector2(player.position.x - transform.position.x, player.position.z - transform.position.z);

        mirrorCamera.eulerAngles = new Vector3(
            mirrorCamera.eulerAngles.x,
            cameraRotation + (planeRotation - Angle(distance)),
            mirrorCamera.eulerAngles.z);
    }

    private float Angle(Vector2 vector2)
    {
        if(vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(vector2.x,vector2.y) * Mathf.Rad2Deg;
        }
    }
}
