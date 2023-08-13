using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private Vector2 currentDirection;
    private Rigidbody2D rb;
    private bool canChangeDirection = true;
    private KeyCode[] playerKeys;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDirection = Vector2.right; // Initial direction

        // Set player keys based on the GameObject's tag
        if (gameObject.CompareTag("Player1"))
            playerKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
        else if (gameObject.CompareTag("Player2"))
            playerKeys = new KeyCode[] { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

        // Start snake movement immediately
        rb.velocity = currentDirection * moveSpeed;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        // Move the snake automatically in its current direction
        MoveSnake(currentDirection);
    }

    private void HandleInput()
    {
        if (!canChangeDirection)
            return;

        if (Input.GetKeyDown(playerKeys[0]) && currentDirection != Vector2.down)
            ChangeDirection(Vector2.up);
        else if (Input.GetKeyDown(playerKeys[1]) && currentDirection != Vector2.up)
            ChangeDirection(Vector2.down);
        else if (Input.GetKeyDown(playerKeys[2]) && currentDirection != Vector2.right)
            ChangeDirection(Vector2.left);
        else if (Input.GetKeyDown(playerKeys[3]) && currentDirection != Vector2.left)
            ChangeDirection(Vector2.right);
    }

    private void ChangeDirection(Vector2 newDirection)
    {
        canChangeDirection = false;
        currentDirection = newDirection;
        UpdateHeadSprite();
        Invoke("EnableChangeDirection", 0.2f); // Delay to prevent rapid direction changes
    }

    private void EnableChangeDirection()
    {
        canChangeDirection = true;
    }

    private void MoveSnake(Vector2 direction)
    {
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void UpdateHeadSprite()
    {
        // Change snake head sprite based on movement direction
        if (currentDirection == Vector2.up)
            GetComponent<SpriteRenderer>().sprite = upSprite;
        else if (currentDirection == Vector2.down)
            GetComponent<SpriteRenderer>().sprite = downSprite;
        else if (currentDirection == Vector2.left)
            GetComponent<SpriteRenderer>().sprite = leftSprite;
        else if (currentDirection == Vector2.right)
            GetComponent<SpriteRenderer>().sprite = rightSprite;
    }
}