using System.Runtime.CompilerServices;

namespace DCAPI.API
{
    using REST;

    //디시콘 API (https://dccon.dcinside.com/)
    ref struct dccon {
        readonly REST client;

        public dccon(REST rest)
            => client = rest;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json PackageDetail(long package_idx)
            => client.PostXHR("https://dccon.dcinside.com/index/package_detail",
                new (string, string)[] {
                    ("package_idx", package_idx.ToString())});
    }
}