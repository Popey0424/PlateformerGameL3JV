using UnityEngine;
using UnityEngine.UI;

public class JumpChargeUI : MonoBehaviour
{
    [SerializeField] private Slider _jumpChargeSlider; 
    [SerializeField] private PlayerController _playerController; 

    private void Start()
    {
        
        _jumpChargeSlider.value = 0;
    }

    private void Update()
    {
        
        if (_playerController.IsChargingJump())
        {
            
            _jumpChargeSlider.value = _playerController.GetJumpChargePercentage();
        }
        else
        {
            
            _jumpChargeSlider.value = 0;
        }
    }
}
