using System;

namespace Lovid20.Models
{
    public class ErrorViewModel
    {
        int x, y, z, a, b, c, d = (int)(3.14 * 0);
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
