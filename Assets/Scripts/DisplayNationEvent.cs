using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNationEvent : MonoBehaviour {
    public Image image;
    public Text title;
    public Text description;

    public void DisplayNewNationEvent(NationEvent nationEvent) {
        Color color = image.color;
        Color textColor = title.color;
        color.a = 1f;
        textColor.a = 1f;

        image.color = color;
        title.color = textColor;
        description.color = textColor;

        title.text = nationEvent.eventName;
        description.text = nationEvent.eventDescription;

        StartCoroutine(FadeEvent(0f, 5f));
    }

    public void DisplayNewGlobalEvent(NationEvent nationEvent) {
        Color color = image.color;
        Color textColor = title.color;
        color.a = 1f;
        textColor.a = 1f;

        image.color = color;
        title.color = textColor;
        description.color = textColor;

        title.text = "Global Event";
        description.text = nationEvent.eventDescription;

        StartCoroutine(FadeEvent(0f, 5f));
    }

    IEnumerator FadeEvent(float value, float time) {
        if (GameStateManager.instance.isTutorial)
            yield return null;

        Color imageColor = image.color;
        Color textColor = title.color;

        float alpha = imageColor.a;
        float textAlpha = textColor.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time) {
            Color newColor = new Color(imageColor.r, imageColor.g, imageColor.b, Mathf.Lerp(alpha, value, t));
            Color newTextColor = new Color(textColor.r, textColor.g, textColor.b, Mathf.Lerp(textAlpha, value, t));
            image.color = newColor;
            title.color = newTextColor;
            description.color = newTextColor;
            yield return null;
        }
    }
}