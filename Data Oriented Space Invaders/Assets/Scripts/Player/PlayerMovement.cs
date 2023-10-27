using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private void Update()
    {
        var input = Input.GetAxis("Horizontal");
        if (input == 0f)
        {
            return;
        }

        Move(input);
    }

    private void Move(float input)
    {
        var speed = input * _speed;
        var velocity = speed * Vector3.right;
        var deltaPosition = velocity * Time.deltaTime;
        var newPosition = transform.position + deltaPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, -GameSettings.Instance.Width, GameSettings.Instance.Width);
        transform.position = newPosition;
    }
}
