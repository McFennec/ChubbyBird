using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Transform[] spriteList;

    private float spriteCount;
    [SerializeField] private float endPoint = -254f;
    [SerializeField] private float spriteWidth = 128f;
    [SerializeField] private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        spriteCount = spriteList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    // Call this method to scroll the sprites to the left
    void Scroll()
    {
        foreach (Transform hill in spriteList)
        {
            hill.position += Vector3.left * speed * GameManager.Instance.GetCurrentDifficulty() * Time.deltaTime;

            if (hill.position.x <= endPoint)
            {
                hill.position = new Vector3(hill.position.x + spriteWidth * spriteCount, hill.position.y, hill.position.z);
            }
        }
    }
}
