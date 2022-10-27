using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordUpdater : MonoBehaviour {

    public void UpdatePassword(string firstname, string surname, string email, string password) {
        WWWForm form = new WWWForm();
        form.AddField("firstnamePost", firstname);
        form.AddField("surnamePost", surname);
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);

        WWW clientInsertData = new WWW("http://localhost/MIW_APP/UpdatePassword.php", form);

    }
}
