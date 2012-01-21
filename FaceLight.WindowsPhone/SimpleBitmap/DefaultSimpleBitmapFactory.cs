namespace FaceLight.Mobile.SimpleBitmap
{
    public class DefaultSimpleBitmapFactory : ISimpleBitmapFactory
    {
        #region ISimpleBitmapFactory Members

        public ISimpleBitmap Create(int pixelWidth, int pixelHeight)
        {
            return new VerySimpleBitmap(pixelWidth, pixelHeight);
        }

        #endregion
    }
}