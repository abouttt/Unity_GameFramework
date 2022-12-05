using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Managers.Resource.Instantiate("Cube");
            Managers.Sound.Play("TestSound", Enums.Sound.Effect);
        }
    }
}
