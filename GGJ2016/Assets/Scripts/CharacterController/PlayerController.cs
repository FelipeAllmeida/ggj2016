using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    #region Public Data
    #endregion

    #region Private Data

    [SerializeField] private float _moveSpeed = 0.25f;
    [SerializeField] private GameObject _ritualItem;
    [SerializeField] private TotemController _totem;

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
        }
    }

    private void DetectInputs()
    {
        if (Input.GetKey(KeyCode.R))
        {
            FindObjectOfType<LevelGenerator>().NextLevel();
            Destroy(this.gameObject);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.Translate((_moveSpeed / 2) * Time.deltaTime, 0f, (_moveSpeed / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.Translate((_moveSpeed / 2) * Time.deltaTime, 0f, (-_moveSpeed / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            transform.Translate((-_moveSpeed / 2) * Time.deltaTime, 0f, (-_moveSpeed / 2) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.Translate((-_moveSpeed / 2) * Time.deltaTime, 0f, (_moveSpeed / 2) * Time.deltaTime);
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
            Destroy(p_collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
