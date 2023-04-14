using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borracha : MonoBehaviour
{
    private Vector2 startposition = new Vector2(0f, -2.56f);
    private Vector2 endposition = Vector2.zero;

    private float showDuration = 0.5f;
    private float duration = 1f;
    private int ftime = 60;

    private SpriteRenderer spriteRenderer;

    public bool IsHittable = true;

    public float minHideTime = 0.1f;
    public float maxHideTime = 0.3f;
    public float minShowTime = 0.1f;
    public float maxShowTime = 0.3f;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;
        while (ftime > 0)
        {
            float elapsed = 0f;
            float hideTime = Random.Range(minHideTime, maxHideTime);
            while (elapsed < hideTime)
            {
                transform.localPosition = Vector2.Lerp(start, end, elapsed / hideTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = end;

            float showTime = Random.Range(minShowTime, maxShowTime);
            yield return new WaitForSeconds(showTime);

            elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = start;
            ftime--;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ShowHide(startposition, endposition));
    }

    // Method to disable the borracha game object
    public void DisableBorracha()
    {
        gameObject.SetActive(false);
    }

    // Method to enable the borracha game object
    public void EnableBorracha()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowHide(startposition, endposition));
    }
}

