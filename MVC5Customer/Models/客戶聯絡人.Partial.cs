namespace MVC5Customer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人
    {
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress(ErrorMessage ="Email格式錯誤")]
        public string Email { get; set; }
        
        [StringLength(12, ErrorMessage="欄位長度不得大於 11 個字元")]
        //[RegularExpression(@"[0-9]{4}\-[0-9]{3}\-[0-9]{3}", ErrorMessage = "手機格式錯誤")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "手機格式錯誤")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
    public class SearchContactViewMode
    {
        public string ContactQuery { get; set; }
    }
}
