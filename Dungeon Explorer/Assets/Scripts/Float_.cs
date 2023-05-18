using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float_ : MonoBehaviour
{
    [SerializeField] private float Distance;
    [SerializeField] private float down_Distance;
    [SerializeField] private Direction _direction; 
    [SerializeField] private float _speed = 0.5f;

    private float _percent = 0.0f;

    private Vector3 top, bottom;

    // Define direction up and down
    public enum Direction { UP, DOWN };

    void Start()
    {
        top = new Vector3(transform.position.x, transform.position.y + Distance, transform.position.z);
        bottom = new Vector3(transform.position.x, transform.position.y - down_Distance, transform.position.z);

    }

    void Update()
    {
        Apply_FloatingEffect();
    }

    // Apply the floating effect between the given positions
    void Apply_FloatingEffect()
    {
        if (_direction == Direction.UP && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(top, bottom, _percent);
        }
        else if (_direction == Direction.DOWN && _percent < 1)
        {
            _percent += Time.deltaTime * _speed;
            transform.position = Vector3.Lerp(bottom, top, _percent);
        }

        if (_percent >= 1)
        {
            _percent = 0.0f;
            if (_direction == Direction.UP)
            {
                _direction = Direction.DOWN;
            }
            else
            {
                _direction = Direction.UP;
            }
        }
    }
}
