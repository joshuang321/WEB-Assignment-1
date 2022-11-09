using Microsoft.AspNetCore.Http;

namespace web_asignment1_Y2S1_2022.Models
{
	public class RenovatedProductViewModel : Product
	{
		public IFormFile fileToUpload { get; set; }

		public RenovatedProductViewModel() { }
		public RenovatedProductViewModel(int ProductId) : base(ProductId) { }
	}
}
