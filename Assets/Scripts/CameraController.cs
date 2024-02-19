using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float heightOffset = 5f; 
    public float sideOffset = 5f; 
    private int currentCameraIndex = 0;
    public int CameraPositions = 5;

    void Update()
    {
        // Switch camera position on 'E' key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchCameraPosition(true);
        }

        // Switch camera position on 'Q' key press
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchCameraPosition(false);
        }
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 playerPosition = player.position;
        Vector3 abovePosition = new Vector3(playerPosition.x, playerPosition.y + heightOffset, playerPosition.z);
        Vector3 rightSidePosition = new Vector3(playerPosition.x + sideOffset, playerPosition.y, playerPosition.z);
        Vector3 leftSidePosition = new Vector3(playerPosition.x - sideOffset, playerPosition.y, playerPosition.z);
        Vector3 frontSidePosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + sideOffset);
        Vector3 backSidePosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - sideOffset);
        switch (currentCameraIndex)
        {
            case 0:
                transform.position = abovePosition;
                break;
            case 1:
                transform.position = rightSidePosition;
                break;
            case 2:
                transform.position = leftSidePosition;
                break;
            case 3:
                transform.position = frontSidePosition;
                break;
            case 4:
                transform.position = backSidePosition;
                break;
        }

        transform.LookAt(playerPosition);
    }

    void SwitchCameraPosition(bool forward)
    {
        // Toggle between camera positions
        if (forward)
        {
            currentCameraIndex = (currentCameraIndex + 1) % CameraPositions; 
        }
        else
        {
            currentCameraIndex = (currentCameraIndex - 1 + CameraPositions) % CameraPositions;
        }
    }
}
