using System.Collections.Generic;
using UnityEngine;

public class TransformChanger : MonoBehaviour
{
    [SerializeField] private List<Transform> thicknessPieces;
    [SerializeField] private Transform upperBody;
    [SerializeField] private Transform torso;
    [SerializeField] private Transform root;

    [SerializeField] private float defaultThicknessMultiplier = 0.01f;
    [SerializeField] private float defaultHeightMultiplier = 0.005f;

    public float DefaultThicknessMultiplier => defaultThicknessMultiplier;
    public float DefaultHeightMultiplier => defaultHeightMultiplier;

    private CapsuleCollider capsuleCollider;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Ba�lang��da kaydedilen kal�nl�k ve y�kseklik levelini �ek
        int thicknessLevel = DataController.Instance.GameData.thicknessLevel;
        int heightLevel = DataController.Instance.GameData.heightLevel;

        //Kaydedilen levele g�re boyutlar� ayarla
        ChangeThickness(thicknessLevel * defaultThicknessMultiplier);
        ChangeHeight(heightLevel * defaultHeightMultiplier);
    }

    //Y�ksekli�i de�i�tir
    public void ChangeHeight(float value)
    {
        torso.localScale += new Vector3(0, value, 0);
        upperBody.position += new Vector3(0, value * 2, 0);

        capsuleCollider.height += value * 2;
        capsuleCollider.center += new Vector3(0, value, 0);

        //G�vdenin boyutu 0.05den k���kse karakteri �ld�r
        if (torso.localScale.y < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }

    //Kal�nl�k par�alar�n� ayarla
    public void ChangeThickness(float value)
    {
        foreach(Transform item in thicknessPieces)
        {
            item.localScale += new Vector3(value, 0, value);
        }
        root.localScale += new Vector3(value, value * 0.5f, value);

        //G�vdenin boyutu 0.05den k���kse karakteri �ld�r
        if(root.localScale.x < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }

}