namespace BlogApp.Api.Commons.Helpers
{
    public class ImageHelper
    {
        public static string MakeImageName(string filename)
        {
            string strpath = Path.GetExtension(filename);

            string guid = Guid.NewGuid().ToString();
            return "IMG_" + guid + filename + strpath;
        }
    }
}
