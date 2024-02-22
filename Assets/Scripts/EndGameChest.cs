using UnityEngine;

public class EndGameChest : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Open");
            GameManager.Instance.SetState(GameState.End); //Oyun durumunu bitti olarak ayarla

            InputManager.Instance.enabled = false;
            PlayerController.Instance.CancelMovement();
            //Input manageri kapatmayý ekle
        }
    }
}
