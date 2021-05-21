using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    public delegate void OnMoveComplete();
    protected OnMoveComplete _onMoveComplete;
    [SerializeField] private float _spd = 0.5f;

    [SerializeField]
    private Vector2 _target;
   public void TravelToCenter(OnMoveComplete callback)
    {
        StartCoroutine(ReturnToMiddle(callback));
    }

    IEnumerator ReturnToMiddle(OnMoveComplete cb)
    {
        _onMoveComplete = cb;
        while((Vector2)transform.position != _target)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target, Time.deltaTime * _spd);
            yield return null;

        }
        yield return new WaitForSeconds(1.5f);
        _onMoveComplete?.Invoke();
    }
}
