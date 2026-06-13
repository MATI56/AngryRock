using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public int CurrentCoins;

    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private TextMeshProUGUI _coinsText;

   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        InputManager.Instance.InputActions.Player.Shop.performed += OpenShop;
        _coinsText.SetText(CurrentCoins.ToString() + "G");
    }

    public void OpenShop(InputAction.CallbackContext context)
    {
        BalllController.Instance.IsActive = false;
        _shopPanel.SetActive(true);
        InputManager.Instance.InputActions.Player.Shop.performed -= OpenShop;
        InputManager.Instance.InputActions.Player.Shop.performed += CloseShop;
    }
    public void CloseShop(InputAction.CallbackContext context)
    {
        BalllController.Instance.IsActive = true;
        _shopPanel.SetActive(false);
        InputManager.Instance.InputActions.Player.Shop.performed += OpenShop;
        InputManager.Instance.InputActions.Player.Shop.performed -= CloseShop;
    }

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;
        _coinsText.SetText(CurrentCoins.ToString() + "G");
    }
    public bool RemoveCoins(int amount)
    {
        if (CurrentCoins >= amount)
        {
            CurrentCoins -= amount;
            _coinsText.SetText(CurrentCoins.ToString() + "G");
            return true;
        }
        return false;
    }
}
