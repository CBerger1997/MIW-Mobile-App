using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour {
    public GameObject scrollbar;
    private float scroll_pos = 0;
    public float[] scrollPositions;
    float distance;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    int btnNumber;

    private void Start() {
        scrollPositions = new float[transform.childCount];
        distance = 1f / (scrollPositions.Length - 1);
    }

    void Update() {

        if (runIt) {
            FrameRelay(takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f) {
                time = 0;
                runIt = false;
            }
        }

        for (int i = 0; i < scrollPositions.Length; i++) {
            scrollPositions[i] = distance * i;
        }

        if (Input.GetMouseButton(0)) {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        } else {
            for (int i = 0; i < scrollPositions.Length; i++) {
                if (scroll_pos < scrollPositions[i] + (distance / 2) && scroll_pos > scrollPositions[i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, scrollPositions[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < scrollPositions.Length; i++) {
            if (scroll_pos < scrollPositions[i] + (distance / 2) && scroll_pos > scrollPositions[i] - (distance / 2)) {
                Debug.LogWarning("Current Selected Level" + i);
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                for (int j = 0; j < scrollPositions.Length; j++) {
                    if (j != i) {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }


    }

    private void FrameRelay(Button btn) {
        for (int i = 0; i < scrollPositions.Length; i++) {
            if (scroll_pos < scrollPositions[i] + (distance / 2) && scroll_pos > scrollPositions[i] - (distance / 2)) {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, scrollPositions[btnNumber], 1f * Time.deltaTime);
            }
        }

        for (int i = 0; i < btn.transform.parent.transform.childCount; i++) {
            btn.transform.name = ".";
        }

    }
    //public void WhichBtnClicked(Button btn) {
    //    btn.transform.name = "clicked";
    //    for (int i = 0; i < btn.transform.parent.transform.childCount; i++) {
    //        if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked") {
    //            btnNumber = i;
    //            takeTheBtn = btn;
    //            time = 0;
    //            scroll_pos = (scrollPositions[btnNumber]);
    //            runIt = true;
    //        }
    //    }
    //}
}