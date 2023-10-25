using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustBreathePlayerView : View
{
    [SerializeField] private Image logoFull;

    float emptyImageVal = 0.048f;
    float fullImageVal = 0.388f;

    float secondsIn;
    float secondsOut;

    float t = 0.0f;

    public bool isBreathingOut = false;

    [SerializeField] private JustBreatheSetupView justBreatheSetup;

    public override void Initialise ()
    {
        logoFull.fillAmount = emptyImageVal;
    }

    public override void Show ()
    {
        base.Show ();

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
            logoFull.fillAmount = Mathf.Lerp ( logoFull.fillAmount, fullImageVal, t / secondsIn );

            t += Time.deltaTime;

            if ( logoFull.fillAmount >= fullImageVal - 0.001f )
            {
                logoFull.fillAmount = fullImageVal;
                t = 0.0f;
                isBreathingOut = true;
            }
        }
        else
        {
            logoFull.fillAmount = Mathf.Lerp ( logoFull.fillAmount, emptyImageVal, t / secondsOut );

            t += Time.deltaTime;

            if ( logoFull.fillAmount <= emptyImageVal + 0.001f )
            {
                logoFull.fillAmount = emptyImageVal;
                t = 0.0f;
                isBreathingOut = false;
            }
        }
    }
}
