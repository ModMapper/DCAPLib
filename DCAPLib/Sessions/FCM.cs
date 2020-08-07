using System;

namespace DCAPI
{
    //FCM 토큰을 위한 클래스 (미구현)
    internal static class FCM {
        private const string mainvalue = ":APA91bFMI-0d1b0wJmlIWoDPVa_V5Nv0OWnAefN7fGLegy6D76TN_CRo5RSUO-6V7Wnq44t7Rzx0A4kICVZ7wX-hJd3mrczE5NnLud722k5c-XRjIxYGVM9yZBScqE3oh4xbJOe2AvDe";
        private static readonly Random rand = new Random();
        
        //FCM 등록 토큰 (ClientToken)
        public static readonly string FCMToken;
        
        static FCM() {
            FCMToken = GenerateToken();
            //통신을 통한 FCM 토큰 가져오기...
        }
        
        private static string GenerateToken() {
            var buf = new byte[10];
            rand.NextBytes(buf);
            return Convert.ToBase64String(buf) + mainvalue;
        }
    }
}