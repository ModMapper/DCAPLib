using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DCAPI.API {
    using REST;

    //디시인사이드 ID관련 API (https://dcid.dcinside.com/)
    ref struct DCID {
        readonly REST client;

        public DCID(REST rest)
            => client = rest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json AppKey(string value_token, string signature, string pkg, long? vCode, string vName, string client_token)
            => client.Post("https://dcid.dcinside.com/join/mobile_app_key_verification_3rd.php", 
                new (string, string)[] {
                    ("value_token",     value_token),
                    ("signature",       signature),
                    ("pkg",             pkg),
                    ("vCode",           vCode?.ToString()),
                    ("vName",           vName),
                    ("client_token",    client_token)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json Login(string user_id, string user_pw, string mode, string client_token)
            => client.Post("https://dcid.dcinside.com/join/mobile_app_login.php",
                new (string, string)[] {
                    ("user_id",         user_id),
                    ("user_pw",         user_pw),
                    ("mode",            mode),
                    ("client_token",    client_token)});
    }
}