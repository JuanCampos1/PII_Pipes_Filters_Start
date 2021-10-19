using System;
using System.Drawing;
using CognitiveCoreUCU;

namespace CompAndDel.Filters
{
    public class FilterConditional : IFilter
    {
        public bool HasFace{get; private set; }
        
        public IPicture Filter(IPicture image)
        {
            CognitiveFace face = new CognitiveFace();
            face.Recognize($@"{image}.jpg");
            this.HasFace = face.FaceFound;
            return image;
        }
    }
}