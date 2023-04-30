using UnityEngine;
using UnityEngine.UI;

public class ScrollSwipe : MonoBehaviour
{
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    public GameObject scrollbar;
    public float scroll_pos = 0;
    public float[] scrollPositions;

    public float swipeSpeed = 0.5f;

    float distance;

    bool isDisableMovement = false;

    public int selection { get; set; }

    public delegate void OnSelectionChangeDelegate();
    public event OnSelectionChangeDelegate OnSelectionChange;

    private void Awake()
    {
        scrollPositions = new float[transform.childCount];
        distance = 1f / (scrollPositions.Length - 1);

        for (int i = 0; i < scrollPositions.Length; i++)
        {
            scrollPositions[i] = distance * i;
        }

        _leftButton.onClick.AddListener(OnLeftClicked);
        _rightButton.onClick.AddListener(OnRightClicked);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !isDisableMovement)
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else if (!isDisableMovement)
        {
            for (int i = 0; i < scrollPositions.Length; i++)
            {
                if (scroll_pos < scrollPositions[i] + (distance / 2) && scroll_pos > scrollPositions[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, scrollPositions[i], swipeSpeed);
                    selection = i;
                    OnSelectionChange();
                }
            }
        }
        else if (isDisableMovement)
        {
            if (scrollbar.GetComponent<Scrollbar>().value < scrollPositions[selection] || scrollbar.GetComponent<Scrollbar>().value > scrollPositions[selection])
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, scrollPositions[selection], swipeSpeed);
                if (scrollbar.GetComponent<Scrollbar>().value - scrollPositions[selection] > -0.05f && scrollbar.GetComponent<Scrollbar>().value - scrollPositions[selection] < 0.05f)
                {
                    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                    isDisableMovement = false;
                }
            }
        }
    }

    private void OnLeftClicked()
    {
        if (selection > 0)
        {
            selection--;
            OnSelectionChange();
            isDisableMovement = true;
        }
    }

    private void OnRightClicked()
    {
        if (selection < scrollPositions.Length - 1)
        {
            selection++;
            OnSelectionChange();
            isDisableMovement = true;
        }
    }

    public void PresetPosition(int val)
    {
        selection = val;
        scrollbar.GetComponent<Scrollbar>().value = scrollPositions[selection];
        scroll_pos = scrollPositions[selection];
    }
}