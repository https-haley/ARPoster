using System.Collections;
using UnityEngine;
using UnityEngine.UI;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

enum PlayerState
{
    Idle,
    Listening,
    Talking
}

public class PandaController : MonoBehaviour
{
    public GroceryAIChat groceryAI;

    public Text inputField;
    public Text robotOutput;
    public Text myHandleTextBox;

    private VoiceTest voiceTest;
    private Animator animator;
    private PlayerState playerState = PlayerState.Idle;

    void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif

        GameObject voiceC = transform.Find("VoiceController").gameObject;
        voiceTest = voiceC.GetComponent<VoiceTest>();

        voiceTest.InitPluin();

        animator = GetComponent<Animator>();

        robotOutput.text = robotOutput.text = "Hi! I'm Panda, your AR fruit poster assistant. Ask me about the fruits!";

        if (voiceTest.isIntialised())
        {
            voiceTest.TTS(robotOutput.text);
        }

        animator.Play("idle1");
    }

    public void MicButtonPressed()
    {
        if (playerState != PlayerState.Idle)
            return;

        if (voiceTest == null)
        {
            inputField.text = "Voice system not ready.";
            return;
        }

        voiceTest.myResponse = "";
        playerState = PlayerState.Listening;

        inputField.text = "Listening... speak now";
        robotOutput.text = "";

        Debug.Log("Mic button pressed");
        voiceTest.GetSpeech();
        Debug.Log("GetSpeech called");

        StopAllCoroutines();
        StartCoroutine(WaitForSpeechResult());
    }

    private IEnumerator WaitForSpeechResult()
    {
        float timer = 0f;
        float maxWait = 12f;

        while (timer < maxWait)
        {
            if (!string.IsNullOrWhiteSpace(voiceTest.myResponse))
            {
                string userAudioResponse = voiceTest.myResponse.Trim();

                inputField.text = "My question is: " + userAudioResponse;
                robotOutput.text = "Thinking...";

                playerState = PlayerState.Talking;

                if (groceryAI != null)
                {
                    groceryAI.SendMessageToAI(userAudioResponse);
                }
                else
                {
                    robotOutput.text = "Grocery AI is not connected.";
                    playerState = PlayerState.Idle;
                }

                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        myHandleTextBox.text = "I didn't catch that. Press the mic and try again.";
        playerState = PlayerState.Idle;
    }

    public void Speak(string text)
    {
        robotOutput.text = text;

        if (animator != null)
            animator.Play("talking");

        if (voiceTest != null)
            voiceTest.TTS(text);

        StartCoroutine(ReturnToIdle());
    }

    private IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(4f);

        playerState = PlayerState.Idle;

        if (animator != null)
            animator.Play("idle1");
    }

    public void quit()
    {
        Application.Quit();
    }
}