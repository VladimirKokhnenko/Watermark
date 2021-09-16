using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithImage
{
    class PictureProvider
    {
        public string imageName { get; set; }
        public PictureProvider(string imageName)
        {
            this.imageName = imageName;
        }
        public async Task DownloadImageAsync()
        {
            using(WebClient client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(this.imageName), "imageFile.jpg");
            }
        }
    }
}
