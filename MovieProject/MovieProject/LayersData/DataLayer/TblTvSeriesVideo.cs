//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblTvSeriesVideo
    {
        public int TvSeriesVideoId { get; set; }
        public string TvSeriesVideoUrl { get; set; }
        public int TvSeriesId { get; set; }
    
        public virtual TblTvSeries TblTvSeries { get; set; }
    }
}
