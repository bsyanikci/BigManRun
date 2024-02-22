using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private SoundID sound = SoundID.None;

    public virtual void Collect() //Toplandýðýnda effect sesi çal
    {
        AudioManager.Instance.PlayEffect(sound);
    }
}
