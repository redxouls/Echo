using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeManager : MonoBehaviour
{
    public Camera fpsCamera;
    public GameObject grenade;
    public GameObject cursor;
    public float shootForce;
    public float timeBetweenShoot;
    public PlayerMovement player;
    
    private bool aiming;
    private bool readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        aiming = false;
        cursor.SetActive(false);
        grenade.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        HandleInput();
    }

    void HandleInput() {
        if (!readyToShoot && TutorialManager.Instance.space_check == TutorialManager.CheckStatus.Checked)
        {
            return;
        }
        if (Input.GetMouseButton(0) && !PauseController.GamePaused && !player.isDead)
        {
            cursor.SetActive(true);
            aiming = true;
        }
        if (Input.GetMouseButtonUp(0) && aiming)
        {   
            cursor.SetActive(false);
            readyToShoot = false;
            aiming = false;
            Shoot();
            Invoke("Reload", timeBetweenShoot);
        }
    }

    void Shoot() {
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 shootDir = (ray.GetPoint(75) - transform.position).normalized;
        grenade.SetActive(true);
        GameObject newGrenade = Instantiate(grenade, transform.position, Quaternion.identity);
        newGrenade.transform.forward = shootDir;
        newGrenade.GetComponent<Rigidbody>().AddForce(shootDir * shootForce, ForceMode.Impulse);
        grenade.SetActive(false);
    }

    void Reload() {
        readyToShoot = true;
    }
}
