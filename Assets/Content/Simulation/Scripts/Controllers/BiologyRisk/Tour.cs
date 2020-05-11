using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public enum PathType
{
    Continuous,
    Digital,
    None
}

public class Tour : MonoBehaviour
{

    public bool freeNavigationHorizontal = false;
    public bool freeNavigationVertical = false;
    public bool horizontalNavigation = false;
    public bool moveFreehead = false;
    public bool isIsometric = false;
    public float headSpeedRotation = 40;
    public bool lookReference;
    public GameObject gameObjectReference;
    public UnityEngine.UI.Image imageZone;
    public PathType pathType = PathType.Continuous;

    private Camera mainCamera;
    private bool navigationActive = true;
    private Vector3 cameraOrigin;

    public void SetMoveFreehead(bool flag)
    {
        moveFreehead = flag;
    }

    public void SetMoveHorizontal(bool flag)
    {
        horizontalNavigation = flag;
    }

    public bool FreeNavigation
    {
        get
        {
            return navigationActive;
        }
        set
        {
            if (isIsometric)
            {
                cameraOrigin = mainCamera.transform.position;
            }
           
            currentIndex = path.Count - 1;
            navigationActive = value;
        }
    }

    [System.Serializable]
    public class Path
    {
        public string nameofView = "";
        public float pathPosition;
        public Transform target;
    }

    public enum DIRECTION
    {
        FROM,
        BACK
    }

    [SerializeField]
    public List<Path> path;

    public CinemachineVirtualCamera cinemaChineCamera;
    public float advancePath = 0.01f;

    private int currentIndex = 0;
    private float curretPathPosition = 0;
    private bool inZone = false;

    public void Awake()
    {
        mainCamera = Camera.main;
        if (pathType == PathType.Continuous)
        {
            cinemaChineCamera.LookAt = path[0].target;
        }
        else if (pathType == PathType.Digital)
        {
            mainCamera.transform.parent.position = path[0].target.position;
        }
    }

    public void Update()
    {
        if (lookReference)
        {
            if (!isIsometric)
            {
                var rotationReference = Quaternion.LookRotation(gameObjectReference.transform.position - Camera.main.transform.parent.position);
                Camera.main.transform.parent.rotation = Quaternion.Lerp(Camera.main.transform.parent.rotation, rotationReference, Time.deltaTime * 5);
            }
            else
            {
                Vector3 objectPosition = gameObjectReference.transform.position;
                Vector3 cameraDirection = Camera.main.transform.forward * 50;
                objectPosition -= cameraDirection;
                Camera.main.transform.parent.position = objectPosition;
            }
        }
        if (navigationActive)
        {
            if (freeNavigationHorizontal)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    MoveInPack(DIRECTION.FROM);
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    MoveInPack(DIRECTION.BACK);
                }
            }

