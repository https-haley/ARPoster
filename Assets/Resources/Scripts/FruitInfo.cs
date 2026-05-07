using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitInfo : MonoBehaviour
{
    public string fruitName;
    public string price;

    [TextArea(3, 8)]
    public string healthBenefits;

    public float holdTime = 0.6f;

    private float pressStartTime;
    private bool pressingThisFruit = false;
    private bool holdUsed = false;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            StartPress(Input.mousePosition);

        if (Input.GetMouseButton(0))
            CheckHold();

        if (Input.GetMouseButtonUp(0))
            EndPress();
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                StartPress(touch.position);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                CheckHold();

            if (touch.phase == TouchPhase.Ended)
                EndPress();
        }
#endif
    }

    void StartPress(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                pressingThisFruit = true;
                holdUsed = false;
                pressStartTime = Time.time;
            }
        }
    }

    void CheckHold()
    {
        if (!pressingThisFruit || holdUsed)
            return;

        if (Time.time - pressStartTime >= holdTime)
        {
            holdUsed = true;
            OpenFruitDetails();
        }
    }

    void EndPress()
    {
        if (pressingThisFruit && !holdUsed)
        {
            AddFruitToCart();
        }

        pressingThisFruit = false;
        holdUsed = false;
    }

    void AddFruitToCart()
    {
        CartManager.AddItem(fruitName, price);

        if (AddedCartMessage.Instance != null)
            AddedCartMessage.Instance.ShowMessage(fruitName);
    }

    void OpenFruitDetails()
    {
        FruitDataHolder.FruitName = fruitName;
        FruitDataHolder.Price = price;
        FruitDataHolder.HealthBenefits = healthBenefits;

        SceneManager.LoadScene("FruitDetailScene");
    }
}