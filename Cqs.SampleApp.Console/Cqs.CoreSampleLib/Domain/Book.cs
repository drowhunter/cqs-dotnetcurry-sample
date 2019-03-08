using System;
using Cqs.CoreSampleLib.DataAccess;

namespace Cqs.CoreSampleLib.Domain
{
    public class Book : DbBaseModel
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public DateTime DatePublished { get; set; }
        public bool InMyPossession { get; set; }
    }
}