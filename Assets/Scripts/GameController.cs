using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject gun;
    public float reloadTime = 1f;
    public int borrachaLife = 100;

    private List<GameObject> borrachas = new List<GameObject>();
    private int borrachasInPlay = 0;
    private bool isReloading = false;

    private void Start()
    {
        // Find all borracha game objects in the scene and add them to the list
        GameObject[] borrachaObjects = GameObject.FindGameObjectsWithTag("borracha");
        foreach (GameObject borracha in borrachaObjects)
        {
            borrachas.Add(borracha);
        }
    }

    private void Update()
    {
        // If the gun is reloading, wait for reloadTime seconds
        if (isReloading)
        {
            return;
        }

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot a ray from the gun's position in the direction it is facing
            Ray ray = new Ray(gun.transform.position, gun.transform.right);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // If the ray hits a borracha, decrease its life
            if (hit.collider != null && hit.collider.CompareTag("borracha"))
            {
                Borracha borracha = hit.collider.GetComponent<Borracha>();
                if (borracha != null && borracha.IsHittable)
                {
                    borrachaLife--;
                    borracha.IsHittable = false;

                    // If the borracha's life reaches 0, disable it
                    if (borrachaLife <= 0)
                    {
                        borracha.DisableBorracha();
                        borrachasInPlay--;

                        // If all borrachas are disabled, reload
                        if (borrachasInPlay <= 0)
                        {
                            isReloading = true;
                            StartCoroutine(Reload());
                        }
                    }
                    else
                    {
                        // If the borracha is not dead, make it temporarily un-hittable
                        StartCoroutine(MakeBorrachaHittableAgain(borracha));
                    }
                }
            }
        }
    }

    private IEnumerator MakeBorrachaHittableAgain(Borracha borracha)
    {
        // Wait for a short time before making the borracha hittable again
        yield return new WaitForSeconds(0.1f);
        borracha.IsHittable = true;
    }

    private IEnumerator Reload()
    {
        // Wait for reloadTime seconds
        yield return new WaitForSeconds(reloadTime);

        // Enable all borrachas and reset their lives
        foreach (GameObject borracha in borrachas)
        {
            Borracha borrachaScript = borracha.GetComponent<Borracha>();
            if (borrachaScript != null)
            {
                borrachaScript.EnableBorracha();
                borrachaScript.IsHittable = true;
            }
        }

        // Reset the player's borrachaLife and the number of borrachas in play
        borrachaLife = 100;
        borrachasInPlay = borrachas.Count;

        // End the reloading process
        isReloading = false;
    }
}



