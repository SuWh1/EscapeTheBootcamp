using UnityEngine;

public class FlashlightSway : MonoBehaviour
{
    [SerializeField] private float swayAmount = 0.05f;
    [SerializeField] private float swaySpeed = 3f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveZ) > 0.01f;

        if (isMoving)
        {
            float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            float swayY = Mathf.Cos(Time.time * swaySpeed * 2f) * swayAmount * 0.5f;

            Vector3 swayOffset = new Vector3(swayX, swayY, 0f);
            transform.localPosition = initialPosition + swayOffset;
        }
        else
        {
            // Return to initial position smoothly
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * swaySpeed);
        }
    }
}
