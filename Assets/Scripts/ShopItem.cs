
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private BaseBall _ballToBuy;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Image _image;
    private void Start()
    {
        _nameText.SetText(_ballToBuy.GetName());
        _costText.SetText(_ballToBuy.GetCost().ToString());
        _image.sprite = _ballToBuy.GetSprite();
    }
    public bool BuyBall()
    {
        if (ShopManager.Instance.CurrentCoins >= _ballToBuy.GetCost())
        {
            ShopManager.Instance.CurrentCoins -= _ballToBuy.GetCost();
            return true;
        }
        else
        {
            return false;
        }
    }
}
