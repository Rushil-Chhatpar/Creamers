using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{

    private UIDocument _document;

    private Button _justAButton;
    private Button _tapButton;
    private Label _scoreLabel;

    [SerializeField] private Transform _tpPos;
    [SerializeField] private GameObject _creamer;

    [SerializeField] private Dropper _dropper;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _justAButton = _document.rootVisualElement.Q<Button>("JustAButton");
        _tapButton = _document.rootVisualElement.Q<Button>("TapButton");
        _scoreLabel = _document.rootVisualElement.Q<Label>("ScoreLabel");
        _justAButton.RegisterCallback<ClickEvent>(OnButtonClick);
        _tapButton.RegisterCallback<ClickEvent>(OnTapButtonClick);
    }

    private void Start()
    {
        ScoreManager.Instance.UpdateScoreEvent.AddListener(UpdateScore);
    }

    private void OnButtonClick(ClickEvent evt)
    {
        if (_tpPos && _creamer)
        {
            _creamer.gameObject.transform.position = _tpPos.position;
            _creamer.gameObject.transform.rotation = _tpPos.rotation;
        }
    }

    private void OnTapButtonClick(ClickEvent evt)
    {
        if (_dropper)
        {
            _dropper.TapButtonPressed();
        }
    }

    private void UpdateScore(int score)
    {
        _scoreLabel.text = score.ToString();
    }
}
