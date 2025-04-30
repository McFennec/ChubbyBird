using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private  float baseSpeed = 20f;
    [SerializeField] private  float baseDistance = 80f;
    [SerializeField] private  float verticalRangeTop = 50f;
    [SerializeField] private  float verticalRangeBot = 50f;
    [SerializeField] private  float endPoint;
    private float currentSpeed;
    private float currentTimeInterval;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentValues();
        Spawn();
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentValues();

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            ResetTimer();
        }
    }

    // Call this method to update the current speed and time interval
    void UpdateCurrentValues()
    {
        currentSpeed = baseSpeed * GameManager.Instance.GetCurrentDifficulty();
        currentTimeInterval = baseDistance / currentSpeed;
    }

    // Call this method to reset the timer
    void ResetTimer()
    {
        timer = currentTimeInterval;
    }

    // Call this method to spawn the prefab at a random position within the specified range
    void Spawn()
    {
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            transform.position.y + Random.Range(-verticalRangeBot, verticalRangeTop),
            transform.position.z
        );

        GameObject myPrefab = Instantiate(prefab, spawnPos, Quaternion.identity);
        Mover myMover = myPrefab.GetComponent<Mover>();
        if (myMover != null)
        {
            myMover.Speed = baseSpeed;
            myMover.endPoint = endPoint;
        }
        else
        {
            Debug.LogError("Mover component not found on the pipe prefab.");
        }
    }
}
