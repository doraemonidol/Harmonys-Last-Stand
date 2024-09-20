using UnityEngine;

public class MouseOrbit : MonoBehaviour
{
    public Transform Target;
    public float distance;

    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;
    public float CameraDist = 10;

    private float x;
    private float y;

    // Use this for initialization
    private void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.x + 50;
        y = angles.y;
        distance = 30;

        if (GetComponent<Rigidbody>() == true)
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            if (Target)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y += Input.GetAxis("Mouse Y") * ySpeed * 0.05f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                var rotation = Quaternion.Euler(y, x, 0);
                var position = rotation * new Vector3(0, 0, -distance) + Target.position;

                transform.rotation = rotation;
                transform.position = position;
                distance = CameraDist;

                if (Input.GetKey(KeyCode.W))
                {
                    CameraDist -= Time.deltaTime * 6.5f;
                    CameraDist = Mathf.Clamp(CameraDist, 2, 60);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    CameraDist += Time.deltaTime * 6.5f;
                    CameraDist = Mathf.Clamp(CameraDist, 2, 60);
                }
            }
    }

    private float ClampAngle(float ag, float min, float max)
    {
        if (ag < -360)
            ag += 360;
        if (ag > 360)
            ag -= 360;
        return Mathf.Clamp(ag, min, max);
    }
}