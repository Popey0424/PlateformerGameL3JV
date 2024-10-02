using UnityEngine;
using UnityEngine.UI;

public class JumpChargeUI : MonoBehaviour
{
    [SerializeField] private Image _jumpChargeSlider; 
    [SerializeField] private PlayerController _playerController; 

    private void Start()
    {
        
        _jumpChargeSlider.fillAmount = 0;
    }

    private void Update()
    {
        
        if (_playerController.IsChargingJump())
        {
            
            _jumpChargeSlider.fillAmount = _playerController.GetJumpChargePercentage();
        }
        else
        {
            
            _jumpChargeSlider.fillAmount = 0;
        }
    }
}
