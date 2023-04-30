using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePasswordView : View {
    [SerializeField] private GameObject _warningText;
    [SerializeField] private TMP_InputField _curPasswordInputField;
    [SerializeField] private TMP_InputField _newPasswordInputField;
    [SerializeField] private TMP_InputField _newCopyPasswordInputField;
    [SerializeField] private Button _saveButton;

    public override void Initialise () {
        _warningText.SetActive ( false );
        _saveButton.onClick.AddListener ( SaveButtonOnClick );
    }

    private void SaveButtonOnClick () {
        if ( _curPasswordInputField.text == AppManager.instance._uData.password ) {
            if ( _newPasswordInputField.text == _newCopyPasswordInputField.text ) {
                AppManager.instance._uData.password = _newPasswordInputField.text;
                AppManager.instance.SaveUserData ();
                AppManager.instance._tData.password = _newPasswordInputField.text;
                AppManager.instance.SaveTableData ();
                ViewManager.ShowLast ();
            } else {
                _warningText.GetComponent<TextMeshProUGUI> ().text = "Your new passwords don't match";
                _warningText.SetActive ( true );
            }
        } else {
            _warningText.GetComponent<TextMeshProUGUI> ().text = "Your current password is incorrect";
            _warningText.SetActive ( true );
        }
    }
}
