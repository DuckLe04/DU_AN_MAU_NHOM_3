using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    private GameObject CurentTeleporter;

    private Player plS;

    private void Start()
    {
        plS = GetComponent<Player>();
    }
    private void Update()
    {
        InputKey();
    }

    #region ontrigger teleport
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("teleport"))
        {
            CurentTeleporter = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("teleport"))
        {
            if (collision.gameObject == CurentTeleporter)
            {
                CurentTeleporter = null;
            }
        }
    }

    #endregion

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CurentTeleporter != null)
            {

                transform.position = CurentTeleporter.GetComponent<Teleporter>()
                    .GetDistination().position;
            }
        }
    }

}
