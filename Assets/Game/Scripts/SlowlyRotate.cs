using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
    public float Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speed = Time.deltaTime * Speed;
        var rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(speed + rot.x, speed + rot.y, speed + rot.z);
    }
}
