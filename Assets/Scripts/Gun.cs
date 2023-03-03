using System.Collections;
using System;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField]
    Texture2D crosshair;

    [SerializeField]
    float weaponCooldown = 0.4f;

    bool canFire = true;

    private void Start() {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), CursorMode.Auto);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canFire) {
            canFire = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == ShooterTags.Target) {
                    Target target = hit.collider.GetComponent<Target>();
                    if(target != null) {
                        target.TargetShot();
                    }
                }
            }
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(weaponCooldown);
        canFire = true;
    }
}
