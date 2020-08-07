using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCAPI
{
    using API;
    
    //AppId를 가져오기 위한 세션 클래스
    public class Session {
        protected const string  signature   = "ReOo4u96nnv8Njd7707KpYiIVYQ3FlcKHDJE046Pg6s=";
        protected const string  package     = "com.dcinside.app";
        protected const long    vCode       = 30207;
        protected const string  vName       = "3.8.12";
        
        private static readonly SHA256 sha = SHA256.Create();
        //REST 통신을 위한 클라이언트
        protected readonly REST.REST client;
        
        //새 세션 생성
        public Session() : this(new REST.REST()) {}
        
        //REST클라이언트로 부터 새 새션 생성
        public Session(REST.REST rest, bool AutoUpdate = true) {
            client = rest;
            _AppId = GetAppId();
            if(AutoUpdate)
                UpdateToken(this);
        }
        
        private string _AppId;
        public string AppId => _AppId;
        
        //AppId 값 가져오기
        protected string GetAppId() {
            var ret = new DCID(client).AppKey(
                GetValueToken(),
                signature,
                package,
                vCode,
                vName,
                FCM.FCMToken)[0];
            if(!ret["result"])
                throw new REST.DCException(ret["cause"]);
            return ret["app_id"];
        }
        
        //ValueToken 값 가져오기
        protected string GetValueToken() {
            const string prefix = "dcArdchk_";
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(
                prefix + GetTime()));
            return string.Join(null, hash.Select((i) => i.ToString("x2")));
        }
        
        //디시인사이드 서버로부터 시간을 가져옴
        protected string GetTime()
            => new Json2(client).AppCheck()[0]["date"];
        
        //클라이언트 시간을 통해 시간 값 생성 (사용되지 않음)
        [Obsolete] protected static string GetClientTime() {
            var t = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Korea Standard Time");
            var c = CultureInfo.InvariantCulture;
            var f = c.DateTimeFormat;
            var w = c.Calendar.GetWeekOfYear(t, f.CalendarWeekRule, f.FirstDayOfWeek);
            var d = (int)t.DayOfWeek;
            return $"{t.ToString("ddd", f)}{t.DayOfYear - 1}{t.Day}{((d + 6) % 7) + 1}{d}{w - 1:d2}{t:MddMM}";
        }
        
        //일정 시간 (약 11시간)을 기준으로 새 AppId 발급
        private static void UpdateToken(Session session) {
            const int UpdateTime = 82800000;    //갱신 시간 24시간 - @ (23시간)
            const int DelayTime = 20000;        //글쓰기 대기 시간 10초 + @ (20초)
            
            var weakref = new WeakReference<Session>(session);
            Task.Delay(UpdateTime).ContinueWith(Update);
            void Update(Task _) {
                if(!weakref.TryGetTarget(out var s)) return;
                string id = s.GetAppId();
                Task.Delay(DelayTime).ContinueWith((_) => {
                    Interlocked.Exchange(ref s._AppId, id);
                    Task.Delay(UpdateTime).ContinueWith(Update);
                });
            }
        }
        
        //편의를 위해 REST클라이언트로 변환
        public static implicit operator REST.REST(Session session)
            => session.client;
    }
}