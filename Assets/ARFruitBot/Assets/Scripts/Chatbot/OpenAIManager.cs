using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class OpenAIManager : MonoBehaviour
{
    [Header("API Config")]
    [SerializeField] private string apiKey = "AIzaSyDdNSKHptKr1_DXAr1AEha3VgnM6oEfwks";

    private const string MODEL = "gemini-2.5-flash";
    private string API_URL => $"https://generativelanguage.googleapis.com/v1beta/models/{MODEL}:generateContent?key={apiKey}";

    private const string SYSTEM_PROMPT =
        "You are a friendly grocery assistant in an AR shopping app. " +
        "Help customers compare fruit and vegetable prices across stores like Walmart, " +
        "Kroger, and Whole Foods. Suggest budget-friendly alternatives, explain nutritional " +
        "benefits, and answer general grocery questions. Keep responses short and helpful." + 
        "You have access to the following fruit price catalog:\n\n" +
        "FRUIT PRICES BY STORE:\n" +
        "| Fruit      | Our Price | Walmart | Kroger | Whole Foods |\n" +
        "| Orange     | $9        | $7      | $8     | $12         |\n" +
        "| Mango      | $16       | $12     | $14    | $20         |\n" +
        "| Banana     | $20       | $15     | $17    | $25         |\n" +
        "| Strawberry | $21       | $16     | $18    | $26         |\n" +
        "| Peach      | $12       | $9      | $10    | $15         |\n" +
        "| Apple      | $20       | $15     | $17    | $24         |\n\n" +
        "Always use this data when answering price questions. " +
        "Be specific about which store is cheapest. " +
        "Keep responses short, friendly, and helpful.";

    private List<Dictionary<string, object>> conversationHistory = new();

    private void Awake()
    {
        ResetConversation();
    }

    public IEnumerator SendMessage(string userMessage, Action<string> onSuccess, Action<string> onError)
    {
        conversationHistory.Add(new Dictionary<string, object>
        {
            { "role", "user" },
            { "parts", new List<Dictionary<string, string>>
                { new() { { "text", userMessage } } }
            }
        });

        var body = new Dictionary<string, object>
        {
            { "contents", conversationHistory },
            {
                "generationConfig", new Dictionary<string, object>
                {
                    { "temperature", 0.7f },
                    { "maxOutputTokens", 300 }
                }
            }
        };

        string json = JsonConvert.SerializeObject(body);
        byte[] rawBody = Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request = new UnityWebRequest(API_URL, "POST");
        request.uploadHandler = new UploadHandlerRaw(rawBody);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                request.downloadHandler.text);
            var candidates = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(
                responseJson["candidates"].ToString());
            var content = JsonConvert.DeserializeObject<Dictionary<string, object>>(
                candidates[0]["content"].ToString());
            var parts = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(
                content["parts"].ToString());

            string reply = parts[0]["text"].Trim();

            conversationHistory.Add(new Dictionary<string, object>
            {
                { "role", "model" },
                { "parts", new List<Dictionary<string, string>>
                    { new() { { "text", reply } } }
                }
            });

            onSuccess?.Invoke(reply);
        }
        else
        {
            Debug.LogError($"Gemini Error: {request.error}\n{request.downloadHandler.text}");
            onError?.Invoke("Sorry, I couldn't reach the assistant. Try again!");
        }
    }

    public void ResetConversation()
    {
        conversationHistory = new List<Dictionary<string, object>>
        {
            // Inject system prompt as a user/model exchange at the start
            new()
            {
                { "role", "user" },
                { "parts", new List<Dictionary<string, string>>
                    { new() { { "text", SYSTEM_PROMPT } } }
                }
            },
            new()
            {
                { "role", "model" },
                { "parts", new List<Dictionary<string, string>>
                    { new() { { "text", "Understood! I'm ready to help with grocery questions." } } }
                }
            }
        };
    }
}