using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    public Transform opposingWall;
    public bool negativeRange;
    private int randomOffset;
    public float wallPadding;
    Vector3 screenBounds;
    public enum WallPos
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public WallPos wallPos = WallPos.Left;
    // Start is called before the first frame update
    void Start()
    {
        if (negativeRange) randomOffset = -1;
        if (!negativeRange) randomOffset = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (wallPos)
        {
            case WallPos.Left:
                screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.transform.position.z));
                transform.position = new Vector3(screenBounds.x - 100, 0);
                break;
            case WallPos.Right:
                screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
                transform.position = new Vector3(screenBounds.x + 100, 0);
                break;
            case WallPos.Top:
                screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.transform.position.z));
                transform.position = new Vector3(0, screenBounds.y + 100);
                break;
            case WallPos.Bottom:
                screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
                transform.position = new Vector3(0, screenBounds.y - 100);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Boid>() != null)
        {
            Boid boid = collision.GetComponent<Boid>();
            boid.trail.emitting = false;
            switch (wallPos)
            {
                case WallPos.Left:
                    collision.transform.position = new Vector3(opposingWall.position.x - wallPadding, collision.transform.position.y, 0);
                    break;
                case WallPos.Right:
                    collision.transform.position = new Vector3(opposingWall.position.x + wallPadding, collision.transform.position.y, 0);
                    break;
                case WallPos.Top:
                    collision.transform.position = new Vector3(collision.transform.position.x, opposingWall.position.y + wallPadding, 0);
                    break;
                case WallPos.Bottom:
                    collision.transform.position = new Vector3(collision.transform.position.x, opposingWall.position.y - wallPadding, 0);
                    break;
            }
            boid.trail.Clear();
            boid.StartCoroutine(boid.ResetTrailRenderer());
        }
    }
}
