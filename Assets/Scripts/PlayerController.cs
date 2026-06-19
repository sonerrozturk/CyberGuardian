using UnityEngine;
using System.Collections; // Coroutine 

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f; 
    public float xLimit = 8.39f;

    [Header("Start Delay Settings")]
    public float initialFreezeTime = 0f; 

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded; // Checks whether it touches the ground
    private bool canDoubleJump;

    // Karakterin hareket kilidini tutan deðiþken
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Places the character on the specific line you want when the game starts
        transform.position = new Vector3(transform.position.x, -3.6f, 0);

        // Eðer Inspector'dan bekleme süresi girilmiþse, kilidi kapat ve zamanlayýcýyý baþlat
        if (initialFreezeTime > 0f)
        {
            StartCoroutine(EnableMovementAfterDelay());
        }
    }

    // Bekleme süresini sayan zamanlayýcý fonksiyon
    IEnumerator EnableMovementAfterDelay()
    {
        canMove = false; // Hareketi kilitle
        yield return new WaitForSeconds(initialFreezeTime); // Belirtilen saniye kadar bekle
        canMove = true;  // Süre dolunca kilidi aç
    }

    void Update()
    {
        // Eðer karakter kilitliyse (bekleme süresindeyse) hareket kodlarýný okuma
        if (!canMove)
        {
            // Karakterin saða sola kaymasýný engelle ama yerçekimi (aþaðý düþme) çalýþmaya devam etsin
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            
            anim.SetFloat("Speed", 0);
            return; 
        }

        // 1. LEFT-RIGHT MOVEMENT
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // 2. JUMPING 
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // If character is on the ground (Normal Jump)
            if (isGrounded)
            {
                PerformJump();
                canDoubleJump = true; // Give 1 more try in the air
            }
            // If character is in the air AND double jump is available
            else if (canDoubleJump)
            {
                PerformJump();
                canDoubleJump = false; // Used the try, cannot jump again until touching ground
            }
        }

        // Animation speed
        anim.SetFloat("Speed", Mathf.Abs(moveX));

        // Rotating the character
        if (moveX > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveX < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    // Physics function to check if it touches the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Tugla"))
        {
            isGrounded = true;
        }
    }

    private void PerformJump()
    {
        // Reset fall speed
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        // Apply jump power
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Update states
        isGrounded = false;
        anim.SetTrigger("Jump"); 
    }
}