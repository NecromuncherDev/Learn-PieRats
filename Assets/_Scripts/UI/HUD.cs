using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Image crewImage;
    [SerializeField] private Image ammoImage;
    [SerializeField] private TextMeshProUGUI crewText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Start()
    {
        GameManager.Instance.player.OnCrewChanged += UpdateCrewUI;
        GameManager.Instance.player.OnAmmoChanged += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.player.OnCrewChanged -= UpdateCrewUI;
            GameManager.Instance.player.OnAmmoChanged -= UpdateAmmoUI;
        }
    }

    private void UpdateAmmoUI(uint amount) => UpdateStockUI(ammoText, (int)amount);

    private void UpdateCrewUI(uint amount) => UpdateStockUI(crewText, (int)amount);

    private void UpdateStockUI(TextMeshProUGUI text, int amount) { text.text = $"{amount}"; }
}
