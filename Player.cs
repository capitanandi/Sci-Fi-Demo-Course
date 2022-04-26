using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private AudioClip _shootSound;
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 50;

    private bool isReloading = false;

    public bool hasCoin = false;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _audioSource = GameObject.Find("Weapon").GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;

        if(_controller == null)
        {
            Debug.LogError("Character Controller is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL.");
        }
        else
        {
            _audioSource.clip = _shootSound;
        }

        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            _audioSource.Stop();
            muzzleFlash.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Shoot()
    {
        muzzleFlash.Play();
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);

        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 1.0f);

            //check if hit the crate (crate behavor to swap with cracked)
            //destroy
            Destructible crate = hitInfo.transform.GetComponent<Destructible>();
            if(crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        isReloading = false;
    }

    public void CollectCoin()
    {
        hasCoin = true;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
