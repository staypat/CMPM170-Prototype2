using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right keys
        float moveVertical = Input.GetAxis("Vertical");     // W/S or Up/Down keys

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical) * moveSpeed * Time.deltaTime;

        // Move the player in the direction they are facing
        transform.Translate(movement, Space.Self);
    }
}
