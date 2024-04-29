using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordView : View, IDataPersistence
{
    [SerializeField] private GameObject _warningText;
    [SerializeField] private TMP_InputField _curPasswordInputField;
    [SerializeField] private TMP_InputField _newPasswordInputField;
    [SerializeField] private TMP_InputField _newCopyPasswordInputField;
    [SerializeField] private Button _saveButton;

    private string password;
    private string username;
    private bool isPasswordChanged = false;

    public override void Initialise ()
    {
        _warningText.SetActive ( false );
        _saveButton.onClick.AddListener ( SaveButtonOnClick );
    }

    public override void Show() {
        base.Show();

        _curPasswordInputField.text = "";
        _newPasswordInputField.text = "";
        _newCopyPasswordInputField.text = "";
    }

    private void SaveButtonOnClick ()
    {
        _warningText.SetActive(false);
        if ( _newPasswordInputField.text == _newCopyPasswordInputField.text )
        {
            StartCoroutine(DatabaseHandler.ChangeUserPassword(username, _curPasswordInputField.text, _newPasswordInputField.text));
        }
        else
        {
            _warningText.GetComponent<TextMeshProUGUI> ().text = "Your new passwords don't match";
            _warningText.SetActive ( true );
        }
    }
    public void LoadData ( UserData data )
    {
        username = data.username;
        password = data.password;
    }

    public void SaveData ( ref UserData data )
    {
        if ( isPasswordChanged )
        {
            data.password = password;
        }
    }
}
