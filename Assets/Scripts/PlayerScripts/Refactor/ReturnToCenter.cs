using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToCenter : MonoBehaviour
{
    public delegate void OnMoveComplete();
    protected OnMoveComplete _onMoveComplete;
    [SerializeField] private float _spd = 0.5f;
   public void TravelToCenter(OnMoveComplete callback)
    {
        StartCoroutine(ReturnToMiddle(callback));
    }

    IEnumerator ReturnToMiddle(OnMoveComplete cb)
    {
        _onMoveComplete = cb;
        while((Vector2)transform.position != Vector2.zero)
        {
            transform.position = Vector2.MoveTowards(transform.position, Vector2.zero, Time.deltaTime * _spd);
            yield return null;

        }
        yield return new WaitForSeconds(1.5f);
        _onMoveComplete?.Invoke();
    }
}
