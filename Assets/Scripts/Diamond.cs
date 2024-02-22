using UnityEngine;

public class Diamond : Collectable
{
    [SerializeField] private GameObject diamondParticle;

    public override void Collect()
    {
        base.Collect();
        //LevelCurrenye ekle
        CurrencyManager.Instance.AddLevelCurrency(1);
        
        //Diamond effecti oluþtur
        Instantiate(diamondParticle, transform.position, Quaternion.identity);
        
        //Diamondý deaktif yap
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player diamond'a çarptýðýnda topla
        if(other.gameObject.CompareTag("Player"))
            Collect();
    }
}
