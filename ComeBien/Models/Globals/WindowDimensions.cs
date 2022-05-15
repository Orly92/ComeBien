using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Models.Globals
{
    public class WindowDimensions
    {
        public WindowDimensions()
        {
            Height = 600;
            Width = 800;
        }
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
