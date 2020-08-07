namespace DCAPI.Emoticons
{
    using API;
    
    //디시콘 API
    public struct Emoticon {
        //디시콘 url과 패키지 Id, 디시콘 Id로부터 디시콘을 생성합니다. 
        public Emoticon(string title, string image, long package, long detail)
            => (Title, Image, Package, Detail) = (title, image, package, detail);
        
        //디시콘의 제목입니다. 설정이 가능합니다.
        public string Title { get; set; }
        
        //디시콘의 이미지 URL입니다.
        public string Image { get; }
        
        //디시콘의 패키지 번호입니다.
        public long Package { get; }
        
        //해당 디시콘의 번호입니다.
        public long Detail { get; }
        
        //서버로부터 디시콘의 HTML을 가져옵니다.
        public string GetHTML(Session session, IUser user) {
            var ret = new App(session).DCCon(
                user?.UserId,
                Package, Detail,
                "insert",
                session.AppId);
            if(!(ret[0]?["result"] ?? true))
                throw new REST.DCException(ret[0]["cause"]);
            return ret["img_tag"];
        }
        
        //디시콘의 HTML을 생성합니다.
        public string MakeHTML() {
            var s = System.Net.WebUtility.HtmlEncode(Title);
            return $"<img src='{Image}' class='written_dccon' alt='{s}' conalt='{s}' title='{s}'>";
        }
    }
}