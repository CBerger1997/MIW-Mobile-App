using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {
    float _fadeTime = 2;

    private void Start() {
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 0.0f);
        StartCoroutine(FadeTextToFullAlpha());
    }

    private IEnumerator FadeTextToFullAlpha() {

        float startAlpha = GetComponent<Text>().color.a;

        float rate = 1.0f / _fadeTime;
        float alphaProgress = 0.0f;

        while (alphaProgress < 1.0f) {
            Color alphaColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(alphaColor.r, alphaColor.g, alphaColor.b, Mathf.Lerp(startAlpha, 1, alphaProgress));

            alphaProgress += rate * Time.deltaTime;

            yield return null;
        }
    }
    //public IEnumerator FadeTextToZeroAlpha(float time, Text text) {
    //    _welcomeText.color = new Color(_welcomeText.color.r, _welcomeText.color.g, _welcomeText.color.b, 1.0f); ;

    //    while (text.color.a > 0.0f) {
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
    //        yield return null;
    //    }
    //}
}
