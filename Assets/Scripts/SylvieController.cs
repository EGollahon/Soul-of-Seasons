using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvieController : MonoBehaviour
{
    Rigidbody2D charRigidbody;
    BoxCollider2D boxCollider;
    Animator charAnimator;

    public float walkSpeed = 5.0f;
    public float jumpSpeed = 5.0f;
    public float gravity = -5.0f;

    Vector2 movement = new Vector2(0, 0);

    public bool isJumping = false;
    public bool isFalling = false;
    public float jumpTimer = -1.0f;

    string seasonOnLastUpdate;

    void Start() {
        charRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        // charAnimator = GetComponent<Animator>();

        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];
        int seasonLayer = LayerMask.NameToLayer(SeasonManager.seasonArray[SeasonManager.currentSeasonIndex]);
        gameObject.layer = seasonLayer;
    }

    void Update() {
        movement = Vector2.zero;
        movement.x = Input.GetAxis("Horizontal");

        if (isJumping) {

        } else if (isFalling) {

        } else if (movement.x > 0) {
            // charAnimator.SetBool("isWalking", true);
            // charAnimator.SetBool("isFacingLeft", false);
        } else if (movement.x < 0) {
            // charAnimator.SetBool("isWalking", true);
            // charAnimator.SetBool("isFacingLeft", true);
        } else {
            // charAnimator.SetBool("isWalking", false);
        }

        if (jumpTimer >= 0) {
            if (Input.GetAxis("Jump") < 1) {
                isJumping = false;
                isFalling = true;
                jumpTimer = -1.0f;
            } else {
                jumpTimer -= Time.deltaTime;
            }
            if (jumpTimer < 0) {
                isJumping = false;
                isFalling = true;
            }
        } else if (!isFalling) {
            if (Input.GetAxis("Jump") > 0) {
                isJumping = true;
                jumpTimer = 0.75f;
            }
        }

        if (seasonOnLastUpdate != SeasonManager.seasonArray[SeasonManager.currentSeasonIndex]) {
            int seasonLayer = LayerMask.NameToLayer(SeasonManager.seasonArray[SeasonManager.currentSeasonIndex]);
            gameObject.layer = seasonLayer;
        }
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];
    }

    void FixedUpdate() {
        Vector2 position = charRigidbody.position;
        Vector2 newPos = position;

        newPos.x += walkSpeed * movement.x * Time.deltaTime;
        if (!isJumping) {
            newPos.y += gravity * Time.deltaTime;
        } else {
            newPos.y += jumpSpeed * Time.deltaTime;
        }

        charRigidbody.MovePosition(newPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            isFalling = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tile" && !isJumping)
        {
            isFalling = true;
        }
    }
}
