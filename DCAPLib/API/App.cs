using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DCAPI.API {
    using REST;
    
    //디시인사이드 앱 API (http://app.dcinside.com/)
    ref struct App {
        readonly REST client;

        public App(REST rest)
            => client = rest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json TotalSearch(string keyword, string app_id, string confirm_id)
            => client.Post("http://app.dcinside.com/api/_total_search.php",
                new (string, string)[] {
                    ("keyword",     keyword),
                    ("app_id",      app_id),
                    ("confirm_id",  confirm_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryList(string id, long page, string app_id, string confirm_id)
            => client.Get("http://app.dcinside.com/api/gall_list_new.php",
                new (string, string)[] {
                    ("id",          id),
                    ("page",        page.ToString()),
                    ("app_id",      app_id),
                    ("confirm_id",  confirm_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryView(string id, long no, string app_id, string confirm_id)
            => client.Get("http://app.dcinside.com/api/gall_view_new.php",
                new (string, string)[] {
                    ("id",          id),
                    ("no",          no.ToString()),
                    ("app_id",      app_id),
                    ("confirm_id",  confirm_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json CommentNew(string id, long no, string app_id)
            => client.Get("http://app.dcinside.com/api/comment_new.php",
                new (string, string)[] {
                    ("id",      id),
                    ("no",      no.ToString()),
                    ("app_id",  app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json RelationList(string id, string app_id)
            => client.Get("http://app.dcinside.com/api/relation_list.php",
                new (string, string)[] {
                    ("id",      id),
                    ("app_id",  app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json MyGall(string user_id, string app_id)
            => client.Post("http://app.dcinside.com/api/mygall.php",
                new (string, string)[] {
                    ("user_id", user_id),
                    ("app_id",  app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json DCCon(string user_id, long? package_idx, long? detail_idx, string type, string app_id)
            => client.Post("http://app.dcinside.com/api/dccon.php",
                new (string, string)[] {
                    ("user_id",     user_id),
                    ("package_idx", package_idx?.ToString()),
                    ("detail_idx",  detail_idx?.ToString()),
                    ("type",        type),
                    ("app_id",      app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json HitRecommend(string id, long no, string confirm_id, string app_id)
            => client.Post("http://app.dcinside.com/api/hit_recommend",
                new (string, string)[] {
                    ("id",          id),
                    ("no",          no.ToString()),
                    ("confirm_id",  confirm_id),
                    ("app_id",      app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json Recommend(string id, string confirm_id, long no, string app_id)
            => client.Post("http://app.dcinside.com/api/_recommend_up.php",
                new (string, string)[] {
                    ("id",          id),
                    ("confirm_id",  confirm_id),
                    ("no",          no.ToString()),
                    ("app_id",      app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json NonRecommend(string id, string confirm_id, long no, string app_id)
            => client.Post("http://app.dcinside.com/api/_recommend_down.php",
                new (string, string)[] {
                    ("id",          id),
                    ("confirm_id",  confirm_id),
                    ("no",          no.ToString()),
                    ("app_id",      app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryModify(string password, string user_id, string id, long no, string app_id)
            => client.Post("http://app.dcinside.com/api/gall_modify.php",
                new (string, string)[] {
                    ("password",    password),
                    ("user_id",     user_id),
                    ("id",          id),
                    ("no",          no.ToString()),
                    ("app_id",      app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryDelete(string write_pw, string user_id, string client_token, string id, long no, string mode, string app_id)
            => client.Post("http://app.dcinside.com/api/gall_modify.php",
                new (string, string)[] {
                    ("write_pw",        write_pw),
                    ("user_id",         user_id),
                    ("client_token",    client_token),
                    ("id",              id),
                    ("no",              no.ToString()),
                    ("mode",            mode),
                    ("app_id",          app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json CommentOK(string id, long no, string board_id, string best_chk, int? best_comno, string mode,
                string comment_nick, string comment_pw, string user_id, string client_token, string comment_memo, long? detail_idx, string app_id)
            => client.Post("http://app.dcinside.com/api/comment_ok.php",
                new (string, string)[] {
                    ("id",              id),
                    ("no",              no.ToString()),
                    ("board_id",        board_id),
                    ("best_chk",        best_chk),
                    ("best_comno",      best_comno?.ToString()),
                    ("mode",            mode),
                    ("comment_nick",    comment_nick),
                    ("comment_pw",      comment_pw),
                    ("user_id",         user_id),
                    ("client_token",    client_token),
                    ("comment_memo",    comment_memo),
                    ("detail_idx",      detail_idx?.ToString()),
                    ("app_id",          app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json CommentDelete(string comment_pw, string user_id, string client_token, string id, long no,
                string board_id, string mode, string best_chk, int? best_comno, long comment_no, string app_id)
            => client.Post("http://app.dcinside.com/api/comment_del.php",
                new (string, string)[] {
                    ("comment_pw",      comment_pw),
                    ("user_id",         user_id),
                    ("client_token",    client_token),
                    ("id",              id),
                    ("no",              no.ToString()),
                    ("board_id",        board_id),
                    ("mode",            mode),
                    ("best_chk",        best_chk),
                    ("best_comno",      best_comno?.ToString()),
                    ("comment_no",      comment_no.ToString()),
                    ("app_id",          app_id)});

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json AdSearchList(string id, long no, string mode,
                string category, int is_minor, string app_id, string subject, string memo, string ua)
            => client.Post("http://app.dcinside.com/api/_ad_search_link2.php",
                new (string, string)[] {
                    ("id",          id),
                    ("no",          no.ToString()),
                    ("mode",        mode),
                    ("category",    category),
                    ("is_minor",    is_minor.ToString()),
                    ("app_id",      app_id),
                    ("subject",     subject),
                    ("memo",        memo),
                    ("ua",          ua)});
    }
}   
