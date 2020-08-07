namespace DCAPI.Gallery
{
    //댓글 API
    public struct Comment {
        readonly Session session;
        
        //갤러리 Id, 게시글 번호, 댓글 번호로부터 댓글을 가져옵니다ㅣ.
        public Comment(Session session, string id, long no, long comment) {
            this.session = session;
            (Id, No, CommentNo) = (id, no, comment);
        }
        
        //갤러리의 Id입니다.
        public string Id { get; }
        
        //게시글의 번호입니다.
        public long No { get; }
        
        //댓글의 번호입니다.
        public long CommentNo { get; }
        
        
    }
}