using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Testawd : MonoBehaviour
{
    [Inject]
    private void Initialize(PlayerHUD playerHUD)
    {
        Debug.Log("awd");
    }
}
