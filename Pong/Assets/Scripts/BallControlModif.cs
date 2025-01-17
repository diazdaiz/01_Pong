﻿using UnityEngine;

public class BallControlModif : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidBody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float initialForce;
    float yInitialForce;
    float xInitialForce;

    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    void ResetBall() {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidBody2D.velocity = Vector2.zero;
    }

    void PushBall() {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        // nilai 2/3 agar nilai yRandomInitialForce tidak secara tidak sengaja mendominasi xInitialForce
        yInitialForce = (2f / 3f) * initialForce;
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Besar x agar lajunya tetap sesuai InitialForce
        xInitialForce = Mathf.Pow(Mathf.Pow(initialForce, 2f) - Mathf.Pow(yRandomInitialForce, 2f), 1.0f / 2.0f);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (randomDirection < 1.0f) {
            // Gunakan gaya untuk menggerakkan bola ini.
            rigidBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));
        }
        else {
            rigidBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }

    void RestartGame() {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    void Start() {
        rigidBody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        // Mulai game
        RestartGame();
    }

    // Ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision) {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin {
        get { return trajectoryOrigin; }
    }
}
