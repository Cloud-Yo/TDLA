using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodge : MonoBehaviour
{
    [SerializeField] private float _rayDist = 2f;
    [SerializeField] private float _moveDist = 1.5f;
    [SerializeField] private float _spd = 1.5f;
    [SerializeField] private LayerMask _hitLM;
    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private bool _dodging = false;
    [SerializeField] private bool _canDodge = false;
    public bool CanDodge { get { return _canDodge; } set { _canDodge = value; } }


    // Update is called once per frame
    void Update()
    {
        if (_canDodge)
        {
            Dodge();
        }

    }

    public bool DodgeCheck(out RaycastHit2D hitInfo)
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,_boxSize, 0, Vector2.down, _rayDist,_hitLM);
        if(hit.collider != null)
        {
            hitInfo = hit;
            return true;
        }
        hitInfo = hit;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + ((Vector3)Vector2.down * _rayDist), _boxSize);
    }

    private void Dodge() 
    {
        RaycastHit2D info;
        if (DodgeCheck(out info) && !_dodging)
        {
            _dodging = true;
            StartCoroutine(MoveLaterally(info.collider.transform.position.x));
        }
    }

    IEnumerator MoveLaterally(float x)
    {
        Debug.Break();
        if (transform.position.x <= x)
        {
            float d = transform.position.x - _moveDist;
            while (transform.position.x > d)
            {
                transform.Translate(Vector2.left * _spd * Time.deltaTime);
                yield return null;
            }
        }
        else if(transform.position.x > x)
        {
            float d = transform.position.x +_moveDist;
            while (transform.position.x < d)
            {
                transform.Translate(Vector2.right * _spd * Time.deltaTime);
                yield return null;
            }
        }
        _dodging = false;
    }
}
