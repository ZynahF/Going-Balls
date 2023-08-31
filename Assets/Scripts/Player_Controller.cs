using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Player_Controller : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject finalTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private bool hasWon = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        finalTextObject.SetActive(false);
        SceneManager.UnloadScene("Level 2");
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = count.ToString();
        countText.enabled = false;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        if (!hasWon && other.gameObject.CompareTag("Finish") && count >= 11)
        {
            hasWon = true;
            winTextObject.SetActive(true);
            StartCoroutine(LoadSceneWithDelay("Level 2", 3f));

        }

        if (other.gameObject.CompareTag("Done") && count >= 16)
        {
            finalTextObject.SetActive(true);

        }
    }


    IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
        SceneManager.UnloadScene("Level 1");
    }


}
