using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace DCAPI.API {
    using REST;
    
    //게시글 및 댓글작성 API (http://upload.dcinside.com/)
    ref struct Upload {
        readonly REST client;
    
        public Upload(REST rest)
            => client = rest;
    
        public Json GalleryWrite(string id, string app_id, string mode, string client_token,
                string subject, string name, string password, string user_id, string[] memo_block, long?[] detail_idx) {
            var form = new List<(string, string)>() {
                ("id",              id),
                ("app_id",          app_id),
                ("mode",            mode),
                ("client_token",    client_token),
                ("subject",         subject),
                ("name",            name),
                ("password",        password),
                ("user_id",         user_id)};
            for(int i=0; i < memo_block.Length; i++)
                if(memo_block[i] != null)
                    form.Add(($"memo_block[{i}]", memo_block[i]));
            for(int i=0; i < detail_idx.Length; i++)
                if(detail_idx[i] != null)
                    form.Add(($"detail_idx[{i}]", detail_idx[i]?.ToString()));
            return client.Post("http://upload.dcinside.com/_app_write_api.php", form);
        }
        
        public Json CommentUpload(string best_chk, string gall_id, string mode, string file_name, (Stream data, string mediatype, string filename) upfile,
                string user_no, string comment_nick, string password, string user_id, string client_token, string comment_txt, string app_id) {
            using var form = new MultipartFormDataContent();
            using var file = new StreamContent(upfile.data);
            file.Headers.ContentType.MediaType = upfile.mediatype;
            form.Add(new StringContent(best_chk),       "best_chk");
            form.Add(new StringContent(gall_id),        "gall_id");
            form.Add(new StringContent(mode),           "mode");
            form.Add(new StringContent(file_name),      "file_name");
            form.Add(file,                              "upfile",   upfile.filename);
            form.Add(new StringContent(user_no),        "user_no");
            form.Add(new StringContent(comment_nick),   "comment_nick");
            form.Add(new StringContent(password),       "password");
            form.Add(new StringContent(user_id),        "user_id");
            form.Add(new StringContent(client_token),   "client_token");
            form.Add(new StringContent(comment_txt),    "comment_txt");
            form.Add(new StringContent(app_id),         "app_id");
            return client.Post("http://upload.dcinside.com/_app_upload.php", form);
        }
    }
}