using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.WebSocket;
using System;
using Newtonsoft.Json.Linq;

public class GameSocket : MonoBehaviour
{
    string address = "ws://127.0.0.1:8899";
    WebSocket webSocket;

    public GameManager gm;
    public int num;

    private void Update()
    {
       //CheckNum(num);
    }

    private void Start()
    {
        num = 0; // 无抬手动作，向前直线飞行

        if (webSocket == null)
        {
            webSocket = new WebSocket(new Uri(address));

#if !UNITY_WEBGL
            webSocket.StartPingThread = true;
#endif

            // Subscribe to the WS events
            webSocket.OnOpen += OnOpen;
            webSocket.OnMessage += OnMessageRecv;
            webSocket.OnClosed += OnClosed;

            // Start connecting to the server
            webSocket.Open();
        }
    }

    public void Destroy()
    {
        if (webSocket != null)
        {
            webSocket.Close();
            webSocket = null;
        }
    }

    void OnOpen(WebSocket ws)
    {
        Debug.Log("OnOpen: ");
        JObject job = new JObject();
        job.Add(new JProperty("from", "345"));
        job.Add(new JProperty("type", "enter"));
        job.Add(new JProperty("roomid", "123333"));
        job.Add(new JProperty("message", "连接成功！"));

        webSocket.Send(job.ToString());
    }

    void OnMessageRecv(WebSocket ws, string message)
    {
        Debug.LogFormat("OnMessageRecv: msg={0}", message);
        if (message != null)
        {
            int.TryParse(message, out num);

            CheckNum(num);

        }

    }

    void OnBinaryRecv(WebSocket ws, byte[] data)
    {
        Debug.LogFormat("OnBinaryRecv: len={0}", data.Length);
    }

    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        Debug.LogFormat("OnClosed: code={0}, msg={1}", code, message);
        Destroy();
    }

    void OnError(WebSocket ws, Exception ex)
    {
        string errorMsg = string.Empty;
#if !UNITY_WEBGL || UNITY_EDITOR
        if (ws.InternalRequest.Response != null)
        {
            errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
        }
#endif
        Debug.LogFormat("OnError: error occured: {0}\n", (ex != null ? ex.Message : "Unknown Error " + errorMsg));
        webSocket = null;
    }


    void CheckNum(int i)
    {
        if (i == 0)
        {
            return;
        }

        if (i == 1)
        {
            Debug.Log(i);
            gm.LongPressRight(true);
        }
        
        if (i == 2)
        {
            gm.LongPressRight(false);
        }

        if (i == 3)
        {
            gm.LongPressLeft(true);
        }
        
        if (i == 4)
        {
            gm.LongPressLeft(false);
        }

        if (i == 5)
        {
            gm.Shoot();
        }

        if (i == 6)
        {
            gm.RestartGame();
        }

    }

}
