using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap colTileMap;
    [SerializeField] int movementSpacesAllowed;

    private PlayerMovement controls;

    private Vector2 movement;

    private void Awake()
    {
        controls = new PlayerMovement();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Movement.performed -= ctx => Move(ctx.ReadValue<Vector2>());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction) && movementSpacesAllowed > 0)
        {
            transform.position += (Vector3)direction;
            movementSpacesAllowed--;
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || colTileMap.HasTile(gridPosition))
        {
            return false;
        }
        return true;
    }
}
