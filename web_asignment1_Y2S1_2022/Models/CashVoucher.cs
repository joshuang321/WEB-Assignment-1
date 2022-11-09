using System.ComponentModel.DataAnnotations;
using System;

namespace web_asignment1_Y2S1_2022.Models
{
	public class CashVoucher
	{
			[Required(ErrorMessage = "This field cannot be left blank!")]
			public int IssuingID { get; set; }


			[StringLength(9, ErrorMessage = "Your member ID is too long!")]
			public string MemberID { get; set; }

			[Required(ErrorMessage = "This field cannot be left blank!")]
			public double Amount { get; set; }

			[Required(ErrorMessage = "This field cannot be left blank!")]
			public int MonthIssuedFor { get; set; }

			[Required(ErrorMessage = "This field cannot be left blank!")]
			public int YearIssuedFor { get; set; }

			[DataType(DataType.Date)]
			[DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
			public DateTime DateTimeIssued { get; set; }

			public string VoucherSN { get; set; }

			[Required(ErrorMessage = "This field cannot be left blank!")]
			public double Status { get; set; }

			[DisplayFormat(DataFormatString = "dd/MM/yyyy", ApplyFormatInEditMode = true)]
			public DateTime DateTimeRedeemed { get; set; }

	}
}
