using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private float speed = 20f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float endPoint;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // Call this method to move the object to the left
    private void Move()
    {
        float currSpeed = speed * GameManager.Instance.GetCurrentDifficulty();
        transform.position += Vector3.left * currSpeed * Time.deltaTime;

        if (transform.position.x < endPoint)
        {
            Destroy(gameObject);
        }
    }
}