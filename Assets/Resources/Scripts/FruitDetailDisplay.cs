using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FruitDetailDisplay : MonoBehaviour
{
    void Start()
    {
        SetText("FruitNameText", FruitDataHolder.FruitName);
        SetText("PriceText", "Price: " + FruitDataHolder.Price);
        SetText("HealthBenefitsText", FruitDataHolder.HealthBenefits);

        var btn = GameObject.Find("BackButton")?.GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(GoBack);
    }

    void SetText(string objName, string value)
    {
        var go = GameObject.Find(objName);
        if (go == null) return;
        var t = go.GetComponent<TMP_Text>();
        if (t != null) t.text = value;
    }

    public void GoBack()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
