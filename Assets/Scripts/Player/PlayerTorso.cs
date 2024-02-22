using UnityEngine;

public class PlayerTorso : MonoBehaviour
{
    [SerializeField] private TransformChanger transformChanger;
    [SerializeField] private Material bodyMat;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle")) //�arpan nesne Obstacle tagine sahipse
        {
            float value = other.bounds.size.y * 0.5f;  //�arpan nesnenin y boyutunu 0.5 ile �arp
            transformChanger.ChangeHeight(-value); //transformChanger ile y�ksekli�i ayarla


            //�arpan engel boyutuna g�re karakterde bir par�alanmay� sim�le etmek i�in
            //Bir kaps�l olu�tur ve gerekli bile�enleri ekle ve kuvvet uygula
            GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Capsule); 

            piece.GetComponent<MeshRenderer>().material = bodyMat;
            piece.GetComponent<Collider>().enabled = false;
            Rigidbody pieceRb = piece.AddComponent<Rigidbody>();
            
            piece.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            piece.transform.localScale = new Vector3(transform.lossyScale.x, value , transform.lossyScale.z);

            pieceRb.AddForce(-1, 1, -0.5f, ForceMode.Impulse);
            pieceRb.AddTorque(75, 15, 45);
        }
    }
}