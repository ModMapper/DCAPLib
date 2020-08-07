using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DCAPI.Emoticons
{
    using API;
    
    //디시콘 패키지 API
    public class Package : IReadOnlyList<Emoticon> {
        private readonly Emoticon[] items;
        
        //디시콘 패키지 번호로부터 패키지를 가져옵니다.
        public Package(REST.REST rest, long index) {
            var ret = new dccon(rest).PackageDetail(index);
            var info = ret["info"];
            Title = info["title"];
            Description = info["description"];
            Index = long.Parse(info["package_idx"]);
            MainImage = GetImageURL(info["main_img_path"]);
            ListImage = GetImageURL(info["list_img_path"]);
            Seller = (info["seller_name"], info["seller_id"]);
            SaleCount = long.Parse(info["sale_count"]);
            RegisterDate = DateTime.Parse(info["reg_date"]);
            Mandoo = int.Parse(info["mandoo"]);
            
            var detail = ret["detail"];
            items = new Emoticon[detail.Length];
            for(int i=0; i < items.Length; i++) {
                var item = detail[i];
                items[i] = new Emoticon(
                    item["title"],
                    GetImageURL(item["path"]),
                    Index,
                    /*long.Parse(item["idx"])*/ -1);    //detail idx 값이 아닌듯...
            }
        }
        
        //디시콘의 제목입니다.
        public string Title { get; }
        
        //디시콘의 설명입니다.
        public string Description { get; }
        
        //패키지의 번호입니다.
        public long Index;
        
        //패키지의 메인 이미지 URL입니다.
        public string MainImage { get; }
        
        //패키지의 목록 이미지 URL입니다.
        public string ListImage { get; }
        
        //판매자의 정보입니다.
        public (string Name, string Id) Seller;
        
        //해당 디시콘의 판매량입니다.
        public long SaleCount { get; }
        
        //디시콘의 등록일입니다.
        public DateTime RegisterDate { get; }
        
        //디시콘의 가격 (만두)입니다.
        public int Mandoo { get; }
        
        //총 디시콘의 수량입니다.
        public int Count => items.Length;
        
        public Emoticon this[int index] => items[index];
        
        public IEnumerator<Emoticon> GetEnumerator()
            => (IEnumerator<Emoticon>)items.GetEnumerator();
            
        IEnumerator IEnumerable.GetEnumerator()
            => items.GetEnumerator();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static string GetImageURL(string no)
            => $"https://dcimg5.dcinside.com/dccon.php?no={no}";
    }
}
