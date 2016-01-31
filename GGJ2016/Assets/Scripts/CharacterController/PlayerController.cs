using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    #region Public Data
    public Action onDeath;
    #endregion

    #region Private Data
    [SerializeField] private const float _constMoveSpeed = 10f;
    private float _moveSpeed = 12f;
    [SerializeField] private GameObject _storedItem;
    [SerializeField] private int _keyQuantity = 0;
    [SerializeField] private TotemController _totem;
    private float _switchTime = 0.25f;
    private bool _buttonActivated = false;
    #endregion

    void Start()
    {
        _totem = FindObjectOfType<TotemController>();
        _totem.onVictory += delegate
        {
            Destroy(this.gameObject);
        };
    }

    void Update()
    {
        _moveSpeed = _constMoveSpeed;
        LayerMask __mask = 1 << LayerMask.NameToLayer("Slow");
        if (Physics.Raycast(transform.position, Vector3.down,3f, __mask))
        {
            _moveSpeed *= 0.5f;
        }
         __mask = 1 << LayerMask.NameToLayer("Fast");
        if (Physics.Raycast(transform.position, Vector3.down, 3f, __mask))
        {
            _moveSpeed += 10f;
        }
        __mask = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(transform.position, Vector3.down, 3f, __mask))
        {
            _buttonActivated = false;
        }
        if (Time.time > _switchTime && _buttonActivated == false)
        {
            __mask = 1 << LayerMask.NameToLayer("Button");
            if (Physics.Raycast(transform.position, Vector3.down, 3f, __mask))
            {
                _buttonActivated = true;
                Spikes[] __spikes = FindObjectsOfType<Spikes>();
                foreach (Spikes p_spikes in __spikes)
                {                    
                    p_spikes.Toggle();
                }
                _switchTime = Time.time + 0.25f;

            }
        }
        DetectInputs();
        CheckCollision();
    }

    private void CheckCollision()
    {
        Collider[] __hits = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider __hit in __hits)
        {
            if (__hit.tag == "RitualItem" && _storedItem == null)
            {
                _storedItem = __hit.gameObject;
                __hit.gameObject.transform.SetParent(transform);
                __hit.gameObject.transform.localPosition = new Vector3(0f, 1f, 0f);
                __hit.tag = "ToDeliverItem";
            }
            if (__hit.tag == "Totem")
            {
                if (_storedItem != null)
                {
                    __hit.GetComponent<TotemController>().AddRitualItem(_storedItem);
                    _storedItem = null;
                }
            }
            if (__hit.tag == "Trap")
            {
                FindObjectOfType<LevelGenerator>().ResetLevel();
                onDeath();
                Destroy(this.gameObject);
            }
        }
    }

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.R))
        {
            FindObjectOfType<LevelGenerator>().ResetLevel();
            FindObjectOfType<LevelGenerator>().NextLevel();
            Destroy(this.gameObject);
        }
        float __angularMovement = ((_moveSpeed * 3) / 4);
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Translate(__angularMovement * Time.deltaTime, 0f, __angularMovement * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(-__angularMovement * Time.deltaTime, 0f, __angularMovement * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Translate(__angularMovement * Time.deltaTime, 0f, -__angularMovement * Time.deltaTime);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate(-__angularMovement * Time.deltaTime, 0f, -__angularMovement * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, _moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -_moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_moveSpeed * Time.deltaTime, 0f, 0f);
        }
    }


    void OnTriggerEnter(Collider p_collider)
    {
        if (p_collider.gameObject.tag == "Ghost")
        {
            FindObjectOfType<LevelGenerator>().ResetLevel();
            onDeath();
            Destroy(this.gameObject);
        }
    }

    
}
