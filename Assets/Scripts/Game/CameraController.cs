using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraParent;
    public Transform indicator;
    public float speed = 40f;
    public float rotateSpeed = 40f;
    public float moveSpeed = 1f;

    public DragPower dragPower;

    public Transform player;
    public GameObject Line;

    private Quaternion up;

    private Quaternion camRotation;
    private float lookUpMin = -50;
    private float lookUpMax = 50;

    public float newMinX;
    public float newMaxX;

    public float newMinY;
    public float newMaxY;

    public float newMinZ;
    public float newMaxZ;

    public float cameraSmoothFactor = 1;

    public bool cancelShot;

    public RectTransform overHeadUI;

    void Start()
    {
        cancelShot = false;
        Line.SetActive(false);
        overHeadUI.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!dragPower.canRotate)
        {
            cameraParent.LookAt(new Vector3(dragPower.ball.transform.position.x, dragPower.ball.transform.position.y, dragPower.ball.position.z));
        }
    }

    void Update()
    {
        overHeadUI.LookAt(cameraParent);
        overHeadUI.position = new Vector3(dragPower.ball.position.x, dragPower.ball.position.y + 0.5f, dragPower.ball.position.z);

        Line.transform.rotation = Quaternion.Euler(0, (camRotation.y - 180), 0);
        Line.transform.localPosition = new Vector3(0, 0, 0);

        indicator.localPosition = new Vector3(0, 0, 0);
        indicator.rotation = Quaternion.Euler(0, 0, 0);

        if (dragPower.canRotate)
        {
            if (Input.GetMouseButton(1) && Time.timeScale == 1)
            {
                camRotation.x += speed * Input.GetAxis("Mouse Y") * cameraSmoothFactor * (-1);
                camRotation.y += speed * Input.GetAxis("Mouse X") * cameraSmoothFactor;

                camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

                cameraParent.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);

                Line.SetActive(true);  
            }
        }
       
        if (dragPower.isMoving)
        {
            overHeadUI.gameObject.SetActive(true);
            Line.SetActive(false);
        }

        if (!dragPower.isMoving)
        {
            overHeadUI.gameObject.SetActive(false);
            Line.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1) && dragPower.preparingToShoot)
        {
            cancelShot = true;
        }

        if (dragPower.canRotate && Time.timeScale == 1)
        {
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                p_Velocity += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                p_Velocity += new Vector3(0, 0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }

            cameraParent.Translate(p_Velocity * moveSpeed, Space.Self);
        }

        float minX = player.position.x - newMinX;
        float maxX = player.position.x - newMaxX;

        float minY = player.position.y - newMinY;
        float maxY = player.position.y - newMaxY;

        float minZ = player.position.z - newMinZ;
        float maxZ = player.position.z - newMaxZ;

        float posX = Mathf.Clamp(transform.position.x, minX, maxX);
        float posY = Mathf.Clamp(transform.position.y, minY, maxY);
        float posZ = Mathf.Clamp(transform.position.z, minZ, maxZ);

        cameraParent.position = new Vector3(posX, posY, posZ);
    }
}