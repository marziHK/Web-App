using System.ComponentModel.DataAnnotations;

namespace HosseinkhaniTest.Models.ViewModels
{
    public class PersonnelCreateViewModel
    {
        [MaxLength(50)]

        [Required(ErrorMessage ="وارد کردن این فیلد اجباری است.")]
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است.")]
        [Display(Name = "نام خانوادگی")]

        public string? LastName { get; set; }

        [MaxLength(10)]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است.")]
        [Display(Name = "کد ملی")]
        public int NationalCode { get; set; }

        [MinLength(4)]
        [MaxLength(10)]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است.")]
        [Display(Name = "کد پرسنلی")]
        public string? PersonalCode { get; set; }

        [Required(ErrorMessage = "لطفا حداقل یک فایل بارگزاری نمایید")]
        [Display(Name = "آپلود فایل‌ها")]
        public List<IFormFile> Files { get; set; }
    }
}
