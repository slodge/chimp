using System;
using System.Collections.Generic;
using FaceLight.Mobile.Filters;
using FaceLight.Mobile.Segmentation;
using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile
{
    public class FaceLight
    {
        private readonly IFilter _dilateFilter;
        private readonly IFilter _erodeFilter;
        private readonly HistogramMinMaxSegmentator _segmentator;

        public FaceLight()
            : this(new DefaultSimpleBitmapFactory())
        {
        }

        public FaceLight(ISimpleBitmapFactory factory)
        {
            try
            {
                SkinColorFilter = new ColorRangeFilter(factory)
                                      {
                                          LowerThreshold = new YCbCrColor(0.10f, -0.15f, 0.05f),
                                          UpperThreshold = new YCbCrColor(1.00f, 0.05f, 0.20f)
                                      };
                _erodeFilter = new Erode5x5Filter(factory);
                _dilateFilter = new Dilate5x5Filter(factory);
                _segmentator = new HistogramMinMaxSegmentator();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ColorRangeFilter SkinColorFilter { get; set; }

        public IEnumerable<Segment> Process(ISimpleBitmap bmpToProcess)
        {
            if (bmpToProcess == null)
                return null;

            ISimpleBitmap skin = SkinColorFilter.Process(bmpToProcess);
            ISimpleBitmap erode = _erodeFilter.Process(skin);
            ISimpleBitmap dilate = _dilateFilter.Process(erode);
            dilate = _dilateFilter.Process(dilate);
            dilate = _dilateFilter.Process(dilate);

            Histogram histogram = Histogram.FromWriteableBitmap(dilate);
            _segmentator.Histogram = histogram;
            _segmentator.ThresholdLuminance = histogram.Max*0.1f;
            IEnumerable<Segment> foundSegments = _segmentator.Process(dilate);

            return foundSegments;
        }
    }
}