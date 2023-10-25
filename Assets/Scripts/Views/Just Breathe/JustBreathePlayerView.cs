using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JustBreathePlayerView : View
{
    [SerializeField] private Image logoFull;
    [SerializeField] private TMP_Text breathingText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Button endSessionButton;
    [SerializeField] private JustBreatheSetupView justBreatheSetup;

    float emptyImageVal = 0.048f;
    float fullImageVal = 0.388f;

    float secondsIn;
    float secondsOut;

    float t = 0.0f;
    bool isBreathingOut = false;

    public override void Initialise ()
    {
        endSessionButton.onClick.AddListener ( delegate
        { ViewManager.Show<MainMenuView> ( false ); } );
    }

    public override void Show ()
    {
        base.Show ();

        logoFull.fillAmount = emptyImageVal;

        switch ( justBreatheSetup.BreathingDropdown.value )
        {
            case 0:
                secondsIn = 4f;
                secondsOut = 7f;
                break;
            case 1:
                secondsIn = 5f;
                secondsOut = 9f;
                break;
            case 2:
                secondsIn = 6f;
                secondsOut = 10f;
                break;
        }
    }

    private void Update ()
    {
        if ( !isBreathingOut )
        {
            logoFull.fillAmount = Mathf.Lerp ( emptyImageVal, fullImageVal, t / secondsIn );

            t += Time.deltaTime;

            timerText.text = ( ( int ) t + 1 ).ToString ();

            if ( logoFull.fillAmount >= fullImageVal - 0.001f )
            {
                logoFull.fillAmount = fullImageVal;
                t = 0.0f;
                isBreathingOut = true;
                breathingText.text = "Breathing Out";
            }
        }
        else
        {
            logoFull.fillAmount = Mathf.Lerp ( fullImageVal, emptyImageVal, t / secondsOut );

            t += Time.deltaTime;

            timerText.text = ( ( int ) t + 1 ).ToString ();

            if ( logoFull.fillAmount <= emptyImageVal + 0.001f )
            {
                logoFull.fillAmount = emptyImageVal;
                t = 0.0f;
                isBreathingOut = false;
                breathingText.text = "Breathing In";
            }
        }
    }
}
