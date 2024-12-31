using AutoMapper;
using WatchReadShare.Application.Contracts.Persistence;
using WatchReadShare.Application.Features.Genres.Create;
using WatchReadShare.Application.Features.Genres.Dto;
using WatchReadShare.Application.Features.Genres.Update;
using System.Net;
using WatchReadShare.Application.Features.Movies.Dto;
using WatchReadShare.Application.Features.Categories.Dto;
using WatchReadShare.Domain.Entities;
using WatchReadShare.Application.Features.Movies.Create;

namespace WatchReadShare.Application.Features.Genres
{
    public class GenreService(IGenreRepository genreRepository, IUnitOfWork unitOfWork , IMapper mapper) : IGenreService
    {
        public async Task<ServiceResult<GenreDto?>> GetByIdAsync(int id)
        {
            var genre = await genreRepository.GetByIdAsync(id);
            if (genre is null)
            {
                return ServiceResult<GenreDto?>.Fail("Tür bulunamadı.", HttpStatusCode.NotFound);
            }
            
            var genreDto = mapper.Map<GenreDto>(genre);
            return ServiceResult<GenreDto?>.Success(genreDto);
        }

        public async Task<ServiceResult<List<GenreDto>>> GetAllListAsync()
        {
            var genre = await genreRepository.GetAllAsync();
            var genreDto = mapper.Map<List<GenreDto>>(genre);
            return ServiceResult<List<GenreDto>>.Success(genreDto);


        }

        public async Task<ServiceResult<List<GenreDto>>> GetPagedAllList(int pageNumber, int pageSize)
        {
            var genre = await genreRepository.GetAllPagedAsync(pageNumber, pageSize);
            var genreDto = mapper.Map<List<GenreDto>>(genre);
            return ServiceResult<List<GenreDto>>.Success(genreDto);
        }

        public async Task<ServiceResult<CreateGenreResponse>> CreateAsync(CreateGenreRequest request)
        {
            var anyGenre = await genreRepository.AnyAsync(x => x.Name == request.Name);
            if (anyGenre)
            {
                return ServiceResult<CreateGenreResponse>.Fail("Tür ismi veri tabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }
            var genre = mapper.Map<Genre>(request);
            await genreRepository.AddAsync(genre);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateGenreResponse>.SuccessAsCreated(new CreateGenreResponse(genre.Id), $"api/genre/{genre.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateGenreRequest request)
        {
            var isGenreNameExist = await genreRepository.AnyAsync(x => x.Name == request.Name && x.Id != request.Id);
            if (isGenreNameExist)
            {
                return ServiceResult.Fail("Tür ismi veri tabanında bulunmaktadır.", HttpStatusCode.BadRequest);
            }
            var genre = mapper.Map<Genre>(request);
            genre.Id = request.Id;
            genreRepository.Update(genre);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var genre = await genreRepository.GetByIdAsync(id);
            if (genre is null)
            {
                return ServiceResult.Fail("Tür Bulunamadı.", HttpStatusCode.NotFound);
            }
            genreRepository.Delete(genre);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
