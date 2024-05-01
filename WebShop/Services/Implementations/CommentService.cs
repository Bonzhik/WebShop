using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly UserManager<User> _userManager;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CommentService
            (
            ICommentRepository commentRepository,
            UserManager<User> userManager,
            IFeedbackRepository feedbackRepository,
            IProductRepository productRepository,
            IMapper mapper
            )
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
            _feedbackRepository = feedbackRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(CommentW commentDto)
        {
            Comment comment = await MapFromDto(commentDto);
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = DateTime.UtcNow;

            return await _commentRepository.AddAsync(comment);
        }

        public async Task<bool> DeleteAsync(int commentId)
        {
            Comment comment = await _commentRepository.GetAsync(commentId);

            if (comment == null)
            {
                throw new NotFoundException($"Отзыв {commentId} не найден");
            }

            return await _commentRepository.DeleteAsync(comment);
        }

        public async Task<List<CommentR>> GetAllAsync()
        {
            List<Comment> comments = await _commentRepository.GetAllAsync();

            List<CommentR> commentDtos = new List<CommentR>();
            foreach (var comment in comments)
            {
                commentDtos.Add(MapToDto(comment));
            }
            return commentDtos;
        }

        public async Task<CommentR> GetAsync(int id)
        {
            Comment comment = await _commentRepository.GetAsync(id);
            return MapToDto(comment);
        }

        public async Task<List<CommentR>> GetByFeedbackAsync(int feedbackId)
        {
            Feedback feedback = await _feedbackRepository.GetAsync(feedbackId);

            if (feedback == null)
                throw new NotFoundException($"Отзыв {feedbackId} не существует");

            List<Comment> comments = await _commentRepository.GetByFeedBackAsync(feedback);

            List<CommentR> commentDtos = new List<CommentR>();
            foreach (var comment in comments)
            {
                commentDtos.Add(MapToDto(comment));
            }
            return commentDtos;
        }

        public async Task<List<CommentR>> GetByParentCommentAsync(int parentCommentId)
        {
            Comment parentComment = await _commentRepository.GetAsync(parentCommentId);

            if (parentComment == null)
                throw new NotFoundException($"Отзыв {parentCommentId} не существует");

            List<Comment> comments = await _commentRepository.GetByParentCommentAsync(parentComment);

            List<CommentR> commentDtos = new List<CommentR>();
            foreach (var comment in comments)
            {
                commentDtos.Add(MapToDto(comment));
            }
            return commentDtos;
        }

        public async Task<List<CommentR>> GetByUserAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException($"Отзыв {userId} не существует");

            List<Comment> comments = await _commentRepository.GetByUserAsync(user);

            List<CommentR> commentDtos = new List<CommentR>();
            foreach (var comment in comments)
            {
                commentDtos.Add(MapToDto(comment));
            }
            return commentDtos;
        }

        public async Task<bool> UpdateAsync(CommentW commentDto)
        {
            Comment comment = await MapFromDto(commentDto);
            comment.UpdatedAt = DateTime.UtcNow;

            return await _commentRepository.UpdateAsync(comment);
        }
        private async Task<Comment> MapFromDto(CommentW commentDto)
        {
            Comment comment = new Comment
            {
                Id = commentDto.Id,
                Text = commentDto.Text,
                User = await _userManager.FindByIdAsync(commentDto.UserId),
                Feedback = await _feedbackRepository.GetAsync(commentDto.FeedbackId),
                ParentComment = commentDto.ParentCommentId == 0 ? null
                        : await _commentRepository.GetAsync(commentDto.ParentCommentId),
                Product = await _productRepository.GetAsync(commentDto.ProductId),
            };
            return comment;
        }
        private CommentR MapToDto(Comment comment)
        {
            CommentR commentR = _mapper.Map<CommentR>(comment);
            commentR.User = _mapper.Map<UserR>(comment.User);
            commentR.HaveComments = comment.Comments.Count > 0;
            return commentR;
        }
    }
}
