using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFloating : MonoBehaviour
{
    public int maxCnt;
    int cnt;
    public float value = 0.1f;


    void Update()
    {
        cnt++;
        Vector3 position = transform.position;
        if(cnt > maxCnt / 2)
        {
            position.y += value / 2;
        }
        else
        {
            position.y += value;
        }
        transform.position = position;

        if(cnt >= maxCnt)
        {
            cnt = 0;
            value = -value;
        }
    }
}
