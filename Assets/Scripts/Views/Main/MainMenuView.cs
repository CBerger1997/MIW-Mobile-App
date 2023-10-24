using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [SerializeField] private Button _affirmationButton;
    [SerializeField] private Button _visualisationsButton;
    [SerializeField] private Button _justBreathButton;
    [SerializeField] private Button _journalButton;
    [SerializeField] private Button _checkInButton;
    [SerializeField] private Button _commitmentButton;
    [SerializeField] private Button _prioiritisationButton;

    [SerializeField] private CheckinManager _checkInManager;
    [SerializeField] private CommitmentManager _commitmentManager;

    public override void Initialise ()
    {
        _affirmationButton.onClick.AddListener ( () => ViewManager.Show<AffirmationMenuView> () );

        _visualisationsButton.onClick.AddListener ( () => ViewManager.Show<VisualisationsMenuView> () );

        _justBreathButton.onClick.AddListener ( () => ViewManager.Show<JustBreatheSetupView> () );

        _journalButton.onClick.AddListener ( () => ViewManager.Show<JournalMenuView> () );

        _prioiritisationButton.onClick.AddListener ( () => ViewManager.Show<PrioritisationSelectionView> () );

        _checkInButton.onClick.AddListener ( OnCheckInClicked );

        _commitmentButton.onClick.AddListener ( OnCommitmentClicked );
    }

    private void OnCheckInClicked ()
    {
        _checkInManager.isHistoryAvailable = true;

        ViewManager.Show<CheckInMoodMenu> ();
    }

    private void OnCommitmentClicked ()
    {
        if ( _commitmentManager._commitmentExists )
        {
            ViewManager.Show<SelfCommitmentMainView> ();
        }
        else
        {
            ViewManager.Show<SelfCommitmentEditView> ();
        }
    }
}