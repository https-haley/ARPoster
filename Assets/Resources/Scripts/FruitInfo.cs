using UnityEngine;

public class FruitInfo : MonoBehaviour
{
    public string fruitName;
    public string price;

    private FruitInfoDisplay display;
    private Vector3 originalScale;
    private bool hasOriginalScale = false;

    private static FruitInfo lastSelected;

    void Start()
    {
        display = FindObjectOfType<FruitInfoDisplay>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    SelectFruit();
                }
            }
        }
    }

    void SelectFruit()
    {
        if (!hasOriginalScale)
        {
            originalScale = transform.localScale;
            hasOriginalScale = true;
        }

        if (lastSelected == this)
        {
            if (display != null)
                display.ShowInfo(fruitName, price);

            return;
        }

        if (lastSelected != null)
        {
            lastSelected.ResetFruit();
        }

        transform.localScale = originalScale * 1.15f;

        if (display != null)
            display.ShowInfo(fruitName, price);

        lastSelected = this;
    }

    void ResetFruit()
    {
        if (hasOriginalScale)
            transform.localScale = originalScale;
    }
}