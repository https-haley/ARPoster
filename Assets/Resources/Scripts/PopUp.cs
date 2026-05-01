using UnityEngine;

public class PopUp : MonoBehaviour
{
    Vector3 targetScale;

    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine(Animate());
    }

    System.Collections.IEnumerator Animate()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }
    }
}