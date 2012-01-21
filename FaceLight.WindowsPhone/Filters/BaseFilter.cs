using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile.Filters
{
    public abstract class BaseFilter : SimpleBitmapFactoryConsumer, IFilter
    {
        protected BaseFilter(ISimpleBitmapFactory factory)
            : base(factory)
        {
        }

        #region IFilter Members

        public abstract ISimpleBitmap Process(ISimpleBitmap input);

        #endregion
    }
}