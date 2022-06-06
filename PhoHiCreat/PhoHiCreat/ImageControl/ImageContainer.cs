using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace PhoHiCreat.ImageControl
{
    public class ImageContainer
    {
        public List<BitmapImage> image_list;

        public ImageContainer()
        {

        }

        public void initialize_ImageContainer(Array images)
        {
            image_list = new List<BitmapImage>();
            foreach (string filepath in images)
            {
                Uri filePath_uri = new Uri(filepath, UriKind.Absolute);
                BitmapImage image = new BitmapImage(filePath_uri);
                image_list.Add(image);
            }
        }
    }
}
