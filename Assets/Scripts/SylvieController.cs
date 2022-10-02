using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvieController : MonoBehaviour
{
    Rigidbody2D ivyRigidbody;
    BoxCollider2D boxCollider;
    Animator ivyAnimator;

    public float walkSpeed = 5.0f;
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
        ivyRigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        ivyAnimator = GetComponent<Animator>();

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
            ivyAnimator.SetFloat("LookDirection", 1.0f);
            ivyAnimator.SetBool("IsMoving", true);
        } else if (movement.x < 0) {
            ivyAnimator.SetFloat("LookDirection", -1.0f);
            ivyAnimator.SetBool("IsMoving", true);
        } else {
            ivyAnimator.SetBool("IsMoving", false);
        }

        if (jumpTimer >= 0) {
            if (Input.GetAxis("Jump") < 1) {
                isJumping = false;
                isFalling = true;
                jumpTimer = -1.0f;
                ivyAnimator.SetBool("IsJumping", false);
                ivyAnimator.SetBool("IsFalling", true);
            } else {
                jumpTimer -= Time.deltaTime;
                float t = jumpTimer * 2.0f;
                calculatedJumpSpeed = (30 * t * (1 - t)) + (Mathf.Pow(t, 2) * 15);
            }
            if (jumpTimer < 0) {
                isJumping = false;
                isFalling = true;
                fallTimer = 0.5f;
                ivyAnimator.SetBool("IsJumping", false);
                ivyAnimator.SetBool("IsFalling", true);
            }
        } else if (!isFalling) {
            if (Input.GetAxis("Jump") > 0) {
                isJumping = true;
                jumpTimer = 0.5f;
                ivyAnimator.SetTrigger("Jump");
                Debug.Log("jump");
                ivyAnimator.SetBool("IsJumping", true);
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
        Vector2 position = ivyRigidbody.position;
        Vector2 newPos = position;

        newPos.x += walkSpeed * movement.x * Time.deltaTime;
        if (isFalling) {
            newPos.y += calculatedGravity * Time.deltaTime;
        } else if (isJumping) {
            newPos.y += calculatedJumpSpeed * Time.deltaTime;
        }

        ivyRigidbody.MovePosition(newPos);
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
            ivyAnimator.SetTrigger("Land");
            Debug.Log("land1");
            ivyAnimator.SetBool("IsFalling", false);
        } else if (collision.gameObject.tag == "Pond" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] != "Winter") {
            ivyRigidbody.position = collision.gameObject.GetComponent<WaterController>().respawnPoint;
            isFalling = false;
            ivyAnimator.SetTrigger("Land");
            Debug.Log("land2");
            ivyAnimator.SetBool("IsFalling", false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pond" && SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] != "Winter") {
            ivyRigidbody.position = collision.gameObject.GetComponent<WaterController>().respawnPoint;
            isFalling = false;
            ivyAnimator.SetTrigger("Land");
            Debug.Log("land3");
            ivyAnimator.SetBool("IsFalling", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!isJumping && (collision.gameObject.tag == "Tile" || collision.gameObject.tag == "SpringLeaves" || collision.gameObject.tag == "AutumnLeaves"))
        {
            isFalling = true;
            ivyAnimator.SetBool("IsFalling", true);
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
