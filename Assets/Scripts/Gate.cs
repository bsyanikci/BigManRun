using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GateType
{
    Height,
    Thickness
}

public class Gate : MonoBehaviour
{
    public GateType gateType = GateType.Height;
    public MeshRenderer gateMeshRenderer;
    public TextMeshPro valueText;
    public Image header;
    public Image arrow;
    public Sprite positiveThicknessSprite;
    public Sprite negativeThicknessSprite;
    public Sprite positiveHeightSprite;
    public Sprite negativeHeightSprite;
    public Color headerRedColor;
    public Color headerBlueColor;
    public int value;
    public float alphaValue = 0.8f;
    private float heightMultiplier = 0.005f;
    private float thicknessMultiplier = 0.01f;


    private void Awake()
    {
        SetGate(); //Ba�lang��da Gate'leri ayarla
    }

    private void OnValidate()
    {
        SetGate(); //De�erlerde bir de�i�iklik oldu�unda Gate'i ayarla
    }

    private void OnTriggerEnter(Collider other)
    {
        // �arp��an objenin TransformChanger bile�enine sahip olup olmad���n� kontrol et
        if (other.gameObject.GetComponent<TransformChanger>() != null)
        {
            // Objeden TransformChanger bile�enini al
            TransformChanger transformChanger = other.gameObject.GetComponent<TransformChanger>();
            if (gateType == GateType.Height)  // Kap� t�r� "Height" ise
            {
                // TransformChanger ile playerin y�ksekli�ini de�er ve �arpan ile de�i�tir
                transformChanger.ChangeHeight(value * heightMultiplier);
            }
            else
            {
                // TransformChanger ile playerin geni�li�ini de�er ve �arpan ile de�i�tir
                transformChanger.ChangeThickness(value * thicknessMultiplier);
            }
            // Kap�y� etkisizle�tir
            gameObject.SetActive(false);
        }
    }

    private void SetGate()
    {
        /*
        if (gateMeshRenderer == null || gateMeshRenderer.sharedMaterial == null)
            return;
        */
        if (gateMeshRenderer == null)
            return;

        //Bir materyal olu�turup transparent olarak ayarlay�p meshRenderera atama i�lemi
        if (gateMeshRenderer.sharedMaterial == null)
        {
            Material material = new Material(Shader.Find("Standard"));
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            gateMeshRenderer.sharedMaterial = material;
        }

        // Her bir kap� i�in yeni bir malzeme �rne�i olu�tur
        Material gateMaterialInstance = new Material(gateMeshRenderer.sharedMaterial);

        // Kap� t�r�ne g�re malzeme ve g�r�n�m ayarlar�n� yap
        switch (gateType)
        {
            case GateType.Height:
                if (value < 0)
                {
                    header.color = headerRedColor;
                    arrow.sprite = negativeHeightSprite;
                    gateMaterialInstance.color = new Color(headerRedColor.r, headerRedColor.g, headerRedColor.b, alphaValue);
                    valueText.text = value.ToString();
                }
                else
                {
                    header.color = headerBlueColor;
                    arrow.sprite = positiveHeightSprite;
                    gateMaterialInstance.color = new Color(headerBlueColor.r, headerBlueColor.g, headerBlueColor.b, alphaValue);
                    valueText.text = "+" + value;
                }
                break;

            case GateType.Thickness:
                if (value < 0)
                {
                    header.color = headerRedColor;
                    arrow.sprite = negativeThicknessSprite;
                    gateMaterialInstance.color = new Color(headerRedColor.r, headerRedColor.g, headerRedColor.b, alphaValue);
                    valueText.text = value.ToString();
                }
                else
                {
                    header.color = headerBlueColor;
                    arrow.sprite = positiveThicknessSprite;
                    gateMaterialInstance.color = new Color(headerBlueColor.r, headerBlueColor.g, headerBlueColor.b, alphaValue);
                    valueText.text = "+" + value;
                }
                break;
        }

        // Yeni malzeme �rne�ini kap�ya ata
        gateMeshRenderer.material = gateMaterialInstance;
    }
}