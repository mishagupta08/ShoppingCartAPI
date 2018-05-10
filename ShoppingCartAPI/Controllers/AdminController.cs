using Newtonsoft.Json;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Repository;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingCartAPI.Controllers
{
    public class AdminController : ApiController
    {
        public AdminRepository repository;

        [HttpPost, Route("api/Admin/ManageUser/{operation}")]
        public async Task<IHttpActionResult> ManageUser(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<AdminUserMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageUser(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageGroup/{operation}")]
        public async Task<IHttpActionResult> ManageGroup(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<GroupMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageGroup(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageState/{operation}")]
        public async Task<IHttpActionResult> ManageState(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<StateMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageState(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageBanner/{operation}")]
        public async Task<IHttpActionResult> ManageBanner(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<BannerMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageBanner(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageBrand/{operation}")]
        public async Task<IHttpActionResult> ManageBrand(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<BrandMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageBrand(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageCategory/{operation}")]
        public async Task<IHttpActionResult> ManageCategory(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<CategoryMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageCategory(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageOffer/{operation}")]
        public async Task<IHttpActionResult> ManageOffer(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<OfferImageMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageOffer(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageProduct/{operation}")]
        public async Task<IHttpActionResult> ManageProduct(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<ProductMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageProduct(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageShoppingCart/{operation}")]
        public async Task<IHttpActionResult> ManageShoppingCart(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<CartMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageShoppingCart(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageShoppingUser/{operation}")]
        public async Task<IHttpActionResult> ManageShoppingUser(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<ShoppingUser>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageShoppingUser(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        

        [HttpPost, Route("api/Admin/ManageProductReviews/{operation}/{editId}")]
        public async Task<IHttpActionResult> ManageProductReviews(string operation, string editId)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<ProductRemark>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageProductReviews(filters, editId, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageProductImages/{operation}/{editId}")]
        public async Task<IHttpActionResult> ManageProductImages(string operation, string editId)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<List<ProductImage>>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageProductImages(filters, editId, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageSubCategory/{operation}")]
        public async Task<IHttpActionResult> ManageSubCategory(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<SubCategoryMaster>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageSubCategory(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageCustomer/{operation}")]
        public async Task<IHttpActionResult> ManageCustomer(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<CustomerDetail>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageCustomer(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/LoginAdminUser")]
        public async Task<IHttpActionResult> LoginAdminUser()
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<LoginModel>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.LoginAdminUser(filters.username, filters.password);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/GetSubCategoryByCategoryId/{Id}")]
        public async Task<IHttpActionResult> GetSubCategoryByCategoryId(string Id)
        {
            this.repository = new AdminRepository();
            var result = await this.repository.GetSubCategoryByCategoryId(Id);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/GetOrderStatus")]
        public async Task<IHttpActionResult> GetOrderStatus()
        {
            this.repository = new AdminRepository();
            var result = await this.repository.GetOrderStatus();
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Admin/ManageOrder/{operation}")]
        public async Task<IHttpActionResult> ManageOrder(string operation)
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var filters = JsonConvert.DeserializeObject<OrderDetail>(detail);
            this.repository = new AdminRepository();
            var result = await this.repository.ManageOrder(filters, operation);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

    }
}