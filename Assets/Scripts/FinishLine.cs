using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem confetti;

    [SerializeField]
    private SoundID confettiSound = SoundID.None;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetState(GameState.Won); //Oyun durumunu kazandý olarak ayala
            AudioManager.Instance.PlayEffect(confettiSound);
            confetti.Play(); //Confetti effectini oynat
        }
    }
}