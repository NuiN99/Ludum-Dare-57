using NuiN.NExtensions;
using UnityEngine;

public class Leviathan : MonoBehaviour
{
    public bool IsDoneCharging => _isCharging && _chargeTimer.IsComplete && Vector3.Distance(Player.Instance.transform.position, transform.position) > 250f;

    [SerializeField] Transform head;
    [SerializeField] float chargeRange = 25f;
    [SerializeField] float chargeSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float chargeDuration = 20f;
    [SerializeField] float rotateSpeed;
    [SerializeField] float raycastCheckDist;
    [SerializeField] float raycastCheckOffsetY;
    [SerializeField] float raycastOrientSpeed;
    [SerializeField] LayerMask groundMask;

    bool _isCharging;
    Vector3 _chargeDirection;

    Timer _chargeTimer;
    
    void Awake()
    {
        _chargeTimer = new Timer(chargeDuration);
    }

    void OnEnable()
    {
        _isCharging = false;
        _chargeDirection = Vector3.zero;
        _chargeTimer.Restart();
    }

    void Update()
    {
        if (_isCharging)
        {
            if (Physics.Raycast(head.position.Add(y:raycastCheckOffsetY), Vector3.down, raycastCheckDist, groundMask))
            {
                _chargeDirection.y += raycastOrientSpeed * Time.deltaTime;
            }
            
            transform.position += _chargeDirection * (chargeSpeed * Time.deltaTime);
            Rotate(_chargeDirection);
            return;
        }

        float distFromPlayer = Vector3.Distance(Player.Instance.transform.position, head.position);
        Vector3 playerDir = VectorUtils.Direction(transform.position, Player.Instance.transform.position);
        if (distFromPlayer <= chargeRange && !_isCharging)
        {
            Debug.Log("Leviathan Charging!");
            _chargeTimer.Restart();
            _isCharging = true;
            _chargeDirection = VectorUtils.Direction(transform.position, Player.Instance.transform.position + Player.Instance.RB.linearVelocity);
        }
        else
        {
            if (Physics.Raycast(head.position.Add(y:raycastCheckOffsetY), Vector3.down, out RaycastHit hit, raycastCheckDist, groundMask))
            {
                transform.position += Vector3.up * (hit.distance * raycastOrientSpeed * Time.deltaTime);
            }
            
            transform.position += playerDir * (moveSpeed * Time.deltaTime);
            Rotate(playerDir);
        }
    }

    void Rotate(Vector3 dir)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(head.position.Add(y:raycastCheckOffsetY), Vector3.down * raycastCheckDist);
    }
}