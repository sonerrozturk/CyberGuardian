using UnityEngine;
using TMPro;

public class FallingPassword : MonoBehaviour
{
    [Header("Password Settings")]
    public TMP_Text passwordText;

    [Header("Password Lists")]
    private string[] strongPasswords = {
        "Gvnl!2024", "Mavi*Bulut*5", "Kilit-65@!", "Robot?321!", "Zirhlý*Park",
        "A?bc_987", "Siber.Kalkan.1", "M@vi_Bulut9", "K3di!Uctu?", "Y1ld1z#P@rla",
        "#G1zliMeyv5", "C1lek_R3celi!", "7Ucan*Kus!", "P@t1zler_2", "S1h1rli&Kutu",
        "!K@hramn_8", "Z3ka#Kupu1", "Uzm@n_Oyuncu7", "G!zli_K@p1", "D0ga#Yuruyusu!",
        "K@rtl.Baki!9", "P!nguen_Ad1mi", "Ayd1nl1k#Gec3", "R3nkli.K@lem!", "S3ssiz!Gemi9",
        "K@ptan_M@rt1", "C#lik_Kalkan!", "G0k#Kusag1_", "P@rlak_Gun$", "Oyun_Ustas1!"
    };

    private string[] weakPasswords = {
        "123456", "þifre123", "fenerbahce", "istanbul34", "admin",
        "þifrem", "000000", "qwerty", "galatasaray", "elif2016",
        "111111", "oyuncu1", "asdfgh", "kral123", "123456789",
        "besiktas", "1234", "123123", "isim123", "deneme",
        "deneme123", "parola", "parola123", "trabzonspor", "ankara06",
        "izmir35", "qazwsx", "12345", "12345678", "112233"
    };

    public bool isCorrectPassword;
    private bool isPlaced = false;

    void Awake()
    {
        isCorrectPassword = (Random.value > 0.5f);

        string selectedText = isCorrectPassword ?
            strongPasswords[Random.Range(0, strongPasswords.Length)] :
            weakPasswords[Random.Range(0, weakPasswords.Length)];

        if (passwordText != null) passwordText.text = selectedText;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = isCorrectPassword ? new Color(0.7f, 1f, 0.7f) : new Color(1f, 0.7f, 0.7f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // STEP 1: Hitting the ground or the stack
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Untagged"))
        {
            if (isCorrectPassword)
            {
                if (!isPlaced)
                {
                    isPlaced = true;
                    GameManager.instance.AddCorrectBrick();

                    // Boxes can now physically tilt and topple over.
                    gameObject.tag = "Ground"; // Make it part of the stack
                }
            }
            else
            {
                GameManager.instance.RestartGame();
            }
            return;
        }

        // STEP 2: Collision with the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPlaced)
            {
                Destroy(gameObject);
            }
        }
    }
}