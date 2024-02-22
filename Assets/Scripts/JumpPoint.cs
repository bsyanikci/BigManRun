using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    [SerializeField] private Transform jumpEndPoint;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //Çarpan nesne player tagýna sahipse
        {
            if(PlayerController.Instance != null) 
            {
                PlayerController.Instance.Jump(jumpEndPoint.position, jumpPower, jumpDuration); //Playerý jumpEndPoint pozisyonuna zýplat
            }
        }
    }
}
