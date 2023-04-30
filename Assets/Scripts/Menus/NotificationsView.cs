using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationsView : View {
    [SerializeField] private List<Toggle> _notificationToggles;
    [SerializeField] Button _saveButton;

    private Toggle _currentToggle;

    public override void Initialise () {
        foreach ( Toggle toggle in _notificationToggles ) {
            toggle.onValueChanged.AddListener ( delegate { OnToggleValueChange ( toggle ); } );
        }

        _saveButton.onClick.AddListener ( SaveChangesOnClick );

        _currentToggle = AppManager.instance._uData.isNotificationsOn == true ? _notificationToggles[ 0 ] : _notificationToggles[ 1 ];
        _currentToggle.isOn = true;
    }

    private void OnToggleValueChange ( Toggle toggle ) {
        if ( toggle.isOn ) {
            _currentToggle = toggle;
            foreach ( Toggle t in _notificationToggles ) {
                if ( t != toggle ) {
                    t.isOn = false;
                }
            }
        } else if ( _currentToggle.isOn == false ) {
            _currentToggle.isOn = true;
        }
    }

    private void SaveChangesOnClick () {
        AppManager.instance._uData.isNotificationsOn = _notificationToggles[ 0 ].isOn == true ? true : false;
        AppManager.instance.SaveUserData ();
        ViewManager.ShowLast ();
    }
}
