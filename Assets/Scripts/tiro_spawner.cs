using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tiro_spawner : MonoBehaviour
{

    public GameObject bullet;
    public GameObject gun;
    public Text HP;
    public int lifes = 5;
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Collider>().CompareTag("hole")){
            Destroy(collision.gameObject);
            HP.text = string.Format("{0}", --lifes);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBullet",0.1f,0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBullet(){
        float spawn_position_x = gun.transform.position.x;
        spawn_position_x = spawn_position_x + 1.31f;
        GameObject new_bullet = Instantiate(bullet, new Vector3(spawn_position_x, -3.5f, 0), Quaternion.identity);
        Destroy(new_bullet, 1.2f);
    }
}
