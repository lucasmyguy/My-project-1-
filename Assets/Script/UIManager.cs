using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument playerHudUI;
    [SerializeField] private UIDocument resultUI;
    [SerializeField] private UIDocument mainMenuUI;
    private VisualElement playerHUDRoot;
    private VisualElement resultRoot;
    private VisualElement mainMenuRoot;

    private ProgressBar hpBar;

    private VisualElement resultPanel;
    private Label resultLabel;
    private Button restartBtn;
    private Button quitBtn;

    private VisualElement mainMenuPanel;
    private Button startBtn;
    private Button quitBtn2;
    

    void Start()
    {
        playerHUDRoot = playerHudUI.rootVisualElement;
        hpBar = playerHUDRoot.Q<ProgressBar>("HpBar");

        resultRoot = resultUI.rootVisualElement;
        resultPanel = resultRoot.Q<VisualElement>("ResultPanel");
        resultLabel = resultRoot.Q<Label>("ResultLabel");
        restartBtn = resultRoot.Q<Button>("RestartButton");
        quitBtn = resultRoot.Q<Button>("QuitButton");

        mainMenuRoot = mainMenuUI.rootVisualElement;
        mainMenuPanel = mainMenuRoot.Q<VisualElement>("MainMenuPanel");
        startBtn = mainMenuRoot.Q<Button>("StartBtn");
        quitBtn2 = mainMenuRoot.Q<Button>("QuitBtn");


        restartBtn.RegisterCallback<ClickEvent>(OnRestartClicked);
        quitBtn.RegisterCallback<ClickEvent>(OnQuitClicked);
        quitBtn2.RegisterCallback<ClickEvent>(OnQuitClicked);
        startBtn.RegisterCallback<ClickEvent>(OnStartClicked);

        resultPanel.style.display = DisplayStyle.None;

        Time.timeScale = 0;
    }


    public void UpdateHp(float hp)
    {
        hpBar.value = hp;
        hpBar.title = hp + "/400";
    }

    public void HandleWinLose(bool r)
    {
        resultLabel.text = r ? "Victory" : "Defeat";

        resultPanel.style.display = DisplayStyle.Flex;
    }

    private void OnRestartClicked(ClickEvent evt)
    {
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene);
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnStartClicked(ClickEvent evt)
    {
        Time.timeScale = 1;
        mainMenuPanel.style.display = DisplayStyle.None;

    }
}
