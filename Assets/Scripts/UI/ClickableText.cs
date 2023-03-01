using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class ClickableText : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {

        var text = GetComponent<TextMeshProUGUI>();

        if (eventData.clickCount > 0) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.GetTouch(0).position, Camera.main);

            if (linkIndex > -1) {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                var linkId = linkInfo.GetLinkText();

                Application.OpenURL("tel://" + linkId);
            }
        }

    }
}
