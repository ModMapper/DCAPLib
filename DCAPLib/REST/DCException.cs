using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DCAPI.REST
{
    //디시인사이드 API 오류입니다.
    public class DCException : Exception {
        public DCException(string cause) : base(cause) {}
    }
}