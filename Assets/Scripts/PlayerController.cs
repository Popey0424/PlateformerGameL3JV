using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _inputs;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _acceleration;

    [SerializeField] private float _groundOffset;
    [SerializeField] private float _groundRadius;

    Collider2D[] _collidersGround = new Collider2D[2];
    public LayerMask _GroundLayer;

    [Header("Jump")]
    [SerializeField] private bool _inputJump;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _jumpForceMin = 5f;  
    [SerializeField] private float _jumpForceMax = 15f; 
    [SerializeField] private float _maxHoldTime = 2.0f; 
    private float _jumpHoldTime = 0f; 
    [SerializeField] private float _fallMultiplier = 0.1f;  
    [SerializeField] private float _lowJumpMultiplier = 2f;

    [Header("TeleportPlayer")]
    [SerializeField] private Vector2 _offsetCollisionBox;
    [SerializeField] private Vector2 _offsetToReplace;
    private Vector2 _collisionBox;
    private float[] directions = new float[] { -1f, 1f };
    public GameObject goPlatform;
    private bool _isOnPlatform = false;
    private Vector3 _lastPlateformPosition;

    [Header("Facing")]
    private bool isFacingRight = true;

    private bool playerDie = false;

    private float _timeSinceGrounded;
    private float _coyoteTime = 0.1f;

    private bool _isChargingJump = false;  
    private bool _isInAir = false;

    //Player animation states
    Animator _animator;
    string _currentState;
    const string PLAYER_IDLE = "F_PlayerIdle";
    const string PLAYER_WALK = "F_PlayerWalk";
    const string PLAYER_JUMP = "F_PlayerJump";
    const string PLAYER_CHARGE_JUMP = "F_PlayerChargeJump";
    const string PLAYER_FALL = "F_PlayerFall";
    private float _waitEndJump = 0.28f;
    private float _timerEndJump = 1f;
    private bool _isJumping = false;


   
    void Start()
    {
        if (GameManager.Instance.GetLastCheckpointPosition() == Vector2.zero)
        {
            GameManager.Instance.SaveCheckpoint(transform.position);
        }
        else
        {
            Respawn();
        }

        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        HandleInputs();
        if (_isJumping == true)
        {
            _timerEndJump += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleGrounded();
        HandleJumpCharge();
        HandleJumpRelease();
        HandleJumpPhysics();
        HandlePlatform();

        if (!isAnimationPlaying(_animator, PLAYER_JUMP))
        {
            if (_isGrounded)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else if (!_isGrounded && (_timerEndJump > _waitEndJump))
            {
                ChangeAnimationState(PLAYER_FALL);
            }
            else if (_isChargingJump == true)
            {
                ChangeAnimationState(PLAYER_CHARGE_JUMP);
            }
            else if (_inputJump == true)
            {
                ChangeAnimationState(PLAYER_JUMP);
            }
        }
    }

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            
            _isChargingJump = true;
            _jumpHoldTime = 0f;
            _timerEndJump = 0f;
            _isJumping = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && _isChargingJump)
        {
            
            _isChargingJump = false;
            _inputJump = true;
            _timerEndJump = 0f;
            _isJumping = true;
        }
    }

    private void HandleMovements()
    {
        if (!_isInAir) 
        {
            var velocity = _rb.velocity;
            Vector2 wantedVelocity = new Vector2(_inputs.x * _walkSpeed, velocity.y);
            _rb.velocity = Vector2.MoveTowards(velocity, wantedVelocity, _acceleration * Time.deltaTime);
        }

        if (_inputs.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (_inputs.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void HandleGrounded()
    {
        _timeSinceGrounded += Time.deltaTime;

        Vector2 point = (Vector2)(transform.position + Vector3.up * _groundOffset);
        bool currentGrounded =
            Physics2D.OverlapCircleNonAlloc(point, _groundRadius, _collidersGround, _GroundLayer) > 0;

        if (!currentGrounded && _isGrounded)
        {
            _timeSinceGrounded = 0;
        }

        _isGrounded = currentGrounded;

        if (_isGrounded)
        {
            _isInAir = false; 
        }
    }

    private void HandleJumpCharge()
    {
        if (_isChargingJump)
        {
            
            _jumpHoldTime += Time.deltaTime;

            
            _jumpHoldTime = Mathf.Clamp(_jumpHoldTime, 0f, _maxHoldTime);

          
        }
    }

    private void HandleJumpRelease()
    {
        if (_inputJump && _isGrounded)
        {
            
            float jumpForce = Mathf.Lerp(_jumpForceMin, _jumpForceMax, _jumpHoldTime / _maxHoldTime);

            
            float jumpDirection = isFacingRight ? 1f : -1f;

           
            _rb.velocity = new Vector2(jumpDirection * _walkSpeed, jumpForce);

            
            _isInAir = true;

            
            _inputJump = false;
            _jumpHoldTime = 0f;

            
            
        }
    }

    private void HandleJumpPhysics()
    {
        if (_rb.velocity.y < 0) 
        {
            _rb.gravityScale = _fallMultiplier;  
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) 
        {
            _rb.gravityScale = _lowJumpMultiplier;  
        }
        else
        {
            _rb.gravityScale = 1f; 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * _groundOffset, _groundRadius);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void DieAndRespawn()
    {
        Debug.Log("Le joueur est mort !");
        Respawn();
    }

    private void Respawn()
    {
        transform.position = GameManager.Instance.GetLastCheckpointPosition();
        _rb.velocity = Vector2.zero;
    }

    public bool IsChargingJump()
    {
        return _isChargingJump;
    }

    
    public float GetJumpChargePercentage()
    {
        return Mathf.Clamp01(_jumpHoldTime / _maxHoldTime);
    }

    private void HandlePlatform()
    {
        if (_isOnPlatform == false) return;

        var difference = goPlatform.transform.position - _lastPlateformPosition;
        _lastPlateformPosition = goPlatform.transform.position;
        _rb.transform.position += difference;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MoovingPlatform"))
        {
            goPlatform = collision.gameObject;
            _isOnPlatform = true;
            _lastPlateformPosition = goPlatform.transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MoovingPlatform"))
        {
            _isOnPlatform = false;
        }
    }


    private void ChangeAnimationState(string newState)
    {
        if (newState == _currentState)
        {
            return;
        }

        _animator.Play(newState);
        _currentState = newState;
    }

    private bool isAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
