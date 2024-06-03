using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hook : MonoBehaviour
{
    [SerializeField] private RopeControllerSimple _ropeControllerSimple;
    private Cargo _currentCargo;
    private bool _hasCargo;
    private Rigidbody rigi;

    private bool _insideUnloadZone;

    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, 0f, transform.position.z), Color.green);
    }

    public void HookMove(float scale)
    {
        if (rigi.IsSleeping())
        {
            rigi.WakeUp();
        }

        // var newY = Mathf.Clamp(_springJoint.maxDistance + moveSpeed * scale * Time.deltaTime, minLimit, maxLimit);
        // //transform.DOLocalMoveY(newY, 1f);
        // _springJoint.maxDistance = newY;
        _ropeControllerSimple.UpdateWinch(scale);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("HookAutoUnload"))
        {
            _insideUnloadZone = true;
            if (_hasCargo)
            {
                UnloadCargo(other);
            }
        }

        if (!_hasCargo && !_insideUnloadZone)
        {
            if (other.transform.CompareTag("HookAutoLoad"))
            {
                LoadCargo(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("HookAutoUnload"))
        {
            _insideUnloadZone = false;
        }
    }

    private void LoadCargo(Transform cargoHookupTransform)
    {
        var cargoHookup = cargoHookupTransform.GetComponent<CargoHookup>();
        var hookTransform = transform;
        hookTransform.position = cargoHookup.hookPos.position;
        cargoHookup.Cargo.UpdateState(true, rigi);
        _ropeControllerSimple.UpdateCargoMass(cargoHookup.Cargo.massInKg);
        //cargoHookup.Cargo.transform.SetParent(hookTransform);
        _hasCargo = true;
        _currentCargo = cargoHookup.Cargo;
    }

    private void UnloadCargo(Collider other)
    {
        if (_hasCargo)
        {
            //_currentCargo.transform.parent = null;
            other.GetComponent<MeshRenderer>().material = _currentCargo.cargoMat;
            _currentCargo.UpdateState(false);
            _ropeControllerSimple.UpdateCargoMass(0f);
            //_currentCargo = null;
            Destroy(_currentCargo.gameObject);
            _hasCargo = false;
        }
    }
}