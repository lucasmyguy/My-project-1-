using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private UIDocument healthBarUI;
    private StatsComponent stats;
    private VisualElement root;
    private ProgressBar healthBar;

    private void Awake()
    {
        stats = GetComponentInParent<StatsComponent>();
    }
    void Start()
    {
        root = healthBarUI.rootVisualElement;
        healthBar = root.Q<ProgressBar>("HealthBar");
        healthBar.highValue = stats.maxHealth;
    }

    private void Update()
    {
        healthBar.value = stats.health;
    }
}
