using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//If we have a stiff rope, such as a metal wire, then we need a simplified solution
//this is also an accurate solution because a metal wire is not swinging as much as a rope made of a lighter material
public class RopeControllerSimple : MonoBehaviour
{
    //Objects that will interact with the rope
    public Transform whatTheRopeIsConnectedTo;
    public Transform whatIsHangingFromTheRope;

    //Line renderer used to display the rope
    private LineRenderer lineRenderer;

    //A list with all rope sections
    public List<Vector3> allRopeSections = new();

    //Rope data
    private float _ropeLength = 1f;
    [SerializeField] private float minRopeLength = 1f;

    [SerializeField] private float maxRopeLength = 57f;

    //Mass of the hook the rope is carrying
    private float loadMass = 25f;

    //Mass of attachedCargo
    private float cargoMass = 0f;

    //How fast we can add more/less rope
    [SerializeField] private float winchSpeed = 2f;

    [SerializeField] private float ropeWidth = 0.1f;

    //The joint we use to approximate the rope
    private SpringJoint _springJoint;

    private void Start()
    {
        _springJoint = whatTheRopeIsConnectedTo.GetComponent<SpringJoint>();

        //Init the line renderer we use to display the rope
        lineRenderer = GetComponent<LineRenderer>();

        //Init the spring we use to approximate the rope from point a to b
        UpdateSpring();

        //Add the weight to what the rope is carrying
        whatIsHangingFromTheRope.GetComponent<Rigidbody>().mass = loadMass;
    }

    private void Update()
    {
        //Add more/less rope
        //UpdateWinch();

        //Display the rope with a line renderer
        DisplayRope();
    }

    public void UpdateCargoMass(float newMass)
    {
        cargoMass = newMass;
        UpdateSpring();
    }

    //Update the spring constant and the length of the spring
    private void UpdateSpring()
    {
        //Someone said you could set this to infinity to avoid bounce, but it doesnt work
        //kRope = float.inf

        //
        //The mass of the rope
        //
        //Density of the wire (stainless steel) kg/m3
        const float density = 7750f;
        //The radius of the wire
        const float radius = 0.02f;

        var volume = Mathf.PI * radius * radius * _ropeLength;

        var ropeMass = volume * density;

        //Add what the rope is carrying
        ropeMass += loadMass;
        ropeMass += cargoMass;
        //Debug.Log("RopeMass is " + ropeMass);

        //
        //The spring constant (has to recalculate if the rope length is changing)
        //
        //The force from the rope F = rope_mass * g, which is how much the top rope segment will carry
        var ropeForce = ropeMass * 9.81f;

        //Use the spring equation to calculate F = k * x should balance this force, 
        //where x is how much the top rope segment should stretch, such as 0.01m

        //Is about 146000
        var kRope = ropeForce / 0.01f;

        //print(ropeMass);

        //Add the value to the spring
        _springJoint.spring = kRope * 1.0f;
        _springJoint.damper = kRope * 0.8f;

        //Update length of the rope
        _springJoint.maxDistance = _ropeLength;
    }

    //Display the rope with a line renderer
    private void DisplayRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;


        //Update the list with rope sections by approximating the rope with a bezier curve
        //A Bezier curve needs 4 control points
        var a = whatTheRopeIsConnectedTo.position;
        var d = whatIsHangingFromTheRope.position;

        //Upper control point
        //To get a little curve at the top than at the bottom
        Vector3 B = a + whatTheRopeIsConnectedTo.up * (-(a - d).magnitude * 0.1f);
        //B = A;

        //Lower control point
        Vector3 C = d + whatIsHangingFromTheRope.up * ((a - d).magnitude * 0.5f);

        //Get the positions
        BezierCurve.GetBezierCurve(a, B, C, d, allRopeSections);


        //An array with all rope section positions
        Vector3[] positions = new Vector3[allRopeSections.Count];

        for (int i = 0; i < allRopeSections.Count; i++)
        {
            positions[i] = allRopeSections[i];
        }

        //Just add a line between the start and end position for testing purposes
        //Vector3[] positions = new Vector3[2];

        //positions[0] = whatTheRopeIsConnectedTo.position;
        //positions[1] = whatIsHangingFromTheRope.position;


        //Add the positions to the line renderer
        lineRenderer.positionCount = positions.Length;

        lineRenderer.SetPositions(positions);
    }

    //Add more/less rope
    public void UpdateWinch(float scale)
    {
        bool hasChangedRope = false;

        //More rope
        if (scale > 0 && _ropeLength < maxRopeLength)
        {
            _ropeLength += winchSpeed * scale * Time.deltaTime;

            hasChangedRope = true;
        }
        else if (scale < 0 && _ropeLength > minRopeLength)
        {
            _ropeLength += winchSpeed * scale * Time.deltaTime;

            hasChangedRope = true;
        }


        if (hasChangedRope)
        {
            _ropeLength = Mathf.Clamp(_ropeLength, minRopeLength, maxRopeLength);

            //Need to recalculate the k-value because it depends on the length of the rope
            UpdateSpring();
        }
    }
}