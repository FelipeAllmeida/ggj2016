using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TotemController : MonoBehaviour
{
    #region Public Data

    public Transform spinnerTransform;
    public Action onVictory;
    public float itenRadius = 1f;

    #endregion


    #region Private Data
    [SerializeField] private int _itensQuantity = 0;
    [SerializeField] private List<GameObject> _listRitualItem = new List<GameObject>();
    private PlayerController _playerController;

    #endregion

    void Start()
    {
        _itensQuantity = GameObject.Find("World").GetComponent<LevelGenerator>().itemQuantity;
        _playerController = FindObjectOfType<PlayerController>();
        _playerController.onDeath += delegate
        {
            Destroy(this.gameObject);
        };
    }

    void Update()
    {
        spinnerTransform.transform.Rotate(new Vector3(0f, -5f, 0f));
        if (_listRitualItem.Count >= _itensQuantity)
        {
                onVictory();
                FindObjectOfType<LevelGenerator>().NextLevel();
                Destroy(this.gameObject);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < _listRitualItem.Count; i ++)
        {
            Destroy(_listRitualItem[i]);
        }
        _listRitualItem.Clear();
    }

    public void AddRitualItem(GameObject p_gameObject)
    {
        _listRitualItem.Add(p_gameObject);
        p_gameObject.transform.SetParent(spinnerTransform);
        //p_gameObject.transform.localPosition()
        if (_listRitualItem.Count == 1)
        {
            p_gameObject.transform.localPosition = new Vector3(0f, 0f, itenRadius);
        }
        else if (_listRitualItem.Count == 2)
        {
            p_gameObject.transform.localPosition = new Vector3(0f, 0f, -itenRadius);
        }
        else if (_listRitualItem.Count == 3)
        {
            _listRitualItem[1].transform.localPosition = new Vector3(itenRadius, 0f, -itenRadius);
            _listRitualItem[2].transform.localPosition = new Vector3(-itenRadius, 0f, -itenRadius);
        }
        if (_listRitualItem.Count > _itensQuantity)
        {
            // victory
        }
    }
}
