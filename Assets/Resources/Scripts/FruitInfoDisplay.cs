using UnityEngine;
using TMPro;

public class FruitInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    public void ShowInfo(string fruitName, string price)
    {
        infoText.text = fruitName + "  •  " + price;
    }
}