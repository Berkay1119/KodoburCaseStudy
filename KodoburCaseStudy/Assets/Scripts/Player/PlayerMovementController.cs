using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float playerWalkSpeed;
    [SerializeField] private float playerRunSpeed;
    private Vector3 _moveVector;
    private void Update()
    {
        _moveVector= new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        Move(_moveVector, Input.GetKey(KeyCode.LeftShift) ? playerRunSpeed : playerWalkSpeed);
    }

    private void Move(Vector3 moveVector,float speed)
    {
        Quaternion rotation = transform.rotation;
        if (moveVector.z!=0)
        {
            Vector3 direction = rotation * new Vector3(0,0,moveVector.z);

            Vector3 movement = direction * (speed * Time.deltaTime);
            
            transform.Translate(movement);
        }
        
        if (moveVector.x!=0)
        {
            Vector3 direction = rotation * new Vector3(moveVector.x,0,0);

            Vector3 movement = direction * (speed * Time.deltaTime);
            
            transform.Translate(movement);
        }
    }
}
