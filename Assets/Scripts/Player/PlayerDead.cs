using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject head;
    public void Death()
    {
        GameObject headClone = Instantiate(head, head.transform.position, Quaternion.identity); //karakterin kafasýnýn bir kopyasýný oluþtur
        headClone.AddComponent<SphereCollider>(); //Sphere collider bileþeni ekle
        Rigidbody headCloneRb = headClone.AddComponent<Rigidbody>(); //fizik bileþeni ekle
        headCloneRb.AddForce(0, 2, 5, ForceMode.Impulse); // bir kuvvet uygula

        GameManager.Instance.SetState(GameState.End); //Oyun durumunu bitti olarak ayarla
        gameObject.SetActive(false); //ve nesneyi deaktif et
    }
}
