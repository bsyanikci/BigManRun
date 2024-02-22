using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void Awake()
    {
        Instantiate(levelManager);
    }

}
