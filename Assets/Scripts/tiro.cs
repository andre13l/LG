using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tiro : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;

    void Start(){
        InvokeRepeating("SpawnBullet",0,1);
    }

    void Update()
    {
        
    }

    void SpawnBullet(){
        float spawn_position_x = gun.transform.position.x;
        float spawn_position_y = gun.transform.position.y;
        Instantiate(bullet, new Vector3(spawn_position_x, spawn_position_y, 0), Quaternion.identity);
    }
}
