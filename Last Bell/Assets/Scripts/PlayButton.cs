using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string sceneName;
    public AudioSource clickSound;
    public RawImage fadeImage;
    public float fadeDuration = 2f; // slower fade

    public DialogueTrigger dialogueRef;
    public Button playButton; // reference to the Play button

    public void PlayGame()
    {
        if (playButton != null)
            playButton.interactable = false; // disable the button immediately

        StartCoroutine(FadeAndLoad());
    }

    private System.Collections.IEnumerator FadeAndLoad()
    {
        if (clickSound != null)
        {
            AudioSource tempAudio = Instantiate(clickSound, transform.position, Quaternion.identity);
            DontDestroyOnLoad(tempAudio.gameObject);
            tempAudio.Play();
            Destroy(tempAudio.gameObject, tempAudio.clip.length);
        }

        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        dialogueRef.TriggerDialogue();
        //SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Real Level");
    }
}
