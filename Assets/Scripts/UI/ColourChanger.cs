using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourChanger : MonoBehaviour {

    [SerializeField] Gradient gradient;
    [SerializeField] float duration;
    float t1 = 0f;
    float t2 = 0f;
    bool isBackLerp = false;

    void Update () {
        float value;

        if ( !isBackLerp ) {
            value = Mathf.Lerp ( 0f, 1f, t1 );
            t1 += Time.deltaTime / duration;
        } else {
            value = Mathf.Lerp ( 1f, 0f, t2 );
            t2 += Time.deltaTime / duration;
        }

        Color color = gradient.Evaluate ( value );
        GetComponent<Image> ().color = color;

        if ( value == 1 ) {
            isBackLerp = true;
            t1 = 0f;
        } else if ( value == 0 ) {
            isBackLerp = false;
            t2 = 0f;
        }
    }
}