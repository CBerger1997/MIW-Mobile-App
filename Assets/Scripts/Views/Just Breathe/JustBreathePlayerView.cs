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

    public int breathingIterations;

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

        StartCoroutine ( BreathingInCounter () );
    }

    IEnumerator BreathingInCounter ()
    {
        yield return new WaitForSeconds ( 1 );

        while ( !isBreathingOut )
        {
            _logoFull.fillAmount = Mathf.Lerp ( emptyImageVal, fullImageVal, t / _secondsIn );

            t += Time.deltaTime;

            if ( t < _secondsIn )
            {
                _timerText.text = ( ( int ) t + 1 ).ToString ();
            }

            if ( _logoFull.fillAmount >= fullImageVal - 0.001f )
            {
                _logoFull.fillAmount = fullImageVal;
                t = 0.0f;
                isBreathingOut = true;
                _breathingText.text = "Breathing Out";
                StartCoroutine ( BreathingOutCounter () );
            }

            yield return new WaitForEndOfFrame ();
        }
    }

    IEnumerator BreathingOutCounter ()
    {
        yield return new WaitForSeconds ( 1 );

        while ( isBreathingOut )
        {
            _logoFull.fillAmount = Mathf.Lerp ( fullImageVal, emptyImageVal, t / _secondsOut );

            t += Time.deltaTime;

            if ( t < _secondsOut )
            {
                _timerText.text = ( ( int ) t + 1 ).ToString ();
            }

            if ( _logoFull.fillAmount <= emptyImageVal + 0.001f )
            {
                _logoFull.fillAmount = emptyImageVal;
                t = 0.0f;
                isBreathingOut = false;
                _breathingText.text = "Breathing In";

                if ( breathingIterations > 0 )
                {
                    breathingIterations--;
                    StartCoroutine ( BreathingInCounter () );
                }
                else
                {
                    _currentAudioSource.clip = _bellClip;
                    _currentAudioSource.Play ();

                    yield return new WaitForSeconds ( 6 );

                    ViewManager.Show<MainMenuView> ( false );
                }
            }

            yield return new WaitForEndOfFrame ();
        }
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

    public void SetBreathingIterations ( int iterations )
    {
        breathingIterations = iterations;
    }
}