            if (freeNavigationVertical)
            {

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    MoveInPack(DIRECTION.FROM);
                }

                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    MoveInPack(DIRECTION.BACK);
                }
            }

            if (horizontalNavigation)
            {
                MoveWithMouse();
            }

            /* if (moveFreehead)
            {
                MoveHead();
            } */

            if (isIsometric)
            {
                MoveIsometric();
            }
        }

    }

    public void SetReference(GameObject reference)
    {
		RemoveReference();
        gameObjectReference = reference;
		if(gameObjectReference.transform.childCount>0)
        	gameObjectReference.transform.GetChild(0).gameObject.SetActive(true);
        lookReference = true;
    }
    public void RemoveReference()
    {
		if(gameObjectReference != null && gameObjectReference.transform.childCount>0) 
			gameObjectReference.transform.GetChild(0).gameObject.SetActive(false);
        imageZone.transform.parent.transform.parent.gameObject.SetActive(false);
        lookReference = false;
    }
    public void SetReferenceImage(Sprite _sprite)
    {
		RemoveReference();
        imageZone.sprite = _sprite;
        imageZone.transform.parent.transform.parent.gameObject.SetActive(true);
    }

    private void MoveWithMouse()
    {
        float mouseXPosition = Input.mousePosition.x;
        //0 - > 1024

        const int fieldOfAction = 100;

        if (mouseXPosition < fieldOfAction && inZone == false && mouseXPosition > 0)
        {
            MoveInPack(DIRECTION.BACK);
            inZone = true;
        }
        else if (mouseXPosition > Screen.width - fieldOfAction && inZone == false && mouseXPosition < Screen.width)
        {
            MoveInPack(DIRECTION.FROM);
            inZone = true;
        }
        else if ((mouseXPosition > 100 && mouseXPosition < Screen.width - fieldOfAction))
        {
            inZone = false;
        }

    }

    public void MoveDirection(int dir)
    {
        if (horizontalNavigation)
            MoveInPack((DIRECTION) dir);
    }

    public void MoveInPack(DIRECTION dir)
    {
        float maxLimit = path[path.Count - 1].pathPosition;
        float currentPathPosition = cinemaChineCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;

        if (currentPathPosition <= 0)
        {
            cinemaChineCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        }
        else if (currentPathPosition >= maxLimit)
        {
            cinemaChineCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = maxLimit;
        }
        float sign = 1;
        if (dir == DIRECTION.BACK)
        {
            sign = -1;
        }

        if (currentPathPosition >= 0 && currentPathPosition <= maxLimit)
        {
            cinemaChineCamera.LookAt = path[currentIndex].target;
            currentPathPosition = currentPathPosition + sign * advancePath;

            cinemaChineCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = currentPathPosition;
            if (currentIndex < path.Count - 2)
                cinemaChineCamera.LookAt = path[currentIndex + 1].target;
        }
    }

    public void MovetoPosition(int index)
    {
        if (pathType == PathType.Continuous)
        {
            cinemaChineCamera.LookAt = path[index].target;
            cinemaChineCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = path[index].pathPosition;
        }
        else if (pathType == PathType.Digital)
        {
            mainCamera.transform.parent.position = path[index].target.position;
        }
    }

    private void MoveHead()
    {
        float mouseXPosition = Input.mousePosition.x;
        float mouseYPosition = Input.mousePosition.y;
        const int areaOfMove = 50;

        Vector3 lookHorizontal = new Vector3(0, 1, 0) * headSpeedRotation * Time.deltaTime;
        Vector3 lookVertical = new Vector3(1, 0, 0) * headSpeedRotation * Time.deltaTime;

        // Move Horizontal 
        // Move left
        if (mouseXPosition < areaOfMove && mouseXPosition > 0)
        {
            mainCamera.transform.parent.rotation = mainCamera.transform.parent.rotation * Quaternion.Euler(-lookHorizontal);
        }
        // Move right
        else if (mouseXPosition > Screen.width - areaOfMove && mouseXPosition < Screen.width)
        {
            mainCamera.transform.parent.rotation = mainCamera.transform.parent.rotation * Quaternion.Euler(lookHorizontal);
        }

        // Move down
        if (mouseYPosition > 72 && mouseYPosition < areaOfMove + 72)
        {
            mainCamera.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(lookVertical);
        }
        // Move up
        else if (mouseYPosition > (Screen.height - areaOfMove - 72) && mouseXPosition < Screen.height)
        {
            mainCamera.transform.rotation = mainCamera.transform.rotation * Quaternion.Euler(-lookVertical);
        }
    }

    public void MoveIsometric()
    {
        float mouseXPosition = Input.mousePosition.x;
        float mouseYPosition = Input.mousePosition.y;
        const int areaOfMove = 40;

        //Limites de navegacion para la cámara
        const int vertLimit = 25;
        const int horLimit = 22;

        //Distancias con respecto al origen
        float verticalDist = 0.0f;
        float horizontalDist = 0.0f;

        float speedMove = 0.2f * mainCamera.orthographicSize / 15f;
        // Move Horizontal
        // Move left
        Vector3 movementVector = new Vector3();

        movementVector = mainCamera.transform.parent.right;
        float cameraX = mainCamera.transform.position.x;
        float cameraZ = mainCamera.transform.position.z;
        Vector3 newPosition = new Vector3();
        Vector3 tempVector = new Vector3();

        //MOVE LEFT
        if (mouseXPosition < areaOfMove && mouseXPosition > 0)
        {   
            newPosition = mainCamera.transform.parent.position + movementVector * -speedMove;
            tempVector.Set(newPosition.x - cameraOrigin.x, cameraOrigin.y, newPosition.z - cameraOrigin.z);
            horizontalDist = Vector3.Project(tempVector, movementVector).magnitude;
            if (horizontalDist < horLimit) {
                print(horizontalDist);
                //if (Mathf.Pow(newPosition.x - cameraOrigin.x, 2) + Mathf.Pow(newPosition.z - cameraOrigin.z, 2) < Mathf.Pow(areaOfMove,2) )
                mainCamera.transform.parent.position = mainCamera.transform.parent.position + movementVector * -speedMove;
            }
        }
        //MOVE RIGHT
        else if (mouseXPosition > Screen.width - areaOfMove && mouseXPosition < Screen.width)
        {
            newPosition = mainCamera.transform.parent.position + movementVector * speedMove;
            tempVector.Set(newPosition.x - cameraOrigin.x, cameraOrigin.y, newPosition.z - cameraOrigin.z);
            horizontalDist = Vector3.Project(tempVector, movementVector).magnitude;
            if (horizontalDist < horLimit) {
                print(horizontalDist);
                //if (Mathf.Pow(newPosition.x - cameraOrigin.x, 2) + Mathf.Pow(newPosition.z - cameraOrigin.z, 2) < Mathf.Pow(areaOfMove, 2))
                mainCamera.transform.parent.position = mainCamera.transform.parent.position + movementVector * speedMove;
            }
        }

        movementVector = mainCamera.transform.parent.forward;
        movementVector.y = 0;

        //MOVE DOWN
        if (mouseYPosition > 72 && mouseYPosition < areaOfMove + 72)
        { 
            newPosition = mainCamera.transform.parent.position + movementVector * -speedMove;
            tempVector.Set(newPosition.x - cameraOrigin.x, cameraOrigin.y, newPosition.z - cameraOrigin.z);
            verticalDist = Vector3.Project(tempVector, movementVector).magnitude;
            if (verticalDist < vertLimit) {
                print(verticalDist);
                //if (Mathf.Pow(newPosition.x - cameraOrigin.x, 2) + Mathf.Pow(newPosition.z - cameraOrigin.z, 2) < Mathf.Pow(areaOfMove, 2))
                mainCamera.transform.parent.position = mainCamera.transform.parent.position + movementVector * -speedMove;
            }
        }
        //MOVE UP
        else if (mouseYPosition > (Screen.height - areaOfMove - 72) && mouseYPosition < Screen.height)
        {
            newPosition = mainCamera.transform.parent.position + movementVector * speedMove;
            tempVector.Set(newPosition.x - cameraOrigin.x, cameraOrigin.y, newPosition.z - cameraOrigin.z);
            verticalDist = Vector3.Project(tempVector, movementVector).magnitude;
            if (verticalDist < vertLimit) {
                print(verticalDist);
                //if (Mathf.Pow(newPosition.x - cameraOrigin.x, 2) + Mathf.Pow(newPosition.z - cameraOrigin.z, 2) < Mathf.Pow(areaOfMove, 2))
                mainCamera.transform.parent.position = mainCamera.transform.parent.position + movementVector * speedMove;
            }
        }
    
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // zoom in
        {
            if ((mainCamera.orthographicSize >= 2f && Input.GetAxis("Mouse ScrollWheel") < 0f) || (mainCamera.orthographicSize <= 20f && Input.GetAxis("Mouse ScrollWheel") > 0f))
                mainCamera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * 3;
        }  
    }

    public void MoveToPlace(int placeIndex)
    {
        mainCamera.transform.parent.position = path[placeIndex].target.position;
        //mainCamera.transform.localEulerAngles = new Vector3(0 ,180, 0);
    }
}