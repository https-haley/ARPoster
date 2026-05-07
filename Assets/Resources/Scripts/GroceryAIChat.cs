using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class GroceryAIChat : MonoBehaviour
{
    public Text inputField;
    public Text conversationText;
    public Button sendButton;
    public PandaController panda;

    private string apiKey = "";

    void Start()
    {
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(SendMessageFromButton);
        }
    }

    public void SendMessageFromButton()
    {
        if (inputField != null)
        {
            SendMessageToAI(inputField.text);
        }
    }

    public void SendMessageToAI(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
            return;

        if (conversationText != null)
            conversationText.text += "\nYou: " + userMessage;

        if (inputField != null)
            inputField.text = "";

        StartCoroutine(CallAI(userMessage));
    }

    IEnumerator CallAI(string userMessage)
    {
        string url = "https://api.openai.com/v1/responses";

        string prompt = "You are Panda, a friendly AI store clerk for an augmented reality fruit poster. "
            + "You are part of the poster experience, so you present and explain the fruits on the poster to customers. "

            + "The poster shows these fruits: Orange, Mango, Banana, Strawberry, Peach, and Apple. "

            + "Fruit prices: "
            + "Orange - $15, "
            + "Mango - $16, "
            + "Banana - $20, "
            + "Strawberry - $21, "
            + "Peach - $12, "
            + "Apple - $20. "

            + "All fruits are located in aisle 1. "
            + "Milk is in aisle 3. "
            + "Bread is in aisle 2. "
            + "Snacks are in aisle 5. "
            + "Frozen food is in aisle 6. "

            + "You mainly focus on fruits and the AR poster experience, but you can also help with general grocery store questions. "

            + "You can answer questions about fruits, prices, health benefits, recipes, smoothies, and grocery items in the store. "

            + "Keep responses short, friendly, and conversational. "
            + "Customer asks: " + userMessage;

        string json = "{\"model\":\"gpt-4.1-mini\",\"input\":\"" + EscapeJson(prompt) + "\"}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            if (conversationText != null)
                conversationText.text += "\nBot Error: " + request.error + "\n" + request.downloadHandler.text;
        }
        else
        {
            OpenAIResponse data = JsonUtility.FromJson<OpenAIResponse>(request.downloadHandler.text);
            string aiReply = data.output[0].content[0].text;

            if (conversationText != null)
                conversationText.text += "\nBot: " + aiReply;

            if (panda != null)
                panda.Speak(aiReply);
        }
    }

    string EscapeJson(string text)
    {
        return text.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}

[System.Serializable]
public class OpenAIResponse
{
    public OutputItem[] output;
}

[System.Serializable]
public class OutputItem
{
    public ContentItem[] content;
}

[System.Serializable]
public class ContentItem
{
    public string text;
}