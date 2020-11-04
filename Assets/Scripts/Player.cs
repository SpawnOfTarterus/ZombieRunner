using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject gunControl = null;
    RigidbodyFirstPersonController personController;

    private void Start()
    {
        personController = GetComponent<RigidbodyFirstPersonController>();
    }

    public void TogglePlayerControls()
    {
        personController.enabled = !personController.enabled;
        gunControl.SetActive(false);
        if(personController.enabled) { return; }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
