using System.Runtime.CompilerServices;

namespace DCAPI.API
{
    using REST;

    //디시인사이드 갤러리 정보 API (http://json2.dcinside.com/)
    ref struct Json2 {
        readonly REST client;

        public Json2(REST rest)
            => client = rest;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json AppCheck()       => client.Get("http://json2.dcinside.com/json0/app_check_A_rina.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json UpdateNotice()   => client.Get("http://json2.dcinside.com/json0/update_notice_A_rina.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json AppNotice()      => client.Get("http://json2.dcinside.com/json0/app_dc_notice.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json MainContent()    => client.Get("http://json2.dcinside.com/json3/main_content.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryRanking() => client.Get("http://json2.dcinside.com/json1/ranking_gallery.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json MinorRanking()   => client.Get("http://json2.dcinside.com/json1/mgallmain/mgallery_ranking.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json CategoryNames()  => client.Get("http://json2.dcinside.com/json3/category_name.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json GalleryNames()   => client.Get("http://json2.dcinside.com/json3/gall_name.php");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json AdCharge()       => client.Get("http://json2.dcinside.com/json1/app_ad_charge.php");
    }
}