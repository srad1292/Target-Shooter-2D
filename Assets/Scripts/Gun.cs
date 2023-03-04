using System.Collections;
using System;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    Texture2D crosshair;

    [SerializeField]
    float weaponCooldown = 0.4f;

    [SerializeField] float reloadTime = 1.1f;

    [SerializeField]
    NumberDisplayManager hud;

    [SerializeField]
    int clipSize = 8;

    bool canFire = true;
    bool reloading = false;
    int ammo;

    private void Start() {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
        ammo = clipSize;
        hud.UpdateAmmoDisplay(ammo);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canFire && !reloading) {
            FireWeapon();
        }
        else if((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space)) && !reloading) {
            StartCoroutine(Reload());
        }
    }

    private void FireWeapon() {
        canFire = false;
        ammo--;
        hud.UpdateAmmoDisplay(ammo);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == ShooterTags.Target) {
                Target target = hit.collider.GetComponent<Target>();
                if (target != null) {
                    target.TargetShot();
                }
            }
        }
        StartCoroutine(ammo == 0 ? Reload() : Cooldown());
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(weaponCooldown);
        canFire = true;
    }

    IEnumerator Reload() {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = clipSize;
        hud.UpdateAmmoDisplay(ammo);
        reloading = false;
        canFire = true;
    }
}
