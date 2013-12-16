namespace EbayApplication.Web.Models.ProductModels
{
    using System;
    using EbayApplication.Web.Models.AuctionModels;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;

    public class ProductDetailedViewModel
    {
        public AuctionDetailedViewModel Auction { get; set; }

        public ProductViewModel Product { get; set; }

        public Guid ProductId { get; set; }

        //[StringLength(250, ErrorMessage = "The question for the product is too long")]
        //[MinLength(5, ErrorMessage = "The minimum length is 5 simbols")]
        //[DataType(DataType.MultilineText)]
        //public string Content { get; set; }

        public ProductDetailedViewModel()
        {
        }

        public ProductDetailedViewModel(ProductViewModel product)
        {
            this.Product = product;
          //  this.ProductId = product.Id;
        }

        public ProductDetailedViewModel(ProductViewModel product, AuctionDetailedViewModel auction)
            :this(product)
        {
            this.Auction = auction;
            
        }

        public IQueryable<ProductFlatViewModel> SimilarProducts { get; set; }
    }
}