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
    private float _moveSpeed = 10f;
    [SerializeField] private GameObject _ritualItem;
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
            if (__hit.tag == "RitualItem" && _ritualItem == null)
            {
                _ritualItem = __hit.gameObject;
                __hit.gameObject.transform.SetParent(transform);
                __hit.gameObject.transform.localPosition = new Vector3(0f, 1f, 0f);
                __hit.tag = "ToDeliverItem";
            }
            if (__hit.tag == "Totem")
            {
                if (_ritualItem != null)
                {
                    __hit.GetComponent<TotemController>().AddRitualItem(_ritualItem);
                    _ritualItem = null;
                }
            }
            if (__hit.tag == "Trap")
            {
                FindObjectOfType<LevelGenerator>().EraseSpawns();
                onDeath();
                Destroy(this.gameObject);
            }
        }
    }

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.R))
        {
            FindObjectOfType<LevelGenerator>().NextLevel();
            Destroy(this.gameObject);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -_moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-_moveSpeed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(_moveSpeed * Time.deltaTime, 0f, 0f);
        }
    }


    void OnTriggerEnter(Collider p_collider)
    {
        if (p_collider.gameObject.tag == "Ghost")
        {
            FindObjectOfType<LevelGenerator>().EraseSpawns();
            onDeath();
            Destroy(this.gameObject);
        }
    }

    
}
