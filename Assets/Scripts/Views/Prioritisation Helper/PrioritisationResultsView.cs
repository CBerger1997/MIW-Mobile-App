using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrioritisationResultsView : View
{
    public TMP_Text textResultPrefab;
    public PrioritisationComparisonView comparisonView;
    public GameObject contentView;
    public Button homeButton;

    private List<RankedItem> rankedItems;
    private List<TMP_Text> resultsTexts;

    public override void Initialise ()
    {
        homeButton.onClick.AddListener ( delegate
        { ViewManager.ClearHistory (); ViewManager.Show<MainMenuView> ( false ); } );
        resultsTexts = new List<TMP_Text> ();
    }

    public override void Show ()
    {
        base.Show ();

        rankedItems = comparisonView.rankedItems;

        if ( resultsTexts.Count > 0 )
        {
            foreach ( var text in resultsTexts )
            {
                Destroy ( text.gameObject );
            }
        }

        resultsTexts.Clear ();

        for ( int i = 0; i < rankedItems.Count; i++ )
        {
            TMP_Text newText = Instantiate ( textResultPrefab, contentView.transform );
            newText.text = ( i + 1 ) + ": " + rankedItems[ i ].Name;
            resultsTexts.Add ( newText );
        }
    }
}
