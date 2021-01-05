using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FSWDFinalProject.DATA.EF//.Metadata
{
    #region UserDetails Metadata
    public class UserDetailsMetadata
    {
        //public string UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* First name required")]
        [StringLength(50, ErrorMessage ="* First name must be less than 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Lsat name required")]
        [StringLength(50, ErrorMessage = "* Last name must be less than 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "Resume File")]
        [DisplayFormat(NullDisplayText ="[-N/A-]")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(UserDetailsMetadata))]
    public partial class UserDetail
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region Applications Metadata
    public class ApplicationsMetadata
    {
        //public int ApplicationId { get; set; }

        [Display(Name = "Open Position ID")]
        [Required(ErrorMessage ="* ID required")]
        [Range(0, int.MaxValue, ErrorMessage = "* ID must be greater than 0.")]
        public int OpenPositionId { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "* ID required")]
        [StringLength(128, ErrorMessage = "* ID must be at least 128 character.")]
        public string UserId { get; set; }

        [Display(Name = "Application Date")]
        [Required(ErrorMessage = "* Application Date required")]
        [DisplayFormat(DataFormatString ="{0:d}",ApplyFormatInEditMode =true)]
        public System.DateTime ApplicationDate { get; set; }

        [Display(Name="Manager Notes")]
        [DisplayFormat(NullDisplayText ="[-N/A-]")]
        public string ManagerNotes { get; set; }

        [Display(Name = "Application Status ID")]
        [Required(ErrorMessage = "* Applcation Status ID required")]
        [Range(0, int.MaxValue, ErrorMessage = "* ID must be greater than 0")]
        public int ApplicationStatusId { get; set; }

        [Display(Name ="Resume File")]
        public string ResumeFilename { get; set; }
    }

    [MetadataType(typeof(ApplicationsMetadata))]
    public partial class Application { }
    #endregion

    #region Locations Metadata
    public class LocationsMetadata
    {
        //public int LocationId { get; set; }

        [Display(Name ="Store Number")]
        [Required(ErrorMessage ="* Store Number required.")]
        [StringLength(10, ErrorMessage ="* Store Number must be less than 10 characters.")]
        public string StoreNumber { get; set; }

        [Required(ErrorMessage = "* City required.")]
        [StringLength(50, ErrorMessage = "* City must be less than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "* State required.")]
        [StringLength(10, ErrorMessage = "* State must be 2 characters.")]
        public string State { get; set; }

        [Display(Name = "Manager ID")]
        [Required(ErrorMessage = "* ID required.")]
        [StringLength(128, ErrorMessage = "* ID must be at least 128 character.")]
        public string ManagerId { get; set; }
    }

    [MetadataType(typeof(LocationsMetadata))]
    public partial class Location { }
    #endregion

    #region OpenPositions Metadata
    public class OpenPositionsMetadata
    {
        //public int OpenPositionId { get; set; }

        [Display(Name = "Position ID")]
        [Required(ErrorMessage ="* ID required.")]
        [Range(0, int.MaxValue, ErrorMessage = "* ID must be greater than 0.")]
        public int PositionId { get; set; }

        [Display(Name = "Location ID")]
        [Required(ErrorMessage = "* ID required.")]
        [Range(0, int.MaxValue, ErrorMessage = "* ID must be greater than 0.")]
        public int LocationId { get; set; }
    }

    [MetadataType(typeof(OpenPositionsMetadata))]
    public partial class OpenPosition { }
    #endregion

    #region Positions Metadata
    public class PositionsMetadata
    {
        //public int PositionId { get; set; }

        [Required(ErrorMessage ="* Title required.")]
        [StringLength(50, ErrorMessage ="* Title must be less than 50 characters.")]
        public string Title { get; set; }

        [Display(Name ="Description")]
        [DisplayFormat(NullDisplayText ="[-N/A-]")]
        public string JobDescription { get; set; }
    }

    [MetadataType(typeof(PositionsMetadata))]
    public partial class Position { }
    #endregion

    #region ApplicationStatuses Metadata
    public class ApplicationStatusesMetadata
    {
        //public int ApplicationStatusId { get; set; }

        [Display(Name ="Status Name")]
        [Required(ErrorMessage = "* Name required.")]
        [StringLength(50, ErrorMessage = "* Name must be less than 50 characters.")]
        public string StatusName { get; set; }

        [Display(Name = "Description")]
        [DisplayFormat(NullDisplayText = "[-N/A-]")]
        [StringLength(250, ErrorMessage = "* Description must be less than 250 characters.")]
        public string StatusDescription { get; set; }
    }

    [MetadataType(typeof(ApplicationStatusesMetadata))]
    public partial class ApplicationStatus { }
    #endregion
}
