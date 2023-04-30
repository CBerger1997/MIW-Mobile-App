using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
using UnityEditor;

public class ClickableText : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick ( PointerEventData eventData ) {

        var text = GetComponent<TextMeshProUGUI> ();

        if ( eventData.clickCount > 0 ) {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink ( text, Input.GetTouch ( 0 ).position, Camera.main );

            if ( linkIndex > -1 ) {
                var linkInfo = text.textInfo.linkInfo[ linkIndex ];
                var linkId = linkInfo.GetLinkText ();

                if ( linkId.Any ( char.IsDigit ) == true ) {
                    Application.OpenURL ( "tel://" + linkId );
                } else {
                    if ( linkInfo.GetLinkID () == "Feelings" ) {
                        Application.OpenURL ( "https://innercalm.calmpeople.co.uk/pathway/feelings/" );
                    } else if ( linkInfo.GetLinkID () == "SelfEsteem" ) {
                        Application.OpenURL ( "https://innercalm.calmpeople.co.uk/pathway/self-esteem/" );
                    }
                }
            }
        }

    }
}
