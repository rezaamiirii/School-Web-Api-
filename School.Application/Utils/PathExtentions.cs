using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Utils
{
    public static class PathExtentions
    {
        #region user avatar

        public static string AcademyOrgin= "/img/academy/orgin/";
        public static string AcademyOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/academy/orgin/");

        public static string AcademyOrginThumb = "/img/Academy/Thumb/";
        public static string AcademyThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/academy/Thumb/");

        #endregion

        //#region product categories

        //public static string CategoryOrgin = "/img/category/orgin/";
        //public static string CategoryOrginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/orgin/");

        //public static string CategoryThumb = "/img/category/Thumb/";
        //public static string CategoryThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/category/Thumb/");

        //#endregion

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
