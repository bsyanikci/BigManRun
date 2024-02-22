using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject head;
    public void Death()
    {
        GameObject headClone = Instantiate(head, head.transform.position, Quaternion.identity); //karakterin kafas�n�n bir kopyas�n� olu�tur
        headClone.AddComponent<SphereCollider>(); //Sphere collider bile�eni ekle
        Rigidbody headCloneRb = headClone.AddComponent<Rigidbody>(); //fizik bile�eni ekle
        headCloneRb.AddForce(0, 2, 5, ForceMode.Impulse); // bir kuvvet uygula

        GameManager.Instance.SetState(GameState.End); //Oyun durumunu bitti olarak ayarla
        gameObject.SetActive(false); //ve nesneyi deaktif et
    }
}
