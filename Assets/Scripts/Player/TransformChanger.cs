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

        //Baþlangýçda kaydedilen kalýnlýk ve yükseklik levelini çek
        int thicknessLevel = DataController.Instance.GameData.thicknessLevel;
        int heightLevel = DataController.Instance.GameData.heightLevel;

        //Kaydedilen levele göre boyutlarý ayarla
        ChangeThickness(thicknessLevel * defaultThicknessMultiplier);
        ChangeHeight(heightLevel * defaultHeightMultiplier);
    }

    //Yüksekliði deðiþtir
    public void ChangeHeight(float value)
    {
        torso.localScale += new Vector3(0, value, 0);
        upperBody.position += new Vector3(0, value * 2, 0);

        capsuleCollider.height += value * 2;
        capsuleCollider.center += new Vector3(0, value, 0);

        //Gövdenin boyutu 0.05den küçükse karakteri öldür
        if (torso.localScale.y < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }

    //Kalýnlýk parçalarýný ayarla
    public void ChangeThickness(float value)
    {
        foreach(Transform item in thicknessPieces)
        {
            item.localScale += new Vector3(value, 0, value);
        }
        root.localScale += new Vector3(value, value * 0.5f, value);

        //Gövdenin boyutu 0.05den küçükse karakteri öldür
        if(root.localScale.x < 0.1f)
        {
            GetComponent<PlayerDead>().Death();
        }
    }

}