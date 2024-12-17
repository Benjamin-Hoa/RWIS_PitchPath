using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Globalization;

public class MovingObject : MonoBehaviour
{
    private TcpListener listener;       
    public int port = 25000;            
    public GameObject targetObject;     
    public float movementScale = 0.5f;  //????????

    private bool isListening = false;   

    void Start()
    {
        try
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            isListening = true;
            Debug.Log($"Listening for data on port {port}...");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to start TCP listener: {e.Message}");
        }
    }

    void Update()
    {
        if (isListening && listener.Pending())
        {
            using (TcpClient client = listener.AcceptTcpClient())
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

                Debug.Log($"Raw data received: '{message}'");

                if (float.TryParse(message, NumberStyles.Float, CultureInfo.InvariantCulture, out float frequencyDifference))
                {
                    Debug.Log($"Parsed frequency difference: {frequencyDifference}");
                    MoveTargetObject(frequencyDifference);
                }
                else
                {
                    Debug.LogError($"Failed to parse frequency difference. Raw message: '{message}'");
                }
            }
        }
    }

    void MoveTargetObject(float frequencyDifference)
    {
        if (targetObject != null)
        {
            // Calculate movement based on frequency difference
            float movement = frequencyDifference * movementScale;

            Debug.Log($"Moving object by: {movement} units");

            Vector3 newPosition = targetObject.transform.position;
            newPosition.y += movement * Time.deltaTime;
            targetObject.transform.position = newPosition;

            Debug.Log($"Object moved to position: {newPosition}");
        }
        else
        {
            Debug.LogError("Target object is not assigned in the Inspector!");
        }
    }

    void OnApplicationQuit()
    {
        if (isListening)
        {
            listener.Stop();
            Debug.Log("TCP Listener stopped.");
        }
    }
}
