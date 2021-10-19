using System;
using CompAndDel;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class PipeConditionalFork : IPipe
    {
        public IPipe FilterIfTrue { get; }
        public IPipe FilterIfFalse { get; }

        public PipeConditionalFork(IPipe filterIfTrue, IPipe filterIfFalse) 
        {
            this.FilterIfTrue = filterIfTrue;
            this.FilterIfFalse = filterIfFalse;
        }
        public IPicture Send(IPicture picture)
        {
            FilterConditional filterConditional = new FilterConditional();
            filterConditional.Filter(picture);
            if(filterConditional.HasFace)
            {
                return this.FilterIfTrue.Send(picture);
            } 
            else
            {
                return this.FilterIfFalse.Send(picture);
            }
        }
    }
}