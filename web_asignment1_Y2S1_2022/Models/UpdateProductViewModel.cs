using Microsoft.AspNetCore.Http;

namespace web_asignment1_Y2S1_2022.Models
{
    public class UpdateProductViewModel : RenovatedProductViewModel
    {
        public UpdateProductViewModel() { }
        public UpdateProductViewModel(int ProductId) : base(ProductId) { }
    }
}
