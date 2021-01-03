using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldController : MonoBehaviour
{
    [SerializeField] private Tilemap _field;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private GameObject _fogTransition;

    private PlayerControls _playerControls;
    private Animator _playerMovementAnimator;
    private SpriteRenderer _playerSpriteRenderer;
    private Vector3 _nextPosition;
    private Vector2 _previousDirection;
    private bool _moving;
    [SerializeField] private float _inputDelay;
    private float _inputDelayCurrent;
    private bool _waiting;

    private bool _transitioning = false;
    private float fade = 1f;

    private void Awake()
    {
        _field.CompressBounds();

        _playerControls = new PlayerControls();
        
        _playerMovementAnimator = _playerObject.GetComponent<Animator>();
        _playerSpriteRenderer = _playerObject.GetComponent<SpriteRenderer>();

        Vector3 pos = _field.GetCellCenterWorld(new Vector3Int(1, 2, 0));

        // Set Starting Position
        _playerObject.transform.position = pos;
        _moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = _playerControls.Player.Move.ReadValue<Vector2>();
        
        if (_playerObject.transform.position == _nextPosition)
        {
            _moving = false;

            //_playerMovementAnimator.Play("Movement", 0, 0.0f);
            _playerMovementAnimator.SetBool("Moving", false);
        }
        updateDelay();
        if ((movementInput.x != 0 || movementInput.y != 0) && !_waiting)
        {
            _transitioning = true;
            if (!_moving)
            {
                //_playerSpriteRenderer.flipX = movementInput.x < 0;

                _playerMovementAnimator.SetBool("Idle", false);

                if (_previousDirection == movementInput)
                {
                    _moving = true;
                    _playerMovementAnimator.SetBool("Moving", true);
                }
                else
                {
                    _inputDelayCurrent = _inputDelay;
                    _waiting = true;
                    _playerMovementAnimator.SetFloat("Horizontal", movementInput.x);
                    _playerMovementAnimator.SetFloat("Vertical", movementInput.y);
                }

                Vector3Int nextCellLocation = getNextMoveTile(movementInput);
                _nextPosition = _field.GetCellCenterWorld(nextCellLocation);

                TileBase currentTile = _field.GetTile(nextCellLocation);
                if (currentTile == null /*|| currentTile.name == "tilemap_2"*/)
                {
                    _moving = false;
                    _playerMovementAnimator.SetBool("Moving", false);
                }
                _previousDirection = movementInput;
            }
        }

        if (_transitioning)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
                _transitioning = false;
            }
            
            Material mat = _fogTransition.GetComponent<SpriteRenderer>().material;
            mat.SetFloat("_Fade", fade);
        }
    }
    private Vector3Int getNextMoveTile(Vector2 movementVector)
    {
        Vector3Int playerCellLocation = _field.WorldToCell(_playerObject.transform.position);

        playerCellLocation.x += (int)movementVector.x;
        playerCellLocation.y += (int)movementVector.y;

        return playerCellLocation;
    }

    private void FixedUpdate()
    {
        if (_moving)
        {
            _playerObject.transform.position = Vector3.MoveTowards(
                _playerObject.transform.position,
                _nextPosition,
                _movementSpeed * Time.deltaTime);
        }
    }

    private void updateDelay()
    {
        if (_waiting && _inputDelayCurrent > 0)
        {
            _inputDelayCurrent -= Time.deltaTime;
        }
        if (_inputDelayCurrent < 0)
        {
            _inputDelayCurrent = 0;
            _waiting = false;
        }
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
