using ShoppingCartAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace ShoppingCartAPI.Repository
{
    public class AdminRepository
    {
        BIShopEntities entity = new BIShopEntities();

        public async Task<Response> ManageUser(AdminUserMaster userDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || userDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                var user = entity.AdminUserMasters.FirstOrDefault(g => g.UserName == userDetail.UserName && g.Password == userDetail.Password);
                if (user == null)
                {
                    userDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    entity.AdminUserMasters.Add(userDetail);
                    await entity.SaveChangesAsync();
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = "User added successfully.";
                }
                else
                {
                    responseDetail.ResponseValue = "Username already exist.";
                }
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var user = entity.AdminUserMasters.FirstOrDefault(g => g.Id == userDetail.Id);
                if (user == null)
                {
                    responseDetail.ResponseValue = "User not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        user.UserName = userDetail.UserName;
                        user.Password = userDetail.Password;
                        user.IsActive = userDetail.IsActive;
                        user.Remarks = userDetail.Remarks;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "User edit successfully.";
                    }
                    else
                    {
                        entity.AdminUserMasters.Remove(user);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "User deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "UserById")
            {
                var user = entity.AdminUserMasters.FirstOrDefault(g => g.Id == userDetail.Id);
                if (user == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.AdminUserMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageCustomer(CustomerDetail userDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || userDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                var user = entity.CustomerDetails.FirstOrDefault(g => g.Username == userDetail.Username && g.Password == userDetail.Password);
                if (user == null)
                {
                    userDetail.CustomerId = Guid.NewGuid().ToString().Substring(0, 5);
                    entity.CustomerDetails.Add(userDetail);
                    await entity.SaveChangesAsync();
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = "Customer added successf1ully.";
                }
                else
                {
                    responseDetail.ResponseValue = "Username already exist.";
                }
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var user = entity.CustomerDetails.FirstOrDefault(g => g.CustomerId == userDetail.CustomerId);
                if (user == null)
                {
                    responseDetail.ResponseValue = "User not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        user.Username = userDetail.Username;
                        user.Password = userDetail.Password;
                        user.Name = userDetail.Name;
                        user.Address = userDetail.Address;
                        user.CityId = userDetail.CityId;
                        user.Email = userDetail.Email;
                        //user.CityName = await Task.Run(() => entity.FirstOrDefault(g => g.Id == subCategoryDetail.CategoryId).Name);
                        user.MobileNo = userDetail.MobileNo;
                        user.Address = userDetail.Address;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "User edit successfully.";
                    }
                    else
                    {
                        entity.CustomerDetails.Remove(user);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "User deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "UserById")
            {
                var user = entity.CustomerDetails.FirstOrDefault(g => g.CustomerId == userDetail.CustomerId);
                if (user == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    var cityDetail = await Task.Run(() => entity.CityMasters.FirstOrDefault(c => c.Id == user.CityId));
                    if (cityDetail != null)
                    {
                        user.CityName = cityDetail.CityName;
                        user.PinCode = cityDetail.PinCode;
                        var state = await Task.Run(() => entity.StateMasters.FirstOrDefault(c => c.StateId == cityDetail.StateId));
                        if (state != null)
                        {
                            user.StateName = state.StateName;
                        }
                    }

                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.CustomerDetails.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    var cityList = await Task.Run(() => entity.CityMasters.ToList());
                    var statelist = await Task.Run(() => entity.StateMasters.ToList());
                    foreach (var user in list)
                    {
                        var cityDetail = await Task.Run(() => cityList.FirstOrDefault(c => c.Id == user.CityId));
                        if (cityDetail != null)
                        {
                            user.CityName = cityDetail.CityName;
                            user.PinCode = cityDetail.PinCode;
                            var state = await Task.Run(() => statelist.FirstOrDefault(c => c.StateId == cityDetail.StateId));
                            if (state != null)
                            {
                                user.StateName = state.StateName;
                            }
                        }
                    }
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageGroup(GroupMaster groupDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || groupDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                groupDetail.GroupId = Guid.NewGuid().ToString().Substring(0, 5);
                entity.GroupMasters.Add(groupDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Group added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var group = entity.GroupMasters.FirstOrDefault(g => g.GroupId == groupDetail.GroupId);
                if (group == null)
                {
                    responseDetail.ResponseValue = "Group not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        group.GroupName = groupDetail.GroupName;
                        group.Remark = groupDetail.Remark;
                        group.IsActive = groupDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Group edit successfully.";
                    }
                    else
                    {
                        entity.GroupMasters.Remove(group);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Group deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "GroupById")
            {
                var user = entity.GroupMasters.FirstOrDefault(g => g.GroupId == groupDetail.GroupId);
                if (user == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.GroupMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageState(StateMaster stateDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || stateDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                stateDetail.StateId = Guid.NewGuid().ToString().Substring(0, 5);
                entity.StateMasters.Add(stateDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "State added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var state = entity.StateMasters.FirstOrDefault(g => g.StateId == stateDetail.StateId);
                if (state == null)
                {
                    responseDetail.ResponseValue = "State not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        state.StateName = stateDetail.StateName;
                        state.Remark = stateDetail.Remark;
                        state.IsActive = stateDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "State edit successfully.";
                    }
                    else
                    {
                        entity.StateMasters.Remove(state);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "State deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "StateById")
            {
                var state = entity.StateMasters.FirstOrDefault(g => g.StateId == stateDetail.StateId);
                if (state == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(state);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.StateMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageCity(StateMaster stateDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || stateDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                stateDetail.StateId = Guid.NewGuid().ToString().Substring(0, 5);
                entity.StateMasters.Add(stateDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "State added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var state = entity.StateMasters.FirstOrDefault(g => g.StateId == stateDetail.StateId);
                if (state == null)
                {
                    responseDetail.ResponseValue = "State not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        state.StateName = stateDetail.StateName;
                        state.Remark = stateDetail.Remark;
                        state.IsActive = stateDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "State edit successfully.";
                    }
                    else
                    {
                        entity.StateMasters.Remove(state);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "State deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "StateById")
            {
                var state = entity.StateMasters.FirstOrDefault(g => g.StateId == stateDetail.StateId);
                if (state == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(state);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.StateMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageBanner(BannerMaster bannerDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || bannerDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                bannerDetail.BannerId = Guid.NewGuid().ToString().Substring(0, 5);
                entity.BannerMasters.Add(bannerDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Banner added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var banner = entity.BannerMasters.FirstOrDefault(g => g.BannerId == bannerDetail.BannerId);
                if (banner == null)
                {
                    responseDetail.ResponseValue = "State not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        banner.Url = bannerDetail.Url;
                        banner.Remarks = bannerDetail.Remarks;
                        banner.IsActive = bannerDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Banner edit successfully.";
                    }
                    else
                    {
                        entity.BannerMasters.Remove(banner);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Banner deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.BannerMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "BannerById")
            {
                var banner = entity.BannerMasters.FirstOrDefault(g => g.BannerId == bannerDetail.BannerId);
                if (banner == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(banner);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageBrand(BrandMaster brandDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || brandDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                brandDetail.BrandId = Guid.NewGuid().ToString().Substring(0, 5);
                entity.BrandMasters.Add(brandDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Brand added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var brand = entity.BrandMasters.FirstOrDefault(g => g.BrandId == brandDetail.BrandId);
                if (brand == null)
                {
                    responseDetail.ResponseValue = "Brand not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        brand.Name = brandDetail.Name;
                        brand.Remarks = brandDetail.Remarks;
                        brand.IsActive = brandDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Brand edit successfully.";
                    }
                    else
                    {
                        entity.BrandMasters.Remove(brand);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Brand deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.BrandMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "BrandById")
            {
                var brand = entity.BrandMasters.FirstOrDefault(g => g.BrandId == brandDetail.BrandId);
                if (brand == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(brand);
                }
            }
            return responseDetail;
        }

        public async Task<Response> ManageCategory(CategoryMaster categoryDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || categoryDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                categoryDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                entity.CategoryMasters.Add(categoryDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Category added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var category = entity.CategoryMasters.FirstOrDefault(g => g.Id == categoryDetail.Id);
                if (category == null)
                {
                    responseDetail.ResponseValue = "Category not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        category.Name = categoryDetail.Name;
                        category.Remarks = categoryDetail.Remarks;
                        category.IsActive = categoryDetail.IsActive;
                        category.BannerImage = categoryDetail.BannerImage;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Category edit successfully.";
                    }
                    else
                    {
                        entity.CategoryMasters.Remove(category);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Category deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List" || operation == "ActiveList")
            {
                var list = await Task.Run(() => entity.CategoryMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    if (operation == "ActiveList")
                    {
                        list = list.Where(l => l.IsActive == true).ToList();
                    }

                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "CategoryById")
            {
                var category = entity.CategoryMasters.FirstOrDefault(g => g.Id == categoryDetail.Id);
                if (category == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(category);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageSubCategory(SubCategoryMaster subCategoryDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || subCategoryDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                subCategoryDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                subCategoryDetail.CategoryName = await Task.Run(() => entity.CategoryMasters.FirstOrDefault(g => g.Id == subCategoryDetail.CategoryId).Name);
                entity.SubCategoryMasters.Add(subCategoryDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Sub Category added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var subCategory = entity.SubCategoryMasters.FirstOrDefault(g => g.Id == subCategoryDetail.Id);
                if (subCategory == null)
                {
                    responseDetail.ResponseValue = "Sub Category not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        subCategory.Name = subCategoryDetail.Name;
                        subCategory.Remarks = subCategoryDetail.Remarks;
                        subCategory.CategoryId = subCategoryDetail.CategoryId;
                        subCategory.CategoryName = await Task.Run(() => entity.CategoryMasters.FirstOrDefault(g => g.Id == subCategoryDetail.CategoryId).Name);
                        subCategory.IsActive = subCategoryDetail.IsActive;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Sub Category edit successfully.";
                    }
                    else
                    {
                        entity.SubCategoryMasters.Remove(subCategory);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Sub Category deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.SubCategoryMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "SubCategoryById")
            {
                var subCategory = entity.SubCategoryMasters.FirstOrDefault(g => g.Id == subCategoryDetail.Id);
                if (subCategory == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(subCategory);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageOrder(OrderDetail orderDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || orderDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                orderDetail.OrderNo = Guid.NewGuid().ToString().Substring(0, 6);
                orderDetail.OrderDate = DateTime.Now;
                orderDetail.ManufacturerComapany = ConfigurationManager.AppSettings["Company"].ToString();
                entity.OrderDetails.Add(orderDetail);

                if (orderDetail.orderProduct != null)
                {
                    foreach (var product in orderDetail.orderProduct)
                    {
                        var orderProduct = new ProductOrderDetail();
                        orderProduct.Id = Guid.NewGuid().ToString().Substring(0, 5);
                        orderProduct.OrderId = orderDetail.OrderNo;
                        orderProduct.ProductId = product.ProductId;
                        orderProduct.Quantity = product.Quantity;
                        entity.ProductOrderDetails.Add(orderProduct);
                    }
                }
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Order detail added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var order = entity.OrderDetails.FirstOrDefault(g => g.OrderNo == orderDetail.OrderNo);
                if (order == null)
                {
                    responseDetail.ResponseValue = "Order not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        order.OrderStatus = orderDetail.OrderStatus;
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Order status updated successfully.";
                    }
                    else
                    {
                        entity.OrderDetails.Remove(order);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Order deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.OrderDetails.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    await MapOrderist(list);
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "OrderByStatus")
            {
                var list = await Task.Run(() => entity.OrderDetails.Where(o => o.OrderStatus == orderDetail.OrderStatus).ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    await MapOrderist(list);
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "OrderById")
            {
                var order = entity.OrderDetails.FirstOrDefault(g => g.OrderNo == orderDetail.OrderNo);
                if (order == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    var Cityist = await Task.Run(() => entity.CityMasters.ToList());
                    var OrderProduct = await Task.Run(() => entity.ProductOrderDetails.ToList());
                    await MapOrderObject(Cityist, OrderProduct, order);
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(order);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageShoppingCart(CartMaster detail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || detail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                detail.Id = Guid.NewGuid().ToString().Substring(0, 6);
                entity.CartMasters.Add(detail);

                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                var list = await Task.Run(() => entity.CartMasters.Where(c => c.Uid == detail.Uid));
                responseDetail.ResponseValue = list == null ? "0" : list.Count().ToString();
            }
            else if (operation == "Delete")
            {
                var order = entity.CartMasters.FirstOrDefault(g => g.Id == detail.Id);
                if (order == null)
                {
                    responseDetail.ResponseValue = "product not found.";
                }
                else
                {
                    entity.CartMasters.Remove(detail);
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = "Product deleted successfully.";

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "ByUserId" || operation == "CartCount")
            {
                var list = await Task.Run(() => entity.CartMasters.Where(c => c.Uid == detail.Uid));
                if (list == null || list.Count() == 0)
                {
                    if (operation == "CartCount")
                    {
                        responseDetail.ResponseValue = "0";
                    }
                    else
                    {
                        responseDetail.ResponseValue = "No Records found.";
                    }
                }
                else
                {
                    if (operation == "CartCount")
                    {
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = list.Count().ToString();
                    }
                    else
                    {
                        var products = await Task.Run(() => entity.ProductMasters.ToList());
                        if (products == null || products.Count() == 0)
                        {
                            responseDetail.ResponseValue = "No Products found.";
                        }
                        else
                        {
                            var productList = new List<ProductMaster>();
                            foreach (var product in list)
                            {
                                var p = products.FirstOrDefault(pr => pr.Id == product.Pid);
                                if (p != null)
                                {
                                    productList.Add(p);
                                }
                            }

                            responseDetail.Status = true;
                            responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(productList);
                        }
                    }
                }
            }

            return responseDetail;
        }

        private async Task MapOrderist(List<OrderDetail> list)
        {
            var Cityist = await Task.Run(() => entity.CityMasters.ToList());
            var OrderProduct = await Task.Run(() => entity.ProductOrderDetails.ToList());
            if (Cityist != null)
            {
                foreach (var order in list)
                {
                    await MapOrderObject(Cityist, OrderProduct, order);
                }
            }
        }

        private async Task MapOrderObject(List<CityMaster> Cityist, List<ProductOrderDetail> OrderProduct, OrderDetail order)
        {
            var cityDetail = await Task.Run(() => Cityist.FirstOrDefault(c => c.Id == order.CityId));
            order.CityName = cityDetail.CityName;
            order.PinCode = cityDetail.PinCode;

            var state = await Task.Run(() => entity.StateMasters.FirstOrDefault(c => c.StateId == cityDetail.StateId));
            if (state != null)
            {
                order.StateName = state.StateName;
            }

            if (OrderProduct != null)
            {
                var products = await Task.Run(() => OrderProduct.Where(p => p.OrderId == order.OrderNo));
                if (products == null)
                {
                    order.OrderItems = 0;
                    order.OrderQuantity = 0;
                }
                else
                {
                    order.OrderItems = products.Count();
                    decimal qty = 0, totalAmount = 0;
                    foreach (var pro in products)
                    {
                        var productDetail = await Task.Run(() => entity.ProductMasters.FirstOrDefault(c => c.Id == pro.ProductId));
                        if (productDetail != null)
                        {
                            pro.Amount = productDetail.MRP;
                            pro.ProductName = productDetail.Name;
                        }
                        order.orderProduct = new List<ProductOrderDetail>();
                        order.orderProduct.Add(pro);
                        qty += pro.Quantity ?? 0;
                        totalAmount += (pro.Amount * pro.Quantity) ?? 0;
                    }
                    order.TotalAmount = totalAmount;
                    order.OrderQuantity = qty;
                }
            }
        }

        public async Task<Response> GetOrderStatus()
        {
            Response responseDetail = new Response();
            var orderStatusList = await Task.Run(() => entity.OrderDetails.ToString());
            if (orderStatusList == null)
            {
                responseDetail.ResponseValue = "No Records found.";
            }
            else
            {
                responseDetail.Status = true;
                responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(orderStatusList);
            }

            return responseDetail;
        }

        public async Task<Response> GetSubCategoryByCategoryId(string Id)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(Id))
            {
                responseDetail.ResponseValue = "Please send category Id.";
            }

            var subCategoryList = await Task.Run(() => entity.SubCategoryMasters.Where(g => g.CategoryId == Id && g.IsActive == true));
            if (subCategoryList == null)
            {
                responseDetail.ResponseValue = "No Records found.";
            }
            else
            {
                responseDetail.Status = true;
                responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(subCategoryList);
            }

            return responseDetail;
        }

        public async Task<Response> ManageOffer(OfferImageMaster offerDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || offerDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                offerDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                offerDetail.ProductName = await Task.Run(() => entity.ProductMasters.FirstOrDefault(g => g.Id == offerDetail.ProductId).Name);
                entity.OfferImageMasters.Add(offerDetail);
                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Offer Image added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var offer = entity.OfferImageMasters.FirstOrDefault(g => g.Id == offerDetail.Id);
                if (offer == null)
                {
                    responseDetail.ResponseValue = "Offer not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        offer.BannerUrl = offerDetail.BannerUrl;
                        offer.Remarks = offerDetail.Remarks;
                        offer.IsActive = offerDetail.IsActive;
                        offer.ProductId = offerDetail.ProductId;
                        offer.IsActive = offerDetail.IsActive;
                        offer.ProductName = await Task.Run(() => entity.ProductMasters.FirstOrDefault(g => g.Id == offerDetail.ProductId).Name);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Offer edit successfully.";
                    }
                    else
                    {
                        entity.OfferImageMasters.Remove(offer);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Offer deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.OfferImageMasters.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "OfferById")
            {
                var offer = entity.OfferImageMasters.FirstOrDefault(g => g.Id == offerDetail.Id);
                if (offer == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(offer);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageProduct(ProductMaster productDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || productDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                productDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                productDetail.BrandName = await Task.Run(() => entity.BrandMasters.FirstOrDefault(g => g.BrandId == productDetail.BrandId).Name);
                productDetail.CategoryName = await Task.Run(() => entity.CategoryMasters.FirstOrDefault(g => g.Id == productDetail.CategoryId).Name);
                productDetail.SubCategoryName = await Task.Run(() => entity.SubCategoryMasters.FirstOrDefault(g => g.Id == productDetail.SubCategoryId).Name);
                entity.ProductMasters.Add(productDetail);

                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Product added successfully.";
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var product = entity.ProductMasters.FirstOrDefault(g => g.Id == productDetail.Id);
                if (product == null)
                {
                    responseDetail.ResponseValue = "Product not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        product.BrandId = productDetail.BrandId;
                        product.Bv = productDetail.Bv;
                        product.IsActive = productDetail.IsActive;
                        product.CategoryId = productDetail.CategoryId;
                        product.Description = productDetail.Description;
                        product.DetailDescription = productDetail.DetailDescription;
                        product.Dp = productDetail.Dp;
                        product.Featured = productDetail.Featured;
                        product.HomePage = productDetail.HomePage;
                        product.MRP = productDetail.MRP;
                        product.Name = productDetail.Name;
                        product.Recommended = productDetail.Recommended;
                        product.Remarks = productDetail.Remarks;
                        product.ProductImage = productDetail.ProductImage;
                        product.SubCategoryId = productDetail.SubCategoryId;
                        product.Quantity = productDetail.Quantity;
                        product.BrandName = await Task.Run(() => entity.BrandMasters.FirstOrDefault(g => g.BrandId == productDetail.BrandId).Name);
                        product.CategoryName = await Task.Run(() => entity.CategoryMasters.FirstOrDefault(g => g.Id == productDetail.CategoryId).Name);
                        product.SubCategoryName = await Task.Run(() => entity.SubCategoryMasters.FirstOrDefault(g => g.Id == productDetail.SubCategoryId).Name);

                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Offer edit successfully.";
                    }
                    else
                    {
                        entity.ProductMasters.Remove(product);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Offer deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "List" || operation == "Featured" || operation == "Recommended" || operation == "HomePage")
            {
                var list = await Task.Run(() => entity.ProductMasters.ToList());
                if (operation == "Featured")
                {
                    list = list.Where(g => g.Featured == true && g.IsActive == true).ToList();
                }

                if (operation == "Recommended")
                {
                    list = list.Where(g => g.Recommended == true && g.IsActive == true).ToList();
                }

                if (operation == "HomePage")
                {
                    list = list.Where(g => g.HomePage == true && g.IsActive == true).ToList();
                }

                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "Featured")
            {
                var list = await Task.Run(() => entity.ProductMasters.Where(g => g.Featured == true));
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "ProductById")
            {
                var imagesList = entity.ProductMasters.FirstOrDefault(g => g.Id == productDetail.Id);
                if (imagesList == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(imagesList);
                }
            }
            else if (operation == "ByCatId")
            {
                var list = await Task.Run(() => entity.ProductMasters.Where(g => g.CategoryId == productDetail.CategoryId));
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }
            else if (operation == "BySubCatId")
            {
                var list = await Task.Run(() => entity.ProductMasters.Where(g => g.SubCategoryId == productDetail.SubCategoryId));
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageProductImages(List<ProductImage> images, string id, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation))
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                foreach (var image in images)
                {
                    image.Id = Guid.NewGuid().ToString().Substring(0, 5);

                    entity.ProductImages.Add(image);
                }

                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Product Images added successfully.";
            }
            else if (operation == "Delete")
            {
                var image = entity.ProductImages.FirstOrDefault(g => g.Id == id);
                if (image == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    entity.ProductImages.Remove(image);
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = "image deleted successfully.";
                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "Edit")
            {
                foreach (var image in images)
                {
                    var img = entity.ProductImages.FirstOrDefault(g => g.Id == image.Id);
                    if (img == null)
                    {
                        responseDetail.ResponseValue = "Product images not found.";
                    }
                    else
                    {
                        img.Imageurl = image.Imageurl;
                        img.ProductId = image.ProductId;

                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "Image edit successfully.";


                        await entity.SaveChangesAsync();
                    }
                }
            }
            else if (operation == "ById")
            {
                var imagesList = entity.ProductImages.FirstOrDefault(g => g.Id == id);
                if (imagesList == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(imagesList);
                }
            }
            else if (operation == "ByProductId")
            {
                var imagesList = entity.ProductImages.Where(g => g.ProductId == id);
                if (imagesList == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(imagesList);
                }
            }


            return responseDetail;
        }

        public async Task<Response> ManageProductReviews(ProductRemark remark, string id, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation))
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                remark.Id = Guid.NewGuid().ToString().Substring(0, 5);
                remark.CreatedDate = DateTime.Today;
                entity.ProductRemarks.Add(remark);

                await entity.SaveChangesAsync();
                responseDetail.Status = true;
                responseDetail.ResponseValue = "Product Remark added successfully.";
            }
            else if (operation == "ByProductId" || operation == "Rating")
            {
                var remarkList = await Task.Run(() => entity.ProductRemarks.ToList());
                if (remarkList != null && remarkList.Count > 0)
                {
                    remarkList = remarkList.Where(g => g.ProductId == id).ToList();
                }
                if (remarkList == null || remarkList.Count == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    if (operation == "Rating")
                    {
                        var avgRateing = remarkList.Sum(r => r.Rating) / remarkList.Count();
                    }
                    else
                    {
                        responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(remarkList);
                    }
                }
            }

            return responseDetail;
        }

        public async Task<Response> ManageShoppingUser(ShoppingUser userDetail, string operation)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(operation) || userDetail == null)
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            if (operation == "Add")
            {
                var user = entity.ShoppingUsers.FirstOrDefault(g => g.Email == userDetail.Email);
                if (user == null)
                {
                    userDetail.Id = Guid.NewGuid().ToString().Substring(0, 5);
                    entity.ShoppingUsers.Add(userDetail);
                    var id = await entity.SaveChangesAsync();
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = Convert.ToString(id);
                }
                else
                {
                    responseDetail.ResponseValue = "Emaile already regiistered.";
                }
            }
            else if (operation == "Edit" || operation == "Delete")
            {
                var user = entity.ShoppingUsers.FirstOrDefault(g => g.Id == userDetail.Id);
                if (user == null)
                {
                    responseDetail.ResponseValue = "User not found.";
                }
                else
                {
                    if (operation == "Edit")
                    {
                        user.Password = userDetail.Password;
                        user.Name = userDetail.Name;
                        responseDetail.ResponseValue = "User edit successfully.";
                    }
                    else
                    {
                        entity.ShoppingUsers.Remove(user);
                        responseDetail.Status = true;
                        responseDetail.ResponseValue = "User deleted successfully.";
                    }

                    await entity.SaveChangesAsync();
                }
            }
            else if (operation == "UserById")
            {
                var user = entity.ShoppingUsers.FirstOrDefault(g => g.Id == userDetail.Id);
                if (user == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
                }
            }
            else if (operation == "Login")
            {
                var user = entity.ShoppingUsers.FirstOrDefault(g => g.Email == userDetail.Email && g.Password == userDetail.Password);
                if (user == null)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
                }
            }
            else if (operation == "List")
            {
                var list = await Task.Run(() => entity.ShoppingUsers.ToList());
                if (list == null || list.Count() == 0)
                {
                    responseDetail.ResponseValue = "No Records found.";
                }
                else
                {
                    responseDetail.Status = true;
                    responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(list);
                }
            }

            return responseDetail;
        }

        public async Task<Response> LoginAdminUser(string username, string password)
        {
            Response responseDetail = new Response();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                responseDetail.ResponseValue = "Please send complete details.";
            }

            var user = await Task.Run(() => entity.AdminUserMasters.FirstOrDefault(g => g.UserName == username && g.Password == password));

            if (user == null)
            {
                responseDetail.ResponseValue = "Invalid username or password.";
            }
            else
            {
                responseDetail.Status = true;
                responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(user);
            }

            return responseDetail;
        }
    }
}