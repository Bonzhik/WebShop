using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public FeedbackService
            (
            UserManager<User> userManager,
            IFeedbackRepository feedbackRepository,
            ICommentRepository commentRepository,
            IProductRepository productRepository,
            IMapper mapper
            ) 
        {
            _userManager = userManager;
            _feedbackRepository = feedbackRepository;
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(FeedbackW feedbackDto)
        {
            Feedback feedback = await MapFromDto(feedbackDto);
            feedback.CreatedAt = DateTime.UtcNow;
            feedback.UpdatedAt = DateTime.UtcNow;

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
            Feedback feedback = await _feedbackRepository.GetAsync( feedbackId );
            return await _feedbackRepository.DeleteAsync(feedback);
        }

        public async Task<List<FeedbackR>> GetAllAsync()
        {
            List<Feedback> feedbacks = await _feedbackRepository.GetAllAsync();

            List<FeedbackR> feedbackDtos = new List<FeedbackR>();
            foreach (var feedback in feedbacks)
            {
                feedbackDtos.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<FeedbackR> GetAsync(int id)
        {
            Feedback feedback = await _feedbackRepository.GetAsync(id);
            return MapToDto(feedback);
        }

        public async Task<List<FeedbackR>> GetByProductAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(productId);
            List<Feedback> feedbacks = await _feedbackRepository.GetByProductAsync(product);

            List<FeedbackR> feedbackDtos = new List<FeedbackR>();
            foreach (var feedback in feedbacks)
            {
                feedbackDtos.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<List<FeedbackR>> GetByUserAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            List<Feedback> feedbacks = await _feedbackRepository.GetByUserAsync(user);

            List<FeedbackR> feedbackDtos = new List<FeedbackR>();
            foreach (var feedback in feedbacks)
            {
                feedbackDtos.Add(MapToDto(feedback));
            }
            return feedbackDtos;
        }

        public async Task<bool> UpdateAsync(FeedbackW feedbackDto)
        {
            Feedback feedback = await MapFromDto(feedbackDto);
            feedback.UpdatedAt = DateTime.UtcNow;

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
            List<Feedback> feedbacks = await _feedbackRepository.GetByProductAsync(product);
            double rating =Math.Round(feedbacks.Average(x => x.Rating), 1);

            product.Rating = rating;
            try
            {
                await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rating;
        }
    }
}
