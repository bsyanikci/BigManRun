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
        SetGate(); //Baþlangýçda Gate'leri ayarla
    }

    private void OnValidate()
    {
        SetGate(); //Deðerlerde bir deðiþiklik olduðunda Gate'i ayarla
    }

    private void OnTriggerEnter(Collider other)
    {
        // Çarpýþan objenin TransformChanger bileþenine sahip olup olmadýðýný kontrol et
        if (other.gameObject.GetComponent<TransformChanger>() != null)
        {
            // Objeden TransformChanger bileþenini al
            TransformChanger transformChanger = other.gameObject.GetComponent<TransformChanger>();
            if (gateType == GateType.Height)  // Kapý türü "Height" ise
            {
                // TransformChanger ile playerin yüksekliðini deðer ve çarpan ile deðiþtir
                transformChanger.ChangeHeight(value * heightMultiplier);
            }
            else
            {
                // TransformChanger ile playerin geniþliðini deðer ve çarpan ile deðiþtir
                transformChanger.ChangeThickness(value * thicknessMultiplier);
            }
            // Kapýyý etkisizleþtir
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

        //Bir materyal oluþturup transparent olarak ayarlayýp meshRenderera atama iþlemi
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

        // Her bir kapý için yeni bir malzeme örneði oluþtur
        Material gateMaterialInstance = new Material(gateMeshRenderer.sharedMaterial);

        // Kapý türüne göre malzeme ve görünüm ayarlarýný yap
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

        // Yeni malzeme örneðini kapýya ata
        gateMeshRenderer.material = gateMaterialInstance;
    }
}