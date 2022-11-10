using UnityEngine;
using UnityEngine.UI;

public class ScrollSwipe : MonoBehaviour {
    public GameObject scrollbar;
    private float scroll_pos = 0;
    public float[] scrollPositions;
    float distance;
    public int selection;// { set; get; }

    public delegate void OnSelectionChangeDelegate();
    public event OnSelectionChangeDelegate OnSelectionChange;

    private void Awake() {
        scrollPositions = new float[transform.childCount];
        distance = 1f / (scrollPositions.Length - 1);

        for (int i = 0; i < scrollPositions.Length; i++) {
            scrollPositions[i] = distance * i;
        }
    }

    private void Start() {
        selection = AppManager.instance._uData.userAffirmationSelection;
        scrollbar.GetComponent<Scrollbar>().value = scrollPositions[selection];
        scroll_pos = scrollPositions[selection];
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        } else {
            for (int i = 0; i < scrollPositions.Length; i++) {
                if (scroll_pos < scrollPositions[i] + (distance / 2) && scroll_pos > scrollPositions[i] - (distance / 2)) {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, scrollPositions[i], 0.1f);
                    selection = i;
                    OnSelectionChange();
                }
            }
        }
    }
}