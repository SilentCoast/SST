﻿namespace SST.Converters
{
    public sealed class ReverseBooleanConverter : BooleanConverter<bool>
    {
        public ReverseBooleanConverter() :
            base(false, true)
        { }
    }
}
