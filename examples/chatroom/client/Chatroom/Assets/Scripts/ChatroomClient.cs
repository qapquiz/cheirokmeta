using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using Unity.Jobs;


namespace Chat {
    public class ChatroomClient {
        public readonly Chat.ChatClient Client;
        public static AsyncDuplexStreamingCall<StreamRequest, StreamResponse> StreamCall;

        public ChatroomClient(Chat.ChatClient client) {
            this.Client = client;
        }

        ~ChatroomClient() {
            if (ChatroomClient.StreamCall == null) {
                ChatroomClient.StreamCall.Dispose();
            }
        }

        public string Login(string name, string password) {
            try {
                LoginRequest request = new LoginRequest {
                    Name = name,
                    Password = password
                };

                LoginResponse response = Client.Login(request);

                if (response.Exists()) {
                    return response.Token;
                }
            } catch (RpcException e) {
                Debug.Log("RPC failed with error: " + e);
            }

            return "";
        }

        public bool Logout(string token) {
            try {
                LogoutRequest request = new LogoutRequest {
                    Token = token
                };

                LogoutResponse response = Client.Logout(request);

                if (response.Exists()) {
                    return true;
                }
            } catch (RpcException e) {
                Debug.Log("RPC failed with error: " + e);
            }

            return false;
        }

        public void OpenStream(string token) {

            Debug.Log(token);

            var metadata = new Metadata
            {
                { "x-chat-token", token }
            };


            StreamCall = Client.Stream(headers: metadata);
        }


        public async Task SendMessage(string message) {
            try {
                Debug.Log("Send Message");
                await StreamCall.RequestStream.WriteAsync(new StreamRequest {
                    Message = message
                });
            } catch (RpcException e) {
                Debug.Log("RPC failed with error: " + e);
            }
        }

        public async Task CloseStream() {
            await StreamCall.RequestStream.CompleteAsync();
            StreamCall.Dispose();
        }
    }
}