using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PrioritisationComparisonView : View
{

    public PrioritisationSelectionView PrioritisationSelectionView;
    public Button TopItemButton;
    public Button BottomItemButton;
    public List<RankedItem> rankedItems = new List<RankedItem> ();

    List<string> currentItems = new List<string> ();
    int iterations = 0;
    bool isInitialLoop = true;

    public override void Initialise ()
    {
        TopItemButton.onClick.AddListener ( delegate
        { OnRankedItemClicked ( 1 ); } );
        BottomItemButton.onClick.AddListener ( delegate
        { OnRankedItemClicked ( 2 ); } );
    }

    public override void Show ()
    {
        base.Show ();

        rankedItems.Clear ();
        currentItems.Clear ();

        rankedItems = new List<RankedItem> ( PrioritisationSelectionView.rankedItems );

        isInitialLoop = true;
        iterations = 0;

        SelectNextItems ();
    }

    /// <summary>
    /// TODO Fix this for a count of two
    /// TODO Fix this for repeating
    /// </summary>
    private void SelectNextItems ()
    {
        if ( isInitialLoop )
        {
            if ( iterations + 1 <= rankedItems.Count - 1 )
            {
                currentItems.Add ( rankedItems[ iterations ].Name );
                currentItems.Add ( rankedItems[ iterations + 1 ].Name );
                iterations += 2;
            }
            else
            {
                currentItems.Add ( rankedItems[ iterations ].Name );
                iterations = 0;
                currentItems.Add ( rankedItems[ iterations ].Name );
                iterations = rankedItems.Count - 1;
                isInitialLoop = false;
            }

            TopItemButton.GetComponentInChildren<TMP_Text> ().text = currentItems[ 0 ];
            BottomItemButton.GetComponentInChildren<TMP_Text> ().text = currentItems[ 1 ];
        }
        else
        {
            for ( int i = rankedItems.Count; i > 0; i-- )
            {
                int valuesMatched = 0;
                currentItems.Clear ();

                foreach ( var item in rankedItems )
                {
                    if ( item.value == i )
                    {
                        currentItems.Add ( item.Name );
                        valuesMatched++;
                    }

                    if ( valuesMatched == 2 )
                    {
                        TopItemButton.GetComponentInChildren<TMP_Text> ().text = currentItems[ 0 ];
                        BottomItemButton.GetComponentInChildren<TMP_Text> ().text = currentItems[ 1 ];

                        return;
                    }
                }
            }

            List<RankedItem> reorderedRankedList = new List<RankedItem> ();

            for ( int i = 0; i < rankedItems.Count; i++ )
            {
                foreach ( var item in rankedItems )
                {
                    if ( item.value == 5 - i )
                    {
                        reorderedRankedList.Add ( item );
                        continue;
                    }
                }
            }

            rankedItems = reorderedRankedList;

            ViewManager.Show<PrioritisationResultsView> ( false );
        }
    }

    private void ModifyRankedItems ( int value1, int value2 )
    {
        foreach ( var item in rankedItems )
        {
            if ( item.Name == currentItems[ 0 ] )
            {
                if ( value1 == 1 )
                {
                    if ( item.value == 0 )
                    {
                        item.value = rankedItems.Count;
                    }

                    item.lowerComparisonIDs.Add ( currentItems[ 1 ] );
                }
                else
                {
                    if ( item.value == 0 )
                    {
                        item.value = rankedItems.Count - 1;
                    }
                    else
                    {
                        item.value--;
                    }

                    item.upperComparisonIDs.Add ( currentItems[ 1 ] );
                }
            }
            else if ( item.Name == currentItems[ 1 ] )
            {
                if ( value2 == 1 )
                {
                    if ( item.value == 0 )
                    {
                        item.value = rankedItems.Count;
                    }

                    item.lowerComparisonIDs.Add ( currentItems[ 0 ] );
                }
                else
                {
                    if ( item.value == 0 )
                    {
                        item.value = rankedItems.Count - 1;
                    }
                    else
                    {
                        item.value--;
                    }

                    item.upperComparisonIDs.Add ( currentItems[ 0 ] );
                }
            }
        }

        bool valuesToChange = false;

        do
        {
            valuesToChange = false;

            foreach ( var rankedItem in rankedItems )
            {
                foreach ( var comparisonItem in rankedItems )
                {
                    if ( rankedItem.lowerComparisonIDs.Contains ( comparisonItem.Name ) && rankedItem.value == comparisonItem.value )
                    {
                        comparisonItem.value--;
                        valuesToChange = true;
                    }
                }
            }
        } while ( valuesToChange );

        currentItems.Clear ();

        Debug.Log ( "Next Set" );

        foreach ( var item in rankedItems )
        {
            Debug.Log ( "Name: " + item.Name );
            Debug.Log ( "Value: " + item.value );
        }
    }

    public void OnRankedItemClicked ( int value )
    {
        int value1 = 0;
        int value2 = 0;

        if ( value == 1 )
        {
            value1 = 1;
        }
        else
        {
            value2 = 1;
        }

        ModifyRankedItems ( value1, value2 );
        SelectNextItems ();
    }
}
