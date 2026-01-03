using UnityEngine;

public class UIFollowObject : MonoBehaviour
{
    public Transform target;

    public void Init(Transform target)
    {
        this.target = target;
        transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
