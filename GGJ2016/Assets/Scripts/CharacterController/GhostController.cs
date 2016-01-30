using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
    public float moveSpeed = 1f;

    [SerializeField] private TotemController _totem;
    private Transform _playerTransform;
    private bool _canFollow = false;

    void Start()
    {
        ATimer.WaitSeconds(0.25f, delegate
        {
            _totem = FindObjectOfType<TotemController>();
            _totem.onVictory += delegate
            {
                Destroy(this.gameObject);
            };
            _playerTransform = GameObject.Find("Player(Clone)").transform;
            if (_playerTransform != null) _canFollow = true;
        });
    }

    void Update()
    {
        if (_canFollow)
        {
            float __step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, __step);
        }
    }
}
