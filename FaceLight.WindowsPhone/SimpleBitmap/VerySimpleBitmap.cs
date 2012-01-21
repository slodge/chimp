namespace FaceLight.Mobile.SimpleBitmap
{
    public class VerySimpleBitmap : ISimpleBitmap
    {
        public VerySimpleBitmap(int pixelWidth, int pixelHeight)
        {
            PixelHeight = pixelHeight;
            PixelWidth = pixelWidth;
            Pixels = new int[pixelHeight*pixelWidth];
        }

        #region ISimpleBitmap Members

        public int PixelHeight { get; private set; }
        public int PixelWidth { get; private set; }
        public int[] Pixels { get; private set; }

        #endregion
    }
}