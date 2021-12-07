using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int startingBoidCount = 20;
    public float boidSpeed = 2;
    [Header("Line of Sight")]
    [Range(0, 10)] public float viewDistance = 3;
    [Range(0, 10)] public float avoidanceDistance = 2;
    [Header("Priorities")]
    [Range(0, 1)] public float alignmentWeighting = 0.1f;
    [Range(0, 1)] public float cohesionWeighting = 0.3f;
    [Range(0, 1)] public float seperationWeighting = 0.4f;
    [Range(0, 10)] public float randomnessWeighting = 0.2f;
    [Range(0, 1)] public float flockFriendliness = 0.75f;



    public IEnumerator SpawnBoid(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject newBoid = boidPrefab;
        Boid boid = newBoid.GetComponent<Boid>();
        boid.boidClass = (Boid.BirdClass)Random.Range(0, 3);
        boid.viewDistance = (float)Random.Range(1, 10);
        boid.avoidanceDistance = (float)Random.Range(1, 10);
        boid.alignmentWeighting = (float)Random.Range(0, 100)/100;
        boid.cohesionWeighting = (float)Random.Range(0, 100) / 100;
        boid.seperationWeighting = (float)Random.Range(0, 100) / 100;
        boid.randomnessWeighting = (float)Random.Range(0, 100) / 100;
        boid.flockFriendliness = (float)Random.Range(0, 100) / 100;
        boid.boidReactiveness = Random.Range(1, 5);
        Instantiate(newBoid, transform.position, Quaternion.Euler(0, 0, Random.Range(-180, 180)));
    }
    // Start is called before the first frame update
    void Start()
    {
     for(int i = 0; i < startingBoidCount; i++)
        {
            StartCoroutine(SpawnBoid(i/5));
        }
    }

    // Update is called once per frame
    void Update()
    {
       /* foreach(Boid boid in GameObject.FindObjectsOfType<Boid>())
        {
            boid.boidSpeed = boidSpeed;
            boid.viewDistance = viewDistance;
            boid.avoidanceDistance = avoidanceDistance;
            boid.alignmentWeighting = alignmentWeighting;
            boid.cohesionWeighting = cohesionWeighting;
            boid.seperationWeighting = seperationWeighting;
            boid.randomnessWeighting = randomnessWeighting;
            boid.flockFriendliness = flockFriendliness;
        }*/
    }
}
