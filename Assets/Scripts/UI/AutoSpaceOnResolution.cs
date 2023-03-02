using UnityEngine;
using UnityEngine.UI;

public class AutoSpaceOnResolution : MonoBehaviour {
    public void PerformAutoSpace() {
        VerticalLayoutGroup layoutGroup = GetComponent<VerticalLayoutGroup>();
        float parentHeight = transform.parent.parent.GetComponent<RectTransform>().rect.height;
        float totalHeight = 0;

        foreach (Transform child in transform) {
            totalHeight += child.GetComponent<RectTransform>().rect.height;
        }

        float spacingSize = ((parentHeight - totalHeight) / transform.childCount);

        layoutGroup.spacing = spacingSize < 30 ? 30 : spacingSize;
    }
}
