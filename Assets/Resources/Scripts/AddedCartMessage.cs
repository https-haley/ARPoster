using TMPro;
using UnityEngine;
using System.Collections;

public class AddedCartMessage : MonoBehaviour
{
    public static AddedCartMessage Instance;

    public TextMeshProUGUI messageText;

    private Coroutine hideRoutine;

    void Awake()
    {
        Instance = this;
        messageText.text = "";
    }

    public void ShowMessage(string fruitName)
    {
        messageText.text = fruitName + " added to cart!";

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        messageText.text = "";
    }
}