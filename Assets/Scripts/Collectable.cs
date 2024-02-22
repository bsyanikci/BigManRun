using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private SoundID sound = SoundID.None;

    public virtual void Collect() //Topland���nda effect sesi �al
    {
        AudioManager.Instance.PlayEffect(sound);
    }
}
