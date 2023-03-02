using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginMenuView : View {
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private TMP_InputField _usernameInput;
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private GameObject _warningText;
    [SerializeField] private AutoSpaceOnResolution _autoSpaceOnResolution;
    [SerializeField] private bool isLoginSuccessful = false;

    public override void Initialise() {
        _loginButton.onClick.AddListener(delegate { LoginButtonOnClick(); });
        _resetPasswordButton.onClick.AddListener(delegate { ResetPasswordButtonOnClick(); });
        _warningText.gameObject.SetActive(false);

        _autoSpaceOnResolution.PerformAutoSpace();
    }

    private void Update() {
        if (isLoginSuccessful) {
            ViewManager.Show<StartUpMenuView>(false);
        }
    }

    public override void Show() {
        base.Show();

        if (AppManager.instance._uData.username == AppManager.instance._tData.username
         && AppManager.instance._uData.password == AppManager.instance._tData.password) {
            isLoginSuccessful = true;
        }
    }

    public override void Hide() {
        base.Hide();
    }

    public void LoginButtonOnClick() {
        if (_usernameInput.text == AppManager.instance._tData.username
         && _passwordInput.text == AppManager.instance._tData.password) {
            AppManager.instance._uData.username = _usernameInput.text;
            AppManager.instance._uData.password = _passwordInput.text;
            AppManager.instance.SaveUserData();
            ViewManager.Show<StartUpMenuView>();
        } else {
            _warningText.SetActive(true);
        }
    }

    public void ResetPasswordButtonOnClick() {
        //Input password reset code
    }
}
