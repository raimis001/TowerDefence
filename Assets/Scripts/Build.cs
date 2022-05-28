using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Build : MonoBehaviour
{
    public GameObject[] turrets;
    public LayerMask groundLayer;

    GameObject currentTurret;

    public void BuildTurret(int id)
    {
        if (currentTurret)
            Destroy(currentTurret);

        currentTurret = Instantiate(turrets[id]);
        TurretPosition();
    }

    private void Update()
    {
        if (!currentTurret)
            return;


        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(currentTurret);
            currentTurret = null;
            return;
        }

        TurretPosition();

        if (Input.GetMouseButtonDown(0))
        {
            Turret turret = currentTurret.GetComponent<Turret>();
            if (!turret.canPlace)
            {
                UI.ShowWarning("Can`t build there!");
                return;
            }

            if (turret.price > Game.money)
            {
                UI.ShowWarning("You don`t have money for building!");
                return;
            }

            Game.money -= turret.price;
            turret.active = true;
            currentTurret = null;
        }
            
    }

    void TurretPosition()
    {
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(mouse, out RaycastHit hit, Mathf.Infinity, groundLayer))
            return;

        currentTurret.transform.position = hit.point;
    }
}
