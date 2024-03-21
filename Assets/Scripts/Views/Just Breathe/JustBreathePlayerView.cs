using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JustBreathePlayerView : View
{
    [SerializeField] private Image _logoFull;
    [SerializeField] private TMP_Text _breathingText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _endSessionButton;
    [SerializeField] private AudioSource _currentAudioSource;
    [SerializeField] private AudioClip _bellClip;
    [SerializeField] private GameObject _countdownVisuals;
    [SerializeField] private GameObject _breathingVisuals;
    [SerializeField] private TMP_Text _countdownText;

    float emptyImageVal = 0.048f;
    float fullImageVal = 0.388f;

    float _secondsIn;
    float _secondsOut;

    public float breathingLength;

    float t = 0.0f;
    bool isBreathingOut = false;

    int countdownValue = 5;

    public override void Initialise ()
    {
        _endSessionButton.onClick.AddListener ( delegate
        { ViewManager.Show<MainMenuView> ( false ); } );
    }

    public override void Show ()
    {
        base.Show ();

        _logoFull.fillAmount = emptyImageVal;
        _breathingVisuals.SetActive ( false );
        _countdownVisuals.SetActive ( true );

        StartCoroutine ( CountdownTimer () );
    }

    private void Update ()
    {
        //Timings need configuring

        //There is a second delay when saying breathe in or breathe out

        //Make the counter stop at the make value and hold for 1 second

        if ( _breathingVisuals.activeSelf )
        {
            breathingLength -= Time.deltaTime;

            if ( breathingLength <= 0.0f )
            {
                _currentAudioSource.clip = _bellClip;
                _currentAudioSource.Play ();
            }

            if ( !isBreathingOut )
            {
                _logoFull.fillAmount = Mathf.Lerp ( emptyImageVal, fullImageVal, t / _secondsIn );

                t += Time.deltaTime;

                _timerText.text = ( ( int ) t + 1 ).ToString ();

                if ( _logoFull.fillAmount >= fullImageVal - 0.001f )
                {
                    _logoFull.fillAmount = fullImageVal;
                    t = 0.0f;
                    isBreathingOut = true;
                    _breathingText.text = "Breathing Out";
                }
            }
            else
            {
                _logoFull.fillAmount = Mathf.Lerp ( fullImageVal, emptyImageVal, t / _secondsOut );

                t += Time.deltaTime;

                _timerText.text = ( ( int ) t + 1 ).ToString ();

                if ( _logoFull.fillAmount <= emptyImageVal + 0.001f )
                {
                    _logoFull.fillAmount = emptyImageVal;
                    t = 0.0f;
                    isBreathingOut = false;
                    _breathingText.text = "Breathing In";
                }
            }
        }
    }

    IEnumerator CountdownTimer ()
    {
        while ( countdownValue > 0 )
        {
            yield return new WaitForSeconds ( 1 );

            countdownValue--;

            _countdownText.text = countdownValue.ToString ();
        }

        _countdownVisuals.SetActive ( false );
        _breathingVisuals.SetActive ( true );

        _currentAudioSource.Play ();
    }

    public void SetInAndOut ( float secondsIn, float secondsOut )
    {
        _secondsIn = secondsIn;
        _secondsOut = secondsOut;
    }

    public void SetAudioClip ( AudioClip clip )
    {
        _currentAudioSource.clip = clip;
    }

    public void SetBreathingLength ( int length )
    {
        breathingLength = length;
    }
}
