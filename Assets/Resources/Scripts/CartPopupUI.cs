using System.Collections;
using TMPro;
using UnityEngine;
using System.Globalization;

public class CartPopupUI : MonoBehaviour
{
    public GameObject cartPopupPanel;
    public TextMeshProUGUI cartPopupText;
    public float taxRate = 0.095f;
    public Transform popupBox;

    public void OpenCart()
    {
        cartPopupPanel.SetActive(true);
        DisplayCart();

        StopAllCoroutines();
        StartCoroutine(PopupAnimation());
    }

    public void CloseCart()
    {
        cartPopupPanel.SetActive(false);
    }

    public void ClearCart()
    {
        CartManager.ClearCart();
        DisplayCart();
    }

    void DisplayCart()
    {
        if (CartManager.cartItems.Count == 0)
        {
            cartPopupText.text = "Cart Empty";
            return;
        }

        float subtotal = 0f;
        string display = "CART\n\n";

        foreach (CartItem item in CartManager.cartItems.Values)
        {
            float price = ParsePrice(item.itemPrice);
            float lineTotal = price * item.quantity;
            subtotal += lineTotal;

            display += item.itemName + " x" + item.quantity + " - $" + lineTotal.ToString("0.00") + "\n";
        }

        float tax = subtotal * taxRate;
        float total = subtotal + tax;

        display += "\nSubtotal: $" + subtotal.ToString("0.00");
        display += "\nTax: $" + tax.ToString("0.00");
        display += "\nTotal: $" + total.ToString("0.00");

        cartPopupText.text = display;
    }

    float ParsePrice(string priceText)
    {
        priceText = priceText.Replace("$", "").Trim();

        if (float.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out float price))
            return price;

        return 0f;
    }


    IEnumerator PopupAnimation()
    {
        popupBox.localScale = Vector3.zero;

        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, time / duration);
            popupBox.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        popupBox.localScale = Vector3.one;
    }

}