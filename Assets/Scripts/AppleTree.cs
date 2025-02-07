using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject applePrefab;
    public GameObject branchPrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float changeDirChance = 0.1f;
    public float appleDropDelay = 1f;

    void Start()
    {
        //Dropping Apples
        Invoke("DropObject", 2f);
    }

    void DropObject(){
        GameObject fallingObject;

        if (Random.value < 0.20f)  // 15% chance to drop a branch
        {
            fallingObject = Instantiate(branchPrefab);
            fallingObject.tag = "Branch"; // Ensure correct tag
            Debug.Log("Branch spawned with tag: " + fallingObject.tag);
        }
        else
        {
            fallingObject = Instantiate(applePrefab);
        }

        fallingObject.transform.position = transform.position;
        Invoke("DropObject", appleDropDelay);
    }
    void Update()
    {
        //Movement and Direction

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed);       //Move right
        } else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed);      //Move left
        //} else if(Random.value < changeDirChance){
        //    speed *= -1;                    //Change directions
        }
    }

    void FixedUpdate(){
        if(Random.value < changeDirChance){
            speed *= -1;
        }
    }
}
