namespace DCAPI
{
    using API;
    
    //디시인사이드 유저
    public interface IUser {
        internal abstract string UserId { get; }
        
        internal abstract string Nickname { get; }
        
        internal abstract string Password { get; }
        
        public abstract string Name { get; }
    }
    
    //유동(익명) 유저입니다.
    public class Guest : IUser {
        public Guest(string nickname, string password)
            => (Nickname, Password) = (nickname, password);
        
        string IUser.UserId => null;
        
        string IUser.Nickname => Nickname;
        
        string IUser.Password => Password;
        
        string IUser.Name => Nickname;
        
        //유동 닉네임입니다.
        public string Nickname { get; set; }
        
        //유동 비밀번호입니다.
        public string Password { get; set; }
    }
    
    //로그인 (고닉, 반고닉) 유저입니다.
    public class Member : IUser {
        Session session;
        
        //세션으로부터 새 유저를 만듭니다.
        public Member(Session session) {
            this.session = session;
        }
        
        //해당세션으로 유저를 로그인합니다...
        public Member(Session session, string id, string password) {
            this.session = session;
            Login(id, password);
        }
        
        string IUser.UserId => UserId;
        
        string IUser.Nickname => null;
        
        string IUser.Password => null;
        
        string IUser.Name => Name;
        
        //유저의 닉네임입니다.
        public string Name { get; private set; }
        
        //유저의 토큰Id값 입니다.
        public string UserId { get; private set; }
        
        //유저의 고유 번호입니다.
        public string UserNo { get; private set; }
        
        //해당 아이디와 비밀번호로 로그인합니다.
        public (bool result, string cause) Login(string id, string password) {
            var ret = new DCID(session).Login(
                id,
                password,
                "login_normal",
                null)[0]; //Client Token
            if(!(ret["result"] ?? true))
                return (false, ret["cause"]);
            UserId = ret["user_id"];
            UserNo = ret["user_no"];
            return (true, null);
        }
    }
}