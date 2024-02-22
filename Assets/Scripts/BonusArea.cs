using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> bonusModelContainers;
    [SerializeField] private float bonusIncreaseValue = 0.3f;
    private float bonusMultiplier = 1.0f;
    public float BonusMultiplier => bonusMultiplier;
    private void Awake()
    {
        float bonusValue = bonusMultiplier;

        //bonuslar�n yazd��� modellere bonus value'yi ata
        foreach(var container in bonusModelContainers)
        {
            bonusValue += bonusIncreaseValue;
            container.transform.GetChild(0).GetComponentInChildren<TextMeshPro>().text = bonusValue.ToString();
            container.transform.GetChild(1).GetComponentInChildren<TextMeshPro>().text = bonusValue.ToString();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) //Player alana girdikten sonra
            PlayerController.Instance.SetMaxXPosition(0); //Hareket edememesi i�in gidebilece�i pozisyonu 0 ile sabitle
    }
    public void UpdateBonusMultiplier()
    {
        //Bonus �arpan�n� a�t��� engellere g�re artt�r
        bonusMultiplier += bonusIncreaseValue;
        Debug.Log("bonus multiplier " + bonusMultiplier);
    }
}
