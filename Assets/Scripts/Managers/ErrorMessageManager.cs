using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageManager : MonoBehaviour
{
    private static ErrorMessageManager s_instance;

    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _errorText;
    private Animator _anim;

    private void Awake ()
    {
        s_instance = this;

        _closeButton.onClick.AddListener ( CloseButtonOnClick );

        _anim = GetComponent<Animator> ();
    }

    public static void ActivateErrorMessage ( string errorText )
    {
        Debug.Log ( "Error Activate" );

        s_instance._anim.enabled = true;

        s_instance._errorText.text = errorText;

        s_instance._anim.Play ( "Show" );
    }

    private void CloseButtonOnClick ()
    {
        Debug.Log ( "Error Activate" );

        s_instance._anim.Play ( "Hide" );
    }
}
