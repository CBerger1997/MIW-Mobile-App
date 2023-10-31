using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessageManager : MonoBehaviour
{
    private static ErrorMessageManager s_instance;

    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _errorText;

    private void Awake ()
    {
        s_instance = this;

        _closeButton.onClick.AddListener ( CloseButtonOnClick );
    }

    public void SetErrorText ( string errorText )
    {
        _errorText.text = errorText;
    }

    public void ActivateErrorMessage()
    {

    }

    private void CloseButtonOnClick ()
    {

    }
}
