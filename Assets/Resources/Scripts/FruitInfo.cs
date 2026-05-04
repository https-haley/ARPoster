using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitInfo : MonoBehaviour
{
    public string fruitName;
    public string price;
    [TextArea(3, 8)]
    public string healthBenefits;

    private Vector3 originalScale;
    private bool hasOriginalScale = false;
    private static FruitInfo lastSelected;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                    SelectFruit();
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                    SelectFruit();
            }
        }
#endif
    }

    void SelectFruit()
    {
        if (!hasOriginalScale)
        {
            originalScale = transform.localScale;
            hasOriginalScale = true;
        }

        if (lastSelected != null && lastSelected != this)
            lastSelected.ResetFruit();

        transform.localScale = originalScale * 1.15f;
        lastSelected = this;

        FruitDataHolder.FruitName = fruitName;
        FruitDataHolder.Price = price;
        FruitDataHolder.HealthBenefits = healthBenefits;

        SceneManager.LoadScene("FruitDetailScene");
    }

    void ResetFruit()
    {
        if (hasOriginalScale)
            transform.localScale = originalScale;
    }
}
