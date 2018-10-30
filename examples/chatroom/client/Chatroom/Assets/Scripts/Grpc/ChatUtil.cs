
namespace Chat {
    public static class ChatUtil {
        public static bool Exists(this LoginResponse response) {
            return response != null && response.Token.Length != 0;
        }

        public static bool Exists(this LogoutResponse response) {
            return response != null;
        }
    }
}
