using UnityEngine;

public class Level2Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeedX;
    [SerializeField] private float parallaxSpeedY;

    private Transform cameraTransform;
    private float startPositionX, startPositionY;
    private float spriteSizeX, spriteSizeY;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        startPositionX = transform.position.x;
        startPositionY = transform.position.y;
        spriteSizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteSizeY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        float relativeDistX = (cameraTransform.position.x * parallaxSpeedX) + startPositionX;
        float relativeDistY = (cameraTransform.position.y * parallaxSpeedY) + startPositionY;

        transform.position = new Vector3(relativeDistX, relativeDistY, transform.position.z);

        float relativeCameraDistX = cameraTransform.position.x * (1 - parallaxSpeedX);
        float relativeCameraDistY = cameraTransform.position.y * (1 - parallaxSpeedY);

        if (relativeCameraDistX > startPositionX + spriteSizeX)
        {
            startPositionX += spriteSizeX;
        }
        else if (relativeCameraDistX < startPositionX - spriteSizeX)
        {
            startPositionX -= spriteSizeX;
        }

        if (relativeCameraDistY > startPositionY + spriteSizeY)
        {
            startPositionY += spriteSizeY;
        }
        else if (relativeCameraDistY < startPositionY - spriteSizeY)
        {
            startPositionY -= spriteSizeY;
        }
    }
}