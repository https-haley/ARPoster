using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [Header("Chat Display")]
    [SerializeField] private Transform messageContainer;
    [SerializeField] private GameObject userBubblePrefab;
    [SerializeField] private GameObject botBubblePrefab;
    [SerializeField] private ScrollRect scrollRect;

    [Header("Input")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button sendButton;
    [SerializeField] private GameObject loadingIndicator;

    [Header("Suggested Questions")]
    [SerializeField] private GameObject suggestedQPanel;
    [SerializeField] private List<Button> suggestedQButtons;

    private readonly List<string> defaultQuestions = new()
    {
        "Which store has the cheapest produce today?",
        "What fruits are in season right now?",
        "Can you suggest a healthy fruit snack?",
        "What's a good substitute for an expensive fruit?",
        "How do I know if a fruit is ripe?"
    };

    private ChatbotController controller;
    private bool suggestionsUsed = false;

    private void Awake()
    {
        controller = FindObjectOfType<ChatbotController>();
        sendButton.onClick.AddListener(OnSendClicked);
        if (loadingIndicator != null) loadingIndicator.SetActive(false);
    }

    public void ResetUI(string fruitName = null)
    {
        foreach (Transform child in messageContainer)
            Destroy(child.gameObject);

        inputField.text = "";
        if (loadingIndicator != null) loadingIndicator.SetActive(false);
        sendButton.interactable = true;
        suggestionsUsed = false;

        SetupSuggestedButtons(defaultQuestions);
        suggestedQPanel.SetActive(true);
    }

    private void SetupSuggestedButtons(List<string> questions)
    {
        for (int i = 0; i < suggestedQButtons.Count; i++)
        {
            if (i >= questions.Count)
            {
                suggestedQButtons[i].gameObject.SetActive(false);
                continue;
            }

            suggestedQButtons[i].gameObject.SetActive(true);

            string question = questions[i];
            TMP_Text label = suggestedQButtons[i].GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = question;

            suggestedQButtons[i].onClick.RemoveAllListeners();
            suggestedQButtons[i].onClick.AddListener(() => OnSuggestedTapped(question));
        }
    }

    private void OnSuggestedTapped(string question)
    {
        if (suggestionsUsed) return;
        suggestionsUsed = true;
        suggestedQPanel.SetActive(false);
        SubmitMessage(question);
    }

    private void OnSendClicked()
    {
        string text = inputField.text.Trim();
        if (string.IsNullOrEmpty(text)) return;
        inputField.text = "";

        if (!suggestionsUsed)
        {
            suggestionsUsed = true;
            suggestedQPanel.SetActive(false);
        }

        SubmitMessage(text);
    }

    private void SubmitMessage(string message)
    {
        AddBubble(message, isUser: true);
        SetLoading(true);
        controller.SendChatMessage(message, OnBotReply);
    }

    private void OnBotReply(string reply)
    {
        SetLoading(false);
        string cleaned = System.Text.RegularExpressions.Regex.Replace(
            reply, @"\*\*(.*?)\*\*", "<b>$1</b>");
        cleaned = cleaned.Replace("* ", "• ");
        cleaned = System.Text.RegularExpressions.Regex.Replace(
            cleaned, @"[^\u0000-\u007F]", "");
        AddBubble(cleaned, isUser: false);
    }

    private void AddBubble(string text, bool isUser)
{
    GameObject prefab = isUser ? userBubblePrefab : botBubblePrefab;
    GameObject bubble = Instantiate(prefab, messageContainer);
    bubble.GetComponentInChildren<TMP_Text>().text = text;
    StartCoroutine(RebuildAfterFrame(bubble.GetComponent<RectTransform>()));
}

private IEnumerator RebuildAfterFrame(RectTransform bubble)
{
    yield return new WaitForEndOfFrame();

    // Disable CSF so it doesn't override our manual height
    ContentSizeFitter csf = bubble.GetComponent<ContentSizeFitter>();
    if (csf != null) csf.enabled = false;

    TMP_Text tmp = bubble.GetComponentInChildren<TMP_Text>();
    tmp.rectTransform.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Horizontal, 570f);

    tmp.ForceMeshUpdate();

    float bubbleHeight = tmp.preferredHeight + 20f;
    bubble.SetSizeWithCurrentAnchors(
        RectTransform.Axis.Vertical, bubbleHeight);

    LayoutRebuilder.ForceRebuildLayoutImmediate(
        messageContainer.GetComponent<RectTransform>());
    Canvas.ForceUpdateCanvases();
    scrollRect.verticalNormalizedPosition = 0f;
}

    private void SetLoading(bool active)
    {
        sendButton.interactable = !active;
        if (loadingIndicator != null) loadingIndicator.SetActive(active);
    }
}