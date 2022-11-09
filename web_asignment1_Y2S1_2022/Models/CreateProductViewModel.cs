using Microsoft.AspNetCore.Http;
using System;

namespace web_asignment1_Y2S1_2022.Models
{
	public class CreateProductViewModel : RenovatedProductViewModel
	{
		public CreateProductViewModel()
		{
			EffectiveDate = DateTime.Today;
			ProductId = -1;
			isObsolete = false;
		}
	}
}
