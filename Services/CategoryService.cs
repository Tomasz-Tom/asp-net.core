using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRespository _categoryRespository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRespository categoryRespository, IUnitOfWork unitOfWork)
        {
            _categoryRespository = categoryRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categoryRespository.ListAsync();
        }

        public async Task<SaveCategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRespository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new SaveCategoryResponse(category);
            }
            catch (Exception ex)
            {
                //Do som logging stuff
                return new SaveCategoryResponse($"An error ccured when saving the category: {ex.Message}");
            }
        }
    }
}
