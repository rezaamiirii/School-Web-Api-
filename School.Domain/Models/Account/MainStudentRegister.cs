using Shop.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Models.Account
{
    public class MainStudentRegister:BaseEntity
    {

        #region Student Info

        public string? FathersName { get; set; }
        public string? Serial { get; set; }
        public string? Seri { get; set; }
        public string? RowOfBirthCertificate { get; set; }
        public string? BirthCity { get; set; }
        public string? National { get; set; }
        public string? Religion { get; set; }
        public string? SubReligion { get; set; }
        public bool IsLeftHand { get; set; }
        public bool IsHelthy { get; set; }
        public bool LiveWithParents { get; set; } = true;
        public string? Major { get; set; }
        public bool ChildOfShahid { get; set; }
        public string? SchoolLevel { get; set; }
        public string? CityOfCertificcate { get; set; }


        public int StudentId { get; set; }

        #endregion


        #region  ّFather And Mother's Info

        public FathersMajor FathersMajor { get; set; }
        public string? FatherNationalCode { get; set; }

        public string? FatherJob { get; set; }

        public string? FatherPhoneNumber { get; set; }

        public string? MotherName { get; set; }
        public string? MotherLastName { get; set; }

        public string? MotherMajor { get; set; }
        public string? MotherJob { get; set; }

        public string? MotherPhoneNumber { get; set; }

       

        public bool FatherPhoneNumberIsHisOwn { get; set; }
        public bool MotherPhoneNumberIsHerOwn { get; set; }

        public PlaceState PlaceState { get; set; }

        #endregion

        #region relations

        public Student? Student { get; set; }


        #endregion
        #region Other's Info

        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string HousePhoneNumber { get; set; }
        public string RelativesPhoneNuumber { get; set; }


        public string NameOfFormFillOuter { get; set; }

        #endregion
    }
    public enum FathersMajor
    {
        SubAssociates,
        Associates,
        SuperAssociates,
        Bachelor,
        Master,
        Doctoral,
        Seminary


    }
    public enum PlaceState
    {
        Personal,
        Rental,
        Companies,
        Dormitory,
        RelativesHouse,
        Other
    }
}
