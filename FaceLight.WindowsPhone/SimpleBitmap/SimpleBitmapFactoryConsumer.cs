namespace FaceLight.Mobile.SimpleBitmap
{
    public class SimpleBitmapFactoryConsumer
    {
        private readonly ISimpleBitmapFactory _factory;

        protected SimpleBitmapFactoryConsumer(ISimpleBitmapFactory factory)
        {
            _factory = factory;
        }

        protected ISimpleBitmapFactory Factory
        {
            get { return _factory; }
        }
    }
}