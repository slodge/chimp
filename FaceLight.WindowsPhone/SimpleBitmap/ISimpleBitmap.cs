namespace FaceLight.Mobile.SimpleBitmap
{
    public interface ISimpleBitmap
    {
        int PixelHeight { get; }
        int PixelWidth { get; }
        int[] Pixels { get; }
    }
}