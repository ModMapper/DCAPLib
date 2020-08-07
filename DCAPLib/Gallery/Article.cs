using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DCAPI.Gallery
{
    using API;
    using Emoticons;
    
    //게시글 API
    public struct Article {
        readonly Session session;
        
        //갤러리 Id와 게시글 No로부터 게시글을 가져옵니다.
        public Article(Session session, string id, long no) {
            this.session = session;
            (Id, No) = (id, no);
        }
        
        //게시글의 갤러리 Id입니다.
        public string Id { get; }
        
        //게시글의 번호입니다.
        public long No { get; }
        
        //댓글 번호로부터 댓글을 가져옵니다.
        public Comment GetComment(long commentno)
            => new Comment(session, Id, No, commentno);
        
        //게시글을 추천합니다.
        public (bool result, string cause) Recommend() {
            var ret = new App(session).Recommend(Id, null, No, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //게시글을 해당 유저로 추천합니다.
        public (bool result, string cause) Recommend(IUser user) {
            var ret = new App(session).Recommend(Id, user.UserId, No, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //게시글을 비추천합니다.
        public (bool result, string cause) NonRecommend() {
            var ret = new App(session).NonRecommend(Id, null, No, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //게시글을 해당 유저로 비추천합니다.
        public (bool result, string cause) NonRecommend(IUser user) {
            var ret = new App(session).NonRecommend(Id, user.UserId, No, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //게시글을 힛갤 추천합니다.
        public (bool result, string cause) HitRecommend() {
            var ret = new App(session).HitRecommend(Id, No, null, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //게시글을 해당 유저로 힛갤추천합니다.
        public (bool result, string cause) HitRecommend(IUser user) {
            var ret = new App(session).HitRecommend(Id, No, user.UserId, session.AppId)[0];
            return (ret["result"], ret["cause"]);
        }
        
        //유동으로 댓글을 작성합니다.
        public (bool result, string cause, Comment comment) Comment(string nickname, string password, string memo) {
            var ret = new App(session).CommentOK(
                Id, No,
                null, null, null,
                "com_write",
                nickname, password, null,
                FCM.FCMToken,
                memo, null,
                session.AppId)[0];
            if(!(ret["result"] ?? true))
                return (false, ret["cause"], default);
            return (true, null,
                new Comment(session, Id, No, (long)ret["data"]));
        }
        
        //해당 유저로 댓글을 작성합니다.
        public (bool result, string cause, Comment comment) Comment(IUser user, string memo) {
            var ret = new App(session).CommentOK(
                Id, No,
                null, null, null,
                "com_write",
                user.Nickname, user.Password, user.UserId,
                FCM.FCMToken,
                memo, null,
                session.AppId)[0];
            if(!(ret["result"] ?? true))
                return (false, ret["cause"], default);
            return (true, null,
                new Comment(session, Id, No, (long)ret["data"]));
        }
        
        //익명으로 디시콘을 작성합니다.
        public (bool result, string cause, Comment comment) DCCon(string nickname, string password, Emoticon dccon) {
            var ret = new App(session).CommentOK(
                Id, No,
                null, null, null,
                "com_write",
                nickname, password, null,
                FCM.FCMToken,
                dccon.MakeHTML(), dccon.Detail,
                session.AppId)[0];
            if(!(ret["result"] ?? true))
                return (false, ret["cause"], default);
            return (true, null,
                new Comment(session, Id, No, (long)ret["data"]));
        }
        
        //해당 유저로 디시콘을 작성합니다.
        public (bool result, string cause, Comment comment) DCCon(IUser user, Emoticon dccon) {
            var ret = new App(session).CommentOK(
                Id, No,
                null, null, null,
                "com_write",
                user.Nickname, user.Password, user.UserId,
                FCM.FCMToken,
                dccon.MakeHTML(), dccon.Detail,
                session.AppId)[0];
            if(!(ret["result"] ?? true))
                return (false, ret["cause"], default);
            return (true, null,
                new Comment(session, Id, No, (long)ret["data"]));
        }
    }
}