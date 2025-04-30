using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private LayerMask obstacleLayer;

    private bool isCrashed = false;
    [SerializeField] private float fallSpeed = 60f;

    // Update is called once per frame
    void Update()
    {   
        // Handles player movement and rotation
        if (!isCrashed)
        {
            float targetAngle = rb.velocity.y > 0f ? 45f : (rb.velocity.y < -80f ? -45f : 0f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, -90f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.unscaledDeltaTime * 10f);
            transform.position += Vector3.down * fallSpeed * Time.unscaledDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // Call this method to make the player jump
    // This method is called when the player presses the space key
    void Jump()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }
        else
        {
            Debug.LogError("Rigidbody is not assigned in the inspector.");
        }
    }

    // Call this method to check for collisions with the pipes
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & obstacleLayer) != 0)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.bumpSound);
            isCrashed = true;
            Debug.Log("Collision with a pipe detected!");
            GameManager.Instance.GameOver();
        }
    }

    // Call this method to check for collisions with the score zone of the pipes
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("ScoreZone"))
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.scoreSound);
            GameManager.Instance.IncreaseScore();
        }
    }
}
