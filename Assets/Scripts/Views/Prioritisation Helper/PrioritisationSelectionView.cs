using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankedItem
{
    public int Id;
    public string Name;
    public int value;
    public List<string> upperComparisonIDs;
    public List<string> lowerComparisonIDs;

    public RankedItem()
    {
        this.Name = "";
        this.value = 0;
        this.upperComparisonIDs = new List<string>();
        this.lowerComparisonIDs = new List<string>();
    }

    public RankedItem(string Name)
    {
        this.Name = Name;
        this.value = 0;
        this.upperComparisonIDs = new List<string>();
        this.lowerComparisonIDs = new List<string>();
    }
}

public class PrioritisationSelectionView : View
{
    List<GameObject> listItems = new List<GameObject>();

    public List<RankedItem> rankedItems = new List<RankedItem>();
    public TMP_InputField itemNameInputField;

    public Button addButton;
    public Button removeButton;
    public Button startButton;

    public GameObject ItemPrefab;
    public GameObject RankedItemContents;
    public PrioritisationComparisonView comparisonView;

    Color defaultColor = new Color(0.5450981f, 0.8235294f, 0.9215686f);
    Color selectedColor = new Color(0.2470588f, 0.8627451f, 0.6901961f);

    int currentItemIndex = -1;

    bool isBackToMenu = true;

    public override void Initialise()
    {
        addButton.onClick.AddListener(OnAddClicked);
        removeButton.onClick.AddListener(OnRemoveClicked);
        startButton.onClick.AddListener(OnStartClicked);
    }

    public override void Show()
    {
        base.Show();

        isBackToMenu = true;

        if (comparisonView.isComplete)
        {
            foreach (Transform child in RankedItemContents.transform)
            {
                Destroy(child.gameObject);
            }

            rankedItems.Clear();
        }
    }

    public override void Hide()
    {
        base.Hide();

        if (isBackToMenu)
        {
            rankedItems.Clear();
            listItems.Clear();
            currentItemIndex = -1;

            foreach (Transform child in RankedItemContents.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (currentItemIndex >= 0)
        {
            removeButton.interactable = true;
        }
        else
        {
            removeButton.interactable = false;
        }

        if (itemNameInputField.text != "")
        {
            addButton.interactable = true;
        }
        else
        {
            addButton.interactable = false;
        }
        if (listItems.Count > 2)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    void OnAddClicked()
    {
        AddItemToList(itemNameInputField.text);

        GameObject newItem = Instantiate(ItemPrefab, RankedItemContents.transform);
        newItem.GetComponent<Button>().image.color = defaultColor;
        newItem.GetComponentInChildren<TMP_Text>().text = itemNameInputField.text;
        newItem.GetComponent<Button>().onClick.AddListener(delegate { ListItemSelected(newItem.GetComponent<Button>()); });
        listItems.Add(newItem);

        itemNameInputField.text = "";
    }

    void AddItemToList(string Name)
    {
        rankedItems.Add(new RankedItem(Name));
    }

    void ListItemSelected(Button button)
    {
        int index = -1;
        int count = 0;

        foreach (Transform child in RankedItemContents.transform)
        {
            if (child.GetComponent<Button>() == button)
            {
                index = count;
                break;
            }
            else
            {
                count++;
            }
        }

        if (currentItemIndex != index && currentItemIndex != -1)
        {
            listItems[currentItemIndex].GetComponent<Button>().image.color = defaultColor;
        }

        button.image.color = selectedColor;
        currentItemIndex = index;
    }

    public void OnRemoveClicked()
    {
        Destroy(listItems[currentItemIndex].gameObject);
        listItems.RemoveAt(currentItemIndex);
        rankedItems.RemoveAt(currentItemIndex);
        currentItemIndex = -1;
    }

    public void RemoveItemFromList(int value)
    {
        rankedItems.RemoveAt(value);
    }

    public void OnStartClicked()
    {
        isBackToMenu = false;

        ViewManager.Show<PrioritisationComparisonView>();
    }
}
