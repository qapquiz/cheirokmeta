using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Jobs;
using Grpc.Core;
using Chat;


public class Chatroom : MonoBehaviour
{
    private const string IP = "111.223.40.195:6262";
    private const string DOMAIN_NAME = "sandbox.digitopolisstudio.com:6262";

    public Channel Channel;
    public ChatroomClient ChatroomClient;
    private string _token;

    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _chatField;

    private static string _tempChatField = "";
    private JobHandle _receiverJobHandle;

    void Start() {
        Channel = new Channel(DOMAIN_NAME, ChannelCredentials.Insecure);
        ChatroomClient = new ChatroomClient(new Chat.Chat.ChatClient(Channel));
        _token = ChatroomClient.Login("armariya", "super-secret");
        ChatroomClient.OpenStream(_token);
        _receiverJobHandle = StartStreamReceiverJob();
    }

    private void Update() {
        if (_tempChatField.Equals(_chatField.text)) {
            return;
        }

        _chatField.text = _tempChatField;
    }

    public async void SendMessage() {
        string message = _inputField.text;
        _inputField.text = "";

        await ChatroomClient.SendMessage(message);
    }

    private void OnDestroy() {
        ChatroomClient.Logout(_token);
        _receiverJobHandle.Complete();
    }

    public JobHandle StartStreamReceiverJob()
    {
        var job = new StreamReceiverJob();
        return job.Schedule();

        //await Task.Run(async () => {
        //    while (await ChatroomClient.StreamCall.ResponseStream.MoveNext())
        //    {
        //        var currentResponse = ChatroomClient.StreamCall.ResponseStream.Current;

        //        switch (currentResponse.EventCase)
        //        {
        //            case StreamResponse.EventOneofCase.ClientMessage:
        //                Debug.Log($"Name: {currentResponse.ClientMessage.Name} Message: {currentResponse.ClientMessage.Message_}");
        //                _tempChatField = _tempChatField + $"{currentResponse.ClientMessage.Name}: {currentResponse.ClientMessage.Message_}\n";
        //                break;
        //            case StreamResponse.EventOneofCase.ClientLogin:
        //                Debug.Log($"{currentResponse.ClientLogin.Name} is logged in");
        //                break;
        //            case StreamResponse.EventOneofCase.ClientLogout:
        //                Debug.Log($"{currentResponse.ClientLogout.Name} is logged out");
        //                break;
        //            case StreamResponse.EventOneofCase.ServerShutdown:
        //                Debug.Log("Server shutdown");
        //                break;
        //        }
        //    }
        //});
    }

    // this IJob long-run event! 
    public struct StreamReceiverJob : IJob {

        public async void Execute()
        {
            while (await ChatroomClient.StreamCall.ResponseStream.MoveNext())
            {
                var currentResponse = ChatroomClient.StreamCall.ResponseStream.Current;

                switch (currentResponse.EventCase)
                {
                    case StreamResponse.EventOneofCase.ClientMessage:
                        Debug.Log($"Name: {currentResponse.ClientMessage.Name} Message: {currentResponse.ClientMessage.Message_}");
                        _tempChatField = _tempChatField + $"{currentResponse.ClientMessage.Name}: {currentResponse.ClientMessage.Message_}\n";
                        break;
                    case StreamResponse.EventOneofCase.ClientLogin:
                        Debug.Log($"{currentResponse.ClientLogin.Name} is logged in");
                        break;
                    case StreamResponse.EventOneofCase.ClientLogout:
                        Debug.Log($"{currentResponse.ClientLogout.Name} is logged out");
                        break;
                    case StreamResponse.EventOneofCase.ServerShutdown:
                        Debug.Log("Server shutdown");
                        break;
                }
            }
            Debug.Log("Finished Job");
        }
    }
}
