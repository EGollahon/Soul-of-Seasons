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
    public float calculatedJumpSpeed = 0.0f;
    public float calculatedGravity = 0.0f;

    Vector2 movement = new Vector2(0, 0);

    public bool isJumping = false;
    public bool isFalling = false;
    public float jumpTimer = -1.0f;
    public float fallTimer = -1.0f;

    string seasonOnLastUpdate;

    public GameObject UIWinterShard;
    public GameObject UISpringShard;
    public GameObject UISummerShard;
    public GameObject UIAutumnShard;

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
                float t = jumpTimer * 2.0f;
                calculatedJumpSpeed = (30 * t * (1 - t)) + (Mathf.Pow(t, 2) * 15);
            }
            if (jumpTimer < 0) {
                isJumping = false;
                isFalling = true;
                fallTimer = 0.5f;
            }
        } else if (!isFalling) {
            if (Input.GetAxis("Jump") > 0) {
                isJumping = true;
                jumpTimer = 0.5f;
            }
        }

        if (fallTimer >= 0) {
            fallTimer -= Time.deltaTime;
            float t = 1.0f - (fallTimer * 2.0f);
            calculatedGravity = -((30 * t * (1 - t)) + (Mathf.Pow(t, 2) * 15));
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
            newPos.y += calculatedGravity * Time.deltaTime;
        } else {
            newPos.y += calculatedJumpSpeed * Time.deltaTime;
        }

        charRigidbody.MovePosition(newPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.tag == "Tile"
            || (collision.gameObject.tag == "Pond" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == "Winter")
            || (collision.gameObject.tag == "SpringLeaves" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == "Spring")
            || (collision.gameObject.tag == "AutumnLeaves" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == "Autumn")
        )
        {
            isFalling = false;
        } else if (collision.gameObject.tag == "Pond" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] != "Winter") {
            charRigidbody.position = collision.gameObject.GetComponent<WaterController>().respawnPoint;
            isFalling = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pond" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] != "Winter") {
            charRigidbody.position = collision.gameObject.GetComponent<WaterController>().respawnPoint;
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

    void OnTriggerEnter2D(Collider2D collider) {
        if (
            collider.gameObject.tag == "WinterShard" || collider.gameObject.tag == "SpringShard"
            || collider.gameObject.tag == "SummerShard" || collider.gameObject.tag == "AutumnShard"
        ) {
            collider.gameObject.SetActive(false);
            if (collider.gameObject.tag == "WinterShard") {
                SeasonManager.winterShardObtained = true;
                UIWinterShard.SetActive(true);
            } else if (collider.gameObject.tag == "SpringShard") {
                SeasonManager.springShardObtained = true;
                UISpringShard.SetActive(true);
            } else if (collider.gameObject.tag == "SummerShard") {
                SeasonManager.summerShardObtained = true;
                UISummerShard.SetActive(true);
            } else if (collider.gameObject.tag == "AutumnShard") {
                SeasonManager.autumnShardObtained = true;
                UIAutumnShard.SetActive(true);
            }
        }
    }
}
