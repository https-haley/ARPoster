using System;
using UnityEngine;

public class ChatbotController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject chatPanel;

    [Header("Robot (optional until model is imported)")]
    [SerializeField] private GameObject robotModel;
    [SerializeField] private Animator robotAnimator;

    private OpenAIManager openAI;
    private ChatUI chatUI;
    private bool isChatOpen = false;

    private void Awake()
    {
        openAI = FindObjectOfType<OpenAIManager>();
        chatUI = FindObjectOfType<ChatUI>();

        chatPanel.SetActive(false);
        if (robotModel != null) robotModel.SetActive(false);
    }

    public void OnQuestionMarkPressed()
    {
        if (isChatOpen) CloseChat();
        else OpenChat();
    }

    private void OpenChat()
    {
        isChatOpen = true;
        if (robotModel != null) robotModel.SetActive(true);
        if (robotAnimator != null) robotAnimator.SetTrigger("Greet");
        chatPanel.SetActive(true);
        chatUI.ResetUI();
    }

    private void CloseChat()
    {
        isChatOpen = false;
        if (robotModel != null) robotModel.SetActive(false);
        chatPanel.SetActive(false);
        openAI.ResetConversation();
    }

    public void SendChatMessage(string message, Action<string> onReply)
    {
        StartCoroutine(openAI.SendMessage(message, onReply, (error) =>
        {
            onReply?.Invoke(error);
        }));
    }
}