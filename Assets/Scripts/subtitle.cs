using System.Collections;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour
{
    [System.Serializable]
    public class Subtitle
    {
        public string hindiText;
        public string englishText;
        public float displayTime; // Time in seconds
    }   

    public Subtitle[] subtitles; // Array of subtitles
    public TextMeshProUGUI subtitleText; // Assign your TextMeshPro component in the inspector
    public float fadeDuration = 0.5f; // Time taken to fade in/out

    private void Start()
    {
        StartCoroutine(ShowSubtitles());
    }

    private IEnumerator ShowSubtitles()
    {
        foreach (var subtitle in subtitles)
        {
            yield return StartCoroutine(ShowSubtitle(subtitle.hindiText + "\n" + subtitle.englishText, subtitle.displayTime));
        }
        
        subtitleText.text = ""; // Clear text after all subtitles
    }

    private IEnumerator ShowSubtitle(string text, float duration)
    {
        subtitleText.text = text;
        subtitleText.alpha = 0; // Start fully transparent
        subtitleText.transform.localScale = Vector3.one * 0.8f; // Start slightly smaller

        // Fade-in and scale-up animation
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            subtitleText.alpha = Mathf.Lerp(0, 1, progress);
            subtitleText.transform.localScale = Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, progress);
            yield return null;
        }

        yield return new WaitForSeconds(duration); // Wait for the subtitle display duration

        // Fade-out animation
        timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / fadeDuration;
            subtitleText.alpha = Mathf.Lerp(1, 0, progress);
            subtitleText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.2f, progress);
            yield return null;
        }

        subtitleText.text = ""; // Clear text after fade-out
    }
}
