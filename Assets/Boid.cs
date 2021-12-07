using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public enum BirdClass
    {
        Red,
        Orange,
        Green,
        Blue
    }
    public TrailRenderer trail;
    public BirdClass boidClass;
    public Vector3 velocity;
    public float boidSpeed = 2;
    [Range(0, 1)] public float alignmentWeighting;
    [Range(0, 1)] public float cohesionWeighting;
    [Range(0, 1)] public float seperationWeighting;
    [Range(0, 10)] public float randomnessWeighting;
    [Range(0, 1)] public float flockFriendliness;
    public bool DrawGizmos = false;
    List<Boid> Neigbours = new List<Boid>();
    List<Boid> AvoidanceNeighbours = new List<Boid>();
    [Range(0, 10)]public float viewDistance;
    [Range(0, 10)] public float avoidanceDistance;
    [Range(0, 20)] public float boidReactiveness;
    public IEnumerator ResetTrailRenderer()
    {
        yield return new WaitForSeconds(.1f);
        trail.Clear();
        trail.emitting = true;
    }
        void UpdateNeighbours()
    {
        Neigbours.Clear();
        AvoidanceNeighbours.Clear();
        Boid[] _neighbours = GameObject.FindObjectsOfType<Boid>();
        foreach(Boid boid in _neighbours)
        {
            if (boid == this) continue;
            if (Vector3.Distance(transform.position, boid.transform.position) <= viewDistance && boid.boidClass == this.boidClass) Neigbours.Add(boid)  ;
            if (Vector3.Distance(transform.position, boid.transform.position) <= avoidanceDistance) Neigbours.Add(boid);
        }
    }
    private void Awake()
    {
        if (boidClass == BirdClass.Blue) GetComponent<SpriteRenderer>().color = Color.blue;
        if (boidClass == BirdClass.Green) GetComponent<SpriteRenderer>().color = Color.green;
        if (boidClass == BirdClass.Red) GetComponent<SpriteRenderer>().color = Color.red;
        if (boidClass == BirdClass.Orange) GetComponent<SpriteRenderer>().color = new Color(255, 165, 0);
        velocity = transform.up.normalized;
        if(trail)
        {
            trail.startColor = GetComponent<SpriteRenderer>().color;
            trail.endColor = Color.black;
        }
    }
    Vector3 Alignment()
    {
        Vector3 alignmentDirection = Vector3.zero;
        int alignmentCount = 0;
        foreach(Boid boid in Neigbours)
        {
            alignmentDirection += boid.transform.up;
            alignmentCount++;
        }
        if (alignmentCount == 0) return Vector3.zero;
        else return alignmentDirection /= alignmentCount;
    }
    Vector3 Cohesion()
    {
        Vector3 cohesionDirection = Vector3.zero;
        int cohesionCount = 0;
        foreach (Boid boid in Neigbours)
        {
            cohesionDirection += boid.transform.position;
            cohesionCount++;
        }
        if (cohesionCount == 0) return Vector3.zero;
        else
        {
            cohesionDirection /= cohesionCount;
            cohesionDirection -= transform.position;
            return cohesionDirection;
        }
    }
    Vector3 Seperation()
    {
        Vector3 seperationDirection = Vector3.zero;
        int seperationCount = 0;
        foreach (Boid boid in Neigbours)
        {
            seperationDirection += (boid.transform.position - transform.position);
            if (boid.boidClass == this.boidClass) seperationDirection *= (1 - flockFriendliness);
            seperationCount++;
        }
        if (seperationCount == 0) return Vector3.zero;
        else
        {
            seperationDirection /= seperationCount;
            seperationDirection *= -1;
            return seperationDirection;
        }


    }
    void Update()
    {
        UpdateNeighbours();
        if(Alignment() + Cohesion() + Seperation() != Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, (Alignment().normalized * alignmentWeighting) + (Cohesion().normalized * cohesionWeighting) + (Seperation().normalized * seperationWeighting) + (new Vector3(Random.Range(-1,1), Random.Range(-1, 1), 0).normalized * randomnessWeighting), Time.deltaTime * boidReactiveness);
        }
        transform.position += velocity * Time.deltaTime * boidSpeed;
        transform.up = velocity.normalized;
    }
    private void OnDrawGizmos()
    {
        if(DrawGizmos)
        {
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }
    }
}
