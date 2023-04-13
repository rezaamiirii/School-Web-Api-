using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Utils
{
    public static class PathExtentions
    {
        #region College 

        public static string CollegeOrgin= "/img/college/orgin/";
        public static string CollegeOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/college/orgin/");

        public static string CollegeOrginThumb = "/img/college/Thumb/";
        public static string CollegeThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/college/Thumb/");

        #endregion

        #region Top Student

        public static string TopStudentOrgin = "/img/topstudent/orgin/";
        public static string TopStudentOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/topstudent/orgin/");

        public static string TopStudentThumb = "/img/topstudent/Thumb/";
        public static string TopStudentThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/topstudent/Thumb/");

        #endregion

        //#region product image
        //public static string ProductOrgin = "/img/product/orgin/";
        //public static string ProductOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/orgin/");

        //public static string ProductThumb = "/img/product/Thumb/";
        //public static string ProductThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/product/Thumb/");

        //#endregion

        //#region slider image

        //public static string SliderOrgin = "/img/slider/orgin/";
        //public static string SliderOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/slider/orgin/");

        //public static string SliderThumb = "/img/product/Thumb/";
        //public static string SliderThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/slider/Thumb/");


        //#endregion
    }
}
