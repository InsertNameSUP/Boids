using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 1;
    Boid[] Boids;
    // Start is called before the first frame update
    void Start()
    {
        Boids = GameObject.FindObjectsOfType<Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 determinedPos = Vector3.zero;
        foreach (Boid boid in Boids)
        {
            determinedPos += boid.transform.position;
        }
        //transform.position = Vector3.Lerp(transform.position, determinedPos / Boids.Length, Time.deltaTime * cameraSpeed);
       // transform.position = new Vector3(transform.position.x, transform.position.x, -10);
    }
}
