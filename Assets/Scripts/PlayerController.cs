using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _inputs;
    [SerializeField] private bool _inputJump;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _acceleration;


    [SerializeField] private float _groundOffset;
    [SerializeField] private float _groundRadius;

    Collider2D[] _collidersGround = new Collider2D[2];
    public LayerMask _GroundLayer;

    [Header("Jump")]
    [SerializeField] private float _timerNoJump;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _timeMinBetweenJump = 0.1f;
    [SerializeField] private int _velocityFallMin;
    [SerializeField] private float _gravity;
    [SerializeField] private float _gravityUpJump;
    [SerializeField] private float _timeSinceJumpPressed;
    private float _jumpInputTimer = 0.1f;
    [SerializeField] public float _timeSinceGrounded;
    [SerializeField] private float _coyoteTime;


    [Header("Slope Detection")]
    public float _slopeDetectOffset = 0.5f;
    public PhysicsMaterial2D _Friction;
    public PhysicsMaterial2D _NoFriction;
    private bool _isOnSlope = false;
    private RaycastHit2D[] _hitResults = new RaycastHit2D[1];
    private Collider2D _collider;

    [Header("TeleportPlayer")]
    [SerializeField] private Vector2 _offsetCollisionBox;
    [SerializeField] private Vector2 _offsetToReplace;
     private Vector2 _collisionBox;
    private float[] directions = new float[] { -1f, 1f };

    [Header("Facing")]
    private bool isFacingRight = true;

   
 
    


    

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleGrounded();
        HandleJump();
        _timeSinceJumpPressed += Time.deltaTime;
        
    }

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _inputJump = Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            _timeSinceJumpPressed = 0;



    }

    private void HandleMovements()
    {
        var velocity  = _rb.velocity;   
        Vector2 wantedVelocity = new Vector2(_inputs.x * _walkSpeed, velocity.y);
        _rb.velocity = Vector2.MoveTowards( velocity,  wantedVelocity,  _acceleration * Time.deltaTime);

        if(_inputs.x > 0 && !isFacingRight)
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

        if (currentGrounded == false && _isGrounded)
        {
            _timeSinceGrounded = 0;
        }

        _isGrounded = currentGrounded;

        
  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.up * _groundOffset, _groundRadius);
    }

    private void HandleJump()
    {

        _timerNoJump -= Time.deltaTime;

        if (_inputJump && (_rb.velocity.y <= 0 || _isOnSlope) && (_isGrounded || _timeSinceGrounded < _coyoteTime) && _timerNoJump <= 0 && _timeSinceJumpPressed < _jumpInputTimer)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _timerNoJump = _timeMinBetweenJump;

        }
        if (_isGrounded == false) 
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = _gravity;
            }
            else
            {
                _rb.gravityScale = _inputJump ? _gravityUpJump : _gravity;
            }
        }
        else
        {
            _rb.gravityScale = _gravity;
        }

        if (_rb.velocity.y < _velocityFallMin)
            _rb.velocity = new Vector2(_rb.velocity.x, _velocityFallMin);
  
    }

    void handleSlope()
    {
        Vector3 origin = transform.position + Vector3.up * _groundOffset;
        bool slopeRight = Physics2D.RaycastNonAlloc(origin, Vector2.right, _hitResults, _slopeDetectOffset, _GroundLayer) > 0;
        bool slopeLeft = Physics2D.RaycastNonAlloc(origin, Vector2.right, _hitResults, _slopeDetectOffset, _GroundLayer) > 0;

        _isOnSlope = (slopeRight || slopeLeft) && (slopeRight == false || slopeLeft == false);
        if (Mathf.Abs(_inputs.x) < 00.1f && (slopeRight || slopeLeft))
        {
            _collider.sharedMaterial = _Friction;
        }
        else
        {
            _collider.sharedMaterial = _NoFriction;
        }
    }

    private void HandleCorners()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            float dir = directions[i];

            if (Mathf.Abs(_inputs.x) > 0.1f && Mathf.Abs(Mathf.Sign(dir) - Mathf.Sign(_inputs.x)) < 0.001f && _isGrounded == false && _isOnSlope == false)
            {
                Vector3 position = transform.position + new Vector3(_offsetCollisionBox.x + dir * _offsetToReplace.x, _offsetCollisionBox.y, 0);
                int result =
                    Physics2D.BoxCastNonAlloc((Vector2)position, _collisionBox, 0, Vector2.zero, _hitResults, 0, _GroundLayer);

                if (result > 0)
                {
                    position = transform.position + new Vector3(_offsetCollisionBox.x + dir * _offsetToReplace.x, _offsetCollisionBox.y + _offsetToReplace.y, 0);
                    result = Physics2D.BoxCastNonAlloc((Vector2)position, _collisionBox, 0, Vector2.zero, _hitResults, 0, _GroundLayer);

                    if (result == 0)
                    {
                        Debug.Log("éreplace");
                        transform.position += new Vector3(dir * _offsetToReplace.x, _offsetToReplace.y);

                        if (_rb.velocity.y < 0)
                        {
                            _rb.velocity = new Vector2(_rb.velocity.x, 0);
                        }
                    }
                }
            }
        }

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


}
