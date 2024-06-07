using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;

namespace WebShop.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaginationService<Feedback> _paginationsService;
        private readonly IMapper _mapper;
        public FeedbackService
            (
            UserManager<User> userManager,
            IFeedbackRepository feedbackRepository,
            ICommentRepository commentRepository,
            IProductRepository productRepository,
            IPaginationService<Feedback> paginationsService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _feedbackRepository = feedbackRepository;
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _paginationsService = paginationsService;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(FeedbackW feedbackDto)
        {
            Feedback feedback = await MapFromDto(feedbackDto);
            feedback.CreatedAt = DateTime.UtcNow;
            feedback.UpdatedAt = DateTime.UtcNow;

            if (feedback.User == null || feedback.Product == null)
                throw new NotFoundException("Один из параметров не найден");

            if (await _feedbackRepository.IsExists(feedback))
            {
                throw new AlreadyExistsException($"Отзыв к {feedback.Product.Id} от {feedback.User.Id} уже существует");
            }

            if (await _feedbackRepository.AddAsync(feedback))
            {
                await CalcRating(feedback.Product);
                return true;
            }
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int feedbackId)
        {
            Feedback feedback = await _feedbackRepository.GetAsync(feedbackId);

            if (feedback == null)
            {
                throw new NotFoundException($"Отзыв {feedbackId} не найден");
            }

            return await _feedbackRepository.DeleteAsync(feedback);
        }

        public async Task<PaginationResponse<FeedbackR>> GetAllAsync(int page, int pageSize)
        {
            PaginationResponse<Feedback> feedbacks = _paginationsService.Paginate(await _feedbackRepository.GetAllAsync(), page, pageSize);

            PaginationResponse<FeedbackR> feedbackDtos = new PaginationResponse<FeedbackR>
            {
                Page = feedbacks.Page,
                PageSize = feedbacks.PageSize,
                TotalCount = feedbacks.TotalCount,
                HasNextPage = feedbacks.HasNextPage,
                HasPrevPage = feedbacks.HasPrevPage,
                Data = new List<FeedbackR>()
            };
            foreach (var feedback in feedbacks.Data)
            {
                feedbackDtos.Data.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<FeedbackR> GetAsync(int id)
        {
            Feedback feedback = await _feedbackRepository.GetAsync(id);
            return MapToDto(feedback);
        }

        public async Task<PaginationResponse<FeedbackR>> GetByProductAsync(int productId, int page, int pageSize)
        {
            Product product = await _productRepository.GetAsync(productId);

            if (product == null)
                throw new NotFoundException($"Продукт {productId} не найден");

            PaginationResponse<Feedback> feedbacks = _paginationsService.Paginate(await _feedbackRepository.GetByProductAsync(product), page, pageSize);

            PaginationResponse<FeedbackR> feedbackDtos = new PaginationResponse<FeedbackR>
            {
                Page = feedbacks.Page,
                PageSize = feedbacks.PageSize,
                TotalCount = feedbacks.TotalCount,
                HasNextPage = feedbacks.HasNextPage,
                HasPrevPage = feedbacks.HasPrevPage,
                Data = new List<FeedbackR>()
            };
            foreach (var feedback in feedbacks.Data)
            {
                feedbackDtos.Data.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<PaginationResponse<FeedbackR>> GetByUserAsync(string userId, int page, int pageSize)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException($"Пользователь {userId} не найден");

            PaginationResponse<Feedback> feedbacks = _paginationsService.Paginate(await _feedbackRepository.GetByUserAsync(user), page, pageSize);

            PaginationResponse<FeedbackR> feedbackDtos = new PaginationResponse<FeedbackR>
            {
                Page = feedbacks.Page,
                PageSize = feedbacks.PageSize,
                TotalCount = feedbacks.TotalCount,
                HasNextPage = feedbacks.HasNextPage,
                HasPrevPage = feedbacks.HasPrevPage,
                Data = new List<FeedbackR>()
            };
            foreach (var feedback in feedbacks.Data)
            {
                feedbackDtos.Data.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<bool> UpdateAsync(FeedbackW feedbackDto)
        {
            Feedback feedback = await MapFromDto(feedbackDto);
            feedback.UpdatedAt = DateTime.UtcNow;

            if (feedback.User == null || feedback.Product == null)
                throw new NotFoundException("Один из параметров не найден");

            if (await _feedbackRepository.UpdateAsync(feedback))
            {
                await CalcRating(feedback.Product);
                return true;
            }
            else
                return false;
        }
        private async Task<Feedback> MapFromDto(FeedbackW feedbackDto)
        {
            Feedback feedback = new Feedback
            {
                Id = feedbackDto.Id,
                Text = feedbackDto.Text,
                Rating = feedbackDto.Rating,
                User = await _userManager.FindByIdAsync(feedbackDto.UserId),
                Product = await _productRepository.GetAsync(feedbackDto.ProductId)
            };
            return feedback;
        }
        private FeedbackR MapToDto(Feedback feedback)
        {
            FeedbackR feedbackR = _mapper.Map<FeedbackR>(feedback);
            feedbackR.User = _mapper.Map<UserR>(feedbackR.User);
            feedbackR.HaveComments = feedback.Comments.Any() ? true : false;

            return feedbackR;
        }
        private async Task<double> CalcRating(Product product)
        {
            IQueryable<Feedback> feedbacks = await _feedbackRepository.GetByProductAsync(product);
            double rating = Math.Round(feedbacks.Average(x => x.Rating), 1);

            product.Rating = rating;
            await _productRepository.UpdateAsync(product);

            return rating;
        }
    }
}
